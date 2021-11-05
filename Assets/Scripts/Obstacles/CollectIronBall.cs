using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectIronBall : MonoBehaviour
{
    private AudioSource IronBallsSFX;
    private Player Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        IronBallsSFX = GameObject.Find("LevelBoundary").transform.GetChild(3).GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IronBallsSFX.Play();
            this.gameObject.SetActive(false);
            Player.HitIronBall();
        }
    }
}
