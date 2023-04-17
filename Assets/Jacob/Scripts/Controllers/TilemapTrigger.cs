using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Jacob.Scripts.Controllers
{
	public class TilemapTrigger: MonoBehaviour
	{
		[CanBeNull] public TilemapCollider2D tilemap;
		public UnityEvent<TilemapCollider2D> onTrigger;

		private void OnTriggerEnter2D(Collider2D col)
		{
			onTrigger.Invoke(tilemap);
		}
	}
}