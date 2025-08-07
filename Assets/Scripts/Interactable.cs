using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;

public class Interactable : MonoBehaviour
{
    [Header("UI")]
    public Text text;
    [Header("Object Type")]
    public string ObjectType;
    public GameObject Light;
    public bool PlayerInRange;
    //[Header("If equippable")]
    //public GameObject thisObject;
    [Header("If switch")]
    public Material ActivationMaterial;
    public bool SwitchActivated;
    public PlayerStats PlayerValues;

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
           PlayerInRange = true;
           
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
            PlayerInRange = false;
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((ObjectType == "Equippable" && this.gameObject.activeInHierarchy == true && PlayerInRange == true) || (ObjectType == "Switch" && SwitchActivated == false) && PlayerInRange == true)
        {
            text.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                if (ObjectType == "Switch")
                {
                    text.gameObject.SetActive(false);
                    Light.GetComponent<Light>().color = Color.green;
                    gameObject.GetComponent<MeshRenderer>().material = ActivationMaterial;
                    SwitchActivated = true;
                }
                else if (ObjectType == "Equippable")
                {
                    text.gameObject.SetActive(false);
                    if (gameObject.name == "HealPotion")
                    {
                        PlayerValues.HP += 5;
                    }
                    else if (gameObject.name == "DaggerBuff")
                    {
                        PlayerValues.StnDur += 1;
                    }
                    else if (gameObject.name == "SpeedBuff")
                    {

                        PlayerValues.WlkSPD += 2;
                        PlayerValues.RunSPD += 3;
                    }
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
