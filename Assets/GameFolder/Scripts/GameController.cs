using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalGems;
    public GameObject[] UIGems;

    public static GameController instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateUIGems()
    {
        totalGems++;
        switch(totalGems)
        {
            case 1:
                UIGems[0].SetActive(true);
                break;
            case 2:
                UIGems[1].SetActive(true);
                break;
            case 3:
                UIGems[2].SetActive(true);
                break;
        }
    }
}
