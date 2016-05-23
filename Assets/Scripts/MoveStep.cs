using UnityEngine;
using System.Collections;

public class MoveStep : MonoBehaviour {

	public bool horizontal = true;
	public float moveSpeed = 1f;
	public float moveTime = 3f;
	public GameObject step;


	void Start () {
	
		step = GameObject.Find("MoveStepA");
	}
	

	void Update () {
	//----------とりあえず移動まで,　横移動のStepに関しては,moveTime後に反対方向に移動するようにする
		if (horizontal) {
			step.GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, 0);
		}

	
		if (!horizontal){
			step.GetComponent<Rigidbody2D> ().velocity= new Vector2 (0, -moveSpeed);
		}
	}
}
