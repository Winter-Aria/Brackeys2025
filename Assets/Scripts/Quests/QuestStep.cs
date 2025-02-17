using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;


    //Delete itself once the step is finished
    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            Destroy(this.gameObject);
        }
    }
}
