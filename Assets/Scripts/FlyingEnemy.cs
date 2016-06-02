using UnityEngine;
using System.Collections;

public class FlyingEnemy : Enemy {


	void Start () {
		Destroy (this.gameObject, lifetime);
	}
	

	void Update () {
		//移動処理
		base.Move ();
	}
}
