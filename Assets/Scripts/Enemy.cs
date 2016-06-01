using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
 
	public bool isGrounded;
	public float moveSpeed = 3.0f;


	void Start () {
		
	}
	

	void Update () {
		
		/*
		//接地判定
		//レイヤーマスクの番号を指定
		int layer = 1 << 9;
		//接地判定
		isGrounded = Physics2D.Linecast (transform.position, transform.position - transform.up * 1, layer);
		*/


		}



	//移動処理
	public void Move(){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);
	}



}
