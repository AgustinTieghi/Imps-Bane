using PathCreation;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;  
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private GameObject startButton;
    [SerializeField] private ManagementScript manager;
    [SerializeField] private int pathIndex; 
    [SerializeField] private int waveIndex;
    [SerializeField] private List<PathCreator> pathCreators;
    private EnemyScript spawnedScript;
    private Wave currentWave;
    private bool firstWave = true;

    public float timer;
    public float setupTime;
    public int enemiesLeft; 
    public bool inSetupTime = false;

    private void Start()
    {
        startButton.SetActive(false);
        StartWave();
        timer = setupTime;
    }
    void Update()
    {      
        if (enemiesLeft == 0 && waveIndex < waves.Count)
        {
            firstWave = false;
            timer -= Time.deltaTime;
            inSetupTime = true;
            if (waveIndex + 1 > waves.Count - 1)
            {
                SceneManager.LoadScene("YouWon");
            }
            if (timer <= 0)
            {
                StartWave();
                inSetupTime = false;
                timer = setupTime;
            }
        }        
    }

    IEnumerator SpawnNewWave()
    {
        for (int i = 0; i < currentWave.enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, currentWave.enemies.Count);

            GameObject spawnedEnemy = Instantiate(currentWave.enemies[randomEnemy], this.transform.position, Quaternion.identity);
            spawnedEnemy.GetComponent<PathFollower>().pathCreator = pathCreators[pathIndex];
            spawnedScript = spawnedEnemy.GetComponent<EnemyScript>();
            spawnedScript.manager = this.manager;
            spawnedScript.spawner = this;
            yield return new WaitForSeconds(1);
        }
        
    }

    void ChangeEnemyPath()
    {
        if (pathIndex >= pathCreators.Count - 1)
        {
            pathIndex = 0;
        }
        else
        {
            pathIndex++;
        }
    }

    private void StartWave()
    {
        ChangeEnemyPath();
        //Esto lo hice porque sino se skipea la primera wave
        if(firstWave == false)
        {
            waveIndex++;
            
        }    
        currentWave = waves[waveIndex];
        enemiesLeft = currentWave.enemiesToSpawn;

        StartCoroutine(SpawnNewWave());
    }
    public void SetSpawnTimerToZero()
    {
        timer = 0;
    }
}
