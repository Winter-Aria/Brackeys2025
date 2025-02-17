using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

	private void Awake()
	{
		questMap = CreateQuestMap();
	}

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
		//Sends the QuestStateChange() event for each quest in the map 
		foreach (Quest quest in questMap.Values)
		{
			EventManager.Instance.questSystemEvents.QuestStateChange(quest);
		}
	}

	public void StartQuest(string id)
    {
        Debug.Log("Start quest: " + id);
    }

	public void AdvanceQuest(string id)
	{
		Debug.Log("Advance quest: " + id);
	}

	public void FinishQuest(string id)
	{
		Debug.Log("Finish quest: " + id);
	}

	private Dictionary<string, Quest> CreateQuestMap()
    {
        //Loads all QuestInfoSOs from Assets/Resources/Quests
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        //Create the map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach(QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Two quests with same id: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest  = questMap[id];
        if (quest== null)
        {
            Debug.LogError("ID \"" + id + "\" not found in quest map");
        }
        return quest;
    }
}
