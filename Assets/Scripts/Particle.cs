using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	private float particleLifetime = 2.0f;

	void Start () {
		//シュリケンの残骸を消す
		ParticleSystem particleSystem = GetComponent <ParticleSystem>();
		Destroy(this.gameObject, particleLifetime);
	}
}
