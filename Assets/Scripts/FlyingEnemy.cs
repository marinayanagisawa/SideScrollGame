using UnityEngine;
using System.Collections;

public class FlyingEnemy : Enemy {


	void Start () {
		
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
		pc = GameObject.Find ("robo").GetComponent<PlayerController> ();

		Destroy (this.gameObject, lifetime);
	}
	

	void Update () {
		//移動処理
		base.Move ();
	}


	//プレイヤーの弾とヒット時のダメージ計算とスコア計算
	public void FromOnTriggerEnter2D(Collider2D col){

		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "shot") {
			//ダメージ計算
			hp = hp - pc.shotPower; 

			//倒した場合の処理
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
