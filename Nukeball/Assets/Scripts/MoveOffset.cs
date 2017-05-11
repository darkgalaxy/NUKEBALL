using UnityEngine;
using System.Collections;

public class MoveOffset : MonoBehaviour {

	private Material currentMaterial;
	public float speed;
	private float offset;



	// Use this for initialization
	void Start () {

		currentMaterial = GetComponent<Renderer> ().material;


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		offset += speed * 0.001f;

		currentMaterial.SetTextureOffset ("_MainTex", new Vector2 ( offset,0f));
	
	}
}
