using UnityEngine;

public class TabletDetected : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			EventManager.Instance.taskEvents.TabletCollected();
		}
	}
}
