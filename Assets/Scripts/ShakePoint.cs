using UnityEngine;
using System.Collections;

public class ShakePoint : MonoBehaviour {

	private GameObject shaker;
	private GameObject robo;

	//揺れの大きさをインスペクターからチェック
	public bool s = false;
	public bool m = false;
	public bool l = false;


	void Start () {
		shaker = GameObject.Find ("Shaker");
		robo = GameObject.Find ("robo");
	}


	void OnTriggerEnter2D(Collider2D col){

		robo.GetComponent<PlayerController> ().canMove = false;

		if (s) {
			shaker.GetComponent<Shaker> ().shakeOn = true;
			Invoke ("SwitchCanMove", 1.0f);
		} else if (m) {
			shaker.GetComponent<Shaker> ().shakeOnM = true;
			Invoke ("SwitchCanMove", 1.0f);
		}else if (l){
			//boss演出のための揺れ
			shaker.GetComponent<Shaker> ().shakeOnL = true;
			Invoke ("SwitchCanMove", 2.0f);
		}

	}

	//揺れがおさまったら動けるようにし,オブジェクトを削除
	void SwitchCanMove(){
		robo.GetComponent<PlayerController>().canMove =  true;
		Destroy (this);
	}

}
