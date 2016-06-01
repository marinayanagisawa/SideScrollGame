using UnityEngine;
using System.Collections;

public class WalkingEnemy : Enemy {

	public bool isGrounded;

	void Start () {
	
	}
	

	void Update () {

		//レイヤーマスクの番号を指定
		int layer = 1 << 9;
		//接地判定
		isGrounded = Physics2D.Linecast (transform.position, transform.position - transform.up * 1, layer);

		//歩く敵は接地していたら移動
		if (isGrounded) {
			base.Move ();
		}
			
	}
}
