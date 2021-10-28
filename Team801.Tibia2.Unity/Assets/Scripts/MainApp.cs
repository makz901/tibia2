using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team801.Tibia2.Client;

public class MainApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var client = new Client();
        client.Connect("mkz unity");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
