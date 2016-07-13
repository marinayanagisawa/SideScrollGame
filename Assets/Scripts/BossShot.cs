using UnityEngine;
using System.Collections;

public class BossShot : MonoBehaviour {

	public float shotSpeed = 8.0f;
	public float shotLifeTime = 1.5f;
	private GameController gc;


	void Start () {
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
		GetComponent<Rigidbody2D> ().velocity = transform.right.normalized * -shotSpeed;
	}
	

	void Update () {
		//弾の消去
		Destroy (gameObject, shotLifeTime);
	}


	void OnTriggerEnter2D(Collider2D col){

		//敵と弾が当たった時の音とエフェクトの処理
		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "Chara") {

			//Debug.Log ("Hit to player!");
			//音を再生する
			gc.SendMessage ("EnemyExplode");

			Destroy (this.gameObject);

		}

	}



}
