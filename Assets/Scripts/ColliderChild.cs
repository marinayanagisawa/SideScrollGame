using UnityEngine;
using System.Collections;

public class ColliderChild : MonoBehaviour {

	public GameObject parent;

	void Start () {
		parent = transform.parent.gameObject;
	}

	//衝突判定のみ行い,処理はEnemy.csで
	void OnTriggerEnter2D(Collider2D col){
		parent.SendMessage ("FromOnTriggerEnter2D", col);
	}

}
