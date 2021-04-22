using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Entidad encargada de crear muros para eliminar las conexiones entre los Nodos
public class WallPlacer : MonoBehaviour
{

    //Metodo que reconoce si el mouse esta encima de la entidad
    private void OnMouseOver() {
        //Si se presiona el click izquierdo
        //Activar el muro
        if (Input.GetKey(KeyCode.Mouse0)) {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
