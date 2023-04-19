using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class ActivateDeactivate : MonoBehaviour
	{
		public bool clickToTrigger;
		public bool collideToTrigger;
		public List<GameObject> activate;
		public List<GameObject> deactivate;

		private bool _hasCollider;

		private void Awake()
		{
			_hasCollider = TryGetComponent<Collider2D>(out _);
		}

		private void OnMouseDown()
		{
			if (!clickToTrigger && !_hasCollider) return;
			Activate();
			Deactivate();
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (!collideToTrigger && !_hasCollider);
			if (!col.CompareTag("Player")) return;
			Activate();
			Deactivate();
		}

		/// <summary>
		/// Activates GameObjects if theres GameObjects in the activate array
		/// </summary>
		public void Activate()
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
		public void Deactivate()
		{
			if (deactivate.Count <= 0) return;
			foreach (var o in deactivate)
			{
				o.SetActive(false);
			}
		}
	}
}