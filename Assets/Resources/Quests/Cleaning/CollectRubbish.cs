using UnityEngine;

public class CollectRubbish : QuestStep
{
	private int rubbishCollected = 0;
	private int rubbishToComplete = 5;

	[SerializeField] private GameObject rubbishPrefab;

	private void OnEnable()
	{
		EventManager.Instance.taskEvents.rubbishCollected += RubbishCollected;
		EventManager.Instance.uiEvents.tabPressed += TabPressed;
	}

	private void OnDisable()
	{
		EventManager.Instance.taskEvents.rubbishCollected -= RubbishCollected;
		EventManager.Instance.uiEvents.tabPressed -= TabPressed;
	}

	private void Start()
	{
		Instantiate(rubbishPrefab, GameObject.Find("TaskSprites").transform);
	}

	//Incriment amount of rubbish collected upon collecting. If enough is collected, finish the step
	private void RubbishCollected()
	{
		if (rubbishCollected < rubbishToComplete)
		{
			rubbishCollected++;
			EventManager.Instance.questSystemEvents.UpdateProgress(rubbishCollected);
		}

		if (rubbishCollected >= rubbishToComplete)
		{
			EventManager.Instance.questSystemEvents.UpdateProgress(5);
			FinishQuestStep();
		}
	}

	private void TabPressed()
	{
		EventManager.Instance.questSystemEvents.UpdateProgress(rubbishCollected);
	}
}