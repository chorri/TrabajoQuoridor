using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public List<Nodo> simTiles = new List<Nodo>();
    public List<WallPlacer> vPlacers = new List<WallPlacer>();
    public List<WallPlacer> hPlacers = new List<WallPlacer>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform tablero = GameObject.Find("Simulacion").transform;
        for (int i = 0; i < tablero.GetChild(0).childCount; i++)
        {
            simTiles.Add(tablero.GetChild(0).GetChild(i).GetComponent<Nodo>());
        }
        //Vertical
        for (int i = 0; i < tablero.GetChild(1).GetChild(0).childCount; i++)
        {
            vPlacers.Add(tablero.GetChild(1).GetChild(0).GetChild(i).GetComponent<WallPlacer>());
        }
        //Horizontal
        for (int i = 0; i < tablero.GetChild(1).GetChild(1).childCount; i++)
        {
            hPlacers.Add(tablero.GetChild(1).GetChild(1).GetChild(i).GetComponent<WallPlacer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SimulatePlacer(GameObject gO)
    {
        string temp = gO.name;
        if (temp[0] == 'V')
        {
            temp = temp.Remove(0, 1);
            int n = int.Parse(temp);
            vPlacers[n].PlaceWall();
        } else
        {
            temp = temp.Remove(0, 1);
            int n = int.Parse(temp);
            hPlacers[n].PlaceWall();
        }
    }
}
