using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverMenu : MonoBehaviour
{

    public Text FinalComments;

    public void Start()
    {
        FinalComments.text = LevelBoundary.message;
    }
    public void Restart()
    {
       SceneManager.LoadScene("GamePlay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
