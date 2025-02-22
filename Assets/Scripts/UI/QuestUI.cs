using System;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI displayName;
	[SerializeField] private TextMeshProUGUI roomName;
	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private TextMeshProUGUI progressText;

	private float timeToAcknowledge;
	private float timeToComplete;
	private bool timeCompleted = false;
	private bool acknowledged = false;

	private Quest quest;
	private float progress = 0;

	private void OnEnable()
	{
		EventManager.Instance.questSystemEvents.updateProgress += UpdateProgress;
	}

	private void OnDisable()
	{
		EventManager.Instance.questSystemEvents.updateProgress -= UpdateProgress;
	}

	public void Setup(Quest quest)
	{
		this.quest = quest;

		displayName.text = quest.info.displayName;
		roomName.text = quest.info.displayRoom;
		timerText.text = quest.timeToAcknowledge.ToString();
		progressText.text = "0";

		timeToAcknowledge = quest.timeToAcknowledge;
		timeToComplete = quest.timeToComplete;
	}

	private void Update()
	{
		if (quest.state.Equals(QuestState.IN_PROGRESS) && acknowledged == false)
		{
			acknowledged = true;
		}

		if (acknowledged == false && timeCompleted == false)
		{
			timeToAcknowledge -= Time.deltaTime;
			timerText.text = Math.Truncate(timeToAcknowledge).ToString();
		}

		if (timeToAcknowledge <= 0.0f && acknowledged == false)
		{
			EventManager.Instance.questSystemEvents.StartQuest(quest.info.id);
			acknowledged = true;
		}

		if (acknowledged == true && timeCompleted == false)
		{
			timeToComplete -= Time.deltaTime;
			timerText.text = Math.Truncate(timeToComplete).ToString();
			if (timeToComplete <= 0.0f && timeCompleted == false)
			{
				EventManager.Instance.questSystemEvents.QuestUncompleted();
				timeCompleted = true;
			}
		}
		progressText.text = progress.ToString();
	}

	private void UpdateProgress(int progressParam)
	{
		progress = progressParam;
	}
}