using UnityEngine;

//Entidad que detecta una colisión para determinar el nodo inicial de la IA
public class Collider_StartNode : MonoBehaviour
{
    public IA padre;

    //Metodo de Unity que permite detectar la colision inicial con otra entidad
    //Si la entidad tiene el Tag de Nodo asigna el nodo como Nodo Actual
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Nodo") {
            padre.nodoActual = other.transform.parent.GetComponent<Nodo>();
        }
    }
}
