using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Collections;


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

		//(ROSE) 
		public int collisionCount = 0;
		public int targetCollisionCount = 3;
		public GameObject otherObject;
		public List<GameObject> activate;
		public List<GameObject> deactivate;
		public Animator chortAnim;
		public bool isCrunching = false;
		public bool isCrunched = false;

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
		public void HideLayer()
		{
			_mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(layer));
		}


		/// <summary>
		/// This is to keep track of the number of times a collision occurs and then hide a layer at a set number of collisions.
		/// </summary>
		/// <param name="collision"></param>
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				collisionCount++;
				StartCoroutine(Stall(collision, collision.gameObject.GetComponent<Player>()));
			}
		}

		//Animations 

		private IEnumerator Stall(Collision2D collision, Player playerComponent)
		{
			isCrunching = true;
			chortAnim.SetBool("isTouched", isCrunching);
			yield return new WaitForSeconds(3);
			isCrunching = false;
			if (collisionCount >= targetCollisionCount)
			{
				StartCoroutine(Stall2());
				Teleportation(playerComponent);
				HideLayer();
				collisionCount = 0;
				if (!collision.gameObject.CompareTag("Player")) yield break;
				Deactivate();
				Activate();
			}
		}

		IEnumerator Stall2()
		{

			isCrunched = true;
			chortAnim.SetBool("isTouched", isCrunched);
			yield return new WaitForSeconds(3);
			isCrunched = false;
		}

		/// <summary>
		/// Activates GameObjects if theres GameObjects in the activate array
		/// </summary>
		private void Activate()
		{
			if (activate.Count <= 0) return;
			foreach (var o in activate)
			{
				o.SetActive(true);
			}
		}

		/// <summary>
		/// Deactivates GameObjects if theres GameObjects in the deactivate array
		/// </summary>
		private void Deactivate()
		{
			if (deactivate.Count <= 0) return;
			foreach (var o in deactivate)
			{
				o.SetActive(false);
			}
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