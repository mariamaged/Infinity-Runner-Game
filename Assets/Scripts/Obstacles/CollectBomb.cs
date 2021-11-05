using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBomb : MonoBehaviour
{
    private AudioSource BombsSFX;
    private Player Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        BombsSFX = GameObject.Find("LevelBoundary").transform.GetChild(3).GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BombsSFX.Play();
            this.gameObject.SetActive(false);
            Player.HitBomb();
        }
    }
}
