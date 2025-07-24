using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockExit : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelToLoad;
    public bool EndGameInstead = false; //If true, closes the game
    [Header("Enemies Required")]
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;
    public GameObject enemy8;
    public GameObject enemy9;
    public GameObject enemy10;
    public GameObject enemy11;
    public GameObject enemy12;
    public GameObject enemy13;
    public GameObject enemy14;
    public GameObject enemy15;
    public GameObject enemy16;
    public GameObject enemy17;
    [Header("Door Customization")]
    public Material GetMaterial;
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
        if (activated == false && !enemy1 && !enemy2 && !enemy3 && !enemy4  && !enemy5 && !enemy6 && !enemy7 && !enemy8 && !enemy9 && !enemy10 && !enemy11 && !enemy12 && !enemy13 && !enemy14 && !enemy15 && !enemy16 && !enemy17)
        {
            AudioSource.PlayOneShot(_OpenDoor);
            Door.GetComponent<MeshRenderer>().material = GetMaterial;
            activated = true;
        }
    }
}
