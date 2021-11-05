using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBlueSpheres : MonoBehaviour
{
    private AudioSource CollectibleSFX;
    private Player Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        CollectibleSFX = GameObject.Find("LevelBoundary").transform.GetChild(0).GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectibleSFX.Play();
            this.gameObject.SetActive(false);
            Player.HitBlueSphere();
        }
    }
}
