using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class controls diffrent UI Behaviors in the game
public class UIController : MonoBehaviour {
    public GameObject MainGameScene;
    public GameObject WelcomeScrenUI;
    public GameObject HUD;
    public GameObject Dice;
    public static UIController instance;
    public Animator UIAnimator;
    public GameObject Arrow;
    private UIController() {
        //Singleton
    }

    public static UIController GetInstance() {
        return instance;
    }

    void Start() {

        if (instance)  {
            Destroy(gameObject);
            Debug.LogError("Already initialised");
        } else {
            instance = this;
        }

        MainGameScene.SetActive(false);
        WelcomeScrenUI.SetActive(true);
    }

    public void PlayButtonOnClick() {
         UIAnimator.Play("welcomeScreen");
    }

    public void DiceButtonOnClick() {
        Arrow.SetActive(true);
        Arrow.GetComponent<Animator>().Play("Default");
        Arrow.SetActive(false);
        Dice.GetComponent<Dice>().DiceThrown();
        //  diceButton.interactable = false;
    }
}
