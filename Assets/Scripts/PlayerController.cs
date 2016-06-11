﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator anim;
	private GameController gc;

	//移動関連
	public float speed = 5.0f;
	public float jumpHeight = 30.0f;
	private bool moveJ;
	public bool canMove = true;

	//死亡フラグ関連
	public int life = 2;
	public bool dead;
	public GameObject smokeR;
	public bool hitting = false;

	//弾の発射方向切り替えのためのフラグ
	public bool back;

	//ショットの制御用
	public float nextShotTime = 0.5f;
	private bool canShot = true;
	public GameObject muzzle;
	public GameObject shot;

	//接地判定に使用
	public LayerMask groundlayer;
	public bool isGrounded = true;

	//サウンド
	public AudioSource sound;

	void Start () {

		//コンポーネントを取得
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
		sound = GetComponent<AudioSource> ();
		//プレイ中フラグ
		dead = false;
	}


	void Update () {
		
		//足元の接地判定
		//isGrounded = Physics2D.Linecast (transform.position, transform.position - transform.up * 1, groundlayer);
		//足場の端に立った時にジャンプ状態と認識され,動けなくなるバグが発生する模様

		if (isGrounded && canMove) {

			//移動処理	
			if (Input.GetAxisRaw ("Horizontal") > 0.0f) {
				sr.flipX = false;
				rb.velocity = new Vector2 (speed, 0);
				back = false;
			} else if (Input.GetAxisRaw ("Horizontal") < 0.0f) {
				sr.flipX = true;
				rb.velocity = new Vector2 (-speed, 0);
				back = true;
			}

			//移動のアニメーションを設定
			if (rb.velocity.x != 0) {
				anim.SetBool ("walk", true);
			} else {
				anim.SetBool ("walk", false);
			}


			//ジャンプ処理(spaceキーのJumpがたまに効かないので,暫く左Altで様子見)
			if (Input.GetButtonDown ("Fire2")) {
				//ジャンプの挙動がおかしいので,処理の中身はFixedUpdateに移動
				//Debug.Log("Jump!!!");
				moveJ = true;
				anim.SetTrigger ("jump");
			}
		}
			
		//プレイヤーの弾を発射
		if (Input.GetButtonDown ("Fire1")) {
			//撃てる状態であれば,弾を撃つ
			if (canShot && canMove) {
				StartCoroutine (PlayerShot ());
			}
		}
	}
		

	void FixedUpdate(){

		//プレイヤーのポジションを取得
		Vector2 pos = transform.position;
		//足元の判定の大きさを指定
		Vector2 p1 = new Vector2 (pos.x - 0.5f , pos.y - 0.8f);
		Vector2 p2 = new Vector2 (p1.x + 1.0f, p1.y - 0.2f);
		//接地判定し,isGroundedフラグにチェック
		isGrounded = Physics2D.OverlapArea (p1, p2, groundlayer);
		//空中にいたらジャンプ不可
		if (!isGrounded) {
			moveJ = false;
		}

		//ジャンプ処理
		if (moveJ) {
			
			isGrounded = false;
			rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
			//sound.PlayOneShot (sound.clip, 1.0f);
		}

		//プレイヤーの落下チェック(pos使い回しのためここでチェック)
		if (pos.y < -5.0f) {
			//Debug.Log ("Dead flug ON!!");
			dead = true;
		}
	}


	//弾が連射出来ないように,nextShotTime分待つ
	IEnumerator PlayerShot(){
		canShot = false;
		Instantiate (shot, muzzle.transform.position, transform.rotation);
		yield return new WaitForSeconds (nextShotTime);
		canShot = true;
	}
			

	//敵と接触した場合
	void OnCollisionEnter2D(Collision2D col){

		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "Enemy" || layerName == "FlyingEnemy") {
			
			//敵を削除
			Destroy (col.gameObject);

			//ダメージアニメーション中でなければ普通にダメージ処理
			if (!hitting) {
				Damage ();

				//ライフが０なら死亡処理
				if (life < 0) {
					Dead ();
				}
			}
		}
	}

	//ステージギミックと当たった場合
	void OnTriggerEnter2D(Collider2D col){

		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "Gimmick") {

			if (!hitting) {
				Damage ();

				if (life < 0) {
					Dead ();
				} 
			}
		}
	}


	//ダメージ処理
	void Damage(){
		//フラグの書き換え
		hitting = true;
		canMove = false;

		//ライフ値を引く
		life--;
		Debug.Log (life);

		//ダメージアニメーション設定
		anim.SetTrigger ("dmg");
	}


	void Dead(){
		
		dead = true;
		//爆発エフェクト生成
		Instantiate (smokeR, transform.position, smokeR.transform.rotation);

		//GameOver処理
		gc.GameOver ();

		//プレイヤーを消去
		Destroy (this.gameObject);
	}


	//ダメージアニメーション終了後に呼び出す(アニメーション終了フラグ)
	public void FinishHitting(){
		hitting = false;

	}

	void SwitchToCanMove(){
		canMove = true;
	}


}
