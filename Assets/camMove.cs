using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camMove : MonoBehaviour {
	[SerializeField]
	GameObject tower;
	[SerializeField]
	GameObject towerFrame;
	[SerializeField]
	GameObject towerFrameBad;
	[SerializeField]
	GameObject towerWall;
	[SerializeField]
	GameObject goal;
	[SerializeField]
	GameObject spawner;
	[SerializeField]
	GameObject otherCam;
	[SerializeField]
	Texture2D frame;
	[SerializeField]
	Text fpsText;
	[SerializeField]
	int wallTowers;
	[SerializeField]
	int gunTowers;
	[SerializeField]
	Text gText;
	[SerializeField]
	Text wText;
	public ArrayList positions = new ArrayList();
	ArrayList checkedPositions = new ArrayList();
	float deltaTime = 0.0f;
	GameObject curTower;

	public float minx;
	public float minz;
	public float maxx;
	public float maxz;

	bool spawningEnemies = false;

	// Use this for initialization
	void Start () {
		minx = Mathf.Min (goal.transform.position.x, spawner.transform.position.x);
		minz = Mathf.Min (goal.transform.position.z, spawner.transform.position.z);
		maxx = Mathf.Max (goal.transform.position.x, spawner.transform.position.x);
		maxz = Mathf.Max (goal.transform.position.z, spawner.transform.position.z);
		curTower = tower;
		wText.text = "Wall Towers: " + wallTowers;
		gText.text = "Gun Towers: " + gunTowers;
	}

	void guiText(){
		float msec = deltaTime * 1000.0F;
		float fps = 1.0f / deltaTime;
		string text = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);
		fpsText.text = text;
	}
	
	// Update is called once per frame

	bool checkFill(Vector3 pos,float maxx, float maxz, float minx, float minz){
		if (checkedPositions.Contains(pos) || positions.Contains (pos) || pos.z < minz-1 || pos.x < minx-1 || pos.z > maxz+2 || pos.x > maxx+2) {
			return false;
		} else if (goal.transform.position == pos) {
			return true;
		} else {
			checkedPositions.Add (pos);
			return checkFill (new Vector3 (pos.x + 1, pos.y, pos.z),maxx,maxz,minx,minz) || checkFill (new Vector3 (pos.x - 1, pos.y, pos.z),maxx,maxz,minx,minz) || checkFill (new Vector3 (pos.x, pos.y, pos.z + 1),maxx,maxz,minx,minz) || checkFill (new Vector3 (pos.x, pos.y, pos.z - 1),maxx,maxz,minx,minz);
		}
	}

	void Update () {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		guiText ();
		if (Input.GetKey (KeyCode.P)) {
			gameObject.SetActive (false);
			otherCam.SetActive (true);
		}
		if (!spawningEnemies) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			bool canPlace = true;
			if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
				Vector3 pos = new Vector3 (Mathf.Round (hit.point.x), 0.5F, Mathf.Round (hit.point.z));
				if (hit.transform.gameObject.tag == "Ground") {
					if ((towerFrame.transform.position != pos |! towerFrame.activeInHierarchy) && (towerFrameBad.transform.position != pos |! towerFrameBad.activeInHierarchy)) {
						checkedPositions.Clear ();
						positions.Add (pos);
						foreach (Vector3 v in positions) {
							minx = Mathf.Min (minx, v.x);
							minz = Mathf.Min (minz, v.z);
							maxx = Mathf.Max (maxx, v.x);
							maxz = Mathf.Max (maxz, v.z);
						}
						if (!checkFill (spawner.transform.position, maxx, maxz, minx, minz)) {
							towerFrame.SetActive (false);
							canPlace = false;
							towerFrameBad.SetActive (true);
							towerFrameBad.transform.position = pos;
						} else {
							towerFrame.SetActive (true);
							towerFrame.transform.position = pos; 
							towerFrameBad.SetActive (false);
						}
						positions.Remove (pos);
					}
				} else if (hit.transform.gameObject.tag != "Frame") {
					towerFrame.SetActive (false);
					towerFrameBad.SetActive (true);
					if (hit.transform.gameObject.transform.parent != null) {
						towerFrameBad.transform.position = new Vector3 (Mathf.Round (hit.transform.position.x), 0.5F, Mathf.Round (hit.transform.position.z));
					} else {
						towerFrameBad.transform.position = hit.transform.gameObject.transform.position;
					}
					canPlace = false;
				} 
			}
			if (Input.GetButtonDown ("Fire1") && canPlace && gunTowers > 0) {
				positions.Add (towerFrame.transform.position);
				GameObject newTower = Instantiate (tower, towerFrame.transform.position, Quaternion.identity);
				newTower.SetActive (true);
				gunTowers--;
				gText.text = "Gun Towers: " + gunTowers;

			}if (Input.GetButtonDown ("Fire2") && canPlace && wallTowers > 0) {
				positions.Add (towerFrame.transform.position);
				GameObject newTower = Instantiate (towerWall, towerFrame.transform.position, Quaternion.identity);
				newTower.SetActive (true);
				wallTowers--;
				wText.text = "Wall Towers: " + wallTowers;
			}
			if (Input.GetButtonDown ("Jump")) {
				spawningEnemies = true;
				spawner.GetComponent<SpawnEnemies> ().startSpawning = true;
				towerFrame.SetActive (false);
				towerFrameBad.SetActive (false);
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
		if (Input.GetAxis ("WpnChange") < 0F) {
			transform.Translate (Vector3.back);
		} else if (Input.GetAxis ("WpnChange") > 0F) {
			transform.Translate (Vector3.forward);
		}
	}
}
