using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private GameController gc;

	void Start () {
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	void OnTriggerEnter2D(Collider2D col){

		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "Chara") {
			gc.clear = true;
			//-----------------------------------ToDo
			//パーティクルを表示

		}

	}

}
