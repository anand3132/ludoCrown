using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour {

    public static GameController instance;
    public TokenController.TokenType currentPlayer;
    public int currentDiceValue;
    public bool diceThrown = false;

    private GameController() {
        //singleton
    }

    public static GameController GetInstance() {
        return instance;
    }

	void Start () {
        if (instance) {
            Destroy(gameObject);
            Debug.LogError("Already initialised");
        } else {
            instance = this;
        }
        //change according to player profile
        currentPlayer = TokenController.TokenType.RED;
    }
    
	void Update () {
        ProcessInput();
    }

    public TokenController.TokenType GetCurrentPlayerTokeType() {
        return currentPlayer;
    }

    void ProcessInput() {
        if (Input.GetMouseButtonUp(0) || Input.touchCount > 0) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null) {
                var hitObject = hit.collider.gameObject.GetComponent<Token>();
                if (hitObject != null && currentPlayer == hitObject.tokenCurrentType) {
                    hitObject.TokenOnClick();
                } else
                {
                    Debug.Log("Hit Missed");
                }
            }

        }
    }
}
