using System;

public class UIEvents
{
	public event Action<Quest> randomQuestCreated;
	public void RandomQuestCreated(Quest quest)
	{
		if (randomQuestCreated != null)
		{
			randomQuestCreated(quest);
		}
	}

	public event Action<string, int> startNewStep;
	public void StartNewStep(string id, int numOfProgress)
	{
		if (startNewStep != null)
		{
			startNewStep(id, numOfProgress);
		}
	}
}