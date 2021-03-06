using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenController : MonoBehaviour {
    public enum TokenType {
        GREEN
        , BLUE
        , RED
        , YELLOW
        , STATE_MAX
    }

    public Transform TokenParents_RED;
    public Transform TokenParents_GREEN;
    public Transform TokenParents_BLUE;
    public Transform TokenParents_YELLOW;
    public static TokenController instance;
    public bool DiceValueUsed = true;

    private TokenController() {
        //singleTon
    }

    public static TokenController GetInstance() {
        return instance;
    }

    void Start () {

        if (instance)
            Destroy(gameObject);
        else
            instance = this;

       // GameController.GetInstance().GetCurrentPlayerTokeType();
	}
	
    public void PlayStopTokenJumpAnimation(bool _State)  {
        Transform _ParentTranform = GetCurrentTokenParent(GameController.GetInstance().GetCurrentPlayerTokeType());
        Transform[] _tokens = _ParentTranform.GetComponentsInChildren<Transform>();
        foreach(var item in _tokens) {
            if (item.GetComponent<Token>() 
                && item.GetComponent<Animator>()) {
                if (_State && !item.GetComponent<Token>().isTokenActive())  {
                    item.GetComponent<Animator>().Play("TokenJump");
                } else  {
                    item.GetComponent<Animator>().Play("Default");
                }
            }
        }
    }

    public Transform GetCurrentTokenParent(TokenType _type) {
        Transform _ParentTranform=null ;
        switch (_type) {
            case TokenType.RED: {
                    _ParentTranform = TokenParents_RED;
                } break;
            case TokenType.GREEN: {
                    _ParentTranform = TokenParents_GREEN;
                }break;
            case TokenType.BLUE: {
                    _ParentTranform = TokenParents_BLUE;
                } break;
            case TokenType.YELLOW: {
                    _ParentTranform = TokenParents_YELLOW;
                } break;
            default: {
                    Debug.LogError("Unexpected type");
                } break;
        }
        return _ParentTranform;
    }

    //Not used need to remove//
    public void StackToken() {
        Transform _ParentTranform = GetCurrentTokenParent(GameController.GetInstance().GetCurrentPlayerTokeType());
        Transform[] _tokens = _ParentTranform.GetComponentsInChildren<Transform>();
        foreach (var item in _tokens)  {
            if (item.GetComponent<Token>()
                && !item.GetComponent<Token>().isTokenActive())  {
                var _cell = item.GetComponent<Token>().tokenCurrentCell;
                //if (_cell.hasToken) { }

            }
        }
    }
}
