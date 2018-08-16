using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {
    //set from the editor
    public TokenController.TokenType currentTokenType;
    float speed = 10f;
    public bool TokenActiveState;
    public Cells currentCell;
    private bool TokedMoved;
    int trigger = 0;
    // Use this for initialization
    void Start () {

        TokedMoved = false;
        currentCell = null;
        TokenActiveState = false;
        GetComponent<Animator>().enabled = true;
	}
	
    public void SetTokenState(bool _status) {
        TokenActiveState = _status;
    }

    public bool isTokenActive()
    {
        return TokenActiveState;
    }

    float jumpCoolingTime = 0.0f;
    private void Update()
    {
        if ( !isTokenActive())
            return;

        if (currentCell == null)
        {
            currentCell = CellController.GetInstance().GetNextCell(currentTokenType, currentCell);
            TokedMoved = true;
            trigger = 0;
            jumpCoolingTime = 0.0f;
            Debug.Log("Jumped");
            Debug.Log("Token Reached");
            TokenController.GetInstance().DiceValueUsed = true;

        }

        var diffVec = gameObject.transform.position - currentCell.gameObject.transform.position;
        if (currentCell != null)
        {
            jumpCoolingTime -= Time.deltaTime;
            if (diffVec.magnitude > 0.2f)
            {
                if (jumpCoolingTime <= 0.0f)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currentCell.transform.position, Time.deltaTime * speed);
                }
                return;
            } else
            {
                gameObject.transform.position = currentCell.gameObject.transform.position;
            }
        }

        if (!TokedMoved && trigger<GameController.GetInstance().currentDiceValue) 
        {
            currentCell = CellController.GetInstance().GetNextCell(currentTokenType, currentCell);
            trigger++;
            TokenController.GetInstance().DiceValueUsed = true;
            jumpCoolingTime = 0.2f;
            Debug.Log("Jumped");
        }
        else if (!TokedMoved)
        {
            trigger = 0;
            TokedMoved = true;
            // TokenController.GetInstance().DiceValueUsed = true;
            Debug.Log("Token Reached");
        }


    }
    public Transform GetCurrentTokensParent()
    {
        var _ParenTransform = TokenController.GetInstance().GetCurrentTokenParent(currentTokenType);
        return _ParenTransform;
    }
    public TokenController.TokenType GetCurrentTokenType()
    {
        return currentTokenType;
    }

    public void  TokenOnClick() {
        if(!isTokenActive() && GameController.GetInstance().currentDiceValue != 6)
        {
            return;
        }
        if(TokenController.GetInstance().DiceValueUsed)
        {
            return;
        }

        SetTokenState(true);
        GetComponent<Animator>().enabled = false;
        TokenController.GetInstance().PlayStopTokenJumpAnimation(false);
        TokedMoved = false;

    }
}
