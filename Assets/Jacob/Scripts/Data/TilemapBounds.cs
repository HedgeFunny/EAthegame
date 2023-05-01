using UnityEngine;

namespace Jacob.Scripts.Data
{
	public class TilemapBounds
	{
		public TilemapBounds(Bounds bounds, float horizontalSize, float verticalSize)
		{
			Left = bounds.min.x + horizontalSize;
			Right = bounds.max.x - horizontalSize;
			Up = bounds.max.y - verticalSize;
			Down = bounds.min.y + verticalSize;
		}

		public readonly float Left;
		public readonly float Right;
		public readonly float Up;
		public readonly float Down;
	}
}