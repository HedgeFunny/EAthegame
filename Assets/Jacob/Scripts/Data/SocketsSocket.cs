using System;
using Jacob.Scripts.Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Data
{
	[Serializable]
	public class SocketsSocket
	{
		public Socket socket;
		public GameObject correctGameObject;
		public Vector2 incorrectObjectPosition;
		public bool overrideDefaultProtection;
		public UnityEvent onIncorrect;
		[NonSerialized] public bool Correct;
	}
}