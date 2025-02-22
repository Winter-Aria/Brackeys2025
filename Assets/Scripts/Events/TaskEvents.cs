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

	public event Action fuelCanCollected;
	public void FuelCanCollected()
	{
		if (fuelCanCollected != null)
		{
			fuelCanCollected();
		}
	}

	public event Action<bool> enterEngineArea;
	public void EnterEngineArea(bool enterOrExit)
	{
		if (enterEngineArea != null)
		{
			enterEngineArea(enterOrExit);
		}
	}
}
