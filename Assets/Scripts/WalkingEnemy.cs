using UnityEngine;
using System.Collections;

public class WalkingEnemy : Enemy {

	public float moveSpeed = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		//レイヤーマスクの番号を指定
		int layer = 1 << 9;
		//接地判定
		isGrounded = Physics2D.Linecast (transform.position, transform.position - transform.up * 1, layer);


		//GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);

		if (isGrounded) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);
		}


	}
}
