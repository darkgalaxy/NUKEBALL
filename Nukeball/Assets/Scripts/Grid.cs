using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public GameObject GameManager;
	public AudioSource sourceSFX;	

    //public struct gridParam
    // {

        public int index;    // sequence of grid around Earth
        public int gridType; // 0 = Sea; 1 = City; 2 = Forest; 3 = Land; 4 =  Mountain

        public float[] resource;   //= new float [] { 0, 0, 0, 0 } ;     // population , Water, vegetation, economy

	private int numberAtack = 0;
    
    void Awake()
    {
        GameManager = GameObject.FindWithTag("GameController");
    }

    // Use this for initialization
    void Start () {
        
        switch (gridType)
        {
            case 0: // Sea
                {   // population , Water, vegetation, economy
                    resource = new float[] { 2, 6, 2, 6 };
                }
                break;
            case 1: // City
                {   // population , Water, vegetation, economy
                    resource = new float[] { 10,6,3,10 };
                }
                break;
            case 2: // Forest
                {   // population , Water, vegetation, economy
                    resource = new float[] { 4,10,10,6};              
                }
                break;
            case 3: // Land
                {   // population , Water, vegetation, economy
                    resource = new float[] { 5,6,6,10};
                }
                break;
            case 4: // Mountain
                {   // population , Water, vegetation, economy
                    resource = new float[] { 2,4,4,3 };
                }
                break;

        }

        GameManager.SendMessage("AddGlobalResources", resource, SendMessageOptions.RequireReceiver);
        //GetComponent<GlobalVar> 
        //   AddGlobalResources(resource[0], resource[1], resource[2], resource[3]);


        /* Debug.Log("Grid [" + index.ToString() + "]: " + resource[0].ToString() + ", "
           + resource[1].ToString() + ", "
           + resource[2].ToString() + ", "
           + resource[3].ToString()); */
    }


	void OnTriggerEnter2D(Collider2D other)
    {
		GameObject explosion;
        GameManager.SendMessage("DecreaseGlobalResources", resource, SendMessageOptions.RequireReceiver);
        //GlobalVar.instance.DecreaseGlobalResources(resource[0], resource[1], resource[2], resource[3]);
        enabled = false;
        // mudar sprite

		AudioSource sourceSFX = gameObject.AddComponent<AudioSource>();
		sourceSFX.playOnAwake = false;

		if (transform.parent.name == "GridsSubsolo") {
			numberAtack = 2;
		}

		if (numberAtack == 0) {
			explosion = Instantiate(GameManager.GetComponent<GlobalVar> ().prefabExplosion [numberAtack]);
			explosion.transform.position = transform.position;
			explosion.transform.rotation = transform.rotation;
			Destroy (explosion.gameObject, 1f);

			transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = GameManager.GetComponent<GlobalVar> ().spriteResource [gridType];
			if (gridType == 0) { //Water
				sourceSFX.clip = GameManager.GetComponent<GlobalVar> ().sfxExplosion [0];
			} else {
				sourceSFX.clip = GameManager.GetComponent<GlobalVar> ().sfxExplosion [1];
			}

			numberAtack++;
		} else {
			explosion = Instantiate(GameManager.GetComponent<GlobalVar> ().prefabExplosion [1]);
			explosion.transform.position = transform.position;
			explosion.transform.rotation = transform.rotation;
			sourceSFX.clip = GameManager.GetComponent<GlobalVar> ().sfxExplosion [2];
			Destroy (explosion.gameObject, 1.5f);

			Destroy (this.gameObject,1f);
		}

		sourceSFX.Play ();

        
    }



//    private void OnMouseDown()
//    {
//        GameManager.SendMessage("DecreaseGlobalResources", resource, SendMessageOptions.RequireReceiver);
//        enabled = false;
//            
//    }

    // Update is called once per frame
    void Update () {
		
	}
}
