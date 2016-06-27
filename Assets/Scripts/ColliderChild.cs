using UnityEngine;
using System.Collections;

public class ColliderChild : MonoBehaviour {

	public GameObject parent;

	void Start () {
		parent = transform.parent.gameObject;
	}


	void OnTriggerEnter2D(Collider2D col){
		parent.SendMessage ("FromOnTriggerEnter2D", col);
	}

}
