using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {
	int cnt;
	[SerializeField]
	GameObject enm;
	[SerializeField]
	GameObject goal;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cnt++;
		if (cnt > 100) {
			GameObject enemy = Instantiate (enm, this.transform.position ,Quaternion.identity);
			enemy.GetComponent<EnemyMovement> ().goal = goal;
			cnt = 0;
		}
	}
}
