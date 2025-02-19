using UnityEngine;

public class Quest
{
	public QuestInfoSO info;
	public int timeToAcknowledge;
	public QuestState state;

	private int currentQuestStepIndex;

	public Quest(QuestInfoSO questInfo)
	{
		this.info = questInfo;
		this.state = QuestState.NOTIFIED;
		this.currentQuestStepIndex = 0;
		this.timeToAcknowledge = 10;
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
}