using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backPanel : MonoBehaviour {

	public float scrollSpeed = 0.001f;

	void Start () {
		this.transform.localPosition = new Vector2 (17.6f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2(-1 * scrollSpeed,0f));

		if (this.transform.localPosition.x <= -4)
			Start ();
	}
}
