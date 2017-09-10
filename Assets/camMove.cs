using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camMove : MonoBehaviour {
	public GameObject goal;
	public GameObject spawner;
	public GameObject otherCam;
	public Text fpsText;
	public List<GameObject> towers;

	GameObject curTowerGood;
	GameObject curTowerBad;
	int curTower;
	public ArrayList positions = new ArrayList();
	ArrayList checkedPositions = new ArrayList();
	float deltaTime = 0.0f;

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
		curTower = 0;
		changeTower (0);
	}

	void changeTower(int i){
		if (curTower + i > -1 && curTower + i < towers.Count) {
			curTower += i;
			Destroy (curTowerBad);
			Destroy (curTowerGood);
			curTowerGood = Instantiate (towers [curTower].GetComponent<TowerVar> ().towerFrameGood);
			curTowerBad = Instantiate (towers [curTower].GetComponent<TowerVar> ().towerFrameBad);
		}
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
					if ((curTowerGood.transform.position != pos |! curTowerGood.activeInHierarchy) && (curTowerBad.transform.position != pos |! curTowerBad.activeInHierarchy)) {
						checkedPositions.Clear ();
						positions.Add (pos);
						foreach (Vector3 v in positions) {
							minx = Mathf.Min (minx, v.x);
							minz = Mathf.Min (minz, v.z);
							maxx = Mathf.Max (maxx, v.x);
							maxz = Mathf.Max (maxz, v.z);
						}
						if (!checkFill (spawner.transform.position, maxx, maxz, minx, minz)) {
							curTowerGood.SetActive (false);
							canPlace = false;
							curTowerBad.SetActive (true);
							curTowerBad.transform.position = pos;
						} else {
							curTowerGood.SetActive (true);
							curTowerGood.transform.position = pos; 
							curTowerBad.SetActive (false);
						}
						positions.Remove (pos);
					}
				} else if (hit.transform.gameObject.tag != "Frame") {
					curTowerGood.SetActive (false);
					curTowerBad.SetActive (true);
					if (hit.transform.gameObject.transform.parent != null) {
						curTowerBad.transform.position = new Vector3 (Mathf.Round (hit.transform.position.x), 0.5F, Mathf.Round (hit.transform.position.z));
					} else {
						curTowerBad.transform.position = hit.transform.gameObject.transform.position;
					}
					canPlace = false;
				} 
			}
			if (Input.GetButtonDown ("Fire1") && canPlace && towers[curTower].GetComponent<TowerVar>().quantity > 0) {
				positions.Add (curTowerGood.transform.position);
				GameObject newTower = Instantiate (towers[curTower], curTowerGood.transform.position, Quaternion.identity);
				newTower.SetActive (true);
				towers[curTower].GetComponent<TowerVar>().placed();

			}
			if (Input.GetAxisRaw ("Change Tower") > 0) {
				changeTower (1);
			} else if (Input.GetAxisRaw ("Change Tower") < 0) {
				changeTower (-1);
			}

			if (Input.GetButtonDown ("Jump")) {
				spawningEnemies = true;
				spawner.GetComponent<SpawnEnemies> ().startSpawning = true;
				curTowerGood.SetActive (false);
				curTowerBad.SetActive (false);
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
		if (Input.GetAxis ("Zoom") < 0F) {
			transform.Translate (Vector3.back);
		} else if (Input.GetAxis ("Zoom") > 0F) {
			transform.Translate (Vector3.forward);
		}
	}
}
