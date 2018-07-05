using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MovementScript : MonoBehaviour
{
    public float speed;
    public float rotate;
    private float leftConstraint, rightConstraint, bottomConstraint, topConstraint;
    private const float buffer = 0.5f;
    private Animator anim;
    public bool isDying, chrisMode = false;
    private int health;

    // Use this for initialization
    void Start()
    {
        health = 100;
        isDying = false;
        anim = GetComponent<Animator>();
        float distanceZ = Mathf.Abs(Camera.main.transform.position.z + transform.position.z);
        leftConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        topConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (health <= 0)
            {
                anim.SetBool("isDying", true);
                anim.enabled = true;
                isDying = true;
                /*Temporary workaround*/
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),
                    transform.gameObject.GetComponent<Collider2D>());
            }
            else
            {
                Debug.Log("Health: " + health);
                health -= 20;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Restart();
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    void Movement()
    {
        if (!isDying)
        {
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");
            
            if (chrisMode)
            {
//                bool isTraveling = DoTraveling(vertical);
//                bool isRotating = DoRotation(horizontal);
//                if(!isTraveling || !isRotating) anim.enabled = false;
                if (DoRotation(horizontal));
                else if (DoTraveling(vertical));
                else anim.enabled = false;
                
            }
            else
            {
                if (DoTraveling(vertical));
                else if (DoRotation(horizontal));
                else anim.enabled = false;
            }

            DoTorus();
        }
    }

    bool DoRotation(float horizontal)
    {
        if (horizontal > 0 || horizontal < 0)
        {
            transform.Rotate(0, 0, horizontal * -rotate);
            return true;
        }

        return false;
    }

    bool DoTraveling(float vertical)
    {
        if (vertical > 0)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            anim.enabled = true;
            return true;
        }

        return false;
    }

    void DoTorus()
    {
        if (transform.position.x < leftConstraint - buffer)
        {
            transform.position = new Vector3(rightConstraint + buffer, transform.position.y, transform.position.z);
        }

        if (transform.position.x > rightConstraint + buffer)
        {
            transform.position = new Vector3(leftConstraint - buffer, transform.position.y, transform.position.z);
        }

        if (transform.position.y < bottomConstraint - buffer)
        {
            transform.position = new Vector3(transform.position.x, topConstraint + buffer, transform.position.z);
        }

        if (transform.position.y > topConstraint + buffer)
        {
            transform.position = new Vector3(transform.position.x, bottomConstraint - buffer, transform.position.z);
        }
    }
}