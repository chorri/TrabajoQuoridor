using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{

    public Nodo playerPos;
    public List<bool> hPlacers;
    public List<bool> vPlacers;

    public string test;
    public Dictionary<string, int> hDic = new Dictionary<string, int>();
    int hValue = 0;
    public Dictionary<string, int> vDic = new Dictionary<string, int>();
    int vValue = 0;

    public Dictionary<int[], string> results = new Dictionary<int[], string>();

    public void AddState(string hPla, string vPla, Nodo pPos)
    {
        string temp = playerPos.gameObject.name;
        temp = temp.Remove(0, 4);

        int n = int.Parse(temp);

        int[] mazeTemp = new int[3] { hDic[hPla], vDic[vPla], n };

        Debug.Log(temp + " | " + n);
        results[mazeTemp] = "Yes";
    }


    // Start is called before the first frame update
    void Start()
    {
        GameObject gOV = GameObject.Find("WallPlacerVertical");
        for (int i = 0; i < gOV.transform.childCount; i++)
        {
            vPlacers.Add(gOV.transform.GetChild(i));
        }

        GameObject gOH = GameObject.Find("WallPlacerHorizontal");
        for (int i = 0; i < gOH.transform.childCount; i++)
        {
            hPlacers.Add(gOH.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            AddToDiccionary(hDic, ConvertToBinary(hPlacers), true);
            AddToDiccionary(vDic, ConvertToBinary(vPlacers), false);
            AddState(ConvertToBinary(hPlacers), ConvertToBinary(vPlacers), playerPos);

            foreach (int[] item in results.Keys)
            {
                Debug.Log(item[0] + " | "+item[1]+" | " +item[2]);
            }
            foreach (string item in results.Values)
            {
                Debug.Log(item);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //Debug.Log( ConvertToBinary());
            //Debug.Log(diccionario.ContainsKey(this));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log( ConvertToBinary());
            //Debug.Log(diccionario[this]);
        }
    }

    public void AlterHPlacers(int n, bool value)
    {
        hPlacers[n] = value;
    }
    public void AlterVPlacers(int n, bool value)
    {
        vPlacers[n] = value;
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

    void AddToDiccionary(Dictionary<string, int> dic, string key, bool horizontal)
    {
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
