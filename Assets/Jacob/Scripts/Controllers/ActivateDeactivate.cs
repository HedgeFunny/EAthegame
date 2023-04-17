using System.Collections.Generic;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Collider2D))]
	public class ActivateDeactivate : MonoBehaviour
	{
		public bool clickToTrigger;
		public List<GameObject> activate;
		public List<GameObject> deactivate;

		private void OnMouseDown()
		{
			if (!clickToTrigger) return;
			Activate();
			Deactivate();
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (!col.CompareTag("Player")) return;
			Activate();
			Deactivate();
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
	}
}