using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	// Use this for initialization
	public GameObject goal;
	[SerializeField]
	public float health = 3F;
	public Node curNode;
	public GameObject fps;
	void Start () {
		/*
		if (curNode.x > transform.position.x) {
			transform.rotation = Quaternion.Euler(0F,0F,0F);
		} else if (curNode.x < transform.position.x) {
			transform.rotation = Quaternion.Euler(0F,180F,0F);
		} else if (curNode.z > transform.position.z) {
			transform.rotation = Quaternion.Euler(0F,90F,0F);
		} else{
			transform.rotation = Quaternion.Euler(0F,-90F,0F);
		}
		*/
	}

	public void hit (){
		health--;
		if (health <= 0) {
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward*0.1F);

		Vector3 targetDir = new Vector3 (curNode.x, 0.5F, curNode.z) - transform.position;
		transform.rotation = Quaternion.LookRotation (targetDir);
		if (Vector3.Distance(transform.position,new Vector3(curNode.x,1F,curNode.z)) < 0.51F) {
			Debug.Log (Vector3.Distance (transform.position, new Vector3 (curNode.x, 1F, curNode.z)));
			//transform.position = new Vector3 (curNode.x, 1F, curNode.z);
			if (curNode.parent != null) {
				curNode = curNode.parent;
				Debug.Log (curNode.x + " " + curNode.z);
			} else {
				Destroy (this.gameObject);
				if (fps != null) {
					Camera.main.gameObject.SetActive (false);
					fps.SetActive (true);
				}
			}
		}

	}
}
