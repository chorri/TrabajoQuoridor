using System.Collections.Generic;
using UnityEngine;

//Entidad que se encarga de Bloquear los caminos entre los nodos eliminando sus conexiones
public class WallEntity : MonoBehaviour
{
    //Lista de nodos encontrados por la colision del muro
    public List<Nodo> encontrados;

    void Update()
    {
        //En caso hayan nodos encontrados se eliminaran las conexiones entre ellos
        if (encontrados.Count>0) {
            foreach (Nodo nodo in encontrados) {
                foreach (Nodo nodoTotal in encontrados) {
                    nodo.adjacentes.Remove(nodoTotal);
                }
            }
            encontrados.Clear();
        }
    }

    //Agregar Nodos a la lista de encontrados en colision
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "tileArea") {
            encontrados.Add(other.transform.GetComponent<Nodo>());
        }
    }
}
