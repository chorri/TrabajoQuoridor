using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoSelector : MonoBehaviour
{
    TurnManager tM;

    public Nodo nodoRef;

    public Material inactiveM;
    public Material activeM;
    public bool materialActive = false;

    // Start is called before the first frame update
    void Start()
    {
        tM = TurnManager.instance;
        nodoRef = transform.parent.GetComponent<Nodo>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMaterial(materialActive);
    }

    public void ChangeMaterial(bool state)
    {
        transform.GetComponent<MeshRenderer>().material = (state) ? activeM : inactiveM;
    }

    private void OnMouseOver()
    {
        IA currentPlayer = tM.GetCurrentPlayer();
        if (currentPlayer.playerControlled &&
            (currentPlayer.currentState == EstadoIA.Act || currentPlayer.currentState == EstadoIA.Move))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                foreach (Nodo nodo in currentPlayer.nodoActual.adjacentes)
                {
                    nodo.cuerpo.materialActive = false;
                }

                currentPlayer.transform.position = transform.position + Vector3.up;

                if (currentPlayer.currentState == EstadoIA.Act)
                {
                    tM.GetCurrentPlayer().ChangePlayerState(EstadoIA.Move);
                }
                else
                {
                    tM.GetCurrentPlayer().ChangePlayerState(EstadoIA.Check);
                }

            }
        }
    }
}
