using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spwanPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        int animalIndex = Random.Range(0, obstaclePrefabs.Length);

        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefabs[animalIndex], spwanPos, obstaclePrefabs[animalIndex].transform.rotation);
        }
    }
}
