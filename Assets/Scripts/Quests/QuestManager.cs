using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
	private Dictionary<string, Quest> notifiedQuestMap = new Dictionary<string, Quest>();
	private Dictionary<string, Quest> activeQuestMap = new Dictionary<string, Quest>();
	private int totalScore = 0;

	[SerializeField] private GameObject questUIPrefab;
	[SerializeField] private Transform questListParent;
	private GameObject actualPanel;

	private GameObject questUI;
	private LayoutGroup layoutGroup;

	private void OnEnable()
	{
		EventManager.Instance.questSystemEvents.onStartQuest += StartQuest;
		EventManager.Instance.questSystemEvents.onAdvanceQuest += AdvanceQuest;
		EventManager.Instance.questSystemEvents.onFinishQuest += FinishQuest;
	}

	private void OnDisable()
	{
		EventManager.Instance.questSystemEvents.onStartQuest -= StartQuest;
		EventManager.Instance.questSystemEvents.onAdvanceQuest -= AdvanceQuest;
		EventManager.Instance.questSystemEvents.onFinishQuest -= FinishQuest;
	}

	private void Start()
	{
		//CreateRandomQuest();
	}

	private void Update()
	{
		
	}

	private void ChangeQuestState(string id, QuestState state, Dictionary<string, Quest> map)
	{
		Quest quest = GetQuestById(id, map);
		quest.state = state;
		EventManager.Instance.questSystemEvents.QuestStateChange(quest);
	}

	public void StartQuest(string id)
	{
		Debug.Log("Start quest: " + id);

		Quest quest = GetQuestById(id, notifiedQuestMap);
		quest.InstantiateCurrentQuestStep(this.transform);
		ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS, notifiedQuestMap);

		activeQuestMap.Add(quest.info.id, quest);
		notifiedQuestMap.Remove(quest.info.id);
	}

	public void AdvanceQuest(string id)
	{
		Debug.Log("Advancing quest: " + id);
		Quest quest = GetQuestById(id, activeQuestMap);

		quest.MoveToNextStep();

		if (quest.CurrentStepExists())
		{
			quest.InstantiateCurrentQuestStep(this.transform);
		} 
		else
		{
			ChangeQuestState(id, QuestState.REQUIREMENTS_MET, activeQuestMap);
		}
	}

	public void FinishQuest(string id)
	{
		Debug.Log("Finish quest: " + id);
		Quest quest = GetQuestById(id, activeQuestMap);
		ClaimRewards(quest);

		activeQuestMap.Remove(quest.info.id);
		Destroy(questUI);
	}

	private void ClaimRewards(Quest quest)
	{
		totalScore = totalScore + quest.info.score;
	}

	public void CreateRandomQuest()
	{
		QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

		int randomNum = Random.Range(0, allQuests.Length);
		QuestInfoSO questInfo = allQuests[randomNum];
		Quest questToAdd = new Quest(questInfo);

		questUI = Instantiate(questUIPrefab, questListParent);
		questUI.name = questToAdd.info.id;
		questUI.GetComponent<QuestUI>().Setup(questToAdd);

		notifiedQuestMap.Add(questInfo.id, questToAdd);
	}

	private Quest GetQuestById(string id, Dictionary<string, Quest> map)
	{
		Quest quest = map[id];
		if (quest == null)
		{
			Debug.LogError("ID \"" + id + "\" not found in quest map");
		}
		return quest;
	}

	public void ButtonPress()
	{
		CreateRandomQuest();
	}
}