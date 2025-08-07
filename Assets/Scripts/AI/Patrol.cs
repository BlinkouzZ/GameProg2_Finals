using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public AIManager aiManager;

    public Transform[] points;

    public float distance = 1f;

    public int destinationPoint = 0;

    public NavMeshAgent navMeshAgent;

    public Material inactive; //blue
    public Material alert; //Green
    public Material hostile; //red

    public bool isAIHostile = false;
    public bool Stunned = false;

    public GameObject player;
    public GameObject sword;

    public int aiLevel;

    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.autoBraking = false;
        if (aiManager.isAiAwake)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            Debug.LogError("No destination point");
            enabled = false;
            return;
        }
        navMeshAgent.destination = points[destinationPoint].position;
        destinationPoint = (destinationPoint + 1 ) % points.Length;
    }

    private void Update()
    {
        if (aiManager.isAiAwake && Stunned == false)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < distance && !isAIHostile)
            {
                this.GetComponent<Renderer>().material = alert;
                GoToNextPoint();
                sword.gameObject.SetActive(false);
            }
        }

        if (aiManager.isAiAwake && Stunned == false)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < distance && !isAIHostile)
            {
                this.GetComponent<Renderer>().material = alert; //green
                GoToNextPoint();
                sword.gameObject.SetActive(false);
            }
            else if(isAIHostile)
            {
                navMeshAgent.SetDestination(player.transform.position);
                this.GetComponent<Renderer>().material = hostile; //red
                sword.gameObject.SetActive(true);
            }
        }
        else
        {
            this.GetComponent<Renderer>().material = inactive; //blue
            navMeshAgent.autoBraking = true;
            sword.gameObject.SetActive(false);
        }
        
    }

    private void RemoveStun()
    {
        Stunned = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Dagger"))
        {
            
            Stunned = true;
            Invoke("RemoveStun", player.GetComponent<PlayerMovement>().StabStunAmount);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isAIHostile = true;
        }
       
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAIHostile = false;
        }
    }
}
