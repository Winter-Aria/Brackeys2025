using System;
using System.Collections.Generic;

public class QuestSystemEvents
{
	public event Action<string> onStartQuest;
	public void StartQuest(string id)
	{
		if (onStartQuest != null)
		{
			onStartQuest(id);
		}
	}

	public event Action<string> onFinishQuest;
	public void FinishQuest(string id)
	{
		if (onFinishQuest != null)
		{
			onFinishQuest(id);
		}
	}

	public event Action<string> onAdvanceQuest;
	public void AdvanceQuest(string id)
	{
		if (onAdvanceQuest != null)
		{
			onAdvanceQuest(id);
		}
	}

	public event Action<Quest> onQuestStateChange;
	public void QuestStateChange(Quest quest)
	{
		if (onQuestStateChange != null)
		{
			onQuestStateChange(quest);
		}
	}

	public event Action onTaskProgress;
	public void TaskProgress()
	{
		if (onTaskProgress != null)
		{
			onTaskProgress();
		}
	}


	public event Action<int> updateProgress;
	public void UpdateProgress(int progress)
	{
		if (updateProgress != null)
		{
			updateProgress(progress);
		}
	}
}