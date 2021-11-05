using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int desiredLane;
    public float jumpForce;
    public LayerMask groundMask;
    public Text scoreText, coinsHitText, spheresHitText;

    private float modifiedSpeed;
    private long score = 0;
    private int coinsHit = 0, blueSpheresHit = 0, ironBallsHit = 0;
    private bool invincibleMode = false, gameOver = false;
    private SphereCollider col;
    private Rigidbody rb;
    private AudioSource InvincibleSFX, SpeedIncreaseSFX;

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    public bool detectSwipeOnlyAfterRelease = false;
    public float SWIPE_THRESHOLD = 200f;
    public Transform jumpButton;
    void Start()
    {
        modifiedSpeed = speed;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        Transform go = GameObject.Find("LevelBoundary").transform;
        InvincibleSFX = go.GetChild(1).GetComponent<AudioSource>();
        SpeedIncreaseSFX = go.GetChild(2).GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR 
        Debug.Log("Here in android");
        Vector3 fr = Vector3.forward * Time.deltaTime * modifiedSpeed;
        transform.Translate(fr);

        if (Input.touches.Length > 0)
        {
            var t = Input.touches[0].position;
            if (t.x >= Screen.width - 65 && t.x <= Screen.width + 25 &&
                    t.y >= Screen.height - 65 && t.y <= Screen.height + 65)
            {
                Jump();
            }
            else
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        fingerUp = touch.position;
                        fingerDown = touch.position;
                    }

                    //Detects swipe after finger is released
                    if (touch.phase == TouchPhase.Ended)
                    {
                        fingerDown = touch.position;
                        checkSwipe();
                    }
                }
            }
        }
        return;
#endif
        Debug.Log("Here in non android");
        Vector3 fr = Vector3.forward * Time.deltaTime * modifiedSpeed;
        transform.Translate(fr);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                desiredLane--;
                if (desiredLane == -1)
                    desiredLane = 0;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                desiredLane++;
                if (desiredLane == 3)
                    desiredLane = 2;
            }
            MoveDesiredLane();
        }
    }
    private bool IsGrounded()
    {
        return
            Physics.CheckSphere(transform.position, col.radius * 0.9f, groundMask);
    }
    public void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void MoveDesiredLane()
    {
        Vector3 desiredPosition =
            transform.forward * transform.position.z +
            transform.position.y * transform.up;
        switch (desiredLane)
        {
            case 0: desiredPosition += Vector3.left * LevelBoundary.leftSide; break;
            case 2: desiredPosition += Vector3.right * LevelBoundary.rightSide; break;
            case 1: desiredPosition += Vector3.right * 0; break;
        }
        transform.position = desiredPosition;
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }
        else
        {
            MoveDesiredLane();
        }
    }
    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }
    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }
    void OnSwipeUp()
    {

    }
    void OnSwipeLeft()
    {
        desiredLane--;
        if (desiredLane == -1)
            desiredLane = 0;
        MoveDesiredLane();
    }

    void OnSwipeRight()
    {
        desiredLane++;
        if (desiredLane == 3)
            desiredLane = 2;
        MoveDesiredLane();
    }

    public IEnumerator Wait(float seconds, string type)
    {

        yield return new WaitForSeconds(seconds);
        if (type.Equals("blueSphere"))
        {
            modifiedSpeed = speed;
            Debug.Log($"Speed: {modifiedSpeed}");
            SpeedIncreaseSFX.Stop();
        }
        if (type.Equals("coin"))
        {
            invincibleMode = false;
            Debug.Log($"Invincible Mode: {invincibleMode}");
            InvincibleSFX.Stop();
        }
    }
    public void HitCoin()
    {
        coinsHit++;
        score += LevelBoundary.COIN_INCREMENT;
        Debug.Log($"Score: {score}");
        Debug.Log($"Coins Hit: {coinsHit}");
        scoreText.text = "Score:" + score;
        coinsHitText.text = "Coins Hit:" + coinsHit;
        if (coinsHit == LevelBoundary.COIN_LIMIT)
        {
            coinsHit = 0;
            coinsHitText.text = "Coins Hit:" + coinsHit;
            invincibleMode = true;
            Debug.Log($"Invincible Mode: {invincibleMode}");
            InvincibleSFX.Play();
            StartCoroutine(Wait(LevelBoundary.INVINCIBLE_TIME, "coin"));

        }
    }
    public void HitBlueSphere()
    {
        blueSpheresHit++;
        score += LevelBoundary.BLUE_SPHERE_INCREMENT;
        Debug.Log($"Score: {score}");
        Debug.Log($"Blue Spheres Hit: {blueSpheresHit}");
        scoreText.text = "Score:" + score;
        spheresHitText.text = "Blue Spheres Hit:" + blueSpheresHit;

        if (blueSpheresHit == LevelBoundary.BLUE_SPHERES_LIMIT)
        {
            blueSpheresHit = 0;
            spheresHitText.text = "Blue Spheres Hit:" + blueSpheresHit;
            modifiedSpeed = speed * 2;
            Debug.Log($"Speed: {modifiedSpeed}");
            SpeedIncreaseSFX.Play();
            StartCoroutine(Wait(LevelBoundary.SPEED_CHANGE_TIME, "blueSphere"));
        }
    }
    public void HitIronBall()
    {
        if (!invincibleMode)
        {
            ironBallsHit++;
            score += LevelBoundary.IRON_BALL_INCREMENT;
            Debug.Log($"Score: {score}");
            Debug.Log($"Iron Balls Hit: {ironBallsHit}");
            scoreText.text = "Score:" + score;
            if (score <= 0)
            {
                gameOver = true;
                LevelBoundary.GameOver(LevelBoundary.GAME_OVER_REASON.ZERO_SCORE);
            }
        }
    }
    public void HitBomb()
    {
        if (!invincibleMode)
        {
            gameOver = true;
            LevelBoundary.GameOver(LevelBoundary.GAME_OVER_REASON.BOMB);
        }
    }

    public long getScore()
    {
        return score;
    }
}
