using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour {
    private int diceThrowNum;
    public List<Sprite> DiceSprites;
    public Transform currentTokenParent;
    public const int DiceMaxValue = 6;

    public void DiceThrown() {
        diceThrowNum = 6;// Random.Range(1, 7);
        GetComponent<Animator>().enabled = true;
        GameController.GetInstance().currentDiceValue = diceThrowNum;
    }

    public int GetCurrentDiceNum() {
        return diceThrowNum;
    }

    //called after Dice Animation
    public void PostDiceAnimationCallBack()
    {
        TokenController.GetInstance().PlayStopTokenJumpAnimation(false);
        if (diceThrowNum < 0 && !GetComponent<Image>()) return;
        gameObject.GetComponent<Animator>().enabled = false;
        TokenController.GetInstance().DiceValueUsed = false;

        currentTokenParent = TokenController.GetInstance().GetCurrentTokenParent(GameController.GetInstance().GetCurrentPlayerTokeType());
        GameController.GetInstance().diceThrown = true;
        switch (diceThrowNum)
        {
            case 1: { GetComponent<Image>().sprite = DiceSprites[0]; } break;
            case 2: { GetComponent<Image>().sprite = DiceSprites[1]; } break;
            case 3: { GetComponent<Image>().sprite = DiceSprites[2]; } break;
            case 4: { GetComponent<Image>().sprite = DiceSprites[3]; } break;
            case 5: { GetComponent<Image>().sprite = DiceSprites[4]; } break;
            case 6: {
                    TokenController.GetInstance().PlayStopTokenJumpAnimation(true);
                    GetComponent<Image>().sprite = DiceSprites[5];
                } break;
            default:
                Debug.Log("UnExpected Dice Number");
                break;
        }

    }
}
