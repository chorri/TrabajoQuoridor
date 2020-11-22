using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Visual & Control

public class WallPlacer : MonoBehaviour
{

    public void PlaceWall()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }


    //Unity Methods

    private void OnMouseOver() {        
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            PlaceWall();
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
