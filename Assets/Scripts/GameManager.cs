using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialise tasks
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup / 60 > 1)
        {
            //give task
        }
    }
}
