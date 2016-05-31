using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//public float MoveSpeed = 5.0f;
	//public GameObject enemy; 
	public bool isGrounded;

	void Start () {
		
	}
	

	void Update () {
		
		/*
		//接地判定・・・子クラスからなぜか判定が取れないので,とりあえずコメントアウト
		//レイヤーマスクの番号を指定
		int layer = 1 << 9;
		//接地判定
		isGrounded = Physics2D.Linecast (transform.position, transform.position - transform.up * 1, layer);
		*/

		}

}
