using Jacob.Scripts.Data;
using Jacob.Scripts.Data.Clamp;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class ClampControl : ClampingProperties
	{
		private Cam _cam;
		
		private void Awake()
		{
			_cam = Camera.main!.GetComponent<Cam>();
		}

		private void OnEnable()
		{
			if (!clampProperty) return;
			
			_cam.clampVerticalPosition = clampVerticalPosition;
			_cam.clampingTypes = clampingTypes;
			if (_cam.clampingTypes == ClampingTypes.Tilemap)
			{
				_cam.tilemap = tilemap;
			}
			
			_cam.Clamp = clampProperty;
		}

		private void OnDisable()
		{
			_cam.Clamp = false;
		}
	}
}