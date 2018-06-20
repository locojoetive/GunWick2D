using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour {

	public bool player1 = false;

	//Shot
	public Transform aim, shotSpawn;
	public GameObject shot;
	public int weapon = 0;
	Vector3 shootDir;
	float coolDownShotFor = 0.3f, nextShot = 0.0f;

	bool stunned = false, shoot = false, burned = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		shoot = Input.GetMouseButton(0);
		if (!stunned && !burned)
			HandleAttack ();
		HandleWeaponSwitch ();
	}

	void HandleAttack() {
		shootDir = aim.position - transform.position;
		shootDir.z = 0.0f;
		transform.up = shootDir;

		if ((player1 && Input.GetKey (KeyCode.Mouse0) && Time.time > nextShot) 
			|| (!player1 && Input.GetKey(KeyCode.Joystick1Button0) && Time.time > nextShot)) {

			GameObject shotClone = Instantiate (shot, shotSpawn.position, transform.rotation);
			shotClone.GetComponent<ShotScript> ().SetWeapon (weapon);
			nextShot = Time.time + coolDownShotFor;
		}
	}

	public void SetToPlayer1(bool p1){
		player1 = p1;
		aim.GetComponent<PlayerAimScript>().SetToPlayer1 ();
	}

	public void HandleWeaponSwitch(){
		if ((player1 && Input.GetKeyDown (KeyCode.Q)) || (!player1 && Input.GetKeyDown(KeyCode.Joystick1Button2)))
			weapon = (weapon + 1) % 4;
	}

	public void SetBurned(bool b){
		burned = b;
	}
}