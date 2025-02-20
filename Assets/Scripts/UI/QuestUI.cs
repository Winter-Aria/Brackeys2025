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

	[SerializeField] private Button acknowledgeButton;
	[SerializeField] private Button completeButton;

	private Quest quest;
	private float timeToAcknowledge;
	private float timeToComplete;
	private bool timeCompleted = false;
	private float progress = 0;

	private void OnEnable()
	{
		EventManager.Instance.questSystemEvents.updateProgress += UpdateProgress;
	}

	private void OnDisable()
	{
		EventManager.Instance.questSystemEvents.updateProgress -= UpdateProgress;
	}

	public void Setup(Quest quest, QuestManager questManager)
	{
		this.quest = quest;
		timeToComplete = quest.info.timeToComplete;
		timeToAcknowledge = quest.timeToAcknowledge;

		displayName.text = quest.info.displayName;
		roomName.text = quest.info.displayRoom;
		timerText.text = quest.timeToAcknowledge.ToString();
		progressText.text = "0";
	}

	private void Update()
	{
		if (!timeCompleted)
		{
			timeToAcknowledge -= Time.deltaTime;
			timerText.text = Math.Truncate(timeToAcknowledge).ToString();
			if (timeToAcknowledge <= 0.0f && timeCompleted == false)
			{
				timeToComplete -= Time.deltaTime;
				timerText.text = Math.Truncate(timeToComplete).ToString();
				if (timeToComplete <= 0.0f && timeCompleted == false)
				{
					Debug.Log("Finished");
					timeCompleted = true;
				}
			}
		}
		progressText.text = progress.ToString();
	}

	private void UpdateProgress(int progressParam)
	{
		progress = progressParam;
	}
}
