using UnityEngine;
using System.Collections;

public class SetSortingLayer : MonoBehaviour {

	//パーティクルのソーティングレイヤーがInspectorから変更できないようなので,
	//ソーティングレイヤーを変更するだけのスクリプト（必要によっては他のスクリプトと合体させる）

	public string layerName = "Smoke";

	void Start () {
	
		GetComponent<Renderer>().sortingLayerName = layerName;

	}
	

}
