using System;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Collider2D))]
	public class DragAndDrop : MonoBehaviour
	{
		[NonSerialized] public bool IsBeingHeld;

		private Camera _camera;
		private Vector3 MouseWorldPos => _camera.ScreenToWorldPoint(Input.mousePosition);
		private Vector3 _mousePositionOffset;
		private Rigidbody2D _rigidbody;
		private bool _isDraggable = true;

		private void Awake()
		{
			_camera = Camera.main;
			TryGetComponent(out _rigidbody);
		}

		private void OnMouseDown()
		{
			if (!_isDraggable) return;
			_mousePositionOffset = transform.position - MouseWorldPos;
			IsBeingHeld = true;
		}

		private void OnMouseDrag()
		{
			if (!_isDraggable) return;
			transform.position = MouseWorldPos + _mousePositionOffset;
		}

		private void OnMouseUp()
		{
			if (!_isDraggable) return;
			IsBeingHeld = false;
			if (!_rigidbody) return;
			_rigidbody.velocity = new Vector2(0,0);
		}

		public void DisableDragging()
		{
			_isDraggable = false;
		}

		public void EnableDragging()
		{
			_isDraggable = true;
		}
	}
}