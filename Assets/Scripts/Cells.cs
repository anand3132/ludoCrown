using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    public int CellID;
    public bool Globe;
    public bool JumpPower;
    public bool StarPower;
    public Cells Next;

    public bool hasToken;

    public List<Token> Celltoken;

    [Space]
    public TokenController.TokenType nextCellType;
    public TokenController.TokenType currentCellType;



    // Use this for initialization
    void Start()
    {
        CellID = GetComponent<Transform>().GetSiblingIndex();
        hasToken = false;
        Celltoken = new List<Token>();

    }

    public Cells GetNextCell(TokenController.TokenType _Token)
    {
        if (_Token == TokenController.TokenType.RED)
        {
          //  return nextCell;
        }
        
        return null;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
