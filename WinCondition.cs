using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public GameObject parentObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (parentObject.transform.childCount == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);
        }
    }
}
