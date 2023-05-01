using System;
using System.Collections;
using Jacob.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Collider2D))]
	public class Socket : MonoBehaviour
	{
		public UnityEvent onSocketEnter;
		public Action<SocketsSocket> OnSocketEnterAction;
		[NonSerialized] public SocketsSocket SocketsSocket;
		[NonSerialized] public Transform OriginalParent;

		public GameObject HeldObject
		{
			get => _heldObject;
			set
			{
				if (value)
				{
					OriginalParent = value.transform.parent;
					value.transform.parent = transform;
				}

				_heldObject = value;
			}
		}

		private bool _startedCoroutine;
		private bool _breakFromCoroutine;
		private GameObject _heldObject;

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (HeldObject) return;
			if (!col.TryGetComponent<DragAndDrop>(out var obj) || _startedCoroutine || !obj.IsBeingHeld) return;
			print("drop");
			StartCoroutine(HoverCoroutine(obj));
			_startedCoroutine = true;
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.TryGetComponent<DragAndDrop>(out var obj) && _startedCoroutine)
			{
				_breakFromCoroutine = true;
			}
		}

		private IEnumerator HoverCoroutine(DragAndDrop obj)
		{
			while (obj.IsBeingHeld)
			{
				print("being held");
				if (!obj.IsBeingHeld) continue;
				print("waiting for end of frame");
				yield return new WaitForEndOfFrame();

				if (!_breakFromCoroutine) continue;
				_startedCoroutine = false;
				_breakFromCoroutine = false;
				yield break;
			}

			print("let go");
			_startedCoroutine = false;
			_breakFromCoroutine = false;
			SocketObject(obj);
		}

		private void SocketObject(DragAndDrop obj)
		{
			if (obj.Collider2D)
			{
				obj.Collider2D.isTrigger = true;
			}

			if (obj.Rigidbody)
			{
				obj.Rigidbody.isKinematic = true;
			}

			HeldObject = obj.gameObject;
			obj.transform.localPosition = new Vector3(0, 0, 0);
			obj.DisableDragging();
			onSocketEnter?.Invoke();
			OnSocketEnterAction?.Invoke(SocketsSocket);
		}

		public void ClearSocket()
		{
			HeldObject = null;
		}
	}
}