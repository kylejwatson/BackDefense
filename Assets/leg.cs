using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leg : MonoBehaviour {
	public GameObject body;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<LineRenderer> ().SetPosition (0, transform.position);
		GetComponent<LineRenderer> ().SetPosition (1, body.transform.position);
	}
}
