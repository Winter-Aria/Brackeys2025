using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Image image;
    private CanvasGroup group;
    public Transform parentAfterDrag;
	[SerializeField] private GameObject binGameObject;
    private string oldPanelName;
    private string newPanelName;

    private void Start()
    {
        image = GetComponent<Image>();
        group = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
		oldPanelName = eventData.pointerDrag.transform.parent.parent.name;

		parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        group.alpha = .5f;
        image.raycastTarget = false;
	}

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        group.alpha = 1f;
        image.raycastTarget = true;

        newPanelName = eventData.pointerDrag.transform.parent.parent.name;

		if (newPanelName == "MainPanel" && oldPanelName == "SidePanel" && eventData.pointerDrag.transform.name == "NewPart")
        {
            Debug.Log("Added 1");
            EventManager.Instance.taskEvents.NewPartDroppedIn(1);
        }
        else if (newPanelName == "SidePanel" && oldPanelName == "MainPanel" && eventData.pointerDrag.transform.name == "NewPart")
        {
			Debug.Log("Taken away 1");
			EventManager.Instance.taskEvents.NewPartDroppedIn(-1);
		}
    }
}
