using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public string levelToLoad;
    //public GameObject Door;
    [Header("Base Values")]
    public float walkingSpeed;//character's default movement speed
    public float runningSpeed;//character's maximum movement speed
    public float jumpForce; //character's jump power
    public float gravity; //world gravity

    [Header("Camera Reference")]
    public Camera playerCamera; //character's point of view

    [Header("Camera Rotation")]
    public float lookSpeed = 2.0f; //speed sensitivity
    public float lookXLimit = 45.0f;//angle of looking up or down

    [Header("Controller Properties")]
    CharacterController characterController; //reference to the character controller component
    Vector3 moveDirection = Vector3.zero; //identifies the direction for movement
    float rotationX = 0f; //base rotation of character

    [Header("Movement Condition")]
    public bool canMove = true; //identifies if the character is allowed to move

    [Header("Temp Values")]
    public float baseHP; //character's base health
    public float tempHP; //changing health value

    [Header("UI Reference")]
    public GameObject UI;
    public Slider sliderHealth;

    [Header("Audio")]
    public AudioSource HitSource;
    public AudioClip _HitClip;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //automatically gets the character controller

        Cursor.lockState = CursorLockMode.Locked; //locks the cursor to the middle of the screen
        Cursor.visible = false; //hides the cursor 

        tempHP = baseHP;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            HitSource.PlayOneShot(_HitClip);
            tempHP -= 5;
            sliderHealth.value = tempHP;

        }

        if (tempHP <= 0)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        //if (collision.gameObject.tag == "Door" && Door.GetComponent<UnlockExit>().activated == true)//(collision.gameObject.tag == "Door" && collision.gameObject.GetComponent<UnlockExit>().Activated == true)
        //{
        //    Debug.Log("Game closes but only in built mode!");
        //    Application.Quit();

        //}

    }
    void OnTriggerEnter(Collider collision)
    {

       
        if (collision.gameObject.tag == "SlashHit")
        {
            HitSource.PlayOneShot(_HitClip);
            tempHP -= 15;
            sliderHealth.value = tempHP;

        }

        if (tempHP <= 0)
        {
            SceneManager.LoadScene(levelToLoad);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //this is for showing the cursor-----
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Cursor.lockState = CursorLockMode.None; //unlocks the cursor from the middle of the screen
            Cursor.visible = true; //reveals the cursor
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; //locks the cursor to the middle of the screen
            Cursor.visible = false; //hides the cursor 
        }
        //end the cursor conditions-----

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //press left shift to run
        //this will return true if the specific button is pressed (L-Shift)
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        //conditions for movement
        //if ? then : else
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //conditions for jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime; //pulls object down
        }

        characterController.Move(moveDirection * Time.deltaTime); //moves the controller

        if (canMove) //code for mouse-view movement
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit); //limits the angle of view
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
