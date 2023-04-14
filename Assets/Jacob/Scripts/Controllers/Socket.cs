using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Collider2D))]
	public class Socket : MonoBehaviour
	{
		public UnityEvent onSocketEnter;
		private bool _startedCoroutine;
		private bool _breakFromCoroutine;

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.TryGetComponent<DragAndDrop>(out var obj) && !_startedCoroutine && obj.IsBeingHeld)
			{
				print("drop");
				StartCoroutine(HoverCoroutine(obj));
				_startedCoroutine = true;
			}
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
			if (obj.TryGetComponent<BoxCollider2D>(out var collider))
			{
				collider.isTrigger = true;
			}
			
			if (obj.TryGetComponent<Rigidbody2D>(out var rigidBody2D))
			{
				rigidBody2D.isKinematic = true;
			}
			
			obj.transform.parent = transform;
			obj.transform.localPosition = new Vector3(0, 0, 0);
			obj.DisableDragging();
			onSocketEnter?.Invoke();
		}
	}
}