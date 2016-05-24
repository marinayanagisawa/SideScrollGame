using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D rb;
	public SpriteRenderer sr;
	public Animator anim;

	//移動関連
	public float speed = 5.0f;
	public float jumpHeight = 30.0f;
	public bool moveJ;

	//死亡フラグ
	public bool dead = false;

	//弾の発射方向切り替えのためのフラグ
	public bool back;

	//ショットの制御用
	public float nextShotTime = 0.5f;
	public bool canShot = true;
	public GameObject muzzle;
	public GameObject shot;

	//接地判定に使用
	public LayerMask groundlayer;
	public bool isGrounded = true;


	void Start () {


		//コンポーネントを取得
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}


	void Update () {
		
		//足元の接地判定
		//isGrounded = Physics2D.Linecast (transform.position, transform.position - transform.up * 1, groundlayer);
		//足場の端に立った時にジャンプ状態と認識され,動けなくなるバグが発生する模様

		if (isGrounded) {


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
				Debug.Log("Jump!!!");
				moveJ = true;
				anim.SetTrigger ("jump");
			}
		}
			
		//プレイヤーの弾を発射
		if (Input.GetButtonDown ("Fire1")) {
			//撃てる状態であれば,弾を撃つ
			if (canShot) {
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
		}

		//プレイヤーの落下チェック(pos使い回しのためここでチェック)
		if (pos.y < -5.0f) {
			Debug.Log ("Dead flug ON!!");
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
			

	/*
	void OnTriggerEnter2D(Collider col){
		
		//----------------------------ToDo
		//敵と当たったら死亡フラグ
		//プレイヤーの消滅処理

	}
*/



}
