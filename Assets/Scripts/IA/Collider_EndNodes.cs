using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_EndNodes : MonoBehaviour
{
    public IA padre;

    float timerStart;
    float waitTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        timerStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timerStart > waitTime) {
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Nodo") {
            padre.objetivos.Add(other.transform.parent.GetComponent<Nodo>());
        }
    }
}
