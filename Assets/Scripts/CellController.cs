using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour {
    public static CellController instance;

    public Cells RedInitialCell;
    public Cells GreenInitialCell;
    public Cells BlueInitialCell;
    public Cells YellowInitialCell;

    public Cells RedGlobeCell;
    public Cells GreenGlobeCell;
    public Cells BlueGlobeCell;
    public Cells YellowGlobeCell;

    public static CellController GetInstance()
    {
        return instance;
    }
    private CellController() { 
        //Singleton
    }



	// Use this for initialization
	void Start () {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
	}
	
    public Cells GetNextCell(TokenController.TokenType _currentToken,Cells _currentCell)
    {
        Cells _nextCell=null;
        if(_currentCell==null)
        {
             _nextCell = GetCell(_currentToken, true);
        }
        else
        {
            if (_currentCell.Next == null&& _currentCell.CellID==5)
            {
                _nextCell = GetCell(_currentCell.nextCellType,false);

            }else {
                _nextCell = _currentCell.Next;
            }

        }
        return _nextCell;

    }
    private Cells GetCell(TokenController.TokenType _TokenType, bool globeCell)
    {
        Cells _cell =null;
        switch(_TokenType)
        {
            case TokenController.TokenType.RED:
                {
                    _cell = globeCell ? RedGlobeCell : RedInitialCell;
                } break;
            case TokenController.TokenType.GREEN:
                {
                    _cell = globeCell ? GreenGlobeCell : GreenInitialCell;
                } break;
            case TokenController.TokenType.BLUE:
                {
                    _cell = globeCell ? BlueGlobeCell : BlueInitialCell;
                } break;
            case TokenController.TokenType.YELLOW:
                {
                    _cell = globeCell ? YellowGlobeCell : YellowInitialCell;
                } break;
        }
        return _cell;
    }

    
}
