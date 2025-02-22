using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FuelCan : MonoBehaviour
{
	private BoxCollider2D boxCollider2D;

	private void Awake()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	//Call the event to incriment count and delete itself
	private void CollectFuelCan()
	{
		EventManager.Instance.taskEvents.FuelCanCollected();
		Destroy(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (otherCollider.CompareTag("Player"))
		{
			CollectFuelCan();
		}
	}
}