﻿using UnityEngine;
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

	private Animator anim;

	public AudioSource[] sound;


	void Start () {

		robo = GameObject.Find ("robo");
		boss = GameObject.Find ("Boss");
		boss.GetComponent<Rigidbody2D> ().gravityScale = 0;
		wall = GameObject.Find("BossWall");

		pc = robo.GetComponent<PlayerController> ();
		
		//カメラを取得し,サブカメラは不使用にしておく
		mainCamera = GameObject.Find ("Main Camera");
		subCamera = GameObject.Find ("SubCamera");
		subCamera.SetActive(false);

		shaker = GameObject.Find ("Shaker");

		anim = GameObject.Find ("warnning").GetComponent<Animator> ();

		AudioSource[] audiosources= GetComponents<AudioSource> ();
		sound[0] = audiosources [0];
		sound[1] = audiosources [1];
		//sound[0] = GetComponent<AudioSource> ();
		//sound[1] = GetComponent<AudioSource> ();


	}
		

	void OnTriggerEnter2D(Collider2D col){
		//ボス出現演出呼び出し
		StartCoroutine("BossComeUp");
	}


	IEnumerator BossComeUp(){
		//ToDo----------------BGMをストップ（GameController）

		//プレイヤーの入力受付を中止
		pc.canMove = false;
		yield return new WaitForSeconds (0.2f);

		//Boss出現警告をUIで表示
		anim.SetTrigger("warnning");
		yield return new WaitForSeconds (0.5f);

		//警告音
		sound[0].PlayOneShot (sound[0].clip);
		yield return new WaitForSeconds (2.0f);

		//プレイヤーの背後に壁が出現
		wall.GetComponent<Rigidbody2D>().gravityScale = 3;
		sound [1].PlayOneShot (sound [1].clip);
		yield return new WaitForSeconds (1.5f);

		//カメラチェンジ（メインからサブ）
		ChangeCamera();
		yield return new WaitForSeconds (1.0f);

		//Bossの登場演出
		boss.GetComponent<Rigidbody2D> ().gravityScale = 3;
		sound [1].PlayOneShot (sound [1].clip);
		yield return new WaitForSeconds (0.8f);

		//Bossの着地後に画面揺れ
		shaker.GetComponent<Shaker> ().shakeOnL = true;
		yield return new WaitForSeconds (0.3f);

		//コーンの落下演出
		corn.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (2.0f);

		//カメラチェンジ（サブからメイン）
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
