using UnityEngine;
using System.Collections;

public class QuitApplication : MonoBehaviour {

	public AudioClip sound;

	public void Quit()
	{
		StartCoroutine (QuitGame ());

	}

	IEnumerator QuitGame() {
		AudioSource sourceSFX = gameObject.AddComponent<AudioSource>();
		sourceSFX.clip = sound;
		sourceSFX.playOnAwake = false;
		sourceSFX.loop = false;
		sourceSFX.Play();

		Time.timeScale = 1;
		yield return new WaitForSeconds(1f);

		//If we are running in a standalone build of the game
		#if UNITY_STANDALONE
		//Quit the application
		Application.Quit();
		#endif

		//If we are running in the editor
		#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
