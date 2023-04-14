using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Collider2D))]
	public class DragAndDrop : MonoBehaviour
	{
		private Camera _camera;
		private Vector3 MouseWorldPos => _camera.ScreenToWorldPoint(Input.mousePosition);
		private Vector3 _mousePositionOffset;
		private Rigidbody2D _rigidbody;

		private void Awake()
		{
			_camera = Camera.main;
			TryGetComponent(out _rigidbody);
		}

		private void OnMouseDown()
		{
			_mousePositionOffset = transform.position - MouseWorldPos;
		}

		private void OnMouseDrag()
		{
			transform.position = MouseWorldPos + _mousePositionOffset;
		}

		private void OnMouseUp()
		{
			if (!_rigidbody) return;
			_rigidbody.velocity = new Vector2(0,0);
		}
	}
}