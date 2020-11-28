using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinObject : MonoBehaviour
{
    public static WinObject instance;
    public GameObject canvasObj;
    public Text winText;
    bool gameStatus = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool IsThereAWinner()
    {
        return gameStatus;
    }
    public void Win(string pj)
    {
        canvasObj.SetActive(true);
        winText.text = pj +  "!!!! A winner is you!";
        gameStatus = true;
    }
}
