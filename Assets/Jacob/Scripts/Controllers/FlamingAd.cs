using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Collider2D))]
	public class FlamingAd : MonoBehaviour
	{
		[HideInInspector] public bool runObsoleteCode;

		[Obsolete("Unable to find a use for this boolean. Might change in the future.")] [HideInInspector]
		public bool mainSceneActive;

		[Obsolete("We aren't clicking on this anymore. This Event is obsolete.")] [HideInInspector]
		public UnityEvent onClickedEnough;

		public string layer;
		public Vector3 movePlayerTo;

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
			if (!runObsoleteCode) return;

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

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.CompareTag("Player"))
			{
				RunAdCode(col.GetComponent<Player>());
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
		private void RunAdCode([CanBeNull] Player player = null)
		{
			if (!string.IsNullOrWhiteSpace(layer))
				HideLayer();
			if (runObsoleteCode)
				onClickedEnough.Invoke();
			else if (player != null)
				Teleportation(player);
		}

		private void Teleportation(Player player)
		{
			player.transform.position = movePlayerTo;
			//player.DisableControllingMovement();
			//player.DisableJumping();
		}
	}
}