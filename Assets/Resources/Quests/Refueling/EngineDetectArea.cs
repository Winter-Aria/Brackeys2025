using UnityEngine;

public class EngineDetectArea : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			EventManager.Instance.taskEvents.EnterEngineArea(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			EventManager.Instance.taskEvents.EnterEngineArea(false);
		}
	}
}
