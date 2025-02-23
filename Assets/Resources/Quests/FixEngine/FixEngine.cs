using Mono.Cecil.Cil;
using UnityEngine;

public class FixEngine : QuestStep
{
	private int partsFixed = 0;
	private int partsToFix = 3;
	private bool isNearToEngine = false;

	[SerializeField] private GameObject engineFixerUIPanelPrefab;
	private GameObject engineFixerUIPanel;

	private void OnEnable()
	{
		EventManager.Instance.taskEvents.newPartDroppedIn += NewPartDroppedIn;
		EventManager.Instance.taskEvents.enterEngineArea += EnterEngineArea;
	}

	private void OnDisable()
	{
		EventManager.Instance.taskEvents.newPartDroppedIn -= NewPartDroppedIn;
		EventManager.Instance.taskEvents.enterEngineArea += EnterEngineArea;
	}

	private void Start()
	{
		EventManager.Instance.uiEvents.StartNewStep("FixEngine", 3);
	}

	private void Update()
	{
		if (isNearToEngine == true && Input.GetKeyDown(KeyCode.E))
		{
			engineFixerUIPanel = Instantiate(engineFixerUIPanelPrefab, GameObject.Find("Canvas").transform);
		}
	}

	//Incriment amount of rubbish collected upon collecting. If enough is collected, finish the step
	private void NewPartDroppedIn(int progress)
	{
		if (partsFixed < partsToFix)
		{
			partsFixed = partsFixed + progress;
			EventManager.Instance.questSystemEvents.UpdateProgress("FixEngine", partsFixed);
		}

		if (partsFixed >= partsToFix)
		{
			Destroy(engineFixerUIPanel);
			EventManager.Instance.questSystemEvents.UpdateProgress("FixEngine", 3);
			FinishQuestStep();
		}
	}

	private void EnterEngineArea(bool enterOrExit)
	{
		if (enterOrExit)
		{
			isNearToEngine = true;
		} else if (!enterOrExit)
		{
			isNearToEngine = false;
		}
		
	}
}