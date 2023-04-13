using System;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class SettingsMenu : MonoBehaviour
	{
		public Action OnBack;

		public void Back()
		{
			OnBack?.Invoke();
			Destroy(gameObject);
		}
	}
}