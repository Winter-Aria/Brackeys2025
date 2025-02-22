using Unity.VisualScripting;
using UnityEngine;

public class TriggerCrossfade : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();

    }


    private void TriggerTransition()
    {
        anim.SetTrigger("Start");
    }
    
        
    
}
