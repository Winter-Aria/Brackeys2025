using UnityEngine;

public class CollectRubbish : QuestStep
{
	private int rubbishCollected = 0;
	private int rubbishToComplete = 5;

	private void OnEnable()
	{
		EventManager.Instance.taskEvents.rubbishCollected += RubbishCollected;
	}

	private void OnDisable()
	{
		EventManager.Instance.taskEvents.rubbishCollected += RubbishCollected;
	}

	//Incriment amount of rubbish collected upon collecting. If enough is collected, finish the step
	private void RubbishCollected()
	{
		if (rubbishCollected < rubbishToComplete)
		{
			rubbishCollected++;
		}

		if (rubbishCollected >= rubbishToComplete)
		{
			FinishQuestStep();
		}
	}
}