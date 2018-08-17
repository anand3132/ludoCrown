using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class contain token properties 
public class Token : MonoBehaviour {

    //set from the editor
    public TokenController.TokenType tokenCurrentType;
    public bool tokenActiveState;
    private bool tokedMoved;

    public Cells tokenCurrentCell;
    private float tokenMoveSpeed = 10f;
    private int diceCounter = 0;
    private float jumpCoolingTime = 0.0f;
    private Vector2 tokenInitialSize;
    private bool tokenStacked;

    void Start () {

        tokedMoved = false;
        tokenCurrentCell = null;
        tokenActiveState = false;
        tokenStacked = false;
        tokenInitialSize = GetComponent<SpriteRenderer>().size;
        GetComponent<Animator>().enabled = true;
	}
	
    public void SetTokenState(bool _status) {
        tokenActiveState = _status;
    }

    public bool isTokenActive() {
        return tokenActiveState;
    }

    private void Update()
    {
        if ( !isTokenActive())
            return;

        if (tokenCurrentCell == null) {
            tokenCurrentCell = CellController.GetInstance().GetNextCell(tokenCurrentType, tokenCurrentCell);
            tokedMoved = true;
            diceCounter = 0;
            jumpCoolingTime = 0.0f;
            TokenController.GetInstance().DiceValueUsed = true;
            tokenCurrentCell.CelltokenList.Add(this);
 
            UIController.GetInstance().Arrow.SetActive(true);
            UIController.GetInstance().Arrow.GetComponent<Animator>().Play("Arrow");
            StackToken();
        }

        var diffVec = gameObject.transform.position - tokenCurrentCell.gameObject.transform.position;
        if (tokenCurrentCell != null) {
            jumpCoolingTime -= Time.deltaTime;
            if (diffVec.magnitude > 0.2f) {
                if (jumpCoolingTime <= 0.0f) {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, tokenCurrentCell.transform.position, Time.deltaTime * tokenMoveSpeed);
                }
                return;
            } else {
                gameObject.transform.position = tokenCurrentCell.gameObject.transform.position;
            }
        }

        if (!tokedMoved && diceCounter<GameController.GetInstance().currentDiceValue) {
            tokenCurrentCell.CelltokenList.Remove(this);
            tokenCurrentCell = CellController.GetInstance().GetNextCell(tokenCurrentType, tokenCurrentCell);
            tokenCurrentCell.CelltokenList.Add(this);
            diceCounter++;
            TokenController.GetInstance().DiceValueUsed = true;
            jumpCoolingTime = 0.2f;
            Debug.Log("Jumped");
        } else if (!tokedMoved) {
            diceCounter = 0;
            tokedMoved = true;
            // TokenController.GetInstance().DiceValueUsed = true;
            Debug.Log("Token Reached");
            StackToken();
            UIController.GetInstance().Arrow.SetActive(true);
            UIController.GetInstance().Arrow.GetComponent<Animator>().Play("Arrow");
        }
    }

    public Transform GetCurrentTokensParent() {
        var _ParenTransform = TokenController.GetInstance().GetCurrentTokenParent(tokenCurrentType);
        return _ParenTransform;
    }

    public TokenController.TokenType GetCurrentTokenType() {
        return tokenCurrentType;
    }

    public void TokenOnClick() {
        if (!isTokenActive() && GameController.GetInstance().currentDiceValue != Dice.DiceMaxValue)  
            return;
        if (TokenController.GetInstance().DiceValueUsed) 
            return;
        gameObject.GetComponent<SpriteRenderer>().size = tokenInitialSize;
        tokenStacked = false;
        SetTokenState(true);
        TokenController.GetInstance().PlayStopTokenJumpAnimation(false);
        tokedMoved = false;
    }
    private void StackToken()
    {
        if(tokenCurrentCell.CelltokenList.Count > 1)
        {
            foreach(var item in tokenCurrentCell.CelltokenList)
            {
                float radius = item.GetComponent<SpriteRenderer>().bounds.extents.magnitude;
                Debug.Log("Radius : " + radius);
                Vector3 _stackPosition = new Vector3(0, radius, 0);
                if(!tokenStacked) {
                    item.GetComponent<SpriteRenderer>().size *= .75f;
                    tokenStacked = true;
                }
            }
        }
    }

}
