using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadScene : MonoBehaviour
{
    public string nameScene;
    public int indexScene;


    public void NextScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void NextScene(int value)
    {
        SceneManager.LoadScene(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6 && !String.IsNullOrEmpty(nameScene))
        {
            NextScene(nameScene);
        } else if(collision.gameObject.layer == 6)
        {
            NextScene(indexScene);
        }
    }
}
