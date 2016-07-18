using UnityEngine;
using System.Collections;

public class BossPoint : MonoBehaviour {

	private GameObject mainCamera;
	private GameObject subCamera;

	private GameObject robo;
	private GameObject boss;


	void Start () {

		robo = GameObject.Find ("robo");
		boss = GameObject.Find ("Boss");
		boss.GetComponent<Rigidbody2D> ().gravityScale = 0;

		//カメラを取得し,サブカメラは不使用にしておく
		mainCamera = GameObject.Find ("Main Camera");
		subCamera = GameObject.Find ("SubCamera");
		subCamera.SetActive(false);

	
	}

	void OnTriggerEnter2D(Collider col){
		//ボス出現演出呼び出し
		//BossPointの削除

	}


	void BossComeUp(){
		//Todo----------------------ボス出現演出
		//BGMをストップ（GameController）
		//プレイヤーのCanMoveをオフ
		//プレイヤーの背後に壁を作成
		//カメラチェンジ呼び出し（メインからサブへ）
		//BossのGravityScaleを３に変更
		//Bossの着地後に画面揺れ
		//カメラチェンジ呼び出し（サブからメインへ）
		//プレイヤーのCanMoveをオン
		//bossBGMの再生（GameControllerに追加）
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
