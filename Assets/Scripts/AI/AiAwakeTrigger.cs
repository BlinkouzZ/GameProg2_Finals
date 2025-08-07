using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAwakeTrigger : MonoBehaviour
{
    public AIManager AIManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("AI is now awake");
            AIManager.isAiAwake = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AI"))
        {
            AIManager.isAiAway = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AIManager.isAiAwake = false;
        }

        if (other.CompareTag("AI"))
        {
            AIManager.isAiAway = true;
        }
    }
}
