using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosManager : MonoBehaviour
{
    
    Scene_Manager scene_Manager;

    void Start()
    {
        scene_Manager = GetComponent<Scene_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            scene_Manager.GameStart();
        }
    }

}
