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
                        } else
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
        } else
        {
            IAControlled();
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

                DefineCaminoObjetivo();

                IA posWiner=this;
                foreach (IA item in TurnManager.instance.players)
                {
                    if (posWiner.caminoObjetivo.caminoNodo.Count > item.caminoObjetivo.caminoNodo.Count)
                    {
                        posWiner = item;
                    }
                }
                Debug.LogError(gameObject.name +": "+caminoObjetivo.caminoNodo.Count + " vs " + posWiner.gameObject.name+": " +posWiner.caminoObjetivo.caminoNodo.Count);

                //Calcular Muro Optimo

                //Si el siguiente jugador es el posWinner
                if (TurnManager.instance.GetNextPlayer() == posWiner)
                {//Colocar Muro Optimo

                }

                timeStart = Time.time;
                currentState = EstadoIA.Move;
                break;
            case EstadoIA.Move:
                //                if (todosCaminos.Count > 0) {
                //                    if (Time.time - timeStart > maxTime*0.2f) {
                //                        Debug.Log("----");
                //                        todosCaminos[todosCaminos.Count - 1].ShowPath(0.5f);
                //                        todosCaminos[todosCaminos.Count - 1].ShowDirection();
                //                        todosCaminos.RemoveAt(todosCaminos.Count - 1);
                //                        timeStart = Time.time;
                //                    }
                //                } else {
                //Vector3 objetivo = caminoObjetivo.caminoNodo[caminoObjetivo.caminoNodo.Count - 1].transform.position;
                //if (Vector3.Distance(transform.position, objetivo) < 0.1) {
                //    transform.position = objetivo;
                //    currentState = EstadoIA.Wait;
                //} else {
                //    Vector3.Lerp(transform.position, objetivo, 0.15f);
                //}
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

            temp.ShowPath(1);
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
                Debug.Log("WINNER" + gameObject.name);
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
