using UnityEngine;
using UnityEngine.Events;

public class OnActivateDeactivate : MonoBehaviour
{
	public UnityEvent<GameObject> onActivate;
	public UnityEvent<GameObject> onDeactivate;

	private void OnEnable()
	{
		onActivate?.Invoke(gameObject);
	}

	private void OnDisable()
	{
		onDeactivate?.Invoke(gameObject);
	}
}
