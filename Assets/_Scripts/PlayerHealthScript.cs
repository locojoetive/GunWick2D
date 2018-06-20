using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealthScript : MonoBehaviour {
	
	public bool player1 = false;

	public int health = 100;
	public int life = 3;

	private PlayerMoveScript pms;
	private PlayerAttackScript pas;
	public bool  burned = false, stunned = false, slowed = false;

	private float burnedUntil = 0.0f, burnedFor = 5.0f;
	private float slowedUntil = 0.0f;
	public float stunnedUntil = 0.0f;


	private float revertSlowFactor = 0.0f;

	void Start () {
		pms = GetComponent<PlayerMoveScript> ();
		pas = GetComponent<PlayerAttackScript> ();
		GameObject[] player = GameObject.FindGameObjectsWithTag ("Player");
		if (player[1] == gameObject) {
			player1 = true;
			pms.SetToPlayer1 (true);
			pas.SetToPlayer1 (true);
		}

	}

	void Update() {
		if (Time.time > burnedUntil && burned) {
			burned = false;
			if(pms != null) pms.SetBurned (burned);
			if (pas != null) pas.SetBurned (burned);
		}
		if (Time.time > slowedUntil && slowed) {
			slowed = false;
			//Reverse Effect!!
			if(pms != null) pms.setSpeed (revertSlowFactor * pms.getSpeed());

		}

		if (Time.time > stunnedUntil && stunned) {
			stunned = false;
			 pms.SetStunned (false);
		}
	}

	public void Hit(){
		health = health - Random.Range (4, 8);
		if (health <= 0) {
			Debug.Log ("YOU ARE DEAD!");
			life--;
			SceneManager.LoadScene ("TutorialStage", LoadSceneMode.Single);

		}
	}

	public void Burned() {
		if (!burned) {
			burned = true;
			pms.SetBurned (burned);
			pas.SetBurned (burned);
			burnedUntil = Time.time + burnedFor;
		}
	}

	public void Slowed(float slowedFor, float factor) {
		if (!slowed) {
			slowed = true;
			pms.setSpeed (pms.getSpeed() * factor);
			slowedUntil = Time.time + slowedFor;
			revertSlowFactor = 1.0f / factor;
		}
	}

	public void Stunned(float stunnedFor, int damage){
		if (!stunned) {
			stunned = true;
			pms.SetStunned (stunned);
			stunnedUntil = Time.time + stunnedFor;
			health -= damage;
		}
	}


}