using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class DebugSupport : MonoBehaviour
	{
		public Vector2 locationToTeleportTo;
		private GameManager _gameManager;

		private void Awake()
		{
			_gameManager = GameManager.Get();
			_gameManager.SetActiveScene(gameObject);
		}

		private void OnEnable()
		{
			_gameManager.SetActiveScene(gameObject);
		}

		private void OnDisable()
		{
			_gameManager.ClearActiveScene();
		}
	}
}
