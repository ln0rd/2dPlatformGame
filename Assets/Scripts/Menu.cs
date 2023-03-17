using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey("enter"))
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
}
