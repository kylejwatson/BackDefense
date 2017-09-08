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
	[SerializeField]
	GameObject decalHitWall;
	[SerializeField]
	Camera endCamera;
	[SerializeField]
	GameObject curCanvas;
	[SerializeField]
	GameObject endCanvas;
	[SerializeField]
	GameObject fps;
	[SerializeField]
	GameObject spawn;
	int cnt;
	int health = 3;

	// Use this for initialization
	void Start () {
		fps.transform.position = spawn.transform.position;
		fps.transform.Translate (Vector3.up * 5);
	}
		
	public void hit(){
		health--;
		if (health <1) {
			//dies
			endCamera.gameObject.SetActive(true);
			curCanvas.SetActive (false);
			endCanvas.SetActive (true);
			Destroy (fps);
		}
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
				//Instantiate (point, hit.point, new Quaternion (0, 0, 0, 0));
				Vector3 norm =-hit.normal;//Vector3.one * 180 - hit.normal;
				Quaternion rot = Quaternion.LookRotation(norm);
				GameObject newDecal = Instantiate(decalHitWall, hit.point + (hit.normal * 0.001F), rot);

				if (hit.transform.tag == "Tower") {
					newDecal.transform.SetParent (hit.transform);
					hit.transform.GetComponent<towerHealth> ().hit ();
				}
			}
			Debug.DrawRay (ray.origin, ray.direction, Color.red);
			transform.parent.GetComponent<LineRenderer> ().SetPosition (1, hit.point);
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
