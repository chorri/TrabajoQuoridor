using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerOffer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WallPlacer")
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<WallPlacer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
