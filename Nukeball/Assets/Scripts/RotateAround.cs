using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {
	public GlobalVar gameManager;
	public Transform target; // the object to rotate around
	public int speed; // the speed of rotation
	
	void Start() {
		gameManager = FindObjectOfType (typeof(GlobalVar)) as GlobalVar;

		if (target == null) 
		{
			target = this.gameObject.transform;
			Debug.Log ("RotateAround target not specified. Defaulting to parent GameObject");
		}
	}

	// Update is called once per frame
	void Update () {
		//Stop if don't select a player
		if (gameManager.playerSelection < 0)
			return;


        // RotateAround takes three arguments, first is the Vector to rotate around
        // second is a vector that axis to rotate around
        // third is the degrees to rotate, in this case the speed per second
        //transform.RotateAround(target.transform.position,target.transform.up,speed * Time.deltaTime);
        // alterado segundo parametro para girar ao redor do eixo z

       transform.RotateAround(target.transform.position, target.transform.forward, speed * Time.deltaTime);
    }

   
   void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger enter sattelite");
        
        if (collision.tag != "Sattelite")

            StartCoroutine(ReactivateSattelite());

    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        Debug.Log("collision enter sattelite");

        if (collision.transform.tag != "Sattelite")

            StartCoroutine(ReactivateSattelite());

    }


    // somente para teste, desabilitar na versao final
    private void OnMouseDown()
    {

        StartCoroutine(ReactivateSattelite());

    }

    IEnumerator ReactivateSattelite()
    {
     //   Debug.Log("Satelite atingido");
        // esconde satelite / desabilita collider
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;

   
        //aguarda intervalo
        yield return new WaitForSeconds(15f);

        // depois do intervalo, mostra de novo o satelite / habilita collider
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<Collider2D>().enabled = true;


    }



}
