using UnityEngine;
using System.Collections;

public class BossPoint : MonoBehaviour {

	private GameObject mainCamera;
	private GameObject subCamera;
	private GameObject shaker;
	public GameObject corn;

	private GameObject robo;
	private GameObject boss;
	private GameObject wall;
	private PlayerController pc;
	private GameController gc;

	//アニメーター（Warning警告,BossHPゲージ）
	private Animator Wanim;
	public Animator UIanim;

	public AudioSource[] sound;


	void Start () {

		robo = GameObject.Find ("robo");
		boss = GameObject.Find ("Boss");
		boss.GetComponent<Rigidbody2D> ().gravityScale = 0;
		wall = GameObject.Find("BossWall");

		pc = robo.GetComponent<PlayerController> ();
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
			
		//カメラを取得し,サブカメラは不使用にしておく
		mainCamera = GameObject.Find ("Main Camera");
		subCamera = GameObject.Find ("SubCamera");
		subCamera.SetActive(false);

		//壁は見えない状態にしておく
		wall.GetComponent<SpriteRenderer> () .enabled = false;

		shaker = GameObject.Find ("Shaker");

		Wanim = GameObject.Find ("warnning").GetComponent<Animator> ();
		//UIanim = GameObject.Find ("BossHP").GetComponent<Animator> ();


		//インスペクターから配列の大きさ（オーディオ分）を指定
		AudioSource[] audiosources= GetComponents<AudioSource> ();
		sound[0] = audiosources [0];
		sound[1] = audiosources [1];
		sound[2] = audiosources [2];
		//sound[0] = GetComponent<AudioSource> ();
		//sound[1] = GetComponent<AudioSource> ();


	}
		

	void OnTriggerEnter2D(Collider2D col){
		//ボス出現演出呼び出し
		StartCoroutine("BossComeUp");
	}


	IEnumerator BossComeUp(){

		//プレイヤーの入力受付を中止
		pc.canMove = false;
		yield return new WaitForSeconds (0.2f);

		//Boss出現警告をUIで表示
		Wanim.SetTrigger("warnning");
		yield return new WaitForSeconds (0.5f);

		//警告音
		sound[0].PlayOneShot (sound[0].clip);
		yield return new WaitForSeconds (2.0f);

		//gc.StopBGM();

		//壁を可視化
		wall.GetComponent<SpriteRenderer> () .enabled = true;

		//プレイヤーの背後に壁が出現
		wall.GetComponent<Rigidbody2D>().gravityScale = 3;

		//落下音
		sound [1].PlayOneShot (sound [1].clip);
		yield return new WaitForSeconds (0.8f);

		//着地音
		sound [2].PlayOneShot (sound [2].clip);
		yield return new WaitForSeconds (0.7f);

		//カメラチェンジ（メインからサブ）
		ChangeCamera();
		yield return new WaitForSeconds (1.0f);

		//Bossの登場演出
		boss.GetComponent<Rigidbody2D> ().gravityScale = 3;

		//落下音
		sound [1].PlayOneShot (sound [1].clip);
		yield return new WaitForSeconds (0.8f);

		//着地音
		sound [2].PlayOneShot (sound [2].clip);

		//Bossの着地後に画面揺れ
		shaker.GetComponent<Shaker> ().shakeOnL = true;
		yield return new WaitForSeconds (0.3f);

		//ボスのHPゲージのスライドイン
		UIanim.SetTrigger ("BossComeUp");

		//コーンの落下演出
		corn.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (2.0f);

		//カメラチェンジ（サブからメイン）
		ChangeCamera ();
		yield return new WaitForSeconds (1.0f);

		//プレイヤーの入力受付を再開
		pc.canMove = true;
		//yield return new WaitForSeconds (1.0f);

		//gc.BossBGM();

		//出現ポイント,演出用オブジェクトの削除
		Destroy(corn);
		Destroy(this.gameObject);

	}


	//カメラの切り替えを行う
	void ChangeCamera(){
		if (mainCamera.activeSelf) {
			mainCamera.SetActive (false);
			subCamera.SetActive (true);
		} else {
			mainCamera.SetActive (true);
			subCamera.SetActive (false);
		}
	}

}
