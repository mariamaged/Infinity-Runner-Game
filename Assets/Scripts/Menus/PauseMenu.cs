using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseMenuUI;
    private AudioSource PauseSFX, GameSFX;
    void Start()
    {
        PauseSFX = GameObject.Find("LevelBoundary").transform.GetChild(5).GetComponent<AudioSource>();
        GameSFX = GameObject.Find("LevelBoundary").transform.GetChild(4).GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GamePaused = true;
        GameSFX.mute = true;
        PauseSFX.Play();
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePaused = false;
        PauseSFX.Stop();
        GameSFX.mute = false;
    }

    public void Restart()
    {
        GamePaused = false;
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}


