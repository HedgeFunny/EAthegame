using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Jacob.Controllers
{
	[RequireComponent(typeof(TilemapCollider2D))]
	public class TileOutOfBounds : MonoBehaviour
	{
		public GameObject followedObject;
		public TilemapCollider2D colliderToExtendTo;
		public UnityEvent<TilemapCollider2D> whenObjectOutOfBounds;
		public UnityEvent whenObjectInBounds;

		private TilemapCollider2D _tileMapCollider;
		private bool _eventCalled;

		private void Awake()
		{
			_tileMapCollider = GetComponent<TilemapCollider2D>();
		}

		private void Update()
		{
			if (colliderToExtendTo.bounds.Contains(followedObject.transform.position) && !_eventCalled)
			{
				InvokeEvent();
			}

			if (_tileMapCollider.bounds.Contains(followedObject.transform.position) && _eventCalled)
			{
				whenObjectInBounds.Invoke();
				_eventCalled = false;
			}
		}

		private void InvokeEvent()
		{
			whenObjectOutOfBounds.Invoke(colliderToExtendTo);
			_eventCalled = true;
		}
	}
}