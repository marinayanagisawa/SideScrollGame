using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {


	//インスペクタから設定
	public GameObject gimmickObj;

	private AudioSource sound;

	void Start () {

		sound = GetComponent<AudioSource> ();

	}

	void OnTriggerEnter2D(Collider2D col){
		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		//プレイヤーと当たったら,設定しておいたギミックが作動
		if (layerName == "Chara") {
			gimmickObj.GetComponent<Rigidbody2D> ().gravityScale = 2;
			sound.PlayOneShot (sound.clip, 1.0f);

			Destroy (gimmickObj, 2.0f);
		}
	}
}
