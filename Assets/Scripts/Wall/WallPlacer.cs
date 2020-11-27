using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Visual & Control

public class WallPlacer : MonoBehaviour
{
    TurnManager tM;
    MazeManager mM;
    public bool state = true;

    public bool GetState()
    {
        return state;
    }

    private void Start()
    {
        tM = TurnManager.instance;
        mM = MazeManager.instance;
    }

    public void PlaceWall()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<WallEntity>().OnWall();
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<WallEntity>().OnWall();
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(2).GetComponent<PlacerOffer>().OnOffer();
    }

    public void RemoveWall()
    {
        transform.GetChild(0).GetComponent<WallEntity>().OffWall();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).GetComponent<WallEntity>().OffWall();
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).GetComponent<PlacerOffer>().Offer();
        transform.GetChild(2).gameObject.SetActive(false);
        TileManager.instance.SimulatePlaceRemove(this.gameObject);
    }

    public void SimulateRemoveWall()
    {
        transform.GetChild(0).GetComponent<WallEntity>().OffWall();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).GetComponent<WallEntity>().OffWall();
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).GetComponent<PlacerOffer>().Offer();
        transform.GetChild(2).gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        state = true;
        mM.UpdatePlacers(this.gameObject);
    }

    public void TurnOff()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        //gameObject.GetComponent<WallPlacer>().enabled = false;
        state = false;
        mM.UpdatePlacers(this.gameObject);
    }

    //Unity Methods

    private void OnMouseOver() {
        if (tM.GetCurrentPlayer().playerControlled && tM.GetCurrentPlayer().currentState == EstadoIA.Act)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlaceWall();
                TileManager.instance.SimulatePlacer(this.gameObject);
                tM.GetCurrentPlayer().ChangePlayerState(EstadoIA.Check);
                
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
