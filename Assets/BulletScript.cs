using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

	public float speed = 20f;
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, speed * Time.deltaTime, 0);
	}
}
