using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public Nodo inicio;
    public Nodo final;
    
    // Start is called before the first frame update
    void Update()
    {
        AStar(inicio, final);
    }

    void AStar(Nodo ini,Nodo fin) {

        List<Nodo> posibles = new List<Nodo>();
        List<Nodo> revisados = new List<Nodo>();
        posibles.Add(ini);

        //Mientras no se explore el fin
        while (!revisados.Contains(fin)) {
            //Definir cual es el nodo actual entre todos los nodos posibles
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
                MostrarCamino(inicio, final);
                return;
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
    }

    void MostrarCamino(Nodo i, Nodo f) {
        List<Nodo> camino = new List<Nodo>();
        Nodo actual = f;

        while (actual != i) {
            camino.Add(actual);
            actual.GetTransform().GetChild(0).gameObject.SetActive(false);
            actual = actual.direccion;
        }
        camino.Add(actual);
        actual.GetTransform().GetChild(0).gameObject.SetActive(false);
    }

    int CalcDistancia(Nodo a,Nodo b) {
        return (int) Vector3.Distance(a.GetTransform().position, b.GetTransform().position);
    }

}
