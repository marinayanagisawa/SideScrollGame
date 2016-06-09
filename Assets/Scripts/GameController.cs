using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private GameObject robo;
	private PlayerController pc;

	//ゲームオーバー表示用
	private Text gameOverText;

	//スコア合計用（スコア計算本体は,PlayerShotのほうで,衝突判定時に一緒に行っている）
	public int playScore;
	public int highScore;

	//スコア表示用
	private Text scoreText;
	private Text highScoreText;

	//playerPrefsのキー
	public static string highScoreKey = "HighScore";

	//HPゲージ
	private Slider slider;

	//トランジション用
	private Animator animator;

	public bool gameOver;

	void Start () {

		gameOver = false;

		//プレイヤーの状態を取得
		robo = GameObject.Find ("robo");
		pc = robo.GetComponent<PlayerController> ();

		//ゲームオーバー表示のためのテキストを取得
		gameOverText = GameObject.Find ("GameOverText").GetComponent<Text> ();

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
			gameOver= true;
			GameOver();
		}
	}

	public void GameOver(){
		//Debug.Log ("GameOver(), Called!! Score =" + playScore);
		//ゲームオーバーのテキスト表示
		gameOverText.text = "GameOver";

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
		int getScore = PlayerPrefs.GetInt(highScoreKey);
		return getScore;
	}
		
	//タイトルに戻る
	void ReturnToTitle(){
		SceneManager.LoadScene ("title");
	}

}
