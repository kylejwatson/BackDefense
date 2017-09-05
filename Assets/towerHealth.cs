using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerHealth : MonoBehaviour {
	int health = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void hit(){
		health--;
		if (health < 1) {
			Destroy (gameObject);
		}
	}
}
