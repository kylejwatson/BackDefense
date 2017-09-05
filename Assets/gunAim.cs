﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunAim : MonoBehaviour {
	[SerializeField]
	GameObject point;
	[SerializeField]
	Texture2D cross;
	[SerializeField]
	Camera m_camera;
	int cnt;
	// Use this for initialization
	void Start () {
		
	}

	void OnGUI(){
		Rect rect = new Rect (Screen.width / 2 - 6, Screen.height / 2 - 6, 15, 15);
		GUI.DrawTexture (rect, cross);
	}
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = m_camera.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
		transform.parent.GetComponent<LineRenderer> ().SetPosition (0, transform.position);
		if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
			if (Input.GetButtonDown ("Fire1")) {
				Instantiate (point, hit.point, new Quaternion (0, 0, 0, 0));
			}
			Debug.DrawRay (ray.origin, ray.direction, Color.red);
			transform.parent.GetComponent<LineRenderer> ().SetPosition (1, hit.point);
			if (hit.transform.tag == "Tower") {
				hit.transform.GetComponent<towerHealth> ().hit ();
			}
		} else {
			transform.parent.GetComponent<LineRenderer> ().SetPosition (1, transform.position+transform.forward*30);
		}
		if (Input.GetButtonDown ("Fire1")) {
			transform.parent.GetComponent<LineRenderer> ().enabled = true;
		}

		if(transform.parent.GetComponent<LineRenderer> ().enabled){
			cnt++;
			if(cnt>10){
				cnt = 0;
				transform.parent.GetComponent<LineRenderer> ().enabled = false;
			}
		}
	}
}
