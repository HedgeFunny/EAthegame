﻿using System;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Collider2D))]
	public class DragAndDrop : MonoBehaviour
	{
		public bool snapBackToStartingPos;
		[NonSerialized] public bool IsBeingHeld;
		[NonSerialized] public Rigidbody2D Rigidbody;
		[NonSerialized] public Collider2D Collider2D;
		[NonSerialized] public bool OverrideSnap;
		
		private Camera _camera;
		private Vector3 MouseWorldPos => _camera.ScreenToWorldPoint(Input.mousePosition);
		private Vector3 _mousePositionOffset;
		private bool _isDraggable = true;
		private Vector3 _startingPosition;

		private void Awake()
		{
			_camera = Camera.main;
			TryGetComponent(out Rigidbody);
			TryGetComponent(out Collider2D);
			_startingPosition = transform.position;
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
			if (snapBackToStartingPos && !OverrideSnap) SnapBackToStartingPosition();
			if (!Rigidbody) return;
			Rigidbody.velocity = new Vector2(0,0);
		}

		public void DisableDragging()
		{
			_isDraggable = false;
		}

		public void EnableDragging()
		{
			_isDraggable = true;
		}

		public void SnapBackToStartingPosition()
		{
			transform.position = _startingPosition;
		}
	}
}