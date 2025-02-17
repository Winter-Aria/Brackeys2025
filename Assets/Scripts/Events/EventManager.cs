using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

	public TaskEvents taskEvents;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("More than one Event Manager found in scene");
		}
		Instance = this;

		taskEvents = new TaskEvents();
	}

}
