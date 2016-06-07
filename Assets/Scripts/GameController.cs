using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject robo;
	public PlayerController pc;

	public GameObject got;
	public Text text;
	//スコア合計用（スコア計算本体は,PlayerShotの衝突判定時に一緒に行っている）
	public int playScore;
	//スコア表示用
	public Text scoreText;
	//HPゲージ
	public Slider slider;


	void Start () {

		//プレイヤーの状態を取得
		robo = GameObject.Find ("robo");
		pc = robo.GetComponent<PlayerController> ();

		//ゲームオーバー表示のためのテキストを取得
		got = GameObject.Find ("GameOverText");

		//スコア表示のためのテキストを取得
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();

		//HPゲージのスライダー取得
		slider = GameObject.Find("Slider").GetComponent<Slider>();

	
	}
	

	void Update () {
		//スコア表示
		scoreText.text = "Score : " + playScore;

		//ライフ表示
		slider.value = (pc.life + 1);


		//プレイヤーコントローラで死亡フラグを監視して,GameOver()を呼び出す
		if (pc.dead == true) {
			Debug.Log ("Call GameOver fnction!!");
			GameOver();
		}
	}


	public void GameStart(){
		pc.dead = true;
		SceneManager.LoadScene("scene1");
	}


	public void GameOver(){
		Debug.Log ("GameOver(), Called!! Score =" + playScore);
		//ゲームオーバーのテキスト表示
		text = got.GetComponent<Text> ();
		text.text = "GameOver";

		//落下演出のため,カメラ（子要素）を切り離して終了
		robo.transform.DetachChildren ();
		//少し待ってからゲームスタート
		//Invoke("GameStart", 2.0f);
	}
		

}
