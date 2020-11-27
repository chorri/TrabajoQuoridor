using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerOffer : MonoBehaviour
{

    public List<WallPlacer> placers;

    float timeStart;
    bool once = false;

    private void Start()
    {
        timeStart = Time.time;
    }

    private void Update()
    {
        if (Time.time-timeStart >= 1.5f && !once)
        {
            once = true;
            gameObject.SetActive(false);
        }
    }

    public void Offer()
    {
        foreach (WallPlacer other in placers)
        {
            other.gameObject.GetComponent<WallPlacer>().TurnOn();
        }
    }

    public void OnOffer()
    {
        foreach (WallPlacer other in placers)
        {
            other.gameObject.GetComponent<WallPlacer>().TurnOff();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WallPlacer")
        {
            if (!placers.Contains(other.gameObject.GetComponent<WallPlacer>()))
            {
                placers.Add(other.gameObject.GetComponent<WallPlacer>());
            }
        }
    }
}
