using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour {
	private GlobalVar gameManager;

    public int cannonLife;
   // public 

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType (typeof(GlobalVar)) as GlobalVar;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D col) {
		GameObject explosion;

		explosion = Instantiate(gameManager.GetComponent<GlobalVar> ().prefabExplosion [0]);
		explosion.transform.position = transform.position;
		explosion.transform.rotation = transform.rotation;
		explosion.transform.localScale *= 2;
		Destroy (explosion.gameObject, 1f);

		cannonLife--;
	}
}
