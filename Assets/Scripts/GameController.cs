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
	public static int highScore;

	//スコア表示用
	public Text scoreText;
	public Text highScoreText;

	//playerPrefsのキー
	private string highScoreKey = "HighScore";

	//HPゲージ
	public Slider slider;

	//トランジション用
	public Animator animator;


	void Start () {

		//プレイヤーの状態を取得
		robo = GameObject.Find ("robo");
		pc = robo.GetComponent<PlayerController> ();

		//ゲームオーバー表示のためのテキストを取得
		got = GameObject.Find ("GameOverText");

		//スコア表示のためのテキストを取得
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
		highScoreText = GameObject.Find ("HighScoreText").GetComponent<Text> ();

		//HPゲージのスライダー取得
		slider = GameObject.Find("Slider").GetComponent<Slider>();

		//ハイスコアを取得
		highScore = PlayerPrefs.GetInt (highScoreKey, 0);
	
		//トランジション用のアニメーター取得
		animator = GameObject.Find ("Black").GetComponent<Animator> ();
	}
	

	void Update () {

		//ハイスコアの書き換え
		if (highScore < playScore) {
			highScore = playScore;
		}

		//スコア表示
		scoreText.text = "Score : " + playScore;
		highScoreText.text = "High Score : " + highScore;

		//ライフ表示
		slider.value = (pc.life + 1);

		//プレイヤーコントローラで死亡フラグを監視して,GameOver()を呼び出す
		if (pc.dead == true) {
			//Debug.Log ("Call GameOver fnction!!");
			GameOver();
		}
	}

	/*
	public void GameStart(){
		pc.dead = true;
		SceneManager.LoadScene("scene1");
	}
	*/

	public void GameOver(){
		//Debug.Log ("GameOver(), Called!! Score =" + playScore);
		//ゲームオーバーのテキスト表示
		text = got.GetComponent<Text> ();
		text.text = "GameOver";

		//落下演出のため,カメラ（子要素）を切り離して終了
		robo.transform.DetachChildren ();

		//ハイスコアを保持
		PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.Save ();

		//少し待ってからゲームスタート
		animator.SetTrigger ("FadeOut");
		Invoke("ReturnToTitle", 2.0f);
	}
		
	//titleからハイスコアを取得するためのgetter
	public static int GetHighScore(){
		return highScore;
	}
		
	//タイトルに戻る
	void ReturnToTitle(){
		SceneManager.LoadScene ("title");
	}

}
