using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {

	public float shotSpeed = 10.0f;
	public float shotLifeTime = 1.5f;

	private PlayerController pc;
	private GameController gc;

	public GameObject smoke;

	void Start () {

		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
		GetComponent<ParticleSystem> ().Stop ();

		//プレイヤーの状態を取得
		pc = GameObject.Find ("robo").GetComponent<PlayerController> ();
	
		//プレイヤーの向きを見て飛ぶ方向を変更
		if (pc.back == false) {
			Debug.Log ("Fire!");
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (shotSpeed, 0);
		} else if (pc.back == true) {
			Debug.Log ("BackFire!");
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-shotSpeed, 0);
		}

	}

	void Update () {

		//弾の消去
		Destroy (gameObject, shotLifeTime);
	
	}
		
	void OnTriggerEnter2D(Collider2D col){

		//敵と弾が当たった時のみ,両方を消滅させる
		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "Enemy" || layerName == "FlyingEnemy") {

			Debug.Log ("Hit to Enemy!");

			//ヒットした敵オブジェクトを取得
			GameObject enemy = col.gameObject;

				//敵に設定されたスコアにアクセス
				int count = enemy.transform.parent.GetComponent<Enemy> ().score;
				//Debug.Log (count);
				//スコアをGameControllerのスコア合計に追加
				gc.playScore += count;

			//エフェクトを生成
			Instantiate (smoke, transform.position, smoke.transform.rotation);
			Destroy (col.gameObject);
			Destroy (this.gameObject);

		}

	}


}