using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

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
		public void printStats(){
			Debug.Log ("X: " + x + " Z: " + z + " F: " + f + " H: " + h + " G: " + g);
		}
	}

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

	public float minx = -10;
	public float minz = -10;
	public float maxx = 20;
	public float maxz = 20;
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
				
				Node n1 = addNode (curNode, 1, 0);
				Node n2 = addNode (curNode, 0, 1);
				Node n3 = addNode (curNode, 0, -1);
				Node n4 = addNode (curNode, -1, 0);
				open.Remove (new Vector2(curNode.x,curNode.z));

				float smallestF = //open.Values.;
				Node closest = no;
				Debug.Log ("open: " + open.Count);
				foreach (Node n in open.Values) {
					if (n.f <= smallestF) {
						smallestF = n.f;
						closest = n;
					}
				}
				//Debug.Log (open.LastIndexOf (closest));

				//closest.printStats ();
				curNode = closest;
				closed.Add(new Vector2(curNode.x,curNode.z),curNode);
				i++;
			}
			foreach (Node n in closed.Values) {
				n.printStats ();
				Instantiate (point, new Vector3 (n.x, 1F, n.z), Quaternion.Euler(Vector3.zero));
			}

			//camera.SetActive (!camera.activeInHierarchy);
			//fps.SetActive (!fps.activeInHierarchy);
			Debug.Log ("teetet");
		}
	}

	Node addNode(Node node,int x, int z){
		Vector3 v = new Vector3 (node.x + x, 1F, node.z+z);
		//v.x > minx && v.x < maxx && v.z > minz && v.z < maxz && 
		if (!camera.GetComponent<camMove> ().positions.Contains(v) && !closed.ContainsKey(new Vector2 (v.x, v.z))) {
			if (open.ContainsKey (new Vector2 (v.x, v.z))) {
				Debug.Log ("dupe");
				open [new Vector2 (v.x, v.z)].parent = node;
				open [new Vector2 (v.x, v.z)].calculateF ();
				return open [new Vector2 (v.x, v.z)];
			} else {
				Node n = new Node (v.x, v.z, node, goal.transform.position);
				open.Add (new Vector2 (v.x, v.z), n);
				return n;
			}
		}
		return null;
	}
}
