using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	// Use this for initialization
	public GameObject goal;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward*0.1F);
		Vector3 dir = (goal.transform.position - this.transform.position).normalized;
		if (!Physics.Raycast (transform.position, dir, 2F)) {
			Vector3 newDir = Vector3.RotateTowards (transform.forward, dir,0.5F,0.0F);
			transform.rotation = Quaternion.LookRotation (newDir);
		}
		if (Physics.Raycast (transform.position,transform.forward,1F)) {
			for (var i = 0; i < 150; i += 22) {
				Transform t = transform;
				t.Rotate (Vector3.up * i);
				if (!Physics.Raycast (t.position, t.forward, 1F)) {
					transform.Rotate (Vector3.up * i);
					break;
				}
				t.Rotate (Vector3.up * -2*i);
				if (!Physics.Raycast (t.position, t.forward, 1F)) {
					transform.Rotate (Vector3.up * i);
					break;
				}
				t.Rotate(Vector3.up * i);
			}
		}
	}
}
