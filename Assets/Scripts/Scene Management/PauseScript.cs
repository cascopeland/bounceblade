using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseScript : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseMenu;
    public GameObject skillTreeCanvas;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Awake()
    {
        PauseMenu.SetActive(false);
        skillTreeCanvas.SetActive(false);
    }
    public void Resume()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
        skillTreeCanvas.SetActive(false);
        Time.timeScale = 1f;
        gameManager.slowBallCollisionSkillCheck();
        gameManager.swordDamageSkillCheck();
        gameManager.ballDamageSkillCheck();
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            if (isPaused)
                Resume();
            else
                SkillTreePause();
        }
    }

    public void SkillTreePause()
    {
        skillTreeCanvas.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
}
