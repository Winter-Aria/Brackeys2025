using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
	public int numberOfProgressInStep;

	private bool isFinished = false;
	private string questId;

	public void InitializeQuestStep(string questId)
	{
		this.questId = questId;
	}


	//Delete itself once the step is finished
	protected void FinishQuestStep()
	{
		if (!isFinished)
		{
			isFinished = true;

			EventManager.Instance.questSystemEvents.AdvanceQuest(questId);

			Destroy(this.gameObject);
		}
	}
}