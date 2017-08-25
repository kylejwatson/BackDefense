using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour {
	[SerializeField]
	GameObject tower;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray.origin,ray.direction,out hit)) {
				//hit.transform.Translate (-ray.direction * 0.1);
				Instantiate (tower, hit.point - Vector3.up,Quaternion.identity);
			}
		}

		if (Input.mousePosition.x < 0) {
			Camera.main.transform.Translate (Vector3.left/2);
		} else if (Input.mousePosition.x > Screen.width) {
			Camera.main.transform.Translate (Vector3.right/2);
		}
		if(Input.mousePosition.y < 0) {
			Camera.main.transform.Translate (Vector3.down/2);
		} else if (Input.mousePosition.y > Screen.height) {
			Camera.main.transform.Translate (Vector3.up/2);
		}
	}
}
