using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public GameObject robo;
	public GameObject[] enemy;
	public int enemyNum;
	public float generateDis = 2.0f;

	public float dis;


	void Start () {
		

	}

	void Update () {
		
		//ジェネレーターの位置を取得
		Vector2 enemyPos = this.transform.position;
		//プレイヤーの位置を取得
		Vector2 roboPos = robo.transform.position;
		//プレイヤーとジェネレーターの位置を比較
		dis = Vector2.Distance (roboPos, enemyPos);
		//Debug.Log (dis);

		//敵を生成し,ジェネレーターを削除
		if (dis < generateDis) {
			Instantiate (enemy[enemyNum] , transform.position, transform.rotation);
			Destroy (this);
		}

	}

}
