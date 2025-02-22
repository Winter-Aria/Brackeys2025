using UnityEngine;
using UnityEngine.UIElements;

public class RefuelEngines : QuestStep
{
    private bool hasCollectedItem = false;
    private bool hasDeliveredItem = false;

    [SerializeField] private GameObject fuelPrefab;
    private GameObject spawnedItem;
    private bool isNearDeliveryPoint = false;

    private void OnEnable()
    {
		EventManager.Instance.taskEvents.fuelCanCollected += FuelCanCollected;
		EventManager.Instance.taskEvents.enterEngineArea += EnterEngineArea;
    }

    private void OnDisable()
    {
		EventManager.Instance.taskEvents.fuelCanCollected -= FuelCanCollected;
		EventManager.Instance.taskEvents.enterEngineArea -= EnterEngineArea;
    }

    private void Start()
    {
		EventManager.Instance.uiEvents.StartNewStep("Refueling", 2);
		Transform taskParent = GameObject.Find("TaskSprites").transform;
		if (taskParent != null)
        {
            spawnedItem = Instantiate(fuelPrefab, taskParent);
        }
        else
        {
            Debug.LogError("TaskSprites object not found! Cannot instantiate item.");
        }
    }

    private void DeliverItem()
    {
        if (hasCollectedItem && !hasDeliveredItem)
        {
            hasDeliveredItem = true;
            EventManager.Instance.questSystemEvents.UpdateProgress("Refueling", 2);
            FinishQuestStep();
        }
    }

	private void Update()
	{
		if (hasCollectedItem && isNearDeliveryPoint && Input.GetKeyDown(KeyCode.E))
        {
            DeliverItem();
        }
	}
    
    private void FuelCanCollected()
    {
        hasCollectedItem = true;
		EventManager.Instance.questSystemEvents.UpdateProgress("Refueling", 1);
	}

	private void EnterEngineArea(bool enterOrExit)
    {
        if (enterOrExit == true)
        {
            isNearDeliveryPoint = true;
        }
        else if (enterOrExit == false)
        {
            isNearDeliveryPoint = false;
        }
    }
}
