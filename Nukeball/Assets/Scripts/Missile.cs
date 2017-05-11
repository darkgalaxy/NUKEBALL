using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Missile : MonoBehaviour {

	private GlobalVar gameManager;
	private float speed = 5;
	private float rotatingSpeed = 200f;

	private Vector3 launch;
	private Rigidbody2D rb;
	private BoxCollider2D bc;

	public bool isMove;
	private bool isLauched;

	private GameObject center;
	private GameObject target;
	public float speedRotation = 30f;
	public float speedTarget = 0.7f;
	private float sideRotation;

	//private MissileBase missileBase;

	void Start() {
		isMove = false;
		isLauched = false;



		gameManager = FindObjectOfType (typeof(GlobalVar)) as GlobalVar;

		center = new GameObject (); //Instantiate(ew GameObject.Find("Centro").gameObject,Vector3.zero,transform.rotation,transform);
		center.name ="Center";
		center.transform.position = Vector3.zero;
		center.transform.SetParent(transform.parent);

		target = new GameObject ();// transform.FindChild("Center").FindChild("Target").gameObject;// GameObject.FindGameObjectWithTag ("Target");
		target.name ="Target";
		target.transform.position = Vector3.zero;
		target.transform.SetParent( center.transform);

		rb = GetComponent<Rigidbody2D> ();
		rb.bodyType = RigidbodyType2D.Static;
		bc = GetComponent<BoxCollider2D> ();
		bc.enabled = false;

	}

	void FixedUpdate() {
		if (!isMove) {
			launch = transform.GetChild (0).position;
			target.transform.position = launch;
			return;
		}

		//Estrutura do alvo
		center.transform.RotateAround (center.transform.position, Vector3.forward * sideRotation,speedRotation * Time.deltaTime);


		if (!isLauched) {
			transform.position = Vector3.MoveTowards(transform.position,launch,speed * Time.deltaTime);


			if (transform.position != launch) {
				return;
			} else {
				isLauched = true;
				bc.enabled = true;
				rb.bodyType = RigidbodyType2D.Kinematic;
			}
		}


		target.transform.position = Vector3.MoveTowards (target.transform.position, center.transform.position, speedTarget * Time.deltaTime);


		//Segue target
		Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;

		point2Target.Normalize ();

		float value = Vector3.Cross (point2Target, transform.up).z;


		rb.angularVelocity = rotatingSpeed * value;
		rb.velocity = transform.up * speed;
	}



	public void LaunchNow(float side, float sRotation, float sTarget) {
		sideRotation = side;
		speedRotation = sRotation;
		speedTarget = sTarget;

		isMove = true;

	}

	void OnTriggerEnter2D(Collider2D col) {
		GameObject explosion;

		switch (col.tag) {
		case "Finish":
			isMove = false;
			Destroy (center.gameObject);
			Destroy (this.gameObject);
			break;
		case "Missile":
			isMove = false;
			explosion = Instantiate(gameManager.GetComponent<GlobalVar> ().prefabExplosion [0]);
			explosion.transform.position = transform.FindChild("Tip").transform.position;
			explosion.transform.rotation = transform.FindChild("Tip").transform.rotation;
			Destroy (explosion.gameObject, 1f);
			Destroy (center.gameObject);
			Destroy (this.gameObject);
			break;
		case "Player":
			isMove = false;
			explosion = Instantiate(gameManager.GetComponent<GlobalVar> ().prefabExplosion [0]);
			explosion.transform.position = transform.FindChild("Tip").transform.position;
			explosion.transform.rotation = transform.FindChild("Tip").transform.rotation;
			Destroy (explosion.gameObject, 1f);
			Destroy (center.gameObject);
			Destroy (this.gameObject);
			break;
		case "Sattelite":
			isMove = false;
			explosion = Instantiate(gameManager.GetComponent<GlobalVar> ().prefabExplosion [0]);
			explosion.transform.position = transform.FindChild("Tip").transform.position;
			explosion.transform.rotation = transform.FindChild("Tip").transform.rotation;
			Destroy (explosion.gameObject, 1f);
			Destroy (center.gameObject);
			Destroy (this.gameObject);
			break;
		
		}
	}
		
}
