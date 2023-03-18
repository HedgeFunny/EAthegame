using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Controllers
{
	[RequireComponent(typeof(Collider2D))]
	public class FlamingAd : MonoBehaviour
	{
		public bool mainSceneActive;
		public string layer;
		public UnityEvent onClickedEnough;

		private long _timesClicked;
		private long _timesYouHaveToClick;
		private Camera _mainCamera;

		private void Awake()
		{
			GenerateRandomNumber();
			_mainCamera = Camera.main;
		}

		private void OnMouseDown()
		{
			if (mainSceneActive) return;

			if (_timesClicked < _timesYouHaveToClick)
			{
				_timesClicked++;
			}

			if (_timesClicked >= _timesYouHaveToClick)
			{
				RunAdCode();
			}
		}

		/// <summary>
		/// Generates a random number between 3 and 5 that is set to the timesYouHaveToClick number.
		/// </summary>
		private void GenerateRandomNumber()
		{
			_timesYouHaveToClick = Random.Range(2, 5);
		}

		/// <summary>
		/// Hides the layer that you set on the layer property. Uses the Camera's culling mask to hide the layer.
		/// </summary>
		private void HideLayer()
		{
			_mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(layer));
		}

		/// <summary>
		/// Method that contains the code that the Ad should run. You can run this externally or you click down on
		/// the object 3-5 times.
		/// </summary>
		public void RunAdCode()
		{
			if (!string.IsNullOrWhiteSpace(layer))
				HideLayer();
			onClickedEnough.Invoke();
		}
	}
}