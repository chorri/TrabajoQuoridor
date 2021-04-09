using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{

    public int trabajo = 0;
    public TextMesh trabajoTexto;
    public int distancia = 0;
    public TextMesh distanciaTexto;
    public TextMesh totalTexto;

    public Nodo direccion;

    public List<Nodo> adjacentes;

    //Valores Unity
    Transform objeto;
    List<BoxCollider> busquedaAdjacentes;

    // Start is called before the first frame update
    void Start()
    {
        objeto = this.transform;
        totalTexto = transform.Find("Total").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetValues();
        }

        foreach (var i in adjacentes) {
            Debug.DrawLine(objeto.position, i.GetTransform().position, Color.red);
        }
        trabajoTexto.text = trabajo.ToString();
        distanciaTexto.text = distancia.ToString();
    }

    public void ResetValues()
    {
        Debug.Log("Test");
        trabajo = 0;
        trabajoTexto.text = "0";
        distancia = 0;
        distanciaTexto.text = "0";

        totalTexto.text = "-";
    }

    public int CostoTotal() {
        totalTexto.text = (trabajo + distancia).ToString();
        return trabajo + distancia;
    }

    public Transform GetTransform() {
        return objeto;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Nodo" && other.transform.parent != this.transform) {
            adjacentes.Add(other.transform.parent.GetComponent<Nodo>());
        }
        
    }
}
