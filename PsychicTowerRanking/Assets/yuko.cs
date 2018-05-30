using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yuko : MonoBehaviour {

	void Update(){
			//Debug.Log ("Destry");
			//Destroy (gameObject);
		}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.name == "Under") { 
			Debug.Log (other.gameObject.name);
			Destroy (this.gameObject);
			GameObject.Find("prefabGenerator").GetComponent<prefabGenerator>().gameOver();
		}
	}

}
