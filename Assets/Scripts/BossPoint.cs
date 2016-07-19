﻿using UnityEngine;
using System.Collections;

public class BossPoint : MonoBehaviour {

	private GameObject mainCamera;
	private GameObject subCamera;
	private PlayerController pc;
	private GameObject robo;
	private GameObject boss;


	void Start () {

		robo = GameObject.Find ("robo");
		boss = GameObject.Find ("Boss");
		boss.GetComponent<Rigidbody2D> ().gravityScale = 0;

		pc = robo.GetComponent<PlayerController> ();
		
		//カメラを取得し,サブカメラは不使用にしておく
		mainCamera = GameObject.Find ("Main Camera");
		subCamera = GameObject.Find ("SubCamera");
		subCamera.SetActive(false);

	
	}



	void OnTriggerEnter2D(Collider2D col){
		//ボス出現演出呼び出し
		StartCoroutine("BossComeUp");
		//BossPointの削除
		//Destroy(this.gameObject);

	}


	IEnumerator BossComeUp(){
		//Todo----------------------ボス出現演出
		//BGMをストップ（GameController）
		//プレイヤーのCanMoveをオフ
		pc.canMove = false;
		yield return new WaitForSeconds (1.0f);
		//プレイヤーの背後に壁を作成
		//カメラチェンジ呼び出し（メインからサブへ）
		ChangeCamera();
		yield return new WaitForSeconds (1.0f);
		//BossのGravityScaleを３に変更
		boss.GetComponent<Rigidbody2D> ().gravityScale = 3;
		//Bossの着地後に画面揺れ
		//カメラチェンジ呼び出し（サブからメインへ）
		ChangeCamera ();
		yield return new WaitForSeconds (1.0f);
		//プレイヤーのCanMoveをオン
		pc.canMove = true;
		yield return new WaitForSeconds (1.0f);
		//bossBGMの再生（GameControllerに追加）
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
