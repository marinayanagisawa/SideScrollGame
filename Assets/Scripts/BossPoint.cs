using UnityEngine;
using System.Collections;

public class BossPoint : MonoBehaviour {

	private GameObject mainCamera;
	private GameObject subCamera;
	private GameObject shaker;
	public GameObject corn;

	private GameObject robo;
	private GameObject boss;
	private PlayerController pc;

	void Start () {

		robo = GameObject.Find ("robo");
		boss = GameObject.Find ("Boss");
		boss.GetComponent<Rigidbody2D> ().gravityScale = 0;

		pc = robo.GetComponent<PlayerController> ();
		
		//カメラを取得し,サブカメラは不使用にしておく
		mainCamera = GameObject.Find ("Main Camera");
		subCamera = GameObject.Find ("SubCamera");
		subCamera.SetActive(false);

		shaker = GameObject.Find ("Shaker");
	}



	void OnTriggerEnter2D(Collider2D col){
		//ボス出現演出呼び出し
		StartCoroutine("BossComeUp");

	}


	IEnumerator BossComeUp(){
		//ToDo----------------BGMをストップ（GameController）

		//プレイヤーの入力受付を中止
		pc.canMove = false;
		yield return new WaitForSeconds (1.0f);

		//ToDo--------------------Boss出現警告をUIで表示

		//ToDo--------------------プレイヤーの背後に壁を作成

		//カメラチェンジ呼び出し（メインからサブへ）
		ChangeCamera();
		yield return new WaitForSeconds (1.0f);

		//Bossの登場演出
		boss.GetComponent<Rigidbody2D> ().gravityScale = 3;
		yield return new WaitForSeconds (0.8f);

		//Bossの着地後に画面揺れ
		shaker.GetComponent<Shaker> ().shakeOnL = true;
		yield return new WaitForSeconds (0.3f);

		//コーンの落下演出
		corn.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (2.0f);

		//カメラチェンジ呼び出し（サブからメインへ）
		ChangeCamera ();
		yield return new WaitForSeconds (1.0f);

		//プレイヤーの入力受付を再開
		pc.canMove = true;
		yield return new WaitForSeconds (1.0f);

		//ToDo--------------bossBGMの再生（GameControllerに追加）

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
