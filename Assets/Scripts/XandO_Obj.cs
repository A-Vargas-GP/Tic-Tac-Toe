using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XandO_Obj : MonoBehaviour
{
    //Variables meant to detect if the current obj has either X or O
    public bool valX;
    public bool valO;

    //Variable meant to detect if the current obj is occupied by an X or an O
    public bool spotTaken;


    // Start is called before the first frame update
    void Start()
    {
        valX = false;
        valO = false;
        spotTaken = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Detects if the X or O has been placed
    //If either X or O has been placed, it makes it so that another object cannot
    //be placed on top of it
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("X"))
        {
            valX = true;
            spotTaken = true;
        }
        else
        {
            valO = true;
            spotTaken = true;
        }
    }
}
