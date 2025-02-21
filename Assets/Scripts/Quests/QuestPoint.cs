using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestPoint : MonoBehaviour
{
	private string questId;
	[SerializeField] private QuestState currentQuestState;

	[Header("Config")]
	[SerializeField] private bool startButton = true;
	[SerializeField] private bool endButton = true;

	private void Start()
	{
		questId = gameObject.transform.parent.name;
	}

	private void OnEnable()
	{
		EventManager.Instance.questSystemEvents.onQuestStateChange += QuestStateChange;
	}

	private void OnDisable()
	{
		EventManager.Instance.questSystemEvents.onQuestStateChange -= QuestStateChange;
	}

	public void SubmitPressed()
	{
		if (currentQuestState.Equals(QuestState.NOTIFIED) && startButton)
		{
			EventManager.Instance.questSystemEvents.StartQuest(questId);
		}
		else if (currentQuestState.Equals(QuestState.REQUIREMENTS_MET) && endButton)
		{
			EventManager.Instance.questSystemEvents.FinishQuest(questId);
		}
	}

	private void QuestStateChange(Quest quest)
	{
		if (quest.info.id.Equals(questId))
		{
			currentQuestState = quest.state;
		}
	}
}