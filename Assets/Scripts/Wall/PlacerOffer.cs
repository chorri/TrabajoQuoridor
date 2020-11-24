using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerOffer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WallPlacer")
        {
            other.gameObject.GetComponent<WallPlacer>().TurnOff();
        }
    }
}
