using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
 
	public float moveSpeed = 3.0f;
	public int score;
	public float lifetime = 10.0f;
	public float hp = 10.0f;

	public PlayerController pc;
	public GameController gc;

	void Start(){
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
		pc = GameObject.Find ("robo").GetComponent<PlayerController> ();

	}
		
	//移動処理
	public void Move(){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);
	}
		


	public void FromOnTriggerEnter2D(Collider2D col){

		string layerName = LayerMask.LayerToName (col.gameObject.layer);

		if (layerName == "shot") {
			Debug.Log ("FromOnTreggerEnter2D come!!");
			//hp = hp - pc.shotPower; 
			hp = hp - 10.0f;
	
			if (hp <= 0){

				//敵に設定されたスコアにアクセス
				int count = score;
				Debug.Log (count);
				//スコアをGameControllerのスコア合計に追加
				//gc.playScore += count;

				Destroy (this.gameObject);
			}

		}

	}
}
