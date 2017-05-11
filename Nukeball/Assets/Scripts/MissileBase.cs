using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileBase : MonoBehaviour {
	
	public GlobalVar gameManager;
	public StartOptions startOptions;
	private _GC _gc;
	public int cannonLife;
	public bool isAI;
	public bool isTrump;
	private AudioSource audioSFX;	

	//public bool isMove;
	//public GameObject centro;
	//public GameObject target;

	private Vector3 mousePosition;
	private Transform cannon;
	private GameObject missile;

	//private Rigidbody2D rb;

	//missile parameter
	public float speedRotation = 30f;
	public float sideRotation;
	public float speedTarget = 0.7f;
	private int idMissile;
	private Image imageNextMissile;
	private string[] nameMissileTrump = {"AIRSHARK","ZENBOOB"} ;
	private int[] weigthMissileTrump = {90,80}; 
	private int[] powerMissileTrump = {70,80};

	private string[] nameMissileKin = {"REDMELLON","DUCKLAB"} ;
	private int[] weigthMissileKin = {75,90};
	private int[] powerMissileKin = {90, 90};

	void Awake () {

		audioSFX = GetComponent<AudioSource> ();

	}

	void Start() {
		gameManager = FindObjectOfType (typeof(GlobalVar)) as GlobalVar;
		_gc = FindObjectOfType (typeof(_GC)) as _GC;
		startOptions = FindObjectOfType (typeof(StartOptions)) as StartOptions;
		imageNextMissile = startOptions.imgNextMissile;

		cannon = transform.FindChild("Cannon");



		idMissile = Random.Range (0, 2);
		ChooseNextMissile ();
		ChargeMissile ();
	}




	void OnMouseDrag  () {
		
		if (isAI)
			return;
		
		mousePosition = Input.mousePosition;           
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		Quaternion rot  = Quaternion.LookRotation (cannon.transform.position - mousePosition, Vector3.back);


		PositionCannon (rot);
	}
		
	void PositionCannon (Quaternion rot){
		float speedlimit = 35; 

		if (!isAI) {
			if ((rot.eulerAngles.z >= 0f && rot.eulerAngles.z <= 100f) ||
			   (rot.eulerAngles.z >= 320f && rot.eulerAngles.z <= 360f)) { 
				cannon.eulerAngles = new Vector3 (0, 0, rot.eulerAngles.z);

				if (missile != null) {
					missile.transform.eulerAngles = new Vector3 (0, 0, rot.eulerAngles.z);
				}
				if (rot.eulerAngles.z > 30f && rot.eulerAngles.z < 100f) {
					sideRotation = +1f;
					speedRotation = speedlimit * (rot.eulerAngles.z / 70);

				} else {
					sideRotation = -1f;
					speedRotation = speedlimit - ((rot.eulerAngles.z - 290) / 70);
					//print ("A:" + rot.eulerAngles.z + " - S:" + speedRotation);
				}
			}
		} else {
			if ((rot.eulerAngles.z >= 130f && rot.eulerAngles.z <= 200f) ||
			   (rot.eulerAngles.z >= 200f && rot.eulerAngles.z <= 270f)) { 
				cannon.eulerAngles = new Vector3 (0, 0, rot.eulerAngles.z);
				if (missile != null) {
					missile.transform.eulerAngles = new Vector3 (0, 0, rot.eulerAngles.z);
				}
				if (rot.eulerAngles.z < 200f) {
					sideRotation = -1f;
					speedRotation = speedlimit * ((200 - rot.eulerAngles.z) / 70);
				} else {
					sideRotation = +1f;
					speedRotation = speedlimit * (1 - ((270 - rot.eulerAngles.z) / 70));
					//print ("A:" + rot.eulerAngles.z + " - S:" + speedRotation);
				}
			}
		}
	}

	void OnMouseUp() {
		if (isAI )
			return;

		StartCoroutine (LaunchMissile (0.3f));

	}
		


	void GetSpeed() {
		// Busca a força da funçao do sprite

		float p = Mathf.Abs (gameManager.powerBarValue);

		p *= 5f ; //((p / 2));
		p += 0.65f; //Este é o valor minimo	

//			
//		if (p < 0.65f)
//			p = 0.65f;
//
//		if(!isAI)
//			print (p);
		
		speedTarget = p;//Random.Range(0.7f,1f);
	}
	void ChargeMissile() {

		if (isTrump) {
			missile = Instantiate (_gc.prefabMissileTrump [idMissile], cannon.transform.position, cannon.transform.rotation, transform);
		} else {
			missile = Instantiate (_gc.prefabMissileKin [idMissile], cannon.transform.position, cannon.transform.rotation, transform);
		}

		if (isAI) {
			StartCoroutine (StarAI ());
		}
	}

	IEnumerator LaunchMissile (float time) {

		if (missile != null) {
			GetSpeed ();

			missile.GetComponent<Missile> ().LaunchNow (sideRotation, speedRotation, speedTarget);

			if (!isAI) {
				audioSFX.Play ();
			}
			missile = null;
		
//		yield return new WaitForSeconds (time);
//		missile.GetComponent<SpriteRenderer> ().color = Color.white; 
        
		ChooseNextMissile ();

		yield return new WaitForSeconds (3f);
		ChargeMissile ();
		}

	}

	void ChooseNextMissile() {
		idMissile = Random.Range (0, 2);
		if (isAI)
			return;
		
		if(isTrump) {
			imageNextMissile.sprite = _gc.imgMissileTrump [idMissile];
			startOptions.nameMissile.text = nameMissileTrump [idMissile];
			startOptions.powerMissile.text = powerMissileTrump [idMissile].ToString();
			startOptions.weightMissile.text = weigthMissileTrump [idMissile].ToString(); 
		} else {
			imageNextMissile.sprite = _gc.imgMissileKin[idMissile];
			startOptions.nameMissile.text = nameMissileKin [idMissile];
			startOptions.powerMissile.text = powerMissileKin [idMissile].ToString();
			startOptions.weightMissile.text = weigthMissileKin [idMissile].ToString(); 
		}
	}


	IEnumerator StarAI () {
		
		yield return StartCoroutine (SetAngulo());

		StartCoroutine (LaunchMissile (0.5f)); 
	}

	IEnumerator SetAngulo() {
		float timeWait = Random.Range (3f, 6f);
		float timeMov = 0.1f;
		float x = 0;
		float i = -10;
		bool isNegative = false;

		Quaternion rot = cannon.rotation;

		if (rot.z < 0)
			isNegative = true;

		if (Random.Range (0, 10) < 5) {
			i *= -1;
		}

		while (timeWait > 0f) {
			yield return new WaitForSeconds (timeMov);
			rot = cannon.rotation;
			timeWait -= timeMov;
			x += i;

			if ((x < -70f) || (x > 70)) {
				i *= -1;
			}

			if (isNegative && rot.z > 0) {
				isNegative = false;
				i *= -1;
			}
			if (!isNegative && rot.z < 0) {
				isNegative = true;
				i *= -1;
			}
	
			rot.eulerAngles += new Vector3(0f,0f,x);
		
			PositionCannon (rot);
		}

		yield return new WaitForSeconds (0.3f);
	}


	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "Missile") {
			GameObject explosion;

			explosion = Instantiate (gameManager.GetComponent<GlobalVar> ().prefabExplosion [0]);
			explosion.transform.position = transform.position;
			explosion.transform.rotation = transform.rotation;
			explosion.transform.localScale *= 2;
			Destroy (explosion.gameObject, 1f);
			audioSFX.clip = gameManager.GetComponent<GlobalVar> ().sfxExplosion [3];
			audioSFX.Play ();

			if (isAI) {
				gameManager.TakePoint ();
			} else {
				gameManager.TakeDamage ();
			}
		}
	}

}
