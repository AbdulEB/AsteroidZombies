using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurserMovementScript : MonoBehaviour {
	
	public float speed;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float vertical = transform.position.x;
		float horizontal = transform.position.y;
		GameObject sq = GameObject.FindGameObjectWithTag("Player");
		float v2 = sq.transform.position.x;
		float h2 = sq.transform.position.y;
		
		
		
		if (vertical < v2 && horizontal < h2) {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (vertical > v2 && horizontal < h2) {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (vertical < v2 && horizontal > h2) {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (vertical > v2 && horizontal > h2) {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (vertical > v2)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (vertical < v2)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else if (horizontal > h2)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (horizontal < h2)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
		

		
	}
}
