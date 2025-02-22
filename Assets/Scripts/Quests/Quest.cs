using UnityEngine;

public class Quest
{
	public QuestInfoSO info;
	public float timeToShow;
	public QuestState state;

	private bool timeCompleted;
	private int currentQuestStepIndex;
	private bool questStarted = false;
	private float timeToComplete;

	public Quest(QuestInfoSO questInfo)
	{
		this.info = questInfo;
		this.state = QuestState.NOTIFIED;
		this.currentQuestStepIndex = 0;
		this.timeToComplete = questInfo.timeToComplete;
		this.timeToShow = 10;
	}

	public void MoveToNextStep()
	{
		currentQuestStepIndex++;
	}

	public bool CurrentStepExists()
	{
		return (currentQuestStepIndex < info.questStepPrefabs.Length);
	}

	public void InstantiateCurrentQuestStep(Transform parentTransform)
	{
		GameObject questStepPrefab = GetCurrentQuestStepPrefab();
		if (questStepPrefab != null)
		{
			QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform)
				.GetComponent<QuestStep>();
			questStep.InitializeQuestStep(info.id);
		} else
		{
			Debug.Log("Gone wrong");
		}
	}

	//Get the prefab for the current quest step
	private GameObject GetCurrentQuestStepPrefab()
	{
		GameObject questStepPrefab = null;
		if (CurrentStepExists())
		{
			questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
		}
		else
		{
			Debug.LogWarning("Tried to get quest step prefab but index was out of range.");
		}
		return questStepPrefab;
	}

	public void UpdateTimer(float deltaTime, Quest quest)
	{
		if (!timeCompleted)
		{
			if (!questStarted)
			{
				timeToShow = Mathf.Max(0, timeToShow - deltaTime);

				if (timeToShow <= 0.0f)
				{
					EventManager.Instance.questSystemEvents.StartQuest(quest.info.id);
					quest.questStarted = true;
				}
			}

			if (questStarted)
			{
				timeToComplete = Mathf.Max(0, timeToComplete - deltaTime);
				if (timeToComplete <= 0.0f)
				{
					timeCompleted = true;
				}
			}
		}
	}
}