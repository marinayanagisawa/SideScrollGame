using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
 
	public float moveSpeed = 3.0f;
	public int score;
	public float lifetime = 10.0f;
	public float hp = 10.0f;

	//子クラスのダメージ計算とスコア計算に使う
	protected PlayerController pc;
	protected GameController gc;

	protected Animator animator;
		
	//移動処理
	public void Move(){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);
	}
		
}
