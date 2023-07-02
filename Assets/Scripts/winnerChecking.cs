using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class winnerChecking : MonoBehaviour
{
    //Text declaring winner or stalemate
    public TextMeshProUGUI txt;
    public TextMeshProUGUI arrayInside;

    //List of GameObjects that can be added from the Inspector of 'checkWinner' obj
    public List<GameObject> totalObjs;

    //Array that will hold GameObjects
    private GameObject[,] GameArr = new GameObject[3,3];

    //Nested array of a 3x3 for figuring out solution of tic-tac-toe
    //Auto-sets to 0 (nothing placed)
    //If X is placed --> 1
    //If O is placed --> 2
    public int[,] TicTacToeArr = new int[3,3];

    //variable for game ending
    public bool gameEnd = false;

    //variables for detecting if someone has won
    //column
    bool endMatchCX = false;
    bool endMatchCO = false;

    //row
    bool endMatchRX = false;
    bool endMatchRO = false;

    //diagonal1
    bool endMatchDX1 = false;
    bool endMatchDO1 = false;

    //diagonal2
    bool endMatchDX2 = false;
    bool endMatchDO2 = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InsertGameObjectsInArray();
        changeValuesInArray();
        checkForWinner();
        changeTextNone();
        //checkArray();
    }

    //Taking the game objects from the List and inserting them into the 3x3 multi-dimensional array
    /*Ex:  index:   0,1,2,3,4,5,6,7,8
          List --> {1,2,3,4,5,6,7,8,9}
          Array --> [[1,2,3], [4,5,6], [7,8,9]]
                index:  0        1        2

        Another way of seeing the multi-dimensional array:
    index  0     1    2
       0   [1    2    3]
       1   [4    5    6]
       2   [7    8    9]
    */
    void InsertGameObjectsInArray()
    {
        for (int i = 0; i < GameArr.GetLength(0); i++)
        {
            for (int j = 0; j < GameArr.GetLength(1); j++)
            {
                if (i == 0)
                {
                    GameArr[i, j] = totalObjs[j];
                }
                else if (i == 1)
                {
                    GameArr[i, j] = totalObjs[j+3];
                }
                else
                {
                    GameArr[i, j] = totalObjs[j+6];
                }
            }
        }
    }

    //Since the 3x3 array of game objects is the same size as the tic-tac-toe values
    //it will use the tic-tac-toe's size (length and width) and use the same indexes
    //when grabbing values
    void changeValuesInArray()
    {
        for (int i = 0; i < TicTacToeArr.GetLength(0); i++)
        {
            for (int j = 0; j < TicTacToeArr.GetLength(1); j++)
            {
                //If the Game Object's X value is seen as true, it uses the index
                //and changes the element's value to 1 using the same index in the tic-tac-toe value array
                if (GameArr[i,j].GetComponent<XandO_Obj>().valX)
                {
                    TicTacToeArr[i, j] = 1;
                }

                //If the Game Object's O value is seen as true, it uses the index
                //and changes the element's value to 2 using the same index in the tic-tac-toe value array
                if (GameArr[i,j].GetComponent<XandO_Obj>().valO)
                {
                    TicTacToeArr[i, j] = 2;
                }
            }
        }
    }

    //Iterates through the tic-tac-toe array
    //Will be checking the values of either a 1 (meaning X) and 2 (meaning O)
    //In conclusion: checks the columns, rows, and both diagonals
    void checkForWinner()
    {
        for (int i = 0; i < TicTacToeArr.GetLength(0); i++)
        {
            for (int j = 0; j < TicTacToeArr.GetLength(1); j++)
            {                                
                if (TicTacToeArr[i, j] == 1)
                {
                    checkCol(TicTacToeArr, i, j);
                    checkRow(TicTacToeArr, i, j);
                    checkDiagonal1(TicTacToeArr, i, j);
                    checkDiagonal2(TicTacToeArr, i, j);
                }
                if (TicTacToeArr[i,j] == 2)
                {
                    checkCol(TicTacToeArr, i, j);
                    checkRow(TicTacToeArr, i, j);
                    checkDiagonal1(TicTacToeArr, i, j);
                    checkDiagonal2(TicTacToeArr, i, j);
                }
            }
        }
    }

    //0 is nothing
    //1 is X
    //2 is O

    //Checks the columns and grabs the row/col in which either 1 or 2 was found
    void checkCol(int[,] arrVal, int row, int col)
    {
        //Counter variables to find out if there has been 3 X or 3 O
        int findCX = 0;
        int findCO = 0;
        
        //Bools to detect if the X or O has been found based on placement
        bool findMatchColX = false;
        bool findMatchColO = false;

        /*
            Another way of seeing the multi-dimensional array:
          index  0     1    2
            0   [1    2    3]
            1   [4    5    6]
            2   [7    8    9]

            By checking the columns, you want to grab the current position and go backwards then forwards
            through the use of -1, 0, 1

            Ex: at (1,1), to go up the column: (0,1) and (2,1) --> i.e. the first num will be affected
                thus, the row number is affected
        */      
        for (int k = -1; k < 2; k++)
        {
            //Checking that the current position checking will be within the bounds
            if (row+k >= 0 && row+k < 3)
            {
                //Checking the X
                if (arrVal[row+k, col] == 1)
                {
                    findMatchColX = true;
                    findCX++;
                }
                else
                {
                    findMatchColX = false;
                }

                //Checking the O
                if (arrVal[row+k, col] == 2)
                {
                    findMatchColO = true;
                    findCO++;
                }
                else
                {
                    findMatchColO = false;
                }
            }
        }
        
        //if the total X is 3 and the end X is true, then the ending match value will be set to true
        if (findCX == 3 && findMatchColX == true)
        {   
            endMatchCX = findMatchColX;
        }

        //if the total O is 3 and the end O is true, then the ending match value will be set to true
        if (findCO == 3 && findMatchColO == true)
        {        
            endMatchCO = findMatchColO;
        }

        //Used to change the text based on who has won the match first
        changeTextWinnerX(findCX, endMatchCX);
        changeTextWinnerO(findCO, endMatchCO);
    }

    //0 is nothing
    //1 is X
    //2 is O

    //Checks the rows and grabs the row/col in which either 1 or 2 was found
    void checkRow(int[,] arrVal, int row, int col)
    {
        //Counter variables to find out if there has been 3 X or 3 O
        int findX = 0;
        int findO = 0;
        
        //Bools to detect if the X or O has been found based on placement
        bool findMatchRowX = false;
        bool findMatchRowO = false;

        /*
            Another way of seeing the multi-dimensional array:
      index  0     1    2
        0   [1    2    3]
        1   [4    5    6]
        2   [7    8    9]

        By checking the rows, you want to grab the current position and go backwards then forwards
        through the use of -1, 0, 1

        Ex: at (1,1), to go side to side of the row: (1,0) and (1,2) --> i.e. the second num will be affected
            thus, the column number is affected
        */  
        for (int k = -1; k < 2; k++)
        {
            //Checking that the current position checking will be within the bounds
            if (col+k >= 0 && col+k < 3)
            {
                //Checking the X
                if (arrVal[row, col+k] == 1)
                {
                    findMatchRowX = true;
                    findX++;
                }
                else
                {
                    findMatchRowX = false;
                }

                //Checking the O
                if (arrVal[row, col+k] == 2)
                {
                    findMatchRowO = true;
                    findO++;
                }
                else
                {
                    findMatchRowO = false;
                }
            }
        }

        //if the total X is 3 and the end X is true, then the ending match value will be set to true
        if (findX == 3 && findMatchRowX == true)
        {        
            endMatchRX = findMatchRowX;
        }

        //if the total O is 3 and the end O is true, then the ending match value will be set to true
        if (findO == 3 && findMatchRowO == true)
        {        
            endMatchRO = findMatchRowO;
        }

        //Used to change the text based on who has won the match first
        changeTextWinnerX(findX, endMatchRX);
        changeTextWinnerO(findO, endMatchRO);
    }

    void checkDiagonal1(int[,] arrVal, int row, int col)
    {
        //Counter variables to find out if there has been 3 X or 3 O
        int findX = 0;
        int findO = 0;
        
        //Bools to detect if the X or O has been found based on placement
        bool findMatchDiag1X = false;
        bool findMatchDiag1O = false;

        /*
            Another way of seeing the multi-dimensional array:
     index  0     1    2
        0   [1    2    3]
        1   [4    5    6]
        2   [7    8    9]

        By checking the diagonal, you want to grab the current position and go up/left and down/right
        through the use of -1, 0, 1

        Ex: at (1,1), to go up/left and down/right of the row and column: (0,0) and (2,2) --> i.e. both num will be affected
            thus, the row/column number is affected
        */  

        for (int j = -1; j < 2; j++)
        {
            if (row+j >= 0 && row+j < 3 && col+j>=0 && col+j < 3)
            {
                //Checking the X
                if (arrVal[row+j, col+j] == 1)
                {
                    findMatchDiag1X = true;
                    findX++;
                }
                else
                {
                    findMatchDiag1X = false;
                }

                //Checking the O
                if (arrVal[row+j, col+j] == 2)
                {
                    findMatchDiag1O = true;
                    findO++;
                }
                else
                {
                    findMatchDiag1O = false;
                }
            }
        }

        //if the total X is 3 and the end X is true, then the ending match value will be set to true
        if (findX == 3 && findMatchDiag1X == true)
        {        
            endMatchDX1 = findMatchDiag1X;
        }

        //if the total O is 3 and the end O is true, then the ending match value will be set to true
        if (findO == 3 && findMatchDiag1O == true)
        {        
            endMatchDO1 = findMatchDiag1O;
        }

        //Used to change the text based on who has won the match first
        changeTextWinnerX(findX, endMatchDX1);
        changeTextWinnerO(findO, endMatchDO1);
    }

    void checkDiagonal2(int[,] arrVal, int row, int col)
    {
        //Counter variables to find out if there has been 3 X or 3 O
        int findX = 0;
        int findO = 0;
        
        //Bools to detect if the X or O has been found based on placement
        bool findMatchDiag2X = false;
        bool findMatchDiag2O = false;

        /*
            Another way of seeing the multi-dimensional array:
      index  0     1    2
        0   [1    2    3]
        1   [4    5    6]
        2   [7    8    9]

        By checking the diagonal, you want to grab the current position and go up/right and down/left
        through the use of -1, 0, 1

        Ex: at (1,1), to go up/right and down/left of the row and column: (0,2) and (2,0) --> i.e. both num will be affected
            thus, the row/column number is affected
        */  

        for (int j = -1; j < 2; j++)
        {
            if (row+j >= 0 && row+j < 3 && col+(-1*j)>=0 && col+(-1*j) < 3)
            {
                //Checking the X
                if (arrVal[row+j, col+(-1*j)] == 1)
                {
                    findMatchDiag2X = true;
                    findX++;
                }
                else
                {
                    findMatchDiag2X = false;
                }

                //Checking the O
                if (arrVal[row+j, col+(-1*j)] == 2)
                {
                    findMatchDiag2O = true;
                    findO++;
                }
                else
                {
                    findMatchDiag2O = false;
                }
            }
        }

        //if the total X is 3 and the end X is true, then the ending match value will be set to true
        if (findX == 3 && findMatchDiag2X == true)
        {        
            endMatchDX2 = findMatchDiag2X;
        }

        //if the total O is 3 and the end O is true, then the ending match value will be set to true
        if (findO == 3 && findMatchDiag2O == true)
        {        
            endMatchDO2 = findMatchDiag2O;
        }

        //Used to change the text based on who has won the match first
        changeTextWinnerX(findX, endMatchDX2);
        changeTextWinnerO(findO, endMatchDO2);
    }

    //Debugging purposes:
    //Prints out what is currently in the array
    //Can be used for TicTacToe or GameArr
    void checkArray()
    {
        string final = "";

        for (int i = 0; i < TicTacToeArr.GetLength(0); i++)
        {
            for (int j = 0; j < TicTacToeArr.GetLength(1); j++)
            {     
                final = final + TicTacToeArr[i,j];

                //final = final + GameArr[i,j].GetComponent<XandO_Obj>().valX;
            }
        }
        arrayInside.text = final;
    }

    //If it detects that the winner is X, then the text will state that X is the winner
    void changeTextWinnerX(int totX, bool finalResult)
    {
        if (finalResult == true && totX == 3)
        {
            txt.text = "Winner X";
            gameEnd = true;
        }
    }

    //If it detects that the winner is O, then the text will state that O is the winner
    void changeTextWinnerO(int totO, bool finalResult)
    {
        if (finalResult == true && totO == 3)
        {
            txt.text = "Winner O";
            gameEnd = true;
        }
    }

    //If there is no winner, then the text will simply state "No Winner" aka stalemate
    void changeTextNone()
    {
        if (gameEnd == false)
        {
            txt.text = "No Winner";
        }
    }
}