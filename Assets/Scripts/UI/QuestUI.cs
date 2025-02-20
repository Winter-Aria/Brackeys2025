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
	private float progress = 0;

	private void OnEnable()
	{
		EventManager.Instance.questSystemEvents.updateProgress += UpdateProgress;
		UpdateTimerText();
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
		timerText.text = quest.timeToShow.ToString();
		progressText.text = "0";
	}

	private void Update()
	{
		if (gameObject.activeInHierarchy) 
		{
			UpdateTimerText();
		}
	}

	private void UpdateTimerText()
	{
		if (quest != null)
		{
			timerText.text = Mathf.Floor(quest.timeToShow).ToString();
		}
		progressText.text = progress.ToString();
	}

	private void UpdateProgress(int progressParam)
	{
		progress = progressParam;
	}
}
