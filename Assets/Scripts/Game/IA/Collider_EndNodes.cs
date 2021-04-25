using UnityEngine;

//Entidad que detecta el nodo final y lo asigna a la IA
public class Collider_EndNodes : MonoBehaviour
{
    //Referencia principal de la IA
    public IA padre;

    //Medoto de Unity que sirve como el bucle principal del juego
    //Esperamos 2 segundos para activar las colisiones del objeto
    //Esto evita un bug con el orden de ejecución del código
    void Update()
    {
        if (!(Game_Manager.instance.currentGameState == GameState.Start)) {
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    //Metodo de Unity que permite detectar la colision inicial con otra entidad
    //Si la entidad tiene el Tag de Nodo agrega el nodo a los Nodos objetivos
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Nodo") {
            padre.objetivo = other.transform.parent.GetComponent<Nodo>();
        }
    }
}
