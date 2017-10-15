using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerVar : MonoBehaviour {
	public int quantity;
	public GameObject towerFrameGood;
	public GameObject towerFrameBad;
	public GameObject img;
	public Vector3 pos;
	public GameObject text;
	public GameObject textOnCanvas;
	public int quantitySesh;
	// Use this for initialization
	void Start () {
		//placeOnCanvas (100, 100);
	}

	public void placeOnCanvas (float x, float y){
		Canvas[] c = FindObjectsOfType<Canvas> ();
		Canvas main = c[0];
		foreach (Canvas mc in c) {
			if (mc.tag == "MainCanvas")
				main = mc;
		}
		GameObject ri = Instantiate (img);
		ri.transform.SetParent (main.transform, false);
		ri.transform.position = new Vector3 (x, y, 0);
		GameObject i = Instantiate (text);
		i.transform.SetParent (main.transform, false);
		pos = new Vector3 (x, y+74, 0);
		i.transform.position = pos;
		i.GetComponentsInChildren<Text> () [0].text = quantitySesh.ToString();
		textOnCanvas = i;
	}
	public void placed(){
		quantitySesh--;
		textOnCanvas.GetComponentsInChildren<Text> () [0].text = quantitySesh.ToString();
	}
	// Update is called once per frame
	void Update () {

	}
}
