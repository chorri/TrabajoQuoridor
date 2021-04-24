using System.Collections.Generic;
using UnityEngine;

//Entidad que se encarga de guardar la informacion del camino final
public class CaminoCompleto
{

    public List<string> caminoDireccion;
    public List<Nodo> caminoNodo;

    public CaminoCompleto() {
        caminoDireccion = new List<string>();
        caminoNodo = new List<Nodo>();
    }

    //Muestra la direccion en la consola (Igual a print)
    public void ShowDirection() {
        for (int i = 0; i < caminoDireccion.Count; i++) {
            Debug.Log(caminoDireccion[i]);
        }
    }

    //Diferentes Overloads para mostrar el camino creado

    public void ShowPath(int duration) {
        foreach (Nodo actual in caminoNodo) {
            Debug.DrawLine(actual.transform.position + Vector3.up, actual.parent.transform.position + Vector3.up, Color.cyan, duration);
        }
    }
    
    public void ShowPath(int duration,float offset) {
        Vector3 off = new Vector3(0, offset,0);
        foreach (Nodo actual in caminoNodo) {
            Debug.DrawLine(actual.transform.position + off, actual.parent.transform.position + off, Color.black, duration);
        }
    }

    public void ShowPath(int duration,Color c) {
        foreach (Nodo actual in caminoNodo) {
            Debug.DrawLine(actual.transform.position + Vector3.up, actual.parent.transform.position + Vector3.up, c, duration);
        }
    }
}
