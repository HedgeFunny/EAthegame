using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class ManualCoordinatesSupport: MonoBehaviour
	{
		public ManualCoordinatesData manualCoordinates;

		private Cam _camera;

		private void Awake()
		{
			_camera = Camera.main!.GetComponent<Cam>();
		}

		private void OnEnable()
		{
			_camera.ManualCoordinatesData = manualCoordinates;
		}

		private void OnDisable()
		{
			_camera.ManualCoordinatesData = null;
		}
	}
}