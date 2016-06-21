using UnityEngine;
using System.Collections;

public class Shaker : MonoBehaviour {


	public GameObject camera;

	public bool shakeOn = false;

	// Use this for initialization
	void Start () {
	
		//Shack (camera);

	}
	
	// Update is called once per frame
	void Update () {

		if (shakeOn) {
			Shake (camera);
			shakeOn = false;
		}

	}




	public void Shake(GameObject obj){

		iTween.ShakePosition(obj, iTween.Hash("x",0.1f,"y",0.1f,"time",0.7f));

	}

}
