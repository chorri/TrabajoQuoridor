using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public static Pathfinding instance;

    void Start() {
        instance = this;
    }

    public CaminoCompleto AStar(Nodo ini,Nodo fin) {
        CaminoCompleto movimientos = new CaminoCompleto();

        List<Nodo> openList = new List<Nodo>();
        List<Nodo> closeList = new List<Nodo>();
        openList.Add(ini);

        //Mientras no se explore el fin
        while (!closeList.Contains(fin)) {

            if (openList.Count == 0)
            {
                Debug.Log("No hay camino posible");
                break;
            }

            //Definir cual es el nodo actual entre todos los nodos posibles
            Nodo actual = openList[0];
            for (int i = 0; i < openList.Count; i++) {
                if (openList[i].CalcularF() < actual.CalcularF() ||
                    openList[i].CalcularF() == actual.CalcularF() && openList[i].Hs<actual.Hs) {
                    actual = openList[i];
                }
            }

            //Remover y agregar a la lista de revisados el actual
            openList.Remove(actual);
            closeList.Add(actual);

            if (actual==fin) {
                return FinalizarCamino(ini, fin); ;
            }

            foreach (var item in actual.adjacentes) {
                if (closeList.Contains(item)) {
                    continue;
                }

                int nuevoTrabajo = actual.Gs + CalcDistancia(actual, item);
                if (nuevoTrabajo < item.Gs || !openList.Contains(item)) {
                    item.Gs = nuevoTrabajo;
                    item.Hs = CalcDistancia(item, fin);
                    item.direccion = actual;

                    if (!openList.Contains(item)) {
                        openList.Add(item);
                    }
                }
            }
        }
        //Return Empty
        return movimientos;
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
}
