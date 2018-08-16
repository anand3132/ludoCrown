using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public GameObject MainGameScene;
    public GameObject WelcomeScrenUI;
    public GameObject HUD;
    public GameObject Dice;
    public Button diceButton;
    public static UIController instance;

    private UIController() {
        //Singleton
    }

    public static UIController GetInstance()
    {
        return instance;
    }

    void Start () {
        if (instance){
            Destroy(gameObject);
            Debug.LogError("Already initialised");
        } else {
            instance = this;
        }
        ResetMenue();
        MainGameScene.SetActive(false);
        WelcomeScrenUI.SetActive(true);
  

    }
	
    public void PlayButtonOnClick() {
        if(MainGameScene)
        {
            ResetMenue();
            MainGameScene.gameObject.SetActive(true);
            HUD.SetActive(true);
        }
        else
        {
            Debug.LogError("Main Game Scene No Found!!");
        }
    }

    void ResetMenue()
    {
        WelcomeScrenUI.SetActive(false);
        HUD.SetActive(false);
    }

    public void DiceButtonOnClick() {
        Dice.GetComponent<Dice>().DiceThrown();
      //  diceButton.interactable = false;
    }


}
