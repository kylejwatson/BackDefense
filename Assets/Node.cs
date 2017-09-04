using UnityEngine;
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