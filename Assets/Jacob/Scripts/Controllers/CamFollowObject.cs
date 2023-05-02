using System;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
	public class CamFollowObject: MonoBehaviour
	{
		public Transform objectToFollow;
		public UnityEvent onEnable;

		private Cam _cam;
		
		private void Awake()
		{
			_cam = Camera.main!.GetComponent<Cam>();
		}

		private void OnEnable()
		{
			_cam.followedObject = objectToFollow;
			onEnable?.Invoke();
		}

		private void OnDisable()
		{
			_cam.followedObject = null;
		}
	}
}