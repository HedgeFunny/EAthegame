using UnityEngine;
using UnityEngine.Events;

public class OnActivateDeactivate : MonoBehaviour
{
	public UnityEvent onActivate;
	public UnityEvent onDeactivate;

	private void OnEnable()
	{
		onActivate?.Invoke();
	}

	private void OnDisable()
	{
		onDeactivate?.Invoke();
	}
}
