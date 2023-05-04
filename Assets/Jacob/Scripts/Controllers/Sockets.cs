using System.Collections;
using System.Linq;
using Jacob.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
	public class Sockets : MonoBehaviour
	{
		public SocketsSocket[] sockets;
		public UnityEvent onAllSocketsCorrect;

		private void OnEnable()
		{
			foreach (var socketsSocket in sockets)
			{
				socketsSocket.socket.SocketsSocket = socketsSocket;
				socketsSocket.socket.OnSocketEnterAction += EnterSocket;
			}

			StartCoroutine(AllCorrectCoroutine());
		}

		private void OnDisable()
		{
			foreach (var socket in sockets)
			{
				if (socket.socket.HeldObject)
				{
					ResetDraggableObject(socket);
				}

				socket.socket.SocketsSocket = null;
				socket.socket.OnSocketEnterAction -= EnterSocket;
				socket.Correct = false;
			}
			
			StopCoroutine(AllCorrectCoroutine());
		}

		private static void EnterSocket(SocketsSocket socket)
		{
			// Checks if the Socket's HeldObject is not the set Correct GameObject. If it's not, the socket will be
			// cleared
			if (socket.socket.HeldObject != socket.correctGameObject)
			{
				ResetDraggableObject(socket);
			}
			else
			{
				socket.Correct = true;
			}
		}

		private IEnumerator AllCorrectCoroutine()
		{
			while (!sockets.All(socket => socket.Correct))
			{
				yield return new WaitForEndOfFrame();
			}

			print("all are correct");
			onAllSocketsCorrect?.Invoke();
		}

		private static void ResetDraggableObject(SocketsSocket socket)
		{
			// Check if the specific Object has a DragAndDrop script, if it doesn't, exit the method.
			if (!socket.socket.HeldObject.TryGetComponent<DragAndDrop>(out var drop)) return;
			
			socket.onIncorrect?.Invoke();

			drop.transform.parent = socket.socket.OriginalParent;

			socket.socket.ClearSocket();

			if (drop.Collider2D)
			{
				drop.Collider2D.isTrigger = false;
			}

			if (drop.Rigidbody)
			{
				drop.Rigidbody.isKinematic = false;
			}

			drop.EnableDragging();

			if (!socket.overrideDefaultProtection &&
			    socket.incorrectObjectPosition == Vector2.zero)
			{
				if (drop.snapBackToStartingPos)
					drop.SnapBackToStartingPosition();
				return;
			}

			drop.transform.position = socket.incorrectObjectPosition;
		}
	}
}