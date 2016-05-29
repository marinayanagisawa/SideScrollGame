using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float MoveSpeed = 5.0f;
	//public GameObject enemy; 
	public bool isGrounded;

	void Start () {
		
	}
	

	void Update () {
		//GetComponent<Rigidbody2D> ().velocity = new Vector2 (-MoveSpeed, 0);
		//レイヤーマスクの番号を指定
		int layer = 1 << 9;
		//接地判定
		isGrounded = Physics2D.Linecast (transform.position, transform.position - transform.up * 1, layer);
		}



	/*
	void FixedUpdate(){
		//GetComponent<Rigidbody2D> ().AddForce ( new Vector2 (-MoveSpeed, 0));


	}
	*/

}
