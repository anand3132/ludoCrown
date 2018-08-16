using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int GenTokenNo;
    public TokenController.TokenType currentPlayer;
    public int currentDiceValue;
    public bool diceThrown = false;
    public static GameController instance;
    private GameController() {
        //singleton
    }

    public static GameController GetInstance()
    {
        return instance;
    }
	// Use this for initialization
	void Start () {
        if (instance)
        {
            Destroy(gameObject);
            Debug.LogError("Already initialised");
        }
        else
        {
            instance = this;
        }

        //change according to player profile
        currentPlayer = TokenController.TokenType.RED;

    }

	public TokenController.TokenType GetCurrentPlayerTokeType()
    {
        return currentPlayer;
    }
    

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0) || Input.touchCount > 0 ) {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                var hitObject = hit.collider.gameObject.GetComponent<Token>();
                if (hitObject != null 
                    && currentPlayer == hitObject.currentTokenType  )
                {
                    hitObject.TokenOnClick();
                }
            }
            else
            {
                Debug.Log("No Hit ");
            }
        }
	}
}
