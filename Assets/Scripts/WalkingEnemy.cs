using UnityEngine;
using System.Collections;

public class WalkingEnemy : Enemy {

	public bool isGrounded;



	void Start () {

		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
		pc = GameObject.Find ("robo").GetComponent<PlayerController> ();

		Destroy (this.gameObject, lifetime);
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
		
	//ヒット時のダメージ計算とスコア計算
	//（親クラスからではGameControllerなどのオブジェクト取得ができない様なので,それぞれの子クラスに持たせる）
	public void FromOnTriggerEnter2D(Collider2D col){

		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "shot") {
			Debug.Log ("FromOnTreggerEnter2D come!!");
			//-----------------------ToDo
			//ダメージアニメーション（赤点滅など）

			hp = hp - pc.shotPower; 

			if (hp <= 0){

				int count = score;
				Debug.Log (count);
				//スコアをGameControllerのスコア合計に追加
				gc.playScore += count;

				Destroy (this.gameObject);
			}

		}

	}
}
