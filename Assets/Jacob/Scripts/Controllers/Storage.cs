using System.Collections;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class Storage: MonoBehaviour
	{
		public float amountYouWantToGenerate;
		public float timeBetweenGeneration;
		public bool generatingMoney;

		private bool _coroutineStarted;
		private GameManager _gameManager;

		private void Awake()
		{
			_gameManager = GameManager.Get();
			_coroutineStarted = true;
			StartCoroutine(GeneratingCoroutine());
		}

		private void Update()
		{
			CheckIfGeneratingMoney();
		}

		/// <summary>
		/// Checks if you're generating money and if the coroutines not started and if its not, it will start the
		/// coroutine.
		/// </summary>
		private void CheckIfGeneratingMoney()
		{
			if (!generatingMoney || _coroutineStarted) return;
			_coroutineStarted = true;
			StartCoroutine(GeneratingCoroutine());
		}

		private IEnumerator GeneratingCoroutine()
		{
			while (generatingMoney)
			{
				_gameManager.AddMoney(amountYouWantToGenerate);
				yield return new WaitForSeconds(timeBetweenGeneration);
			}

			_coroutineStarted = false;
		}
	}
}