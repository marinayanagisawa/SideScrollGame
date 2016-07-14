using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	//インスペクタからそれぞれ設定する項目
	public GameObject[] enemy;
	public int enemyNum;
	public bool once = true;

	public GameObject robo;
	public float generateDis = 2.0f;
	private float dis;
	public float nextGenerateTime = 3.0f;
	private bool canGenerate = true;
	public float GenerateLimit = 3.0f;
	private GameController gc;

	void Start () {

		robo = GameObject.Find ("robo");
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();

	}

	void Update () {

		if (gc.gameOver == false) {

			//ジェネレーターとプレイヤーの位置を取得
			Vector2 enemyPos = this.transform.position;
			Vector2 roboPos = robo.transform.position;
			//プレイヤーとジェネレーターの位置を比較
			dis = Vector2.Distance (roboPos, enemyPos);
			//Debug.Log (dis);

			//プレイヤーが近づいた時の処理
			if (dis < generateDis) {

				if (once) {
					//一度生成してから消滅
					GenerateOnce ();
					Destroy (this);
				}

				if (!once && dis > GenerateLimit) {
					if (canGenerate) {
						StartCoroutine (Generate ());
					}
				}
			}

		} 

	}


	//敵を生成(once用)
	void GenerateOnce(){
		Instantiate (enemy[enemyNum] , transform.position, transform.rotation);
	}

	//敵を生成
	IEnumerator Generate(){
		canGenerate = false;
		Instantiate (enemy [enemyNum], transform.position, transform.rotation);
		yield return new WaitForSeconds (nextGenerateTime);
		canGenerate = true;

	}
		
}
