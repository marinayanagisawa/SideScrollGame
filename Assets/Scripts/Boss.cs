using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public float bossHp = 80.0f;
	public int score;
	public float distance;

	private Animator anim;
	private PlayerController pc;
	private GameController gc;

	//弾を発射させるオブジェクトをインスペクターから指定しておく
	public GameObject arm;
	public GameObject shot;
	//プレイヤーとの距離を測る時に使用
	//public GameObject robo;


	void Start () {
		anim = gameObject.transform.FindChild ("BossSprite").GetComponent<Animator> ();
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
		pc = GameObject.Find("robo").GetComponent<PlayerController>();
	}
	

	void Update () {
		//ToDo--------------------------プレイヤーとの距離を監視
		//BossShot();
	}



	void BossShot(){
		//弾を撃つ
		Instantiate (shot, arm.transform.position, transform.rotation);
	}


	//アームは弾をすり抜けるようにし,本体に当たった場合に以下の処理
	//ヒット時のダメージ計算とスコア計算
	public void FromOnTriggerEnter2D(Collider2D col){

		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "shot") {
			//ダメージ計算
			bossHp = bossHp - pc.shotPower; 

			//倒した場合の処理
			if (bossHp <= 0){
				int count = score;
				Debug.Log (count);
				//スコアをGameControllerのスコア合計に追加
				gc.playScore += count;

				//撃破処理
				//Defeat();
			}

		}

	}


	void Defeat(){
		
		//Todo------------------------撃破処理
		//撃破後のパーティクル呼び出し
		//アニメーション再生
		//落下後に削除
		//Destroy (this.gameObject);
		//ステージ出口の壁を削除
	}


}
