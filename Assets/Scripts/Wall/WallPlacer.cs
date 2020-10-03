using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver() {        
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
