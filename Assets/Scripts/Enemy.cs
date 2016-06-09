using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
 
	public float moveSpeed = 3.0f;
	public int score;
	public float lifetime = 10.0f;


		
	//移動処理
	public void Move(){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);
	}
		
}
