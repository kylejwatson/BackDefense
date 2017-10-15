using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour {

	public GameObject rightFoot;
	public GameObject leftFoot;
	float dist = 0;
	float d = 0;
	// Use this for initialization
	void Start () {
		dist = Vector3.Distance (transform.position, leftFoot.transform.position);
		d = transform.position.x - leftFoot.transform.position.x;
		Debug.Log (d);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * 0.1f * Input.GetAxisRaw ("Vertical") + Vector3.right * 0.1f *  Input.GetAxisRaw ("Horizontal"));
		if (dist < Vector3.Distance (transform.position, leftFoot.transform.position)) {
			leftFoot.transform.position = new Vector3 (transform.position.x+0.5f*Input.GetAxisRaw ("Horizontal"), 0, rightFoot.transform.position.z+1.5f*Input.GetAxisRaw ("Vertical"));
		}if (dist < Vector3.Distance (transform.position, rightFoot.transform.position)) {
			rightFoot.transform.position = new Vector3 (transform.position.x+0.5f*Input.GetAxisRaw ("Horizontal"), 0,  leftFoot.transform.position.z+1.5f*Input.GetAxisRaw ("Vertical"));
		}
	}
}
