using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
	private int droppedParts = 0;
	public GameObject[] newParts; 
	public Transform partsContainer; 

	public void OnDrop(PointerEventData eventData)
	{
		GameObject droppedObject = eventData.pointerDrag;
		if (droppedObject != null && eventData.pointerDrag.transform.name != "NewPart")
		{
			Destroy(droppedObject);
			droppedParts++;

			if (droppedParts == 3) 
			{
				Invoke(nameof(SpawnNewParts), 0.5f);
			}
		}
	}

	private void SpawnNewParts()
	{
		droppedParts = 0;
		foreach (GameObject newPart in newParts)
		{
			Instantiate(newPart, partsContainer);
		}
	}
}