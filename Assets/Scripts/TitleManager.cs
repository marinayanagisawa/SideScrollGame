using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	private Animator titleAnim;
	private Animator psAnim;
	public Text scoreText;


	void Start () {

		//タイトル画面のアニメーション
		titleAnim = GameObject.Find ("titleSprite").GetComponent<Animator> ();
		psAnim = GameObject.Find ("Pressx").GetComponent<Animator> ();
		//画面上にハイスコアを表示
		scoreText = GameObject.Find ("Highscore").GetComponent<Text> ();
		Invoke ("WriteScore", 1.0f);

	}

	void Update () {
	
		//スタート時のタイトルのアニメーションなどを設定
		if (Input.GetKeyDown (KeyCode.X)) {
			titleAnim.SetTrigger("started");
			psAnim.SetTrigger ("pressed");
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
}
