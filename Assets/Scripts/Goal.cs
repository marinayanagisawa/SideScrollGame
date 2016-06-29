using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private GameController gc;
	private ParticleSystem particle;

	void Start () {
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
		particle = GameObject.Find ("Particle System").GetComponent<ParticleSystem> ();
	}

	//ゴールのポイントに触れるとクリア
	void OnTriggerEnter2D(Collider2D col){

		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "Chara") {
			gc.clear = true;
			//パーティクルを表示
			particle.Play();

		}

	}

}
