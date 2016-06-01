using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
 
	public float moveSpeed = 3.0f;
	public int score;

	void Start () {
		
	}

	void Update () {

		}
		
	//移動処理
	public void Move(){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);
	}



}
