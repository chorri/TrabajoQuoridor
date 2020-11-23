using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public List<WallPlacer> vPlacers;
    public List<WallPlacer> hPlacers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //[vPlacers][hPlacers][TilePos][Action]
    public string[][][][] wallPlaces;
}
