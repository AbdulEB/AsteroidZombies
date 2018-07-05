using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireScript : MonoBehaviour {

	public GameObject bullet;
	public int poolSize = 25;
	private List<GameObject> bullets;
	public AudioClip shootSound;

	private AudioSource source;


	
	// Use this for initialization
	void Start ()
	{
		bullets = new List<GameObject>();
		for (int i = 0; i < poolSize; i++)
		{
			GameObject obj = (GameObject) Instantiate(bullet);
			obj.SetActive(false);
			bullets.Add(obj);
		}
	}

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}
	private void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F) || Input.GetAxis("Fire1") > 0)
		{
			if(!gameObject.GetComponent<MovementScript>().isDying) {
				
				float vol = Random.Range(0.5f, 1.0f);
				source.PlayOneShot(shootSound,vol);
				Fire(); //CANT FIRE IF DEAD!!
}
		}
	}

	void Fire () {
		for (int i = 0; i < bullets.Count; i++)
		{
			if (!bullets[i].activeInHierarchy)
			{
				bullets[i].transform.position = transform.position; 
				bullets[i].transform.rotation = transform.rotation;
				bullets[i].SetActive(true);
				Physics2D.IgnoreCollision(bullets[i].gameObject.GetComponent<Collider2D>(), transform.gameObject.GetComponent<Collider2D>());
				break;
			}
		}
	}
}
