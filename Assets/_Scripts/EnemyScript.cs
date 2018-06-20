using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public int life = 20;
	public float speed, tempSpeed;
	public Transform shotSpawn;
	public GameObject shot;

	public bool detected = false, slowed = false;
	public float radius;
	public LayerMask playerLayer;
	public Transform player;

	private Rigidbody2D rb;
	public float nextDecision, coolDownDecisionFor, nextShot, coolDownShotFor, slowedUntil, slowFor;

	//decided direction
	private float x = 0.0f, y = 0.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement ();
		HandleStates ();
	}

	void HandleMovement(){
		detected = Physics2D.OverlapCircle (transform.position, radius, playerLayer.value);

		if (!detected) {
			if (Time.time > nextDecision) {
				x = Random.Range (-1.0f, 1.0f);
				y = Random.Range (-1.0f, 1.0f);

				nextDecision = Time.time + coolDownDecisionFor;
			}
			Vector2 direction = new Vector2 (x, y);
			direction.Normalize ();
			rb.MovePosition (rb.position + direction * speed);
		} else {
			Vector3 shootDir = player.transform.position - transform.position;
			shootDir.z = 0.0f;
			shootDir.Normalize ();

			transform.right = shootDir;

			if (Time.time > nextShot) {
				Instantiate (shot, shotSpawn.position, transform.rotation);
				nextShot = Time.time + coolDownShotFor;
			}
		}
	}

	void HandleStates(){
		if (slowed && Time.time > slowedUntil){
			slowed = false;
			float temp = 0.0f;
			temp = tempSpeed;
			tempSpeed = speed;
			speed = temp;
		}
	}
	void Hit(){
		life = life - Random.Range (4, 8);
		if (life < 0) {
			Destroy (gameObject);
		}
	}

	public void Slowed() {
		slowed = true;
		slowedUntil = Time.time + slowFor;
		//tempSpeed = speed;
		speed = speed * 0.5f;
		Debug.Log ("New Speed = " + speed);
	}
}