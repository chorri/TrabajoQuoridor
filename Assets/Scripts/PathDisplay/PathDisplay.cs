using System.Collections.Generic;
using UnityEngine;

//Entidad que se encarga de mostrar graficamente la direccion que se tomara
public class PathDisplay : MonoBehaviour
{

    public static PathDisplay instance;

    public List<Vector3> nodes;
    int currentNode = 0;
    float currentDelta;
    public float speed;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        //Al presionar el boton S podremos alternar la visibilidad del objeto
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.GetComponent<MeshRenderer>().enabled = !transform.GetComponent<MeshRenderer>().enabled;
        }


        if(nodes.Count > 1)
        {
            //Mientras no haya llegado al siguiente Nodo avanzara en direccion al Nodo siguiente
            if (Vector3.Distance(transform.position, nodes[currentNode + 1]) >= 0.5f)
            {
                currentDelta += speed * Time.deltaTime;
                transform.LookAt(nodes[currentNode + 1]);
                transform.position = Vector3.MoveTowards(nodes[currentNode], nodes[currentNode + 1], currentDelta);
            }
            else
            {
                //Asignar el siguiente Nodo objetivo y reiniciar la posicion relativa al siguiente nodo
                if (currentNode < nodes.Count - 2)
                {
                    currentNode++;
                }
                else
                {
                    currentNode = 0;
                }
                currentDelta = 0;
            }
        }
    }

    public void SetPathDisplay(CaminoCompleto camino)
    {
        nodes.Clear();
        foreach (Nodo item in camino.caminoNodo)
        {
            nodes.Add(item.GetTransform().position);
        }
    }
}
