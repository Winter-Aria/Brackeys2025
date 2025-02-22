using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Vector3 originalPosition;
	private Transform parentAfterDrag;

	public void OnBeginDrag(PointerEventData eventData)
	{
		originalPosition = transform.position;
		parentAfterDrag = transform.parent;
		transform.SetParent(transform.root); // Move to root to avoid layout issues
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition; // Move with cursor
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent(parentAfterDrag); // Reset parent after drag
	}
}