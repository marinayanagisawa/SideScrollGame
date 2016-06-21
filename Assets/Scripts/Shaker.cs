using UnityEngine;
using System.Collections;

public class Shaker : MonoBehaviour {

	//揺らしたいオブジェクトをインスペクターから設定
	public GameObject camera;

	//他のクラスからフラグを立てる
	public bool shakeOn = false;
	public bool shakeOnM = false;

	void Update () {
		//フラグを見て小揺れ
		if (shakeOn) {
			Shake (camera);
			shakeOn = false;
		}

		//フラグを見て中揺れ
		if (shakeOnM) {
			ShakeM (camera);
			shakeOnM = false;
		}
	}



	//小揺れ用
	public void Shake(GameObject obj){
		iTween.ShakePosition(obj, iTween.Hash("x",0.1f,"y",0.1f,"time",0.7f));
	}
		
	//中揺れ用
	public void ShakeM(GameObject obj){
		iTween.ShakePosition(obj, iTween.Hash("x",0.3f,"y",0.3f,"time",0.8f));
	}
}
