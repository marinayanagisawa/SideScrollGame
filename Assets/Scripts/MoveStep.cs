using UnityEngine;
using System.Collections;

public class MoveStep : MonoBehaviour {

	public bool horizontal = true;
	public float moveSpeed = 1.0f;
	public float moveTime = 5.0f;
	public GameObject step;
	public bool moveR = true;
	public bool moveL = false;


	void Start () {
	
		step = GameObject.Find("MoveStepA");
	}
	

	void Update () {

		//横移動専用　moveTimeで反対に移動する
		if (horizontal) {
			if (moveR) {
				Debug.Log ("moveRight");
				StartCoroutine(MoveR ());

			} else {
				Debug.Log ("moveLeft");
				StartCoroutine(MoveL());
			}
		}
	

		//こちらは縦移動専用
		if (!horizontal){
			step.GetComponent<Rigidbody2D> ().velocity= new Vector2 (0, -moveSpeed);
		}
	}


	IEnumerator MoveR(){
		step.GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, 0);
		yield return new WaitForSeconds (moveTime);
		moveR = false;
		moveL = true;
	}

	IEnumerator MoveL(){
		step.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);
		yield return new WaitForSeconds (moveTime);
		moveR = true;
		moveL = false;
	}
}

