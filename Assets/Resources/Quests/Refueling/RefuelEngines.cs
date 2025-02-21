using UnityEngine;

public class RefuelEngines : MonoBehaviour
{
    private bool hasCollectedItem = false;
    private bool hasDeliveredItem = false;

    [SerializeField] private GameObject itemPrefab;
    private GameObject spawnedItem;
    private bool isNearDeliveryPoint = false;

    private void OnEnable()
    {
        EventManager.Instance.uiEvents.tabPressed += TabPressed;
    }

    private void OnDisable()
    {
        EventManager.Instance.uiEvents.tabPressed -= TabPressed;
    }

    private void Start()
    {
        Transform taskParent = GameObject.Find("TaskSprites")?.transform;
        if (taskParent != null)
        {
            spawnedItem = Instantiate(itemPrefab, taskParent);
        }
        else
        {
            Debug.LogError("TaskSprites object not found! Cannot instantiate item.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasCollectedItem && spawnedItem != null)
        {
            CollectItem();
        }

        if (other.CompareTag("DeliveryPoint"))
        {
            isNearDeliveryPoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DeliveryPoint"))
        {
            isNearDeliveryPoint = false;
        }
    }

    private void CollectItem()
    {
        hasCollectedItem = true;
        EventManager.Instance.questSystemEvents.UpdateProgress(1);
        Destroy(spawnedItem);
        Debug.Log("Item collected!");
    }

    private void Update()
    {
        if (hasCollectedItem && isNearDeliveryPoint && Input.GetKeyDown(KeyCode.E))
        {
            DeliverItem();
        }
    }

    private void DeliverItem()
    {
        if (hasCollectedItem && !hasDeliveredItem)
        {
            hasDeliveredItem = true;
            EventManager.Instance.questSystemEvents.UpdateProgress(2);
            FinishItemCollection();
        }
    }

    private void TabPressed()
    {
        int progress = hasDeliveredItem ? 2 : hasCollectedItem ? 1 : 0;
        EventManager.Instance.questSystemEvents.UpdateProgress(progress);
    }

    private void FinishItemCollection()
    {
        Debug.Log("Item collected and delivered!");
    }
}
