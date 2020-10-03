using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_StartNode : MonoBehaviour
{
    public IA padre;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Nodo") {
            padre.nodoActual = other.transform.parent.GetComponent<Nodo>();
        }
    }
}
