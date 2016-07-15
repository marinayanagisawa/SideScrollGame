using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public float bossHp = 80.0f;
	public int score;
	public float distance;

	private Animator anim;
	private PlayerController pc;
	private GameController gc;
	public GameObject smokeG;

	private bool defeat = false;

	//プレイヤーとの距離を測る時に使用
	public GameObject robo;
	public float shovelMaxDis = 11.0f;
	public float shovelMinDis = 10.0f;
	public float dis;
	public float handMaxDis = 9.0f;
	public float handMinDis = 8.0f;

	public AudioSource sound;

	void Start () {
		robo = GameObject.Find ("robo");
		anim = gameObject.transform.FindChild ("BossSprite").GetComponent<Animator> ();
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
		pc = GameObject.Find("robo").GetComponent<PlayerController>();
		sound = GetComponent<AudioSource> ();
	}
	

	void Update () {

		if (defeat == false) {

			//プレイヤーとの距離を監視
			Vector2 bossPos = this.transform.position;
			Vector2 roboPos = robo.transform.position;
			dis = Vector2.Distance (bossPos, roboPos);


			if (dis < shovelMaxDis && dis > shovelMinDis) {
				Debug.Log ("shovel Start");
				anim.SetTrigger ("shovel");
			}

			if (dis < handMaxDis && dis > handMinDis) {
				Debug.Log ("hand Start");
				anim.SetTrigger ("hand");
			}
		}
	}
		

	//アームは弾をすり抜けるようにし,本体に当たった場合に以下の処理
	//ヒット時のダメージ計算とスコア計算
	public void FromOnTriggerEnter2D(Collider2D col){


		if (defeat == false) {

			string layerName = LayerMask.LayerToName (col.gameObject.layer);

			if (layerName == "shot") {
				//ダメージ計算
				bossHp = bossHp - pc.shotPower; 

				//倒した場合の処理
				if (bossHp <= 0) {
					int count = score;
					Debug.Log (count);
					//スコアをGameControllerのスコア合計に追加
					gc.playScore += count;

					//撃破処理
					Defeat ();
				}
			}
		}
	}


	//撃破後処理
	void Defeat(){
		defeat = true;
		
		//サウンドを鳴らす
		sound.PlayOneShot (sound.clip);

		//撃破後のパーティクル呼び出し
		Instantiate (smokeG, transform.position, smokeG.transform.rotation);

		//アニメーションの再生とコライダーをOFF（アニメーション内にて）
		anim.SetTrigger("clean");

		//落下後に削除
		Destroy (this.gameObject,3.0f);

		//Todo------------------------ステージ出口の壁を削除
	}


}
