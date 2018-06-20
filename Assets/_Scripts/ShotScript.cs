using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

	public Sprite sprite0, sprite1, sprite2, sprite3;
	private SpriteRenderer spriteRenderer;

	public float speed;
	private Rigidbody2D rb;
	private Transform tr;
	private PlayerHealthScript phs;
	private int weaponNumber = -1;
	private bool weaponSet = false;
	private float slowFactor = 0.5f, slowedFor = 5.0f, stunnedFor = 5.0f;
	private int damage = 5;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		tr = GetComponent<Transform> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		Destroy (gameObject, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!weaponSet) {
			if(weaponNumber == 0){
				spriteRenderer.sprite = sprite0;
			}else if (weaponNumber == 1) {
				spriteRenderer.sprite = sprite1;
			} else if (weaponNumber == 2) {
				spriteRenderer.sprite = sprite2;
			} else if (weaponNumber == 3) {
				spriteRenderer.sprite = sprite3;
			}
		}
		rb.MovePosition(tr.position + speed * tr.up);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			phs = other.gameObject.GetComponent<PlayerHealthScript> ();
			if (weaponNumber == 0) {
				phs.Hit();
			} else if (weaponNumber == 1) {
				phs.Burned();
			} else if (weaponNumber == 2) {
				phs.Slowed(slowedFor, slowFactor);
			} else if (weaponNumber == 3) {
				phs.Stunned (stunnedFor, damage);
			}
		} else if (other.gameObject.tag == "destroyable") {
			other.gameObject.SendMessage ("Destroy");
		}
		Destroy (gameObject);
	}

	public void SetWeapon(int wn){
		weaponNumber = wn;
	}

	void SetSpeed (float newSpeed){
		speed = newSpeed;
	}
}