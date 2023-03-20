using UnityEngine;

namespace Jacob.Data
{
	public class TilemapBounds
	{
		public TilemapBounds(Bounds bounds, float horizontalSize)
		{
			Left = bounds.min.x + horizontalSize;
			Right = bounds.max.x - horizontalSize;
		}

		public readonly float Left;
		public readonly float Right;
	}
}