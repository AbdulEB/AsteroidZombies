using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeleteScript : MonoBehaviour {

	void OnEnable() 
	{
		Invoke("Destroy", 5f);	
	}

	void Destroy()
	{
		gameObject.SetActive(false);
	}

	void OnDisable()
	{
		CancelInvoke();
	}
}
