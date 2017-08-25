using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward*0.1F);
	}

	void OnTriggerEnter(Collider col){
		Debug.Log (col.gameObject.tag + "jjhjj");
		if (col.gameObject.tag == "Enemy") {
			Destroy (col.gameObject);
			Destroy (this);
		}
	}
}
