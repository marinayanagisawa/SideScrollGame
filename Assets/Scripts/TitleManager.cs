using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	private Animator titleAnim;
	private Animator psAnim;


	void Start () {

		titleAnim = GameObject.Find ("titleSprite").GetComponent<Animator> ();
		psAnim = GameObject.Find ("Pressx").GetComponent<Animator> ();
	}

	void Update () {
	
		//タイトル画面のアニメーション設定
		if (Input.GetKeyDown (KeyCode.X)) {
			titleAnim.SetTrigger("started");
			psAnim.SetTrigger ("pressed");
			Invoke ("LoadGame", 1.7f);
		}
	}

	//ゲームシーン呼び出し
	private void LoadGame(){
		SceneManager.LoadScene("scene1");
	}
}
