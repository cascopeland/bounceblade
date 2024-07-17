using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void StartGame()
    {
        // todo: change to level 1
        SceneManager.LoadScene("Area_Tutorial");
    }
}

