using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cell properties
public class Cells : MonoBehaviour {

    public int CellID;
    public bool Globe;
    public bool JumpPower;
    public bool StarPower;

    public Cells Next;
    public List<Token> CelltokenList;

    public TokenController.TokenType nextCellType;
    [Space]
    public TokenController.TokenType currentCellType;

    void Start() {
        CellID = GetComponent<Transform>().GetSiblingIndex();
        CelltokenList = new List<Token>();
    }
}
