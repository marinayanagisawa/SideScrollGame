using UnityEngine;
using System.Collections;

public class Gimmick : MonoBehaviour {

	//インスペクタから設定
	public GameObject gimmickObj;
	public Animator animator;

	void Start () {
		
		animator = gimmickObj.GetComponent<Animator>();
	}
		
	void OnTriggerEnter2D(Collider2D col){
		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		//プレイヤーと当たったら,設定しておいたギミックが作動
		if (layerName == "Chara") {
			animator.SetTrigger("switch");

			Destroy (gimmickObj, 2.0f);
			Destroy (this);

		}
	}


}
