using UnityEngine;
using System.Collections;
using TMPro;
public class DelayedExecution : MonoBehaviour
{
	private static DelayedExecution Instance;
	[SerializeField] private TextMeshProUGUI _textMeshProUGUI;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			return;
		}
		Destroy(Instance);
	}
	/*
	 * Implement the function 'Do' below so that it can be called from any context.
	 * You want to pass it a function and a float 'delay'. After 'delay' seconds, the function is to be executed.
	 * You can create as many additional functions as you need.
	 * Assume that this class needs to be a 'MonoBehaviour', so don't change that.
	 */
	// public static void Do(...

	public static void Do(int number,int delay)
	{
		// Started the countdown when called
		Instance.StartCoroutine(Instance.SetCountdown(number,delay));
	}

	IEnumerator SetCountdown(int number,int delay)
	{
		yield return new WaitForSeconds(delay);
		_textMeshProUGUI.SetText(number.ToString());
	}
	

}

