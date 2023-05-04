using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class MainPlayer: MonoBehaviour
	{
		private void Awake()
		{
			if (FindObjectOfType<GameManager>())
            {
				GameManager.Get().MainPlayer = gameObject;
			}
		}
	}
}