using UnityEngine;

public class CollectTablet : QuestStep
{
	private GameObject tabletObject;
	[SerializeField] private GameObject tabletPrefab;

	private void OnEnable()
	{
		EventManager.Instance.taskEvents.tabletCollected += TabletCollected;
	}

	private void OnDisable()
	{
		EventManager.Instance.taskEvents.tabletCollected -= TabletCollected;
	}

	private void Start()
	{
		EventManager.Instance.uiEvents.StartNewStep("DataTransfer", 2);
		tabletObject = Instantiate(tabletPrefab, GameObject.Find("TaskSprites").transform);
	}

	//Incriment amount of rubbish collected upon collecting. If enough is collected, finish the step
	private void TabletCollected()
	{
		Destroy(tabletObject);
		EventManager.Instance.questSystemEvents.UpdateProgress("DataTransfer", 1);
		FinishQuestStep();
	}
}