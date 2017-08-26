using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
	[SerializeField]
	float speed;
	public Vector3 org;
	public float dist;
	public GameObject enm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward*speed);
		if ((org - transform.position).magnitude > dist) {
			Destroy (enm);
			Destroy(this.gameObject);
		}
	}
}
