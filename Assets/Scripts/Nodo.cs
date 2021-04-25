using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{

    public int Gs = 0;
    public TextMesh GsTexto;
    public int Hs = 0;
    public TextMesh HsTexto;
    public TextMesh Fs;     //heuristica

    public Nodo parent;

    public List<Nodo> adjacentes;

    //Valores Unity
    Transform objeto;

    // Asignación de valores Unity
    void Start()
    {
        objeto = this.transform;
        GsTexto = transform.Find("Gs").GetComponent<TextMesh>();
        HsTexto = transform.Find("Hs").GetComponent<TextMesh>();
        Fs = transform.Find("Total").GetComponent<TextMesh>();

    }

    void Update()
    {
        //Presionando R se resetean los valores visuales de los nodos
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ResetValues();
        //}

        //Dibuja la linea roja entre los diferentes Nodos
        foreach (var i in adjacentes) {
            Debug.DrawLine(objeto.position, i.GetTransform().position, Color.red);
        }
        GsTexto.text = Gs.ToString();
        HsTexto.text = Hs.ToString();
    }

    //Metodo para resetear Valores
    public void ResetValues()
    {
        Gs = 0;
        GsTexto.text = "0";
        Hs = 0;
        HsTexto.text = "0";

        Fs.text = "-";
    }

    //Calculo de Heurística
    public int CalcularF() {
        Fs.text = (Gs + Hs).ToString();
        return Gs + Hs;
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
