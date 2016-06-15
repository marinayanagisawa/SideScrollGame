using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private GameObject robo;
	private PlayerController pc;

	//ゲームオーバー表示用
	public Text gameOverText;

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
	private Animator clearAnim;

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
		//クリア表示用のアニメーター取得
		clearAnim = GameObject.Find ("Clear").GetComponent<Animator> ();

		//サウンド取得
		AudioSource[] audiosources= GetComponents<AudioSource> ();
		sound[0] = audiosources [0];
		sound[1] = audiosources [1];
		sound[2] = audiosources [2];
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
			//爆発サウンド(エフェクトはPlayerConrtroller)
			sound[0].PlayOneShot (sound[0].clip);
			gameOver= true;
			GameOver();
		}

		//クリアフラグはGoal.csから受け取り,GameClear()を呼び出す
		if (clear == true) {
			GameClear ();
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
		Invoke("ReturnToTitle", 2.2f);
	}
		
	//titleからハイスコアを取得するためのgetter
	public static int GetHighScore(){
		int getScore = PlayerPrefs.GetInt(highScoreKey);
		return getScore;
	}
		
	public void GameClear(){
		Debug.Log("GameClear(), Called!(from GameContriller)");

		//-----------------------------------ToDo
		//サウンドを鳴らす

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
		yield return new WaitForSeconds (2.0f);
		//背景を暗くする
		animator.SetTrigger ("Back");
		Debug.Log ("ResultStart!!");
		yield return new WaitForSeconds (1.0f);
		//以降リザルトを1行ずつ表示
		result.text = "RESULT";
		yield return new WaitForSeconds (1.0f);

		sound[2].PlayOneShot (sound[2].clip);
		resultScore.text = "Score   " + playScore;
		yield return new WaitForSeconds (1.0f);

		sound[2].PlayOneShot (sound[2].clip);
		resultLife.text = "Life   " + (pc.life + 1) * 100;
		yield return new WaitForSeconds (1.0f);

		resultLine.text = "_________";
		yield return new WaitForSeconds (1.0f);

		sound[2].PlayOneShot (sound[2].clip);
		int total = playScore + (pc.life + 1) * 100;
		resultTotal.text = "Total   " + total;
		playScore = total;

		//ハイスコアの更新と保存
		if (highScore < playScore) {
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

	//敵の爆発音
	public void EnemyExplode(){
		sound[1].PlayOneShot (sound[1].clip);
	}

}
