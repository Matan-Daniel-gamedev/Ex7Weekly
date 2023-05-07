using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        Debug.Log("Screen size: " + screenWidth + " x " + screenHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("ex7");
        }
    }
}
