using System;

public class TaskEvents
{
	public event Action rubbishCollected;
	public void RubbishCollected()
	{
		if (rubbishCollected != null)
		{
			rubbishCollected();
		}
	}
}
