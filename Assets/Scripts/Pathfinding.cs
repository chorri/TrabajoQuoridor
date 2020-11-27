using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public static Pathfinding instance;

//    public Nodo inicio;
//    public Nodo final;

    void Start() {
        instance = this;
    }

    // Start is called before the first frame update
    void Update()
    {

    }

    //O(n^2)
    public CaminoCompleto AStar(Nodo ini,Nodo fin) {
        CaminoCompleto sinCamino = new CaminoCompleto(); 

        List<Nodo> posibles = new List<Nodo>();
        List<Nodo> revisados = new List<Nodo>();
        posibles.Add(ini);

        //Mientras no se explore el fin
        while (!revisados.Contains(fin)) {
            //Definir cual es el nodo actual entre todos los nodos posibles
            
            if (posibles.Count <=0) {
                return sinCamino;
            }

            Nodo actual = posibles[0];
            

            for (int i = 0; i < posibles.Count; i++) {
                if (posibles[i].CostoTotal() < actual.CostoTotal() ||
                    posibles[i].CostoTotal() == actual.CostoTotal() && posibles[i].distancia<actual.distancia) {
                    actual = posibles[i];
                }
            }

            //Remover y agregar a la lista de revisados el actual
            posibles.Remove(actual);
            revisados.Add(actual);

            if (actual==fin) {
                return FinalizarCamino(ini, fin); ;
            }

            foreach (var item in actual.adjacentes) {
                if (revisados.Contains(item)) {
                    continue;
                }

                int nuevoTrabajo = actual.trabajo + CalcDistancia(actual, item);
                if (nuevoTrabajo < item.trabajo || !posibles.Contains(item)) {
                    item.trabajo = nuevoTrabajo;
                    item.distancia = CalcDistancia(item, fin);
                    item.direccion = actual;

                    if (!posibles.Contains(item)) {
                        posibles.Add(item);
                    }
                }
            }
        }
        //Return Empty
        Debug.Log("It is impresive that you manage to find the way and at the same time dont find a way");
        return sinCamino;
    }

    CaminoCompleto FinalizarCamino(Nodo i, Nodo f) {
        CaminoCompleto movimientos = new CaminoCompleto();
        Nodo actual = f;
        
        while (actual != i) {
            //actual.GetTransform().GetChild(0).gameObject.SetActive(false);
            movimientos.caminoNodo.Add(actual);
            movimientos.caminoDireccion.Add(AgregarDireccion(actual.transform.position, actual.direccion.transform.position));
            actual = actual.direccion;
        }

        return movimientos;
    }

    string AgregarDireccion(Vector3 a, Vector3 b) {
        string output="";
        float dX = b.x - a.x;
        float dZ = b.z - a.z;

        if (dX < 0) output = "Right";
        if (dX > 0) output = "Left";
        if (dZ < 0) output = "Up";
        if (dZ > 0) output = "Down";

        return output;
    }

    int CalcDistancia(Nodo a,Nodo b) {
        return (int) Vector3.Distance(a.GetTransform().position, b.GetTransform().position);
    }



    //bfs
    public CaminoCompleto BFS() {
        CaminoCompleto movimientos = new CaminoCompleto();


        return movimientos;
    }
}
