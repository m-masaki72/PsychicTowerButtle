using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using System.IO;
using UnityEngine.SceneManagement;
using NCMB;

public class prefabGenerator : MonoBehaviour
{

	public GameObject pre;
	public Rigidbody2D preRB;

	private float timer;
	public Text score;

	public Object[] prefabs;
	public GameObject mainCamera;

	private string path = "yuko";

	public bool gameOverFlag;
	private bool Flag;
	private bool emitFlag;
	private bool mouceFlag;
	private bool clickFlag;

	public int psycickPower;

	private Vector3 emitPos;

	public void gameOver(){
		if (!gameOverFlag) {
			gameOverFlag = true;
			naichilab.RankingLoader.Instance.SendScoreAndShowRanking (psycickPower);
			GameObject.Find ("ClickField").SetActive (false);
		}
	}

	void Start (){
		GameObject.Find ("ClickField").SetActive (true);
		timer = 0f;
		Flag = true;
		emitFlag = true;
		clickFlag = false;
		emitPos = new Vector3 (0f, 1f, 0f);
		prefabs = Resources.LoadAll (path, typeof(GameObject));
		psycickPower = 0;
		SoundManager.Instance.PlayBGM(0);
		gameOverFlag = false;
	}

	void FixedUpdate(){
		if (preRB && preRB.velocity.magnitude < 0.001f && emitFlag && !Flag) {
			Flag = true;
			emitFlag = true;
		}
	}

	void Update (){
		if (!gameOverFlag) {
			if (Flag && emitFlag) {
				
				//カメライチの調整
				float height = 0f;
				if (pre && emitPos.y <= pre.transform.localPosition.y + 3f + height) {
					emitPos = new Vector3 (0f, pre.transform.localPosition.y + 3f + height, 0f);
					float camPos = pre.transform.localPosition.y + 3f - 2.8f;
					if (camPos > 0f)
						mainCamera.transform.localPosition = new Vector3 (0f, camPos, -10f);
				}

				//GameObjectの発生
				GameObject p = (GameObject)prefabs [Random.Range (0, prefabs.Length)];
				p.GetComponent<SpriteRenderer> ().sortingOrder = 1;
				score.text = "現在のサイキックパワー\n" + ++psycickPower; 
				mouceFlag = true;

				pre = Instantiate (p, emitPos, Quaternion.identity);
				preRB = pre.transform.GetComponent<Rigidbody2D> ();
				preRB.gravityScale = 0;
				preRB.constraints = RigidbodyConstraints2D.FreezeAll; 
				emitFlag = false;
				Flag = false;
			}


			if (Input.GetMouseButton (0) && mouceFlag && clickFlag) {
				Vector3 moucePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				pre.transform.localPosition = new Vector2 (moucePos.x, emitPos.y);
			}
			if (Input.GetMouseButtonUp (0) && clickFlag && mouceFlag) {
				StartCoroutine ("Example");	
				mouceFlag = false;
			}
		}
	}
	IEnumerator Example() {
		preRB.constraints = RigidbodyConstraints2D.None;
		pre.transform.GetComponent<Rigidbody2D> ().gravityScale = 1f;
		yield return new WaitForSeconds(1f);
		emitFlag = true;
		clickFlag = false;
	}


	public void rotateImage(){
		if (mouceFlag) {
			pre.transform.Rotate (new Vector3 (0f, 0f, 45f));
		}
	}
	public void clickMain(){
		clickFlag = true;
	}

	public void reloadScene(){
		SceneManager.LoadScene (0);
	}
}
