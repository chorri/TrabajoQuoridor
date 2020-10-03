using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEntity : MonoBehaviour
{

    public List<Nodo> encontrados;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (encontrados.Count>0) {
            foreach (Nodo nodo in encontrados) {
                foreach (Nodo nodoTotal in encontrados) {
                    nodo.adjacentes.Remove(nodoTotal);
                }
            }
            encontrados.Clear();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "tileArea") {
            encontrados.Add(other.transform.GetComponent<Nodo>());
        }
    }
}
