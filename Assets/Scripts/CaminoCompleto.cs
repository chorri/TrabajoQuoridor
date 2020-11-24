using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaminoCompleto
{

    public List<string> caminoDireccion;
    public List<Nodo> caminoNodo;

    public CaminoCompleto() {
        caminoDireccion = new List<string>();
        caminoNodo = new List<Nodo>();
    }

    public void ShowDirection() {
        for (int i = 0; i < caminoDireccion.Count; i++) {
            Debug.Log(caminoDireccion[i]);
        }
    }

    public void RemoveLast()
    {
        caminoNodo.RemoveAt(caminoNodo.Count-1);
        caminoDireccion.RemoveAt(caminoDireccion.Count - 1);
    }

    public void ShowPath(float duration) {
        foreach (Nodo actual in caminoNodo) {
            Debug.DrawLine(actual.transform.position + Vector3.up, actual.direccion.transform.position + Vector3.up, Color.cyan, duration);
        }
    }
    
    public void ShowPath(int duration,float offset) {
        Vector3 off = new Vector3(0, offset,0);
        foreach (Nodo actual in caminoNodo) {
            Debug.DrawLine(actual.transform.position + off, actual.direccion.transform.position + off, Color.black, duration);
        }
    }

    public void ShowPath(int duration,Color c) {
        foreach (Nodo actual in caminoNodo) {
            Debug.DrawLine(actual.transform.position + Vector3.up, actual.direccion.transform.position + Vector3.up, c, duration);
        }
    }
}
