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

	public event Action tabPressed;
	public void TabPressed()
	{
		if (tabPressed != null)
		{
			tabPressed();
		}
	}
}