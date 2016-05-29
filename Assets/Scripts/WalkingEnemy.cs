using UnityEngine;
using System.Collections;

public class WalkingEnemy : Enemy {

	public float moveSpeed = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);
	}
}
