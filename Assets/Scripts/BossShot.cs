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

		//プレイヤーと弾が当たった時の音とエフェクトの処理
		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		//bossの弾とプレイヤーが接触
		if (layerName == "Chara") {
			gc.SendMessage ("EnemyExplode");
			Destroy (this.gameObject);

		//bossの弾とプレイヤーの弾同士が接触した場合,音の処理がPlayer.cs側にある
		} else if (layerName == "shot") {
			Destroy (this.gameObject);
		}


	}
}