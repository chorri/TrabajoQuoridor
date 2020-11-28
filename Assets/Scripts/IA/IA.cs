using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoIA {
    Wait,
    Act,
    Move,
    Check
}

public class IA : MonoBehaviour
{
    public bool playerControlled;
    bool acted;

    public Nodo nodoActual;
    public bool stacked = false;
    public List<Nodo> objetivos;

    public EstadoIA currentState;
    public CaminoCompleto caminoObjetivo = new CaminoCompleto();

    //Para mostrar todos los caminos
    List<CaminoCompleto> todosCaminos = new List<CaminoCompleto>();
    float timeStart;
    float maxTime = 0.5f;

    public bool showPaths;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangePlayerState(EstadoIA newState)
    {
        timeStart = Time.time;
        currentState = newState;
    }

    // Update is called once per frame
    void Update()
    {
        if (!WinObject.instance.IsThereAWinner())
        {
            if (playerControlled)
            {
                switch (currentState)
                {
                    case EstadoIA.Wait:
                        break;
                    case EstadoIA.Act:
                        foreach (Nodo nodo in nodoActual.adjacentes)
                        {
                            nodo.cuerpo.materialActive = true;
                        }
                        break;
                    case EstadoIA.Move:
                        if (Time.time - timeStart > maxTime / 2)
                        {
                            if (!stacked)
                            {
                                ChangePlayerState(EstadoIA.Check);
                            }
                            else
                            {
                                foreach (Nodo nodo in nodoActual.adjacentes)
                                {
                                    nodo.cuerpo.materialActive = true;
                                }
                            }
                        }
                        break;
                    case EstadoIA.Check:
                        CheckWin();

                        DefineCaminoObjetivo();//Jugador Tiene camino objetivo al final de su turno

                        if (Time.time - timeStart > maxTime / 2)
                        {
                            //CheckStacked
                            currentState = EstadoIA.Wait;
                            TurnManager.instance.NextPlayer();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                IAControlled();
            }
        }
    }

    void IAControlled()
    {
        switch (currentState)
        {
            case EstadoIA.Wait:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentState = EstadoIA.Act;
                    caminoObjetivo = new CaminoCompleto();
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //caminoObjetivo.ShowDirection();
                    caminoObjetivo.ShowPath(3);
                    Debug.Log(gameObject.name+"->"+caminoObjetivo.caminoNodo.Count);
                }



                break;
            case EstadoIA.Act:
                DateTime before = DateTime.Now;
                DefineCaminoObjetivo();
                DateTime after = DateTime.Now;
                TimeSpan time = after.Subtract(before);
                Debug.LogWarning("Duracion de A* en ms: " + time.Milliseconds);

                IA posWiner=this;
                foreach (IA item in TurnManager.instance.players)
                {
                    if (posWiner.caminoObjetivo.caminoNodo.Count > item.caminoObjetivo.caminoNodo.Count)
                    {
                        posWiner = item;
                    }
                }
                //Debug.LogError(gameObject.name +": "+caminoObjetivo.caminoNodo.Count + " vs " + posWiner.gameObject.name+": " +posWiner.caminoObjetivo.caminoNodo.Count);

                if (posWiner.caminoObjetivo.caminoNodo.Count > 0)
                {
                    //Debug.Log(posWiner.caminoObjetivo.caminoNodo[0].gameObject.name);

                    if (true)
                    {
                        //Calcular Muro Optimo
                        TileManager temp = TileManager.instance;
                        WallPlacer bestWall = temp.CalculateBestWall(temp.ReturnEquivalentNode(posWiner.nodoActual),
                                                                        temp.ReturnEquivalentNode(posWiner.caminoObjetivo.caminoNodo[0]),
                                                                        posWiner.caminoObjetivo);

                        Debug.Log(bestWall.name);
                        //
                        if (bestWall != null)
                        {
                            //Si el siguiente jugador es el posWinner
                            int r = UnityEngine.Random.Range(1, 101);

                            if (/*TurnManager.instance.GetNextPlayer() == posWiner ||*/ r >= 23)
                            {//Colocar Muro Optimo
                             //Guardar en Diccionario
                                Debug.Log("Place Wall");
                                bestWall.PlaceWall();
                                MazeManager.instance.UpdatePlacers(bestWall.gameObject);
                                MazeManager.instance.AddToResultDictionary(bestWall.gameObject.name);
                            }
                        }
                    }
                }
                

                timeStart = Time.time;
                currentState = EstadoIA.Move;
                break;
            case EstadoIA.Move:
                if (Time.time - timeStart > maxTime)
                {
                    transform.position = caminoObjetivo.caminoNodo[caminoObjetivo.caminoNodo.Count - 1].transform.position + Vector3.up;
                    caminoObjetivo.RemoveLast();

                    timeStart = Time.time;
                    currentState = EstadoIA.Check;
                }
                //                }

                break;
            case EstadoIA.Check:
                if (Time.time - timeStart > maxTime/2)
                {
                    //WinCheck
                    CheckWin();
                    IACheckStacked();
                    currentState = EstadoIA.Wait;
                    TurnManager.instance.NextPlayer();
                }
                break;
            default:
                break;
        }
    }

    //Greedy
    void CalculateBestWall(IA obj)
    {
        //CaminoCompleto nuevoCamino =
    }

    void DefineCaminoObjetivo()
    {
        caminoObjetivo = new CaminoCompleto();
        foreach (Nodo item in objetivos)
        {
            CaminoCompleto temp;
            temp = Pathfinding.instance.AStar(nodoActual, item);
            
            if (showPaths)
            {
                temp.ShowPath(1);
            }

            todosCaminos.Add(temp);

            if (caminoObjetivo.caminoNodo.Count == 0 || temp.caminoNodo.Count < caminoObjetivo.caminoNodo.Count)
            {
                if (temp.caminoNodo.Contains(item))
                {
                    caminoObjetivo = temp;
                }
            }
        }
    }



    void CheckWin()
    {
        foreach (Nodo item in objetivos)
        {
            if (item == nodoActual)
            {
                WinObject.instance.Win(gameObject.name);
            }
        }
    }

    void IACheckStacked()
    {
        if (stacked && caminoObjetivo.caminoNodo.Count - 1 > 0)    //Deberia checkear si hay forma de moverse a los lados en lugar de ganar
        {
            transform.position = caminoObjetivo.caminoNodo[caminoObjetivo.caminoNodo.Count - 1].transform.position+Vector3.up;
            caminoObjetivo.RemoveLast();
            stacked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stacked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            stacked = false;
        }
    }

    private void OnMouseOver()
    {
        if (!playerControlled)
        {
            caminoObjetivo.ShowPath(0, Color.green);
        }
    }
}
