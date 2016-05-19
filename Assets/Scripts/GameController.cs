using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject robo;
	public PlayerController pc;

	public GameObject got;
	public Text text;

	//enum state{GameStart,GamePlay,GameOver}

	void Start () {

		//プレイヤーの状態を取得
		robo = GameObject.Find ("robo");
		pc = robo.GetComponent<PlayerController> ();

		//ゲームオーバー表示のためのテキストを取得しておく
		got = GameObject.Find ("GameOverText");

	}
	

	void Update () {

		//プレイヤーコントローラで死亡フラグを監視して,GameOver()を呼び出す
		if (pc.dead == true) {
			Debug.Log ("Call GameOver fnction!!");
			GameOver();
		}
	}


	void GameStart(){
		pc.dead = true;
		Application.LoadLevel("scene1");
	}


	void GameOver(){
		Debug.Log ("GameOver(), Called!!");
		//ゲームオーバーのテキスト表示
		text = got.GetComponent<Text> ();
		text.text = "GameOver";

		//落下演出のため,カメラ（子要素）を切り離して終了
		robo.transform.DetachChildren ();
		//少し待ってからゲームスタート
		Invoke("GameStart", 2.0f);
	}


}
