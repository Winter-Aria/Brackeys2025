using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Rubbish : MonoBehaviour
{
	private PolygonCollider2D polygonCollider;

	private void Awake()
	{
		polygonCollider = GetComponent<PolygonCollider2D>();
	}

	//Call the event to incriment count and delete itself
	private void CollectRubbish()
	{
		EventManager.Instance.taskEvents.RubbishCollected();
		Destroy(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (otherCollider.CompareTag("Player"))
		{
			CollectRubbish();
		}
	}
}
