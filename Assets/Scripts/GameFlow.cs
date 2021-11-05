using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameFlow : MonoBehaviour
{
    public GameObject[] Prefabs;
    public int NumTilesScreen;
    public LayerMask hittableMask;
    private Transform PlayerTransform;
    private float SpawnZ;
    private float TileLength = 7.0f;
    private float LeftArea;
    private float NumTiles;
    void Start()
    {
        SpawnZ = TileLength * NumTilesScreen;
        LeftArea = TileLength * 2;
        NumTiles = NumTilesScreen;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 0; i < NumTilesScreen; i++)
        {
            RandomCollectibleGenerator(transform.GetChild(i).gameObject);
            RandomObstacleGenerator(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        if (PlayerTransform.position.z - LeftArea > SpawnZ - (TileLength * NumTilesScreen))
        {
            GameObject tile = SpawnTile();
            RandomCollectibleGenerator(tile);
            RandomObstacleGenerator(tile);
            DestroyTile();
        }
    }
    private GameObject SpawnTile(int prefabIndex = -1)
    {
        GameObject tile = Instantiate(Prefabs[0]);
        tile.transform.SetParent(transform);
        tile.transform.position = Vector3.forward * SpawnZ;
        SpawnZ += TileLength;
        NumTiles++;
        return tile;
    }

    private void DestroyTile()
    {
        Destroy(transform.GetChild(0).gameObject);
        NumTiles--;
    }

    private void RandomCollectibleGenerator(GameObject parent)
    {
        int num = Random.Range(0, 4);
        HashSet<int> set = new HashSet<int>();
        for(int i = 0; i < 3; i++)
        {
            set.Add(i);
        }
        HashSet<string> collectibleTypes = new HashSet<string>();
        collectibleTypes.Add("coin");
        collectibleTypes.Add("blueSphere");

        while (num-- > 0)
        {
            int index = Random.Range(0, set.Count);
            int entry = set.ElementAt(index);
            set.Remove(entry);

            float X = entry == 0 ? -LevelBoundary.leftSide : 
                (entry == 1 ? 0 : 
                LevelBoundary.rightSide);
            float Y = Random.Range(0.16f, 0.25f);

            float oldSpawn = SpawnZ - TileLength;
            float Z = Random.Range(oldSpawn - (TileLength / 2), SpawnZ + (oldSpawn / 2));
            int collectibleIndex = Random.Range(0, collectibleTypes.Count);
            string collectibleEntry = collectibleTypes.ElementAt(collectibleIndex);
            GameObject collectible = collectibleEntry.Equals("coin") ? Instantiate(Prefabs[1]) : Instantiate(Prefabs[2]);
            collectible.transform.position = new Vector3(X, Y, Z);
            //if (!collectibleEntry.Equals("coin"))
            //{
            //    var radius = collectible.GetComponent<SphereCollider>().radius;
            //    while (Physics.CheckSphere(collectible.transform.position, radius, hittableMask)) 
            //    {
            //        collectible.transform.position = new Vector3(X, Y, collectible.transform.position.z + radius);
            //    }
            //}
            //else
            //{
            //    var collider = collectible.GetComponent<CapsuleCollider>();
            //    var radius = collider.radius;
            //    var start = collider.bounds.center;
            //    var right = new Vector3(collider.bounds.center.x, collider.bounds.center.y, collider.bounds.min.z);
            //    var left = new Vector3(collider.bounds.center.x, collider.bounds.center.y, collider.bounds.max.z);
            //    while (Physics.CheckCapsule(start, right, radius, hittableMask) || Physics.CheckCapsule(start, left, radius, hittableMask)) 
            //    {
            //        collectible.transform.position = new Vector3(X, Y, collectible.transform.position.z + radius);
            //    }
            //}
            collectible.transform.SetParent(parent.transform);
        }
    }

    private void RandomObstacleGenerator(GameObject parent)
    {
        int num = Random.Range(0, 4);
        HashSet<int> set = new HashSet<int>();
        for (int i = 0; i < 3; i++)
        {
            set.Add(i);
        }
        HashSet<string> obstacleTypes = new HashSet<string>();
        obstacleTypes.Add("ironBall");
        obstacleTypes.Add("bomb");

        while (num-- > 0)
        {
            int index = Random.Range(0, set.Count);
            int entry = set.ElementAt(index);
            set.Remove(entry);

            float X = entry == 0 ? -LevelBoundary.leftSide :
                (entry == 1 ? 0 :
                LevelBoundary.rightSide);
            float Y = Random.Range(0.16f, 0.25f);

            float oldSpawn = SpawnZ - TileLength;
            float Z = Random.Range(oldSpawn - (TileLength / 2), SpawnZ + (oldSpawn / 2));
            int obstacleIndex = Random.Range(0, obstacleTypes.Count);
            string obstacleEntry = obstacleTypes.ElementAt(obstacleIndex);
            GameObject obstacle = obstacleEntry.Equals("ironBall") ? Instantiate(Prefabs[3]) : Instantiate(Prefabs[4]);
            obstacle.transform.position = new Vector3(X, Y, Z);
            //var radius = obstacle.GetComponent<SphereCollider>().radius;
            //while (Physics.CheckSphere(obstacle.transform.position, radius, hittableMask)) 
            //{
            //    obstacle.transform.position = new Vector3(X, Y, obstacle.transform.position.z + radius);
            //}
            obstacle.transform.SetParent(parent.transform);
        }
    }
}
