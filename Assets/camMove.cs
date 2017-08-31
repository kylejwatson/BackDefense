using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * TODO: Check that spawn can be reached from goal, simple rule, path of least angle? - if theres an angle try to correct by hopping to square in that direction
 * 
 */
public class camMove : MonoBehaviour {
	[SerializeField]
	GameObject tower;
	[SerializeField]
	GameObject towerFrame;
	[SerializeField]
	GameObject towerFrameBad;
	[SerializeField]
	GameObject goal;
	[SerializeField]
	GameObject spawner;
	ArrayList positions = new ArrayList();
	// Use this for initialization
	void Start () {
		
	}

	void OnGUI(){
		GUI.Label (new Rect (100, 100, 100, 30), "Towers: ");
	}
	
	// Update is called once per frame

	bool checkFill(Vector3 pos){
		if (positions.Contains (pos) || pos.z < 0 || pos.x < 0 || pos.z > 30 || pos.x > 30) {
			Debug.Log ("objec stop fill at " + pos.x + ":" + pos.z);
			return false;
		} else if (goal.transform.position == pos) {
			Debug.Log ("objec found goal at " + pos.x + ":" + pos.z);
			return true;
		} else {
			return checkFill (new Vector3 (pos.x + 1, pos.y, pos.z)) || checkFill (new Vector3 (pos.x - 1, pos.y, pos.z)) || checkFill (new Vector3 (pos.x, pos.y, pos.z + 1)) || checkFill (new Vector3 (pos.x, pos.y, pos.z - 1));
		}
	}


	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		towerFrame.transform.position = Vector3.zero;
		towerFrameBad.transform.position = Vector3.zero;
		bool canPlace = true;
		if (Physics.Raycast (ray.origin,ray.direction,out hit)) {
			if (hit.transform.gameObject.tag == "Ground") {
				towerFrame.transform.position = new Vector3 (Mathf.Round (hit.point.x), 1, Mathf.Round (hit.point.z));
				//positions.Add (towerFrame.transform.position);
				//if (checkFill (towerFrame.transform.position)) {

				//}
				//bool canMove = true;
				//Vector3 pos = goal.transform.position;
				//pos.y = 1;

				//float angle = Mathf.Floor (10000*Mathf.Atan2 (pos.x - spawner.transform.position.x, pos.z - spawner.transform.position.z));
				//Debug.Log (angle);//Mathf.Floor (angle * 10000) == Mathf.Floor (Mathf.PI / 2 * 10000));
			} else if (hit.transform.gameObject.tag != "Frame") {
				if (hit.transform.gameObject.transform.parent != null) {
					towerFrameBad.transform.position = new Vector3 (Mathf.Round (hit.transform.position.x),1, Mathf.Round (hit.transform.position.z));
				} else {
					towerFrameBad.transform.position = hit.transform.gameObject.transform.position;
				}
				canPlace = false;
			} 
		}
		if (Input.GetButtonDown("Fire1") && canPlace)
		{
			positions.Add (towerFrame.transform.position);
			Debug.Log(checkFill(spawner.transform.position);
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
