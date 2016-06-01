using UnityEngine;
using System.Collections;

public class SetSortingLayer : MonoBehaviour {

	//パーティクルのソーティングレイヤーがInspectorから変更できないようなので,
	//ソーティングレイヤーを変更するだけのスクリプト

	public string layerName = "Smoke";

	void Start () {
	
		GetComponent<Renderer>().sortingLayerName = layerName;

	}
	

}
