using System;

public class TaskEvents
{
	public event Action rubbishCollected;
	public event Action itemCollected;
	public event Action itemDelivered;

	public void RubbishCollected()
	{
		if (rubbishCollected != null)
		{
			rubbishCollected();
		}
	}
	public void ItemCollected()
	{
		if (itemCollected != null)
		{
			itemCollected();
		}

	}
	public void ItemDelivered()
	{
		if (itemDelivered != null)
		{
			itemDelivered();
		}
	}
}
