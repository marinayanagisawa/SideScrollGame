using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	private Animator titleAnim;
	private Animator psAnim;
	private Text scoreText;

	//サウンド(AudioClipの数を増やしたら,インスペクタから配列数を変更)
	public AudioSource[] sound;

	void Start () {

		//タイトル画面のアニメーション
		titleAnim = GameObject.Find ("titleSprite").GetComponent<Animator> ();
		psAnim = GameObject.Find ("Pressx").GetComponent<Animator> ();

		//画面上にハイスコアを表示
		scoreText = GameObject.Find ("Highscore").GetComponent<Text> ();
		Invoke ("WriteScore", 1.0f);

		//サウンド取得
		AudioSource[] audiosources= GetComponents<AudioSource> ();
		sound[0] = audiosources [0];

	}

	void Update () {
	
		//スタート時のタイトルのアニメーションなどを設定
		if (Input.GetKeyDown (KeyCode.X)) {

			//startサウンドを鳴らす
			sound[0].PlayOneShot (sound[0].clip);

			//タイトル,文字のアニメーション
			titleAnim.SetTrigger("started");
			psAnim.SetTrigger ("pressed");

			//ハイスコアの文字を消す
			scoreText.text = "";

			Invoke ("LoadGame", 1.7f);
		}
	}

	//ハイスコアの表示
	public void WriteScore(){
		int setHighScore = GameController.GetHighScore ();
		scoreText.text = "HighScore " + setHighScore.ToString();
	}


	//ゲームシーン呼び出し
	private void LoadGame(){
		SceneManager.LoadScene("scene1");
	}

	#if UNITY_EDITOR
	//デバック用ボタン
	void OnGUI(){
		if (GUI.Button(new Rect(0,0,100,50),"Reset")){
			//保存データを初期化
			PlayerPrefs.DeleteAll();
			Debug.Log ("HighScore Reset!");
		}
	}
	#endif

}
