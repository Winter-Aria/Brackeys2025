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
}