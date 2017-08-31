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
	public ArrayList positions = new ArrayList();
	ArrayList checkedPositions = new ArrayList();
	float deltaTime = 0.0f;
	// Use this for initialization
	void Start () {
		
	}

	void OnGUI(){
		float msec = deltaTime * 1000.0F;
		float fps = 1.0f / deltaTime;
		string text = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label (new Rect (100, 100, 100, 30), text);

	}
	
	// Update is called once per frame

	bool checkFill(Vector3 pos,float maxx, float maxz, float minx, float minz){
		if (checkedPositions.Contains(pos) || positions.Contains (pos) || pos.z < minz-1 || pos.x < minx-1 || pos.z > maxz+2 || pos.x > maxx+2) {
			//Debug.Log ("objec stop fill at " + pos.x + ":" + pos.z);
			return false;
		} else if (goal.transform.position == pos) {
			//Debug.Log ("objec found goal at " + pos.x + ":" + pos.z);
			return true;
		} else {
			checkedPositions.Add (pos);
			return checkFill (new Vector3 (pos.x + 1, pos.y, pos.z),maxx,maxz,minx,minz) || checkFill (new Vector3 (pos.x - 1, pos.y, pos.z),maxx,maxz,minx,minz) || checkFill (new Vector3 (pos.x, pos.y, pos.z + 1),maxx,maxz,minx,minz) || checkFill (new Vector3 (pos.x, pos.y, pos.z - 1),maxx,maxz,minx,minz);
		}
	}

	void Update () {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool canPlace = true;
		if (Physics.Raycast (ray.origin,ray.direction,out hit)) {
			Vector3 pos = new Vector3 (Mathf.Round (hit.point.x), 1, Mathf.Round (hit.point.z));
			if (hit.transform.gameObject.tag == "Ground") {
				if (towerFrame.transform.position != pos && towerFrameBad.transform.position != pos) {
					checkedPositions.Clear ();
					positions.Add (pos);
					float minx = Mathf.Min (goal.transform.position.x, spawner.transform.position.x);
					float minz = Mathf.Min (goal.transform.position.z, spawner.transform.position.z);
					float maxx = Mathf.Max (goal.transform.position.x, spawner.transform.position.x);
					float maxz = Mathf.Max (goal.transform.position.z, spawner.transform.position.z);
					foreach (Vector3 v in positions) {
						minx = Mathf.Min (minx, v.x);
						minz = Mathf.Min (minz,v.z);
						maxx = Mathf.Max (maxx,v.x);
						maxz = Mathf.Max (maxz,v.z);
					}
					//Debug.Log (minx + " " + minz + " " + maxx + " " + maxz);
					if (!checkFill (spawner.transform.position,maxx,maxz,minx,minz)) {
						towerFrame.transform.position = Vector3.zero;
						canPlace = false;
						towerFrameBad.transform.position = pos;
					} else {
						towerFrame.transform.position = pos; 
						towerFrameBad.transform.position = Vector3.zero;
					}
					positions.Remove (pos);
				}
			} else if (hit.transform.gameObject.tag != "Frame") {
				towerFrame.transform.position = Vector3.zero;
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
