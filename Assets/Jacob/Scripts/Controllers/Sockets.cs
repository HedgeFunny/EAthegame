using System.Collections;
using System.Linq;
using Jacob.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
	public class Sockets: MonoBehaviour
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
			if (socketsSocket.socket.heldObject != socketsSocket.correctGameObject) return;
			socketsSocket.Correct = true;
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