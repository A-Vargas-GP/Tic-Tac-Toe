using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertPlacement : MonoBehaviour
{
    //Prefabs of the X and O
    public GameObject spriteX;
    public GameObject spriteO;

    //Variable for rotating X and O placement
    private static bool placeX;

    //Variable to count how many clicks to stop the game
    public static int totalClicks = 0;

    //Pretty much what it says...STOP THE CLICK
    private bool stopTheClick = false;

    //Stop game once winner gets solved
    // Start is called before the first frame update
    void Start()
    {
        placeX = true;
    }

    //Mouse clicking event detecting a collision with a game object
    void OnMouseDown()
    {
        //Gets the positioning of the obj of which this script is attached to
        //so the prefab can be placed at that spot
        Vector3 colliderPos = this.transform.position;

        //If the current game object that this script is attached to has an open spot and the
        //game has not ended, then place the "Sprites" (or Prefabs) 
        if (this.gameObject.GetComponent<XandO_Obj>().spotTaken == false && stopTheClick == false)
        {
            if (placeX)
            {
                GameObject currSpritePlace = Instantiate(spriteX, colliderPos, Quaternion.identity);
                placeX = false;
            }
            else
            {
                GameObject currSpritePlace = Instantiate(spriteO, colliderPos, Quaternion.identity);
                placeX = true;
            }

            //This is just used so that the boxes can trigger the collision action
            Vector3 newPos = this.transform.position;
            newPos.x+=0.1f;
            this.transform.position = newPos;

            //For each click, it increments the counter by 1
            totalClicks++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StopGame();
    }

    //Grabs the game object's InsertPlacement script and disables it
    //if it detects that the game has ended or the total clicks has gone beyond 9
    void StopGame()
    {
        if (totalClicks >= 9 || GameObject.Find("checkWinner").GetComponent<winnerChecking>().gameEnd == true)
        {
            stopTheClick = true;
        }
    }
}
