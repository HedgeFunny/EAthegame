using UnityEngine;

namespace Jacob.Scripts.Data
{
	[CreateAssetMenu(fileName = "ManualCoordinates", menuName = "TotallyAverageMobileGame/ManualCoordinates")]
	public class ManualCoordinatesData: ScriptableObject
	{
		public float minX;
		public float minY;
		public float maxX;
		public float maxY;
	}
}