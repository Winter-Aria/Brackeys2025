using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI displayName;
	[SerializeField] private TextMeshProUGUI roomName;
	[SerializeField] private TextMeshProUGUI timer;
	[SerializeField] private TextMeshProUGUI progress;

	[SerializeField] private Button acknowledgeButton;
	[SerializeField] private Button completeButton;

	private Quest quest;

	public void Setup(Quest quest, QuestManager questManager)
	{
		this.quest = quest;

		displayName.text = quest.info.displayName;
		roomName.text = quest.info.displayRoom;
		//timer.text = quest.timeToAcknowledge.ToString();
		progress.text = "0";
	}
}
