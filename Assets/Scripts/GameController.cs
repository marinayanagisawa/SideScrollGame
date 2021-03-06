﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private GameObject robo;
	private PlayerController pc;

	//ゲームオーバー表示用
	public Text gameOverText;

	//ポーズ表示
	public Text pauseText;

	//スコア合計用（スコア計算本体は,Enemyの子クラスで,衝突判定時行う）
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

	private Animator clearAnim;
	private Animator newScoreAnim;

	//サウンド(AudioClipの数を増やしたら,必ずインスペクタから配列を増やす！)
	public AudioSource[] sound;

	//Generatorにゲーム中か知らせるフラグ
	public bool gameOver;

	//ゴールの到達を受け取る
	public bool clear = false;

	//リザルト用（Inspectorから設定）
	public Text result;
	public Text resultScore;
	public Text resultLife;
	public Text resultLine;
	public Text resultTotal;

	//ポーズフラグ
	public bool isPause = false;


	void Start () {

		gameOver = false;

		robo = GameObject.Find ("robo");
		pc = robo.GetComponent<PlayerController> ();

		gameOverText = GameObject.Find ("GameOverText").GetComponent<Text> ();
		pauseText = GameObject.Find ("PauseText").GetComponent<Text> ();

		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
		highScoreText = GameObject.Find ("HighScoreText").GetComponent<Text> ();

		//HPゲージのスライダー取得
		slider = GameObject.Find("Slider").GetComponent<Slider>();

		//ハイスコアを取得
		highScore = PlayerPrefs.GetInt (highScoreKey, 0);

		//トランジション用のアニメーター取得
		animator = GameObject.Find ("Black").GetComponent<Animator> ();
		//クリア表示用のアニメーター取得
		clearAnim = GameObject.Find ("Clear").GetComponent<Animator> ();
		newScoreAnim = GameObject.Find ("newScoreImage").GetComponent <Animator> ();

		//サウンド取得(インスペクタから増やしたら,その分を取得する)
		AudioSource[] audiosources= GetComponents<AudioSource> ();
		sound[0] = audiosources [0];
		sound[1] = audiosources [1];
		sound[2] = audiosources [2];
		sound[3] = audiosources [3];
		sound[4] = audiosources [4];
		sound[5] = audiosources [5];

	}
	

	void Update () {

		//ハイスコアの書き換え
		if (highScore < playScore) {
			highScore = playScore;
		}

		//スコア表示
		scoreText.text = "SCORE : " + playScore;
		highScoreText.text = "HIGH SCORE : " + highScore;

		//ライフ表示
		slider.value = (pc.life + 1);

		//プレイヤーコントローラで死亡フラグを監視して,GameOver()を呼び出す
		if (pc.dead == true) {
			//爆発サウンド(エフェクトはPlayerConrtroller)
			sound[0].PlayOneShot (sound[0].clip);
			gameOver= true;
			GameOver();
		}

		//クリアフラグはGoal.csから受け取り,GameClear()を呼び出す
		if (clear == true) {
			GameClear ();
		}


		//ポーズ処理
		if (isPause == false) {
			if (Input.GetKeyDown (KeyCode.S)) {
				isPause = true;
				pauseText.text = "PAUSE";
				Pause ();
			}
		} else {

			if (Input.GetKeyDown (KeyCode.S)) {
				isPause = false;
				pauseText.text = "";
				Resume ();
			}
		}


	}

	public void GameOver(){

		//Debug.Log ("GameOver(), Called!! Score =" + playScore);
		//ゲームオーバーのテキスト表示
		gameOverText.text = "GAME OVER";

		//ステージのBGMを止める
		StopBGM();

		//落下演出のため,カメラ（子要素）を切り離して終了
		robo.transform.DetachChildren ();

		//ハイスコアを保持
		PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.Save ();

		//少し待ってからゲームスタート
		Invoke("ReturnToTitle", 2.2f);
	}
		
	//titleからハイスコアを取得するためのgetter
	public static int GetHighScore(){
		int getScore = PlayerPrefs.GetInt(highScoreKey);
		return getScore;
	}
		
	public void GameClear(){
		Debug.Log("GameClear(), Called!(from GameContriller)");

		//クリアサウンドを鳴らす
		//sound[5].PlayOneShot (sound[5].clip);

		//クリア表示
		clearAnim.SetTrigger("Clear");

		clear = false;
		pc.canMove = false;

		//リザルト画面を表示
		StartCoroutine ("Result");

		//ハイスコアを保持
		PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.Save ();

		Invoke("ReturnToTitle", 10.0f);
	}

	//リザルト処理
	IEnumerator Result(){
		//少し待ってからスタート
		yield return new WaitForSeconds (3.0f);
		//背景を暗くする
		animator.SetTrigger ("Back");
		Debug.Log ("ResultStart!!");
		yield return new WaitForSeconds (1.0f);
		//以降リザルトを1行ずつ表示
		result.text = "RESULT";
		yield return new WaitForSeconds (1.0f);

		sound[2].PlayOneShot (sound[2].clip);
		resultScore.text = "SCORE   " + playScore;
		yield return new WaitForSeconds (1.0f);

		sound[2].PlayOneShot (sound[2].clip);
		resultLife.text = "LIFE   " + (pc.life + 1) * 100;
		yield return new WaitForSeconds (1.0f);

		resultLine.text = "_________";
		yield return new WaitForSeconds (1.0f);

		sound[2].PlayOneShot (sound[2].clip);
		int total = playScore + (pc.life + 1) * 100;
		resultTotal.text = "TOTAL   " + total;
		playScore = total;

		//ハイスコアの更新と保存
		if (highScore < playScore) {
			newScoreAnim.SetTrigger ("New");
			highScore = playScore;
		}
			
		PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.Save ();

		yield return new WaitForSeconds (1.0f);
	}


	//フェードアウトしてからタイトルに戻る
	void ReturnToTitle(){
		animator.SetTrigger ("FadeOut");
		SceneManager.LoadScene ("title");
	}

	//敵の爆発音(PlayerShotクラスから使う)
	public void EnemyExplode(){
		sound[1].PlayOneShot (sound[1].clip);
	}

	//ポーズ,レジューム
	void Pause(){
		Time.timeScale = 0;
	}

	void Resume(){
		Time.timeScale = 1;
	}

	public void StopBGM(){
		sound [3].Stop ();
		sound [4].Stop ();
	}

	public void BossBGM(){
		sound[4].Play(44100);
	}

}
