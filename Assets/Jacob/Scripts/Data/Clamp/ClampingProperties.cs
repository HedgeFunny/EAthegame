using UnityEngine;
using UnityEngine.Tilemaps;

namespace Jacob.Scripts.Data.Clamp
{
	public abstract class ClampingProperties : MonoBehaviour
	{
		public bool clampVerticalPosition;
		public ClampingTypes clampingTypes;

		// Tilemap
		public TilemapCollider2D tilemap;

		public bool clampProperty;
	}
}