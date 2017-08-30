using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour {
	[SerializeField]
	GameObject tower;
	[SerializeField]
	GameObject towerFrame;
	[SerializeField]
	GameObject towerFrameBad;
	// Use this for initialization
	void Start () {
		
	}

	void OnGUI(){
		GUI.Label (new Rect (100, 100, 100, 30), "Towers: ");
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		towerFrame.transform.position = Vector3.zero;
		towerFrameBad.transform.position = Vector3.zero;
		bool canPlace = true;
		if (Physics.Raycast (ray.origin,ray.direction,out hit)) {
			//hit.transform.Translate (-ray.direction * 0.1);
			if (hit.transform.gameObject.tag == "Ground") {
				towerFrame.transform.position = new Vector3 (Mathf.Round (hit.point.x), hit.point.y, Mathf.Round (hit.point.z));
			} else if (hit.transform.gameObject.tag != "Frame") {
				/*if(hit.transform.gameObject.transform.parent.parent != null){
					towerFrameBad.transform.position = hit.transform.gameObject.transform.parent.parent.position;
				}else */
				if(hit.transform.gameObject.transform.parent != null){
					towerFrameBad.transform.position = hit.transform.gameObject.transform.parent.position;
				}else{
					towerFrameBad.transform.position = hit.transform.gameObject.transform.position;
				}
				canPlace = false;
				//hit.transform.gameObject.gameObject
			}
			//hit.point = new Vector3(Mathf.Round(hit.point.x),hit.point.y,Mathf.Round(hit.point.z));
			//Instantiate (tower, hit.point - Vector3.up,Quaternion.identity);
		}
		if (Input.GetButtonDown("Fire1") && canPlace)
		{
			Instantiate (tower, towerFrame.transform.position,Quaternion.identity);
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
