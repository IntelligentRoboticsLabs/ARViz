using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public static void goToScene(string scenename)
    {
        Debug.Log("############ SceneManager - goToScene " + scenename);
        SceneManager.LoadScene(scenename);
    }
}

