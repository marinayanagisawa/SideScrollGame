using UnityEngine;
using System.Collections;

public class BossSprite : MonoBehaviour {



	//弾を発射させるオブジェクトをインスペクターから指定しておく
	public GameObject arm;
	public GameObject shot;



	void BossShot(){
		//弾を撃つ
		Instantiate (shot, arm.transform.position, transform.rotation);
	}

}
