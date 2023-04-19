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

		private void Awake()
		{
			foreach (var socketsSocket in sockets)
			{
				socketsSocket.socket.SocketsSocket = socketsSocket;
				socketsSocket.socket.OnSocketEnterAction += EnterSocket;
			}

			StartCoroutine(AllCorrectCoroutine());
		}

		private static void EnterSocket(SocketsSocket socketsSocket)
		{
			if (socketsSocket.socket.heldObject != socketsSocket.correctGameObject)
			{
				if (!socketsSocket.socket.heldObject.TryGetComponent<DragAndDrop>(out var drop)) return;

				drop.transform.parent = null;

				if (drop.Collider2D)
				{
					drop.Collider2D.isTrigger = false;
				}

				if (drop.Rigidbody)
				{
					drop.Rigidbody.isKinematic = false;
				}

				drop.EnableDragging();

				if (!socketsSocket.overrideDefaultProtection &&
				    socketsSocket.incorrectObjectPosition == Vector2.zero) return;

				drop.transform.position = socketsSocket.incorrectObjectPosition;
			}
			else
			{
				socketsSocket.Correct = true;
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
	}
}