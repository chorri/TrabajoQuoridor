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

    public void SimulatePlaceRemove(GameObject gO)
    {
        string temp = gO.name;
        if (temp[0] == 'V')
        {
            temp = temp.Remove(0, 1);
            int n = int.Parse(temp);
            vPlacers[n].SimulateRemoveWall();
        }
        else
        {
            temp = temp.Remove(0, 1);
            int n = int.Parse(temp);
            hPlacers[n].SimulateRemoveWall();
        }
    }


    public Nodo ReturnEquivalentNode(Nodo obj)
    {
        string temp = obj.gameObject.name;
        temp = temp.Remove(0, 4);

        int n = int.Parse(temp);

        return simTiles[n];
    }

    public WallPlacer CalculateBestWall(Nodo current, Nodo fin,CaminoCompleto shortPath)
    {
        Transform MainPlacersGroup = GameObject.Find("Tablero").transform.GetChild(1);
        WallPlacer best = null;

        float dist = Mathf.Infinity;

        foreach (WallPlacer item in vPlacers)
        {
            //Debug.Log(item.name+"-"+item.state);
            if (item.state)
            {
                float newDist = Vector3.Distance(item.transform.position, current.transform.position);
                if (dist >= newDist)
                {
                    dist = newDist;
                    best = item;
                }
  //              Debug.Log("Entered bc of state");
  //              item.PlaceWall();

  //              if (Pathfinding.instance.AStar(current,fin).caminoNodo.Count > shortPath.caminoNodo.Count)
  //              {
  //                  Debug.Log("Found Best Vertical Placer");
  //                  best = MainPlacersGroup.GetChild(0).GetChild(int.Parse(best.gameObject.name.Remove(0))).GetComponent<WallPlacer>();
  //                  return best;
  //              }
  //              item.SimulateRemoveWall();
            }
        }

        foreach (WallPlacer item in hPlacers)
        {
            if (item.state)
            {
                float newDist = Vector3.Distance(item.transform.position, current.transform.position);
                if (dist >= newDist)
                {
                    dist = newDist;
                    best = item;
                }

     //           item.PlaceWall();

     //           CaminoCompleto tempH = Pathfinding.instance.AStar(current, fin);
     //           Debug.Log("Horizontal Placer->"+tempH.caminoNodo.Count + " | " + shortPath.caminoNodo.Count);
     //           if (tempH.caminoNodo.Count > shortPath.caminoNodo.Count)
     //           {
     //               Debug.Log("Found Best Horizontal Placer");
     //               best = MainPlacersGroup.GetChild(1).GetChild(int.Parse(best.gameObject.name.Remove(0))).GetComponent<WallPlacer>();
     //               return best;
     //           }
     //           item.SimulateRemoveWall();
            }
        }

        if (best != null)
        {
            string temp = best.name;
            temp = temp.Remove(0, 1);
            int n = int.Parse(temp);




            if (best.name[0] == 'V')
            {
                best = MainPlacersGroup.GetChild(0).GetChild(n).GetComponent<WallPlacer>();
            } else
            {
                best = MainPlacersGroup.GetChild(1).GetChild(n).GetComponent<WallPlacer>();
            }
        }

        return best;
    }
}
