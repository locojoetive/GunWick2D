using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimScript : MonoBehaviour {
	public bool player1 = false;

	private new Transform transform;
	private Camera c;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
		c = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
		float x = 0.0f, y = 0.0f;
		if (player1) {
			/*x = Input.GetAxis ("Mouse X");
			y = Input.GetAxis("Mouse Y");*/
			transform.position = c.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, c.nearClipPlane));
		} else {
			x = Input.GetAxis ("Player2HorizontalX");
			y = -Input.GetAxis("Player2VerticalX");
		}
		transform.Translate(new Vector3(x, y, 0.0f));
	}

	public void SetToPlayer1(){
		player1 = true;
	}
}