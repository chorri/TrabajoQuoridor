using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{

    public int trabajo = 0;
    public TextMesh trabajoTexto;
    public int distancia = 0;
    public TextMesh distanciaTexto;
    public TextMesh totalTexto;     //heuristica

    public Nodo direccion;

    public List<Nodo> adjacentes;

    //Valores Unity
    Transform objeto;
    List<BoxCollider> busquedaAdjacentes;

    // Asignación de valores Unity
    void Start()
    {
        objeto = this.transform;
        totalTexto = transform.Find("Total").GetComponent<TextMesh>();
    }

    void Update()
    {
        //Presionando R se resetean los valores visuales de los nodos
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetValues();
        }

        //Dibuja la linea roja entre los diferentes Nodos
        foreach (var i in adjacentes) {
            Debug.DrawLine(objeto.position, i.GetTransform().position, Color.red);
        }
        trabajoTexto.text = trabajo.ToString();
        distanciaTexto.text = distancia.ToString();
    }

    //Metodo para resetear Valores
    public void ResetValues()
    {
        Debug.Log("Test");
        trabajo = 0;
        trabajoTexto.text = "0";
        distancia = 0;
        distanciaTexto.text = "0";

        totalTexto.text = "-";
    }

    //Calculo de Heurística
    public int CostoTotal() {
        totalTexto.text = (trabajo + distancia).ToString();
        return trabajo + distancia;
    }

    //Metodo de Unity
    public Transform GetTransform() {
        return objeto;
    }

    //Metodo de Unity que perite obtener valores de una colision, en este caso se usa para asignar los nodos adjacentes
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Nodo" && other.transform.parent != this.transform) {
            adjacentes.Add(other.transform.parent.GetComponent<Nodo>());
        }
        
    }
}
