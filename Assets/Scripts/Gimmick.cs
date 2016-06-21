using UnityEngine;
using System.Collections;

public class Gimmick : MonoBehaviour {

	//インスペクタから設定
	public GameObject gimmickObj;
	public Animator animator;

	//サウンド
	private AudioSource sound;

	public GameObject shaker;

	void Start () {

		shaker = GameObject.Find ("Shaker");
		animator = gimmickObj.GetComponent<Animator>();
		sound = GetComponent<AudioSource> ();

	}
		
	void OnTriggerEnter2D(Collider2D col){
		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		//プレイヤーと当たったら,設定しておいたギミックが作動
		if (layerName == "Chara") {
			animator.SetTrigger("switch");
			sound.PlayOneShot (sound.clip, 1.0f);
			shaker.GetComponent<Shaker> ().shakeOn = true;


			Destroy (gimmickObj, 2.0f);
			Destroy (this);

		}
	}


}
