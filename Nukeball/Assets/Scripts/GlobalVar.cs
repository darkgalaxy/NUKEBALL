using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class GlobalVar : MonoBehaviour {

	public int playerSelection;  // 0 = Trump; 1 = Kim
	private _GC gc;

	public float[] totalResource = new float[] { 0f, 0f, 0f, 0f };
	public float[] availableResource = new float[] { 0f, 0f, 0f, 0f };
	public float[] availableResourcePercent = new float[] { 100.0f, 100.0f, 100.0f, 100.0f };

	// Objects
	public Sprite[] spriteResource;
	public GameObject[] prefabExplosion;
	public AudioClip[] sfxExplosion; //01- Kabun1, 02- Kabun2, Water Kabun


	public static GlobalVar instance = null;

	public Text TextResourceValue0;
	public Text TextResourceValue1;
	public Text TextResourceValue2;
	public Text TextResourceValue3;
	public GameObject UIobj;
	//public GameObject PowerBarPointer;
	public GameObject sprtPowerBar;
	public GameObject sprtPowerBarPointer;

	public float powerBarPeriod;
	public float powerBarValue;
	private float timeAccum = 0.0f;

	private ShowPanels showPanels;

	public Image ImageResource0;
	public Image ImageResource1;
	public Image ImageResource2;
	public Image ImageResource3;

	public GameObject PanelEndGame;

	private string strEndGameText1;
	private string strEndGameText2;
	private string strEndGameResourceText0;
	private string strEndGameResourceText1;
	private string strEndGameResourceText2;
	private string strEndGameResourceText3;
	private string strEndGameText3;

	//Life Control
	public int cannonLife;
	public int cannonLifeInemy;

	private void Start()
	{
		//Deixar negativo até que seja escolhido um player
		playerSelection = -1;
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		//Get a reference to ShowPanels attached to UI object
		showPanels = UIobj.GetComponent<ShowPanels>();

		//
		gc = FindObjectOfType(typeof(_GC)) as _GC; 
	}


	public void SetPlayerSelection(int chosenPlayer)
	{
		playerSelection = chosenPlayer;
		// Debug.Log("Player selected: " + playerSelection.ToString());

		gc.StartGame (chosenPlayer);

	}

	public int GetPlayerSelection()
	{  
		return playerSelection;
	}

	public void AddGlobalResources(float [] res)
	{
		totalResource[0] += res[0];
		totalResource[1] += res[1];
		totalResource[2] += res[2];
		totalResource[3] += res[3];

		availableResource[0] += res[0];
		availableResource[1] += res[1];
		availableResource[2] += res[2];
		availableResource[3] += res[3];

		//Debug.Log("Total Resource0: " + totalResource[0].ToString());
	}


	public void DecreaseGlobalResources(float[] res)
	{
		RectTransform rt;
		Image img;
		float perc;


		availableResource[0] -= res[0];
		availableResource[1] -= res[1];
		availableResource[2] -= res[2];
		availableResource[3] -= res[3];

		if (totalResource[0] != 0)
			availableResourcePercent[0] = 100.0f * availableResource[0] / totalResource[0];
		if (totalResource[1] != 0)
			availableResourcePercent[1] = 100.0f * availableResource[1] / totalResource[1];
		if (totalResource[02] != 0)
			availableResourcePercent[2] = 100.0f * availableResource[2] / totalResource[2];
		if (totalResource[03] != 0)
			availableResourcePercent[3] = 100.0f * availableResource[3] / totalResource[3];

		// atualizar interface com valores reduzidos
		TextResourceValue0.text = availableResourcePercent[0].ToString("N1");
		TextResourceValue1.text = availableResourcePercent[1].ToString("N1");
		TextResourceValue2.text = availableResourcePercent[2].ToString("N1");
		TextResourceValue3.text = availableResourcePercent[3].ToString("N1");


		rt = ImageResource0.rectTransform;
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f * availableResourcePercent[0] / 100);

		rt = ImageResource1.rectTransform;
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f * availableResourcePercent[1] / 100);

		rt = ImageResource2.rectTransform;
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f * availableResourcePercent[2] / 100);

		rt = ImageResource3.rectTransform;
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f * availableResourcePercent[3] / 100);

		img = ImageResource0.GetComponent<Image>();
		perc = availableResourcePercent[0];
		if (perc >= 50f && perc < 75.0f)
			img.color = new Color(1, 0.92f, 0.016f, 1); // yellow
		else if (perc < 50f)
			img.color = new Color(1, 0, 0, 1); // red

		img = ImageResource1.GetComponent<Image>();
		perc = availableResourcePercent[1];
		if (perc >= 50f && perc < 75.0f)
			img.color = new Color(1, 0.92f, 0.016f, 1); // yellow
		else if (perc < 50f)
			img.color = new Color(1, 0, 0, 1); // red

		img = ImageResource2.GetComponent<Image>();
		perc = availableResourcePercent[2];
		if (perc >= 50f && perc < 75.0f)
			img.color = new Color(1, 0.92f, 0.016f, 1); // yellow
		else if (perc < 50f)
			img.color = new Color(1, 0, 0, 1); // red

		img = ImageResource3.GetComponent<Image>();
		perc = availableResourcePercent[3];
		if (perc >= 50f && perc < 75.0f)
			img.color = new Color(1, 0.92f, 0.016f, 1); // yellow
		else if (perc < 50f)
			img.color = new Color(1, 0, 0, 1); // red

		if (availableResourcePercent[0] < 50.0f)
		if (availableResourcePercent[1] < 50.0f)
		if (availableResourcePercent[2] < 50.0f)
		if (availableResourcePercent[3] < 50.0f)
		{
			//Debug.Log("Planeta destruido!!");
			showPanels.HideGameplayPanel();
			EndGameMessage("Planet Dead");
			//showPanels.ShowEndGamePanel("Planet Dead"); 

		}
	}


	public void TakeDamage()
	{

		cannonLife--;

		if (cannonLife < 0)
		{

			EndGameMessage("Lose");

		}

	}

	public void TakePoint()
	{

		cannonLifeInemy--;

		if (cannonLife < 0)
		{

			EndGameMessage("Win");

		}

	}


	public void FixedUpdate()
	{

		Transform transPtr;
		float maxSize = 1;

		// atualizacao da barra de forca - valores em powerBarValue entre -1 e 1
		timeAccum += (Time.deltaTime * powerBarPeriod);   // testado com 0.01f  
		powerBarValue = Mathf.Sin(timeAccum * 360);
		transPtr = sprtPowerBarPointer.transform;
		transPtr.localPosition = new Vector3(powerBarValue * maxSize /* + maxSize / 2 */, transPtr.localPosition.y, transPtr.localPosition.z);



	}

	void ResetValues()
	{
		Image img;
		RectTransform rt;

		// availableResource[0] = 0;
		// availableResource[1] = 0;
		// availableResource[2] = 0;
		// availableResource[3] = 0;

		img = ImageResource0.GetComponent<Image>();
		img.color = new Color(0, 1, 0, 1); // green
		img = ImageResource1.GetComponent<Image>();
		img.color = new Color(0, 1, 0, 1); // green
		img = ImageResource2.GetComponent<Image>();
		img.color = new Color(0, 1, 0, 1); // green
		img = ImageResource3.GetComponent<Image>();
		img.color = new Color(0, 1, 0, 1); // green

		rt = ImageResource0.rectTransform;
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f);
		rt = ImageResource1.rectTransform;
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f);
		rt = ImageResource2.rectTransform;
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f);
		rt = ImageResource3.rectTransform;
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f);

	}

	void EndGameMessage(string endType)
	{
		Text auxText;
		GameObject textEndGame1 = PanelEndGame.transform.Find("TextEndGame1").gameObject;
		GameObject textEndGame2 = PanelEndGame.transform.Find("TextEndGame2").gameObject;
		GameObject textEndResource0 = PanelEndGame.transform.Find("TextEndResource0").gameObject;
		GameObject textEndResource1 = PanelEndGame.transform.Find("TextEndResource1").gameObject;
		GameObject textEndResource2 = PanelEndGame.transform.Find("TextEndResource2").gameObject;
		GameObject textEndResource3 = PanelEndGame.transform.Find("TextEndResource3").gameObject;

		GameObject textEndGame3 = PanelEndGame.transform.Find("TextEndGame3").gameObject;

		float pcdestr0 = 100f - availableResourcePercent[0];
		float pcdestr1 = 100f - availableResourcePercent[1];
		float pcdestr2 = 100f - availableResourcePercent[2];
		float pcdestr3 = 100f - availableResourcePercent[3];

		switch (endType)
		{
		case "Planet Dead":
			{
				strEndGameText1 = "You didn't defeat your enemy.";
				strEndGameText2 = "And your war has also destroyed: ";
				strEndGameResourceText0 = pcdestr0.ToString("N1") + "% of world population";
				strEndGameResourceText1 = pcdestr1.ToString("N1") + "% of water supplies";
				strEndGameResourceText2 = pcdestr2.ToString("N1") + "% of total vegetation";
				strEndGameResourceText3 = pcdestr3.ToString("N1") + "% of economic activities";
				strEndGameText3 = "Planet is ruined...";

			}
			break;
		case "Win":
			{
				strEndGameText1 = "Congrats! You defeated your enemy. ";
				strEndGameText2 = "BUT, your war has also destroyed: ";
				strEndGameResourceText0 = pcdestr0.ToString("N1") + "% of world population";
				strEndGameResourceText1 = pcdestr1.ToString("N1") + "% of water supplies";
				strEndGameResourceText2 = pcdestr2.ToString("N1") + "% of total vegetation";
				strEndGameResourceText3 = pcdestr3.ToString("N1") + "% of economic activities";
				strEndGameText3 = "Planet is damaged...";


			}
			break;
		case "Lose":
			{
				strEndGameText1 = "You were defeated by your enemy.";
				strEndGameText2 = "and, by the way, your war has also destroyed: ";
				strEndGameResourceText0 = pcdestr0.ToString("N1") + "% of world population";
				strEndGameResourceText1 = pcdestr1.ToString("N1") + "% of water supplies";
				strEndGameResourceText2 = pcdestr2.ToString("N1") + "% of total vegetation";
				strEndGameResourceText3 = pcdestr3.ToString("N1") + "% of economic activities";
				strEndGameText3 = "Planet is damaged...";

			}
			break;



		}

		Debug.Log("txt2= " + strEndGameText2);

		auxText = textEndGame1.GetComponent<Text>(); auxText.text = strEndGameText1;
		auxText = textEndGame2.GetComponent<Text>(); auxText.text = strEndGameText2;

		auxText = textEndResource0.GetComponent<Text>(); auxText.text = strEndGameResourceText0;
		auxText = textEndResource1.GetComponent<Text>(); auxText.text = strEndGameResourceText1;
		auxText = textEndResource2.GetComponent<Text>(); auxText.text = strEndGameResourceText2;
		auxText = textEndResource3.GetComponent<Text>(); auxText.text = strEndGameResourceText3;

		auxText = textEndGame3.GetComponent<Text>(); auxText.text = strEndGameText3;

		showPanels.HideGameplayPanel();
		showPanels.ShowEndGamePanel(endType);

	}



}
