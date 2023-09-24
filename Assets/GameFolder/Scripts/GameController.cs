using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalGems;
    public GameObject[] UIGems;
    public GameObject player;

    [Header("Camera System")]
    public CameraFollow cam;

    [Header("GameObject Gate")]
    public Transform focusGate;

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

        if(totalGems >= 3)
        {
            CamChangeFocus(focusGate, 2f);
            StartCoroutine(IEDisableGate(1f));
        }
    }

    public void CamChangeFocus(Transform transform, float time)
    {
        if(cam != null){ cam.target = transform; }
        player.GetComponent<Player>().isPlayerStopped = true;
        StartCoroutine(IEReturnFocusPlayer(time));
    }

    IEnumerator IEReturnFocusPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        if (player != null) { cam.target = player.transform; }
        player.GetComponent<Player>().isPlayerStopped = false;
    }

    IEnumerator IEDisableGate(float time)
    {
        yield return new WaitForSeconds(time);
        focusGate.GetComponent<Animator>().Play("Explosion");
        SFXController.instance.SFX("Door", 1f);
        yield return new WaitForSeconds(time);
        focusGate.gameObject.SetActive(false);
    }
}
