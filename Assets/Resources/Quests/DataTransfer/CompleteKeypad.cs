using UnityEngine;

public class CompleteKeypad : QuestStep
{
	private bool keypadCompleted;
	private GameObject keypadPrefab;

	[SerializeField] private GameObject keypadUIPrefab;

	private void OnEnable()
	{
		EventManager.Instance.taskEvents.keypadCodeCorrect += KeypadCodeCorrect;
	}

	private void OnDisable()
	{
		EventManager.Instance.taskEvents.keypadCodeCorrect -= KeypadCodeCorrect;
	}

	private void Start()
	{
		keypadPrefab = Instantiate(keypadUIPrefab, GameObject.Find("Canvas").transform);
	}

	//Incriment amount of rubbish collected upon collecting. If enough is collected, finish the step
	private void KeypadCodeCorrect()
	{
		Destroy(keypadPrefab);
		EventManager.Instance.questSystemEvents.UpdateProgress("DataTransfer", 2);
		FinishQuestStep();
	}
}