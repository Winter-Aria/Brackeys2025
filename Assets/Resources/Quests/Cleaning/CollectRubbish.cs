using UnityEngine;

public class CollectRubbish : QuestStep
{
	private int rubbishCollected = 0;
	private int rubbishToComplete = 5;

	[SerializeField] private GameObject rubbishPrefab;

	private void OnEnable()
	{
		EventManager.Instance.taskEvents.rubbishCollected += RubbishCollected;
	}

	private void OnDisable()
	{
		EventManager.Instance.taskEvents.rubbishCollected -= RubbishCollected;
	}

	private void Start()
	{
		EventManager.Instance.uiEvents.StartNewStep("CleaningQuest", 5);
		Instantiate(rubbishPrefab, GameObject.Find("TaskSprites").transform);
	}

	//Incriment amount of rubbish collected upon collecting. If enough is collected, finish the step
	private void RubbishCollected()
	{
		if (rubbishCollected < rubbishToComplete)
		{
			rubbishCollected++;
			EventManager.Instance.questSystemEvents.UpdateProgress("CleaningQuest", rubbishCollected);
		}

		if (rubbishCollected >= rubbishToComplete)
		{
			EventManager.Instance.questSystemEvents.UpdateProgress("CleaningQuest", 5);
			FinishQuestStep();
		}
	}
}