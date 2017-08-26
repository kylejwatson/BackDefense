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
		GameObject aimObject = null;
		foreach (Collider col in cols) {
			GameObject g = col.gameObject;
			if (g != null && g.tag == "Enemy"){
				isAiming = true;
				Vector3 dir = (g.transform.position - this.transform.position);
				Vector3 newDir = Vector3.RotateTowards (transform.forward, dir,1F,0.0F);
				transform.rotation = Quaternion.LookRotation (newDir);
				rot = transform.rotation;
				transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
				aimObject = g;
			}
		}
		if (isAiming) {
			cnt++;
			if (cnt > 50) {
				GameObject newBullet = Instantiate (bul, transform.position, transform.rotation);
				newBullet.GetComponent<BulletMove> ().org = transform.position;
				newBullet.GetComponent<BulletMove> ().dist = (transform.position - aimObject.transform.position).magnitude;
				newBullet.GetComponent<BulletMove> ().enm = aimObject;
				cnt = 0;
			}
		}
	}
}
