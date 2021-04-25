using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoalPlacer : MonoBehaviour
{
    public Transform player;
    public Transform goal;
    Transform parent;
    public Vector3 offset;
    public Material start;
    public Material end;

    void Start()
    {
        parent = transform.parent;
    }

    private void OnMouseOver()
    {
        Debug.Log("Test");
        if (Game_Manager.instance.currentGameState == GameState.Start)
        {
            //Place Player
            if (Input.GetKey(KeyCode.Mouse0))
            {
                player.position = parent.position + offset;
                GetComponent<MeshRenderer>().material = start;
            }

            //Place Goal
            if (Input.GetKey(KeyCode.Mouse1))
            {
                goal.position = goal.position + offset;
                GetComponent<MeshRenderer>().material = end;
            }
        }
    }
}
