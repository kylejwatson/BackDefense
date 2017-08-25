using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAim : MonoBehaviour {

	int cnt =0; 
	[SerializeField]
	GameObject bul;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool isAiming = false;
		Collider[] cols = Physics.OverlapSphere (transform.position, 5F);
		Quaternion rot = transform.rotation;
		foreach (Collider col in cols) {
			GameObject g = col.gameObject;
			if (g.tag == "Enemy"){
				isAiming = true;
				Vector3 dir = (g.transform.position - this.transform.position);
				Vector3 newDir = Vector3.RotateTowards (transform.forward, dir,1F,0.0F);
				transform.rotation = Quaternion.LookRotation (newDir);
				rot = transform.rotation;
				transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
			}
		}
		if (isAiming) {
			cnt++;
			if (cnt > 50) {
				Instantiate (bul, transform.position, rot);
				cnt = 0;
			}
		} else {
			//cnt = 0;
		}
	}
}
