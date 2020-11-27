using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Control de Datos

public class WallEntity : MonoBehaviour
{

    public List<Nodo> encontrados;

    // Update is called once per frame
    void Update()
    {
        if (encontrados.Count>0) {
            foreach (Nodo nodo in encontrados) {
                foreach (Nodo nodoTotal in encontrados) {
                    nodo.adjacentes.Remove(nodoTotal);
                }
            }
            //encontrados.Clear();
        }
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    OnWall();
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    OffWall();
        //}
    }

    public void OffWall()
    {
        foreach (Nodo nodo in encontrados)
        {
            nodo.SetCheckAgain(true);
        }
        gameObject.SetActive(false);
    }

    public void OnWall()
    {   
        foreach (Nodo nodo in encontrados)
        {
            nodo.SetCheckAgain(false);
        }
    }
    

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "tileArea") {
            if (!encontrados.Contains(other.transform.GetComponent<Nodo>()))
            {
                encontrados.Add(other.transform.GetComponent<Nodo>());
                other.transform.GetComponent<Nodo>().SetCheckAgain(false);
            }
            
        }
    }
}
