using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {
	//WHY CANT I GET JUST AN ELEMENT FROM THE DICTIONARY MOTHERFUCKER
	public class Node{
		public float x;
		public float z;
		public int f;
		public int g;
		public int h;
		public Vector3 goal;
		public Node parent;
		public Node(float x, float z, Node parent,Vector3 goal){
			this.x = x;
			this.z = z;
			this.goal = goal;
			this.parent = parent;
			h = 10*Mathf.FloorToInt(Mathf.Abs(x-goal.x) + Mathf.Abs(z-goal.z));
			calculateF();
		}
		public void calculateF(){
			if(parent == null){
				g = 0;
			}else{
				g = parent.g + 10;
			}
			f = h+g;
		}
	}

	Node lastAdded;
	int cnt;
	[SerializeField]
	GameObject goal;
	[SerializeField]
	GameObject camera;
	[SerializeField]
	GameObject enm;
	[SerializeField]
	GameObject fps;
	[SerializeField]
	GameObject point;

	Dictionary<Vector2,Node> closed;
	Dictionary<Vector2,Node> open;
	// Use this for initialization
	void Start () {
		open = new Dictionary<Vector2,Node>();
		closed = new Dictionary<Vector2,Node> ();
	}
	
	// Update is called once per frame
	void Update () {
		cnt++;
		if (cnt > 100) {
			//GameObject enemy = Instantiate (enm, this.transform.position ,Quaternion.identity);
			//enemy.GetComponent<EnemyMovement> ().goal = goal;
			cnt = 0;
		}
		if (Input.GetButtonDown ("Fire2")) {
			Node start = new Node (this.transform.position.x, this.transform.position.z, null, goal.transform.position);
			Node curNode = start;
			int i = 0;
			while (goal.transform.position != new Vector3 (curNode.x, 1F, curNode.z)) {
				
				addNode (curNode, 1, 0);
				addNode (curNode, 0, 1);
				addNode (curNode, 0, -1);
				addNode (curNode, -1, 0);
				open.Remove (new Vector2(curNode.x,curNode.z));

				float smallestF = lastAdded.f;
				Node closest = lastAdded;
				foreach (Node n in open.Values) {
					if (n.f <= smallestF) {
						smallestF = n.f;
						closest = n;
					}
				}
				curNode = closest;
				closed.Add(new Vector2(curNode.x,curNode.z),curNode);
				i++;
			}
			while (curNode.parent != null) {
				Instantiate (point, new Vector3 (curNode.x, 1F, curNode.z), Quaternion.Euler(Vector3.zero));
				curNode = curNode.parent;
			}

			//camera.SetActive (!camera.activeInHierarchy);
			//fps.SetActive (!fps.activeInHierarchy);
		}
	}

	void addNode(Node node,int x, int z){
		Vector3 v = new Vector3 (node.x + x, 1F, node.z+z);

		float minx = camera.GetComponent<camMove>().minx;
		float minz =  camera.GetComponent<camMove>().minz;
		float maxx = camera.GetComponent<camMove>().maxx;
		float maxz = camera.GetComponent<camMove> ().maxz;
		if (!camera.GetComponent<camMove> ().positions.Contains(v) && !closed.ContainsKey(new Vector2 (v.x, v.z)) && v.z >= minz -1 && v.x >= minx-1 && v.z <= maxz+1 && v.x <= maxx+1) {
			if (open.ContainsKey (new Vector2 (v.x, v.z))) {
				open [new Vector2 (v.x, v.z)].parent = node;
				open [new Vector2 (v.x, v.z)].calculateF ();
				lastAdded = open [new Vector2 (v.x, v.z)];
			} else {
				Node n = new Node (v.x, v.z, node, goal.transform.position);
				open.Add (new Vector2 (v.x, v.z), n);
				lastAdded = n;
			}
		}
	}
}
