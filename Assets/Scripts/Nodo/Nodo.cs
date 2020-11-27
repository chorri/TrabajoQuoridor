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
    public NodoSelector cuerpo;
    bool checkAgain = true;

    // Start is called before the first frame update
    void Start()
    {
        objeto = this.transform;
        cuerpo = transform.Find("Cuerpo").GetComponent<NodoSelector>();
        totalTexto = transform.Find("Total").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        DebugText();
    }

    void DebugText()
    {
        foreach (var i in adjacentes)
        {
            Debug.DrawLine(objeto.position, i.GetTransform().position, Color.red);
        }
        trabajoTexto.text = trabajo.ToString();
        distanciaTexto.text = distancia.ToString();
    }

    public int CostoTotal() {
        totalTexto.text = (trabajo + distancia).ToString();
        return trabajo + distancia;
    }

    public Transform GetTransform() {
        return objeto;
    }

    public void SetCheckAgain(bool value)
    {
        checkAgain = value;
        Debug.Log("Entro");
    }


    private void OnTriggerStay(Collider other)
    {
        if (checkAgain)
        {
            if (other.tag == "Nodo" && other.transform.parent != this.transform)
            {
                if (!adjacentes.Contains(other.transform.parent.GetComponent<Nodo>()))
                {
                    adjacentes.Add(other.transform.parent.GetComponent<Nodo>());
                }
            }
        }
    }

//    private void OnTriggerEnter(Collider other) {
//        if (other.tag == "Nodo" && other.transform.parent != this.transform) {
//            if (!adjacentes.Contains(other.transform.parent.GetComponent<Nodo>()))
//            {
//                adjacentes.Add(other.transform.parent.GetComponent<Nodo>());
//            }
//        }
//    }
}
