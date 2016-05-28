using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {


	public float shotSpeed = 10.0f;
	public float shotLifeTime = 1.5f;

	public GameObject robo;
	public PlayerController pc;

	void Start () {

		//プレイヤーの状態を取得
		robo = GameObject.Find ("robo");
		pc = robo.GetComponent<PlayerController> ();

		//プレイヤーの向きを見て発射方向を変更
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

		if (layerName == "Enemy") {
			Destroy (col.gameObject);
			Destroy (this.gameObject);
		}
	



	}


}