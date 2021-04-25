using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    Start,
    Run,
    Result
}

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public GameState currentGameState;
    public GameObject lightObject;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //ResetLevel when pressing O
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (currentGameState)
        {
            case GameState.Start:
                lightObject.SetActive(false);
                break;
            case GameState.Run:
                lightObject.SetActive(true);
                break;
            case GameState.Result:
                lightObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void AlternateActive(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
