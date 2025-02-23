using UnityEngine;

public class EngineDetectArea : MonoBehaviour
{
    [SerializeField] private Animator buttonPrompt;
    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			EventManager.Instance.taskEvents.EnterEngineArea(true);
			buttonPrompt.Play("PromptShow");
		}

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			EventManager.Instance.taskEvents.EnterEngineArea(false);
            buttonPrompt.Play("PromptHide");
        }
	}
}
