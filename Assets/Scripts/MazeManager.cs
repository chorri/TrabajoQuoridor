using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeManager : MonoBehaviour
{

    public static MazeManager instance;

    public Nodo playerPos;
    public List<bool> hPlacers;
    public List<bool> vPlacers;

    public string test;
    public Dictionary<string, int> hDic = new Dictionary<string, int>();
    int hValue = 0;
    public Dictionary<string, int> vDic = new Dictionary<string, int>();
    int vValue = 0;

    public Dictionary<string, string> results = new Dictionary<string, string>();

    
    public void AddState(string hPla, string vPla, Nodo pPos,string rpta)
    {
        string temp = pPos.gameObject.name;
        temp = temp.Remove(0, 4);

        int n = int.Parse(temp);

        string newKey = hDic[hPla] + "|" + vDic[vPla] + "|" + n;

        if (!results.ContainsKey(newKey))
        {
            results[newKey] = rpta;
            Debug.Log(newKey + " | " + results[newKey]);
        }
    }

    public void AddPlacers()
    {
        GameObject gOV = GameObject.Find("WallPlacerVertical");
        for (int i = 0; i < gOV.transform.childCount; i++)
        {
            vPlacers.Add(gOV.transform.GetChild(i).GetComponent<WallPlacer>().state);
        }

        GameObject gOH = GameObject.Find("WallPlacerHorizontal");
        for (int i = 0; i < gOH.transform.childCount; i++)
        {
            hPlacers.Add(gOH.transform.GetChild(i).GetComponent<WallPlacer>().state);
        }
    }

    public void UpdatePlacers(GameObject obj)
    {
        string nam = obj.name;
        int i;
        if (nam[0] == 'V')
        {
            nam = nam.Remove(0, 1);
            //Debug.Log(nam);
            i = int.Parse(nam);
            vPlacers[i] = obj.GetComponent<WallPlacer>().state;
        } else {
            nam = nam.Remove(0,1);
            //Debug.Log(nam);
            i = int.Parse(nam);
            hPlacers[i] = obj.GetComponent<WallPlacer>().state;
        }
    }

    private void Awake()
    {
        if (MazeManager.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AddPlacers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log( ConvertToBinary());
            //diccionario.Add(this, test);
            string temp = playerPos.gameObject.name;
            temp = temp.Remove(0, 4);
            Debug.Log(temp);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Debug.Log( ConvertToBinary());
            //diccionario.Add(this, test);
            //AddToResultDictionary("Test");

            //Diccionario de Paredes Horizontales
            foreach (string item in hDic.Keys)
            {
                Debug.Log(item + " -> " + hDic[item]);
            }
            //Diccionario de Paredes Verticales
            foreach (string item in vDic.Keys)
            {
                Debug.Log(item + " -> " + vDic[item]);
            }

            foreach (string item in results.Keys)
            {
                Debug.Log(item + " -> " + results[item]);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //UpdatePlacers();
            //Debug.Log( ConvertToBinary());
            //Debug.Log(diccionario.ContainsKey(this));
        }


        if (Input.GetKeyDown(KeyCode.E))
        {

            foreach (IA item in TurnManager.instance.players)
            {
                Debug.Log(item.gameObject.name + ": " + item.caminoObjetivo.caminoNodo.Count);
            }

            //Debug.Log( ConvertToBinary());
            //Debug.Log(diccionario[this]);
        }
    }

    public void AddToResultDictionary(string res)
    {
        AddToDicionary(hDic, ConvertToBinary(hPlacers), true);
        AddToDicionary(vDic, ConvertToBinary(vPlacers), false);
        AddState(ConvertToBinary(hPlacers), ConvertToBinary(vPlacers), playerPos,res);
    }

    public string ConvertToBinary(List<bool> placers)
    {
        string rpta = "";

        for (int i = 0; i < placers.Count; i++)
        {
            rpta += placers[i] ? 1 : 0;
        }

        return rpta;
    }

    void AddToDicionary(Dictionary<string, int> dic, string key, bool horizontal)
    {
        if (dic.ContainsKey(key))
        {
            return;
        }
        if (horizontal)
        {
            dic.Add(key,hValue);
            hValue++;
        } else
        {
            dic.Add(key,vValue);
            vValue++;
        }
        
    }
}
