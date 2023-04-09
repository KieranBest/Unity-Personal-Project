using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnSide;
    public int enemyCount;
    
    [SerializeField] int waveNumber = 1;
    [SerializeField] TextMeshProUGUI levelText;

    private int zAxis = 6;
    private int xAxis = 10;
    private float start = 5.0f;
    private float spawnTime = 15.0f;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        InvokeRepeating("GeneratePowerUp", start, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyMovement>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
        levelText.SetText("Level: " + waveNumber);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateEnemySpawnPosition(), enemyPrefab.transform.rotation);
        }

    }

    private Vector3 GenerateEnemySpawnPosition()
    {
        int generateXAxis = Random.Range(-xAxis, xAxis);
        int generateZAxis = Random.Range(-zAxis, zAxis);
        Vector3 randomPos = new Vector3(0, 0, 0);

        switch (GenerateEnemySpawnSide())
        {
            case 1:
                randomPos = new Vector3(xAxis, 0, generateZAxis);
                break;
            case 2:
                randomPos = new Vector3(-xAxis, 0, generateZAxis);
                break;
            case 3:
                randomPos = new Vector3(generateXAxis, 0, zAxis);
                break;
            case 4:
                randomPos = new Vector3(generateXAxis, 0, -zAxis);
                break;
        }
        return randomPos;
    }

    private int GenerateEnemySpawnSide()
    {
        int spawnSide = Random.Range(1,4);

        return spawnSide;
    }

    void GeneratePowerUp()
    {
        Vector3 randomPos = new Vector3(Random.Range(-9,9), 0, Random.Range(-3, 3));
        Instantiate(powerupPrefab, randomPos, powerupPrefab.transform.rotation);
    }
}
