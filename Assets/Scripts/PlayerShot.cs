using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {


	public float shotSpeed = 10.0f;
	public float shotLifeTime = 1.5f;

	public GameObject robo;
	public PlayerController pc;
	public GameObject smoke;
	//public Vector3 pos;

	void Start () {
		
		GetComponent<ParticleSystem> ().Stop ();

		//プレイヤーの状態を取得
		robo = GameObject.Find ("robo");
		pc = robo.GetComponent<PlayerController> ();

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

		if (layerName == "Enemy") {

			Instantiate (smoke, transform.position, smoke.transform.rotation);

			Destroy (col.gameObject);
			Destroy (this.gameObject);


		}

	}


}