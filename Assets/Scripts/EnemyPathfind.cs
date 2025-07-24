//updated enemy script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [Header("AI Navigation")]
    public NavMeshAgent navMeshAgent;
    [Header("Audio")]
    public AudioSource DeathSource;
    public AudioSource HitSource;
    public AudioClip _Deathclip;
    public AudioClip _Hitclip;
    [Header("AI Targetting")]
    public GameObject player;
    //public GameObject detector;
    public bool Active = false;
    public int enemyHealth;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); //automatically references the navmesh agent component
        player = GameObject.Find("Player"); //finds the player clone to set the ai destination/ target
    }

    // Update is called once per frame
    void Update()
    {
        if (Active == true)
        {
            navMeshAgent.SetDestination(player.transform.position); //moves the ai object to the target position

        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            //HitSource.PlayOneShot(_Hitclip);
            AudioSource.PlayClipAtPoint(_Hitclip, this.gameObject.transform.position);
            enemyHealth -= 3;
            if (enemyHealth <= 0)
            {
                //DeathSource.PlayOneShot(_Deathclip);
                AudioSource.PlayClipAtPoint(_Deathclip, this.gameObject.transform.position);
                Destroy(this.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("InRadius"))
        {
            Active = true;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("InRadius"))
        {
            Active = false;
        }
    }
}