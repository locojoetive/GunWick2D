using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour {
	
	public bool player1 = false;

	//Movement
	public float speed = 0.25f, tempSpeed = 0.0f;
	public float x = 0.0f, y = 0.0f;
	public Transform aim;

	//Dash
	private float dashDuration = 0.2f, dashSpeed=0.75f;
	private Vector3 dashDir;
	private float coolDownDashIn = 0.0f, coolDownDashFor = 1.0f, nextDash =0.0f;
	private bool dashing = false;

	//States
	public bool dash = false, burned = false, stunned = false, slowed = false;

	//Random Movement
	private float nextDecision = 0.0f, coolDownDecisionFor = 1.0f;

	private Rigidbody2D rb;
	private new Transform transform;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!stunned) {
			if (burned) {
				HandleRandomMovement ();
			}else {
				HandleDash ();
				HandleMovement ();
			}
		}
	}

	void HandleMovement(){
		//Normal Movement
		if (!dash && !burned) {
			if (player1) {
				x = Input.GetAxis ("Player1Horizontal");
				y = Input.GetAxis ("Player1Vertical");
			} else {
				
				x = Input.GetAxis ("Player2Horizontal");
				y = Input.GetAxis ("Player2Vertical");

			}
			rb.MovePosition(transform.position + new Vector3 (x, y, 0.0f) * speed);
			if(!player1) aim.GetComponent<Transform>().Translate(new Vector3 (x, y, 0.0f) * speed);
		}
	}

	void HandleDash(){
		if (player1)
			dash = Input.GetKeyDown (KeyCode.Space);
		else
			dash = Input.GetKey (KeyCode.JoystickButton3);
		//Set Dash Direction
		if (dash && nextDash < Time.time) {
			x = Input.GetAxis ("Horizontal");
			y = Input.GetAxis ("Vertical");
			dashDir = new Vector3 (x, y, 0.0f);
			dashDir.Normalize ();
			coolDownDashIn = Time.time + dashDuration;
			nextDash = coolDownDashIn + coolDownDashFor;
			dashing = true;
		}
		//Dash
		else if (dashing && Time.time < coolDownDashIn) {
			rb.MovePosition (transform.position + dashSpeed * dashDir); 
		}
		//Reset Dash
		else if (Input.GetKeyUp (KeyCode.Space) && Time.time > coolDownDashIn) {
			coolDownDashIn = Time.time + dashDuration;
			dashDir = Vector3.zero;
			dashing = false;
		}
	}

	public void HandleRandomMovement (){
		if (Time.time > nextDecision) {
			x = Random.Range (-1.0f, 1.0f);
			y = Random.Range (-1.0f, 1.0f);
			nextDecision = Time.time + coolDownDecisionFor;
		}

		Vector3 direction = new Vector3 (x, y, 0.0f);
		direction.Normalize ();
		Debug.Log (direction);
		transform.up = direction;
		rb.MovePosition (transform.position + direction * speed * 2);	
	}

	public void SetBurned(bool burn) {
		burned = burn;
	}

	public void SetStunned(bool stun){
		stunned = stun;
	}

	public void setSpeed(float newSpeed){
		speed = newSpeed;
	}

	public float getSpeed(){
		return speed;
	}

	public void SetToPlayer1(bool p1){
		player1 = p1;
	}

}