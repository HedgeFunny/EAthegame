using System;
using Jacob.Scripts.Controllers;
using UnityEngine;

namespace Jacob.Scripts.Data
{
	[Serializable]
	public class SocketsSocket
	{
		public Socket socket;
		public GameObject correctGameObject;
		[NonSerialized] public bool Correct;
	}
}