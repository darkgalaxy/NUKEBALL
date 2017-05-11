using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _GC : MonoBehaviour {

	public StartOptions startOptions;

	public GameObject prefabMissileBaseTrump;
	public Transform spawnMissileTrump;
	public GameObject[] prefabMissileTrump;
	public Sprite[] imgMissileTrump;
	public Sprite[] imgBannerPlay;

	public GameObject prefabMissileBaseKin;
	public Transform spawnMissileKin;
	public GameObject[] prefabMissileKin;
	public Sprite[] imgMissileKin;

	public bool isAiPlayerTrump;

	public void StartGame(int playerSelect){
		startOptions = FindObjectOfType (typeof(StartOptions)) as StartOptions;

		if (playerSelect == 0) {
			isAiPlayerTrump = false;
		} else {
			isAiPlayerTrump = true;
		}

		startOptions.bannerPlayer.sprite = imgBannerPlay [playerSelect];
		startOptions.bannerPlayer.enabled = true;

		if (isAiPlayerTrump) {
			StartCoroutine( RotateWorld (this.transform, new Vector3(0f,0f,180f),2f));
			//transform.Rotate(new Vector3(0f,0f,180f));
		}


		StartCoroutine (SpawnMissileTrump ());
		StartCoroutine (SpawnMissileKin ());
	


	}

	public void CreateMissibleBaseTrump(){
		StartCoroutine (SpawnMissileTrump ());
	}

	IEnumerator RotateWorld(Transform t, Vector3 target, float duration) {
		Vector3 diffVector = (target - t.position);
		Vector3 rotTemp = Vector3.zero;
		float diffLength = diffVector.magnitude;
		diffVector.Normalize ();

		float counter = 0;
		while (counter < duration) {
			float movAmount = (Time.deltaTime * diffLength) / duration;
			//rotTemp = Quaternion.Euler(t.rotation);
			//rotTemp += Quaternion.Euler (diffVector * movAmount);
			//t.rotation = Quaternion.RotateTowards(t.rotation,Quaternion.Euler(target), movAmount);
			t.Rotate(diffVector * movAmount);
			counter += Time.deltaTime;
			yield return null;
		}


	}

	IEnumerator SpawnMissileTrump() {

		yield return new WaitForSeconds (1f); 
		GameObject tempPrefab;
		tempPrefab = Instantiate (prefabMissileBaseTrump, spawnMissileTrump.transform.position, spawnMissileTrump.transform.rotation, transform);
		tempPrefab.GetComponent<MissileBase> ().isAI = isAiPlayerTrump;
		tempPrefab.GetComponent<MissileBase> ().isTrump = true;
	}


	public void CreateMissibleBaseKin(){
		StartCoroutine (SpawnMissileKin ());
	}


	IEnumerator SpawnMissileKin() {

		yield return new WaitForSeconds (1f); 
		GameObject tempPrefab;
		tempPrefab = Instantiate (prefabMissileBaseKin, spawnMissileKin.transform.position, spawnMissileKin.transform.rotation, transform);
		tempPrefab.GetComponent<MissileBase> ().isAI = !isAiPlayerTrump;
		tempPrefab.GetComponent<MissileBase> ().isTrump = false;

	}

}
