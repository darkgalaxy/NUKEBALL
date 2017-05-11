using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour {

    public float powerBarPeriod = 2.0f;
    private Slider sld;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // sld = GetComponent ("Slider") as Slider;
      //  sld.value = (Time.deltaTime % 60.0f) * powerBarPeriod / powerBarPeriod - 1;
       // Debug.Log("Power= " + sld.value.ToString());
	}
}
