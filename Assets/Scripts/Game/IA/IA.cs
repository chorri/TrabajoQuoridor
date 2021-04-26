using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoIA {
    Wait,
    Act,
    Move
}

public class IA : MonoBehaviour
{
    public Nodo nodoActual;
    public Nodo objetivo;

    public EstadoIA currentState;
    CaminoCompleto caminoObjetivo = new CaminoCompleto();
    public int currentIndex;

    public float speed;
    float currentDelta;

    //Distancia minima para continuar con el siguiente Nodo
    public float minDistance = 0.5f;

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case EstadoIA.Wait:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
                    currentState = EstadoIA.Act;
                    caminoObjetivo = new CaminoCompleto();
                    Game_Manager.instance.currentGameState = GameState.Run;
                }


                //if (Input.GetKeyDown(KeyCode.Q)) {
                //    caminoObjetivo.ShowDirection();
                //    caminoObjetivo.ShowPath(3);
                //    Debug.Log("-");
                //}
                break;
            case EstadoIA.Act:
               
                CaminoCompleto rpta = new CaminoCompleto();
                rpta = Pathfinding.instance.AStar(nodoActual,objetivo);
                PathDisplay.instance.SetPathDisplay(rpta);
                rpta.caminoNodo.Add(nodoActual);

                caminoObjetivo = rpta;

                currentIndex = caminoObjetivo.caminoNodo.Count;
                currentState = EstadoIA.Move;

                Game_Manager.instance.testTimeStart = Time.time;
                break;
            case EstadoIA.Move:

                if (currentIndex > 1)
                {
                    if (Vector3.Distance(transform.position, caminoObjetivo.caminoNodo[currentIndex - 2].GetTransform().position) >= minDistance)
                    {
                        currentDelta += speed * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(caminoObjetivo.caminoNodo[currentIndex -1].GetTransform().position,
                                                                    caminoObjetivo.caminoNodo[currentIndex - 2].GetTransform().position, currentDelta);

                    } else
                    {
                        currentIndex--;
                        currentDelta = 0;
                    }
                    

                    //caminoObjetivo.ShowPath(1, Color.green);
                    //transform.position = caminoObjetivo.caminoNodo[caminoObjetivo.caminoNodo.Count - 1].transform.position;
                    //currentState = EstadoIA.Wait;
                } else
                {
                    currentState = EstadoIA.Wait;
                    Game_Manager.instance.currentGameState = GameState.Result;
                    Game_Manager.instance.testTimeEnd = Time.time;
                }
                break;
            default:
                break;
        }
    }
}
