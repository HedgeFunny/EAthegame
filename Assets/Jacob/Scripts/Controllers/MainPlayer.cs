using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class MainPlayer: MonoBehaviour
	{
		private void Awake()
		{
			GameManager.Get().MainPlayer = gameObject;
		}
	}
}