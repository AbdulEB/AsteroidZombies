using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemy;
    public int poolSize;
    private List<GameObject> enemies;

    // Use this for initialization
    void Start()
    {
        enemies = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject) Instantiate(enemy);
            obj.SetActive(false);
            enemies.Add(obj);
        }

        Spawn();
    }

    // Update is called once per frame
    private void Update()
    {
        if (NoEnemy()) Spawn();
    }

    bool NoEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].activeInHierarchy)
            {
                return false;
            }
        }

        return true;
    }

    void Spawn()
    {
        float distanceZ = Mathf.Abs(Camera.main.transform.position.z + transform.position.z);

        float left = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        float right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        float bottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        float top = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                float x = Random.Range(left, right), y = Random.Range(bottom, top), z = 0f;


                while (Math.Abs(x - transform.position.x) < 10 && Math.Abs(y - transform.position.y) < 10)
                {
                    x = Random.Range(left, right);
                    y = Random.Range(bottom, top);
                }

                enemies[i].transform.position = new Vector3(x, y, z);
            //    enemies[i].transform.rotation = //transform.rotation;
                enemies[i].SetActive(true);
               // enemies[i].GetComponent<EnemyMovementScript>().isDying = false;
                /*THIS TURNS COLLISION ON IF ITS OFF*/
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), enemies[i].GetComponent<Collider2D>(), false);
                /*THIS TURNS COLLISION ON IF ITS OFF*/

                // break;
            }
        }
    }
}