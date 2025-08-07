using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockExit : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelToLoad;
    public bool EndGameInstead = false; //If true, closes the game
    [Header("Switches Required")]
    public GameObject Switch1;
    public GameObject Switch2;
    public GameObject Switch3;
    public GameObject Switch4;
    [Header("Door Customization")]
    public GameObject Door;
    public bool activated = false;
    public AudioSource AudioSource;
    public AudioClip _OpenDoor;


    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player" && activated == true)
        {
            Debug.Log("Game closes but only in built mode!");
            if (EndGameInstead == true)
            {
                Debug.Log("Game closes but only in built mode!");
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (activated == false && Switch1.GetComponent<Interactable>().SwitchActivated == true && Switch2.GetComponent<Interactable>().SwitchActivated == true && Switch3.GetComponent<Interactable>().SwitchActivated == true && Switch4.GetComponent<Interactable>().SwitchActivated == true)
        {
            AudioSource.PlayOneShot(_OpenDoor);
            Door.GetComponent<MeshRenderer>().enabled = false;
            activated = true;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
