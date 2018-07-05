using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class EnemyMovementScript : MonoBehaviour
{
    public float speed;
    private Animator anim;
    public bool isDying;
    private float configuredSpeed;
    

    // Use this for initialization
    void Start()
    {
        configuredSpeed = speed;
        isDying = false;
        anim = GetComponent<Animator>();
        //if (anim != null) anim.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("inRange", true);
            speed = 0;
        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            if (anim != null) // IF WE ARE USING AN ANIMATION
            {
                anim.enabled = false;
//                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),
//                    GameObject.FindGameObjectWithTag("Projectile").GetComponent<Collider2D>(), true);
                anim.SetBool("isDying", true);
                anim.enabled = true;
                isDying = true;
                /*Allows player to not be killed by walking into an exploding enemy*/
                Physics2D.IgnoreCollision(transform.gameObject.GetComponent<Collider2D>(),
                                                    other.gameObject.GetComponent<Collider2D>());
                /*Allows player to not be killed by walking into an exploding enemy*/
            }
            else
            {
                // IF WE ARE USING JUST THE BASIC POLYGON
                gameObject.SetActive(false);
            }
        }
    }
    
    
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            speed = configuredSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDying)
        {
            if (anim != null && !anim.isActiveAndEnabled)
            {
                anim.enabled = true;
            }

            Movement();
        }
    }


    void Movement()
    {
        float vertical = transform.position.y;
        float horizontal = transform.position.x;
        GameObject sq = GameObject.FindGameObjectWithTag("Player");
        float v2 = sq.transform.position.y;
        float h2 = sq.transform.position.x;

        transform.Translate(Vector2.right * speed * Time.deltaTime);

 if (vertical > v2)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else if (vertical < v2)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        if (horizontal > h2)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontal < h2)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
    

   
}