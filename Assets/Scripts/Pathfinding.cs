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

            //Checkeo si esta vacia la lista abierta
            if (openList.Count == 0)
            {
                Debug.Log("No hay camino posible");
                break;
            }

            //Buscar en la lista abierta el Nodo con menor Fs, removerlo de
            //la lista abierta y agregarlo en la lista cerrada
            Nodo actual = Min(openList);
            openList.Remove(actual);
            closeList.Add(actual);

            //Checkear si es el nodo final
            if (actual==fin) {
                return FinalizarCamino(ini, fin); ;
            }

            int n_gs = int.Parse(actual.Fs.text) - actual.Hs;

            //Checkeo de todas las ciudades conectadas con la ciudad N
            foreach (var item in actual.adjacentes) {   
                if (closeList.Contains(item)) {
                    continue;
                }

                int nuevoTrabajo = actual.Gs + CalcDistancia(actual, item);
                item.Gs = nuevoTrabajo;
                item.Hs = CalcDistancia(item, fin);

                //Asignar el nodo Adjacente, crear m en caso exista en la lista abierta y
                //crear dist para determinar la distancia de nodoAdjacente
                Nodo nodoAdjacente = item;
                Nodo m = FindNodoInList(nodoAdjacente,openList);
                int dist = item.Hs;

                if (m != null)
                {
                    Debug.Log("Esta en open");
                    int fs = int.Parse(m.Fs.text);
                    if (fs > n_gs + m.Hs + dist)
                    {
                        m.Fs.text = (n_gs + m.Hs + dist).ToString();
                        m.parent = actual;
                    }
                } else
                {
                    m = FindNodoInList(nodoAdjacente, closeList);
                    Debug.Log("CloseList: " + closeList.Count);
                    if (m != null)
                    {
                        Debug.Log("Esta en Close");
                        int fs = int.Parse(m.Fs.text);
                        Debug.Log(fs + "|" + m.Gs);
                        if (fs > n_gs + m.Hs + dist)
                        {
                            Debug.Log("Ded");
                            m.Fs.text = (n_gs + m.Hs + dist).ToString();
                            m.parent = actual;
                            openList.Add(m);
                            closeList.Remove(m);
                        }
                    } else
                    {
                        m = nodoAdjacente;
                        Debug.Log("No esta en ninguna lista");
                        m.Fs.text = (n_gs + m.Hs + dist).ToString();
                        m.parent = actual;
                        openList.Add(m);
                    }
                }


                //int nuevoTrabajo = actual.Gs + CalcDistancia(actual, item);
                //if (nuevoTrabajo < item.Gs || !openList.Contains(item)) {
                //    item.Gs = nuevoTrabajo;
                //    item.Hs = CalcDistancia(item, fin);
                //    item.parent = actual;
                //
                //    if (!openList.Contains(item)) {
                //        openList.Add(item);
                //    }
                //}
            }
        }
        //Return Empty
        return movimientos;
    }

    Nodo FindNodoInList(Nodo nodo, List<Nodo> lista)
    {
        foreach (Nodo item in lista)
        {
            if (item == nodo)
            {
                return item;
            }
        }

        return null;
    }

    //Metodo que simula la funcion min(openList, key=lambda x: x.fs)
    Nodo Min(List<Nodo> openList)
    {
        Nodo min = openList[0];
        for (int i = 0; i < openList.Count; i++)
        {
            if (openList[i].CalcularF() < min.CalcularF() ||
                openList[i].CalcularF() == min.CalcularF() && openList[i].Hs < min.Hs)
            {
                min = openList[i];
            }
        }

        return min;
    }

    CaminoCompleto FinalizarCamino(Nodo i, Nodo f) {
        CaminoCompleto movimientos = new CaminoCompleto();
        Nodo actual = f;
        
        while (actual != i) {
            //actual.GetTransform().GetChild(0).gameObject.SetActive(false);
            movimientos.caminoNodo.Add(actual);
            movimientos.caminoDireccion.Add(AgregarDireccion(actual.transform.position, actual.parent.transform.position));
            actual = actual.parent;
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
