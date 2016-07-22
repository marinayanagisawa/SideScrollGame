using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

	AudioSource sound;

	void Start () {
		sound = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){

		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "shot") {
			
			sound.PlayOneShot (sound.clip);
			Destroy (col.gameObject);

		}
	}
}
	
