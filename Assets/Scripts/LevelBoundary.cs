using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBoundary : MonoBehaviour
{

    public static float leftSide = 1.5f;
    public static float rightSide = 1.5f;
    public static int COIN_INCREMENT = 15;
    public static int BLUE_SPHERE_INCREMENT = 10;
    public static int COIN_LIMIT = 3;
    public static int BLUE_SPHERES_LIMIT = 3;
    public static int INVINCIBLE_TIME = 5;
    public static int SPEED_CHANGE_TIME = 7;
    public static int IRON_BALL_INCREMENT = -10;
    public float internalLeft;
    public float internalRight;
    public static string message;
    public enum GAME_OVER_REASON
    {
        ZERO_SCORE,
        BOMB
    }

    public static void GameOver(GAME_OVER_REASON reason)
    {
        if(reason == GAME_OVER_REASON.BOMB)
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            long score = player.getScore();
            message = $"GAME OVER.\nThe player was killed by the bomb. Its score was {score}.\nBetter luck next time and take care of bombs.";
        }
        else
        {
            message = $"GAME OVER.\nThe player's score reached 0.\nBetter luck next time and take care of iron balls.";
        }
        SceneManager.LoadScene("GameOver");
    }
    void Start()
    {
        leftSide = internalLeft;
        rightSide = internalRight;
    }
    void Update()
    {
        
    }
}
