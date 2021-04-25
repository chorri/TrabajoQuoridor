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

    //Para mostrar todos los caminos
    List<CaminoCompleto> todosCaminos = new List<CaminoCompleto>();
    //Reloj
    float timeStart;
    float maxTime = 1.5f;

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case EstadoIA.Wait:
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    currentState = EstadoIA.Act;
                    caminoObjetivo = new CaminoCompleto();
                }

                if (Input.GetKeyDown(KeyCode.Q)) {
                    caminoObjetivo.ShowDirection();
                    caminoObjetivo.ShowPath(3);
                    Debug.Log("-");
                }
                break;
            case EstadoIA.Act:
               
                CaminoCompleto rpta = new CaminoCompleto();
                rpta = Pathfinding.instance.AStar(nodoActual,objetivo);
                PathDisplay.instance.SetPathDisplay(rpta);

                todosCaminos.Add(rpta);

                if (caminoObjetivo.caminoNodo.Count == 0 || rpta.caminoNodo.Count < caminoObjetivo.caminoNodo.Count) {
                    caminoObjetivo = rpta;
                }
               
                timeStart = Time.time;
                currentState = EstadoIA.Move;
                break;
            case EstadoIA.Move:
                if (todosCaminos.Count > 0) {
                    if (Time.time - timeStart > maxTime) {
                        todosCaminos[todosCaminos.Count - 1].ShowPath(1);
                        todosCaminos[todosCaminos.Count - 1].ShowDirection();
                        todosCaminos.RemoveAt(todosCaminos.Count - 1);
                        timeStart = Time.time;
                    }
                } else {
                                        if (Time.time - timeStart > maxTime+2) {
                        caminoObjetivo.ShowPath(1, Color.green);
                        transform.position = caminoObjetivo.caminoNodo[caminoObjetivo.caminoNodo.Count - 1].transform.position;
                        currentState = EstadoIA.Wait;
                    }
                }
                
                break;
            default:
                break;
        }
    }
}
