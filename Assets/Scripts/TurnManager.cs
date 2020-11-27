using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<IA> players;
    public int currentPlayer = 0;

    public static TurnManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        players[currentPlayer].currentState = EstadoIA.Act;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MazeManager.instance.playerPos == null)
        {
            MazeManager.instance.playerPos = players[currentPlayer].nodoActual;
        }
    }

    public void NextPlayer()
    {
        currentPlayer++;
        if (currentPlayer >= players.Count)
        {
            currentPlayer = 0;
        }

        players[currentPlayer].currentState = EstadoIA.Act;
    }

    public IA GetCurrentPlayer()
    {
        return players[currentPlayer];
    }

    public IA GetNextPlayer()
    {
        if (currentPlayer+1 >= players.Count)
        {
            return players[0];
        } else
        {
            return players[currentPlayer + 1];
        }
    }
}
