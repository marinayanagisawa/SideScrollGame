using UnityEngine;
using System.Collections;

public class Shaker : MonoBehaviour {

	//揺らしたいオブジェクトをインスペクターから設定
	public GameObject cameraObj;

	//他のクラスからフラグを立てる
	public bool shakeOn = false;
	public bool shakeOnM = false;
	public bool shakeOnL = false;


	void Update () {
		//フラグを見て小揺れ
		if (shakeOn) {
			Shake (cameraObj);
			shakeOn = false;
		}

		//フラグを見て中揺れ
		if (shakeOnM) {
			ShakeM (cameraObj);
			shakeOnM = false;
		}

		//フラグを見て中揺れ
		if (shakeOnL) {
			ShakeL (cameraObj);
			shakeOnL = false;
		}
	}



	//小揺れ用
	public void Shake(GameObject obj){
		iTween.ShakePosition(obj, iTween.Hash("y",0.1f,"time",0.7f));
	}
		
	//中揺れ用
	public void ShakeM(GameObject obj){
		iTween.ShakePosition(obj, iTween.Hash("y",0.3f,"time",0.8f));
	}
		
	//（boss演出）大揺れ用
	public void ShakeL(GameObject obj){
		iTween.ShakePosition(obj, iTween.Hash("y",0.5f,"time",1.0f));
	}
}
