using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Visual & Control

public class WallPlacer : MonoBehaviour
{
    TurnManager tM;
    bool state = true;

    public bool GetState()
    {
        return state;
    }

    private void Start()
    {
        tM = TurnManager.instance;
    }

    public void PlaceWall()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        //gameObject.GetComponent<WallPlacer>().enabled = false;
        state = false;
    }

    //Unity Methods

    private void OnMouseOver() {
        if (tM.players[tM.currentPlayer].playerControlled && tM.players[tM.currentPlayer].currentState == EstadoIA.Act)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlaceWall();
                tM.players[tM.currentPlayer].ChangePlayerState(EstadoIA.Check);
            }
        }
        
    }

    private void OnMouseEnter()
    {
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
    }
}
