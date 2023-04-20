using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class DebugSupport : MonoBehaviour
	{
		/// <summary>
		/// This will enable the Main Player on the Ad your loading.
		/// If this is disabled, the Main Player will be disabled if you load that Ad.
		/// </summary>
		public bool enableMainPlayer;
		/// <summary>
		/// Location (as a Vector2) that the Main Player teleports to when the Ad loads.
		/// </summary>
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
