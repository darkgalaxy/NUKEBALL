using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
    public GameObject PanelEndGame;                           //Store a reference to the Game Object EndGamePanel 
    public GameObject PanelGameplay;							//Store a reference to the Game Object GameplayPanel 
    public GameObject PanelGameplay2;							//Store a reference to the Game Object GameplayPanel 2
    public GameObject PanelCredits;
	public GameObject PanelIntro;
	public GameObject PanelInfo;

    public GameObject ButtonGameOver;
    public GameObject ButtonYouWin;
    public GameObject TextGameOver;
    public GameObject TextYouWin;

    public GameObject PowerBar;

	public AudioClip soundGo,soundBack;
    
    //Call this function to activate and display the Options panel during the main menu
    public void ShowOptionsPanel()
	{
		PlaySFX (soundGo);
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		PlaySFX (soundBack);
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}

	public void ShowIntro()
	{
		PanelIntro.SetActive (true);
	}

	public void HideIntro()
	{
		PlaySFX (soundBack);
		PanelIntro.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}

	public void ShowPanelInfo()
	{

		PanelInfo.SetActive (true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePanelInfo()
	{
		PanelInfo.SetActive (false);
		optionsTint.SetActive(false);

	}


    //Call this function to activate and display the Pause panel during game play
    public void ShowCreditsPanel()
    {
		PlaySFX (soundGo);
        PanelCredits.SetActive(true);
    }

    //Call this function to deactivate and hide the Pause panel during game play
    public void HideCreditsPanel()
    {
		PlaySFX (soundBack);
        PanelCredits.SetActive(false);

    }

    public void ShowGameplayPanel()
    {
		
        //PanelGameplay.GetComponent<RectTranform>.
        PanelGameplay.SetActive(true);
        PanelGameplay2.SetActive(true);
        PowerBar.SetActive(true);

    }

    public void HideGameplayPanel()
    {
		
        PanelGameplay.SetActive(false);
        PanelGameplay2.SetActive(false);
        PowerBar.SetActive(false);
    }

    public void ShowEndGamePanel(string endType)
    {   switch (endType) {
            case "Planet Dead":
                {   
                    ButtonGameOver.SetActive (true);
                    //TextGameOver.SetActive(true);
                    ButtonYouWin.SetActive(false);
                    //TextYouWin.SetActive(false);
                }
                break;
            case "Win":
                {
                    ButtonGameOver.SetActive(false);
                    //TextGameOver.SetActive(false);
                    ButtonYouWin.SetActive(true);
                    //TextYouWin.SetActive(true);

                }
                break;
            case "Lose":
                {
                    ButtonGameOver.SetActive(true);
                    //TextGameOver.SetActive(true);
                    ButtonYouWin.SetActive(false);
                    //TextYouWin.SetActive(false);

                }
                break;     
        }
        PanelEndGame.SetActive(true);
    }
    public void HideEndGamePanel()
    {
        PanelEndGame.SetActive(false);
    }



	void PlaySFX(AudioClip sound) {

		AudioSource sourceSFX = gameObject.AddComponent<AudioSource> ();
		sourceSFX.clip = sound;
		sourceSFX.playOnAwake = false;
		sourceSFX.loop = false;
		sourceSFX.Play ();
	}


}
