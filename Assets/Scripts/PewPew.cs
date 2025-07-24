//fire bullet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireBullet : MonoBehaviour
{
    public AudioSource AudioSource;
    public Rigidbody projectile;
    //public Rigidbody projectileSlash;
    public Transform barrelEnd;
    public float speed = -100f;
    public AudioClip _clip;
    public bool isAuto;
    public float cooldown = 2f;
    public int bulletCount;
    public bool CanAttack = true;
    public Animator animator1;

    public void EndAnimation()
    {
        animator1.SetTrigger("EndAttack");
        CanAttack = true;
    }

    void Update()
    {
        //switch
        if (cooldown <= 0)
        {

            bulletCount = 5;
            cooldown = 2f;
        }
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    isAuto = true;
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    isAuto = false;
        //}

        if (bulletCount > 0 && CanAttack == true)
        {

            if (isAuto)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    //animator1.SetBool("Attack", true);
                    AudioSource.PlayOneShot(_clip);
                    bulletCount--;
                    Rigidbody instantiatedProjectile = Instantiate(projectile, barrelEnd.transform.position, barrelEnd.transform.rotation);
                    instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(speed - 25, 0, 0));
                    //Rigidbody instantiatedSlash = Instantiate(projectile, barrelEnd.transform.position, barrelEnd.transform.rotation);
                    //instantiatedSlash.velocity = transform.TransformDirection(new Vector3(speed - 25, 0, 0));
                    animator1.SetTrigger("Attack");
                    Invoke("EndAnimation", 0.21f);

                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //animator1.SetBool("Attack", true);
                    CanAttack = false;
                    AudioSource.PlayOneShot(_clip);
                    bulletCount--;
                    Rigidbody instantiatedProjectile = Instantiate(projectile, barrelEnd.transform.position, barrelEnd.transform.rotation);
                    instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
                    //Rigidbody instantiatedSlash = Instantiate(projectile, barrelEnd.transform.position, barrelEnd.transform.rotation);
                    //instantiatedSlash.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
                    animator1.SetTrigger("Attack");
                    Invoke("EndAnimation", 0.21f);
                }
            }
        }
        else
        {
            cooldown -= 0.1f;
            //Debug.Log("Reload");
        }

    }
}