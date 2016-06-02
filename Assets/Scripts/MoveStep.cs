using UnityEngine;
using System.Collections;

public class MoveStep : MonoBehaviour {

	public bool horizontal = true;
	public float moveSpeed = 1.0f;
	public float moveTime = 5.0f;
	public GameObject step;
	private bool moveR = true;
	private bool moveL = false;
	private bool moveU = true;
	private bool moveD = false;

	void Start () {

	}
	

	void Update () {

		//横移動専用　moveTimeで左右に移動する
		if (horizontal) {
			if (moveR) {
				StartCoroutine(MoveR ());

			} else if(moveL)  {
				StartCoroutine(MoveL());
			}
		}
	

		//縦移動専用 moveTimeで上下に移動する
		if (!horizontal){
			//step.GetComponent<Rigidbody2D> ().velocity= new Vector2 (0, -moveSpeed);


			if (moveU) {
				StartCoroutine(MoveU());

			} else if(moveD) {
				StartCoroutine(MoveD());
			}


		}
	}

	//左右の移動
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

	//上下の移動
	IEnumerator MoveU(){
		step.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, moveSpeed);
		yield return new WaitForSeconds (moveTime);
		moveU = false;
		moveD = true;
	}

	IEnumerator MoveD(){
		step.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -moveSpeed);
		yield return new WaitForSeconds (moveTime);
		moveU = true;
		moveD = false;
	}
}

