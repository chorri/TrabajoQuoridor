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
    public List<Nodo> objetivos;

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
                foreach (Nodo item in objetivos) {
                    CaminoCompleto temp = new CaminoCompleto();
                    temp = Pathfinding.instance.AStar(nodoActual,item);

                    //temp.ShowPath(1);
                    todosCaminos.Add(temp);

                    if (caminoObjetivo.caminoNodo.Count == 0 || temp.caminoNodo.Count < caminoObjetivo.caminoNodo.Count) {
                        caminoObjetivo = temp;
                    }
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
                    //Vector3 objetivo = caminoObjetivo.caminoNodo[caminoObjetivo.caminoNodo.Count - 1].transform.position;
                    //if (Vector3.Distance(transform.position, objetivo) < 0.1) {
                    //    transform.position = objetivo;
                    //    currentState = EstadoIA.Wait;
                    //} else {
                    //    Vector3.Lerp(transform.position, objetivo, 0.15f);
                    //}
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
