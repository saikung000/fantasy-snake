using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSpawnerManager : MonoSingleton<MapSpawnerManager>
{
    public int gridX = 16, gridZ = 16;
    [SerializeField] private SetupMapScriptableObject setupMapSpawn;
    [SerializeField] private Transform mapParent;
    [SerializeField] private Transform collectHeroParent;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform obstacleParent;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wall;
    [SerializeField] private HeroPresenter heroPrefab;
    [SerializeField] private EnemyPresenter enemyPrefab;
    [SerializeField] private GameObject obstacle1x1Prefab;
    [SerializeField] private GameObject obstacle1x2Prefab;
    [SerializeField] private GameObject obstacle2x1Prefab;
    [SerializeField] private GameObject obstacle2x2Prefab;
    [SerializeField] private SpawnStatScriptableObject spawnStat;
    [SerializeField] private SpawnRateScriptableObject spawnRate;
    [SerializeField] private LayerMask layerMaskCheckObject;
    public int heroLevel = 1;
    public int enemyLevel = 1;
    private List<HeroPresenter> collectHeroList = new List<HeroPresenter>();
    private List<EnemyPresenter> enemyList = new List<EnemyPresenter>();
    private List<GameObject> obstacleList = new List<GameObject>();

    private int StartPositionX => -gridX / 2;
    private int StartPositionZ => -gridZ / 2;

    private int[,] availableGird = new int[0, 0];
    void Start()
    {
        createGrid();
    }

    public void SetupGame()
    {
        availableGird = new int[gridX, gridZ];
        heroLevel = 1;
        enemyLevel = 1;
        clearMap();
        createPlayer();
        createObstacle();
        createCollectHero(setupMapSpawn.collectHero);
        createEnemy(setupMapSpawn.enemy);
    }

    private void clearMap()
    {
        foreach (Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }
        obstacleList.Clear();

        foreach (Transform child in collectHeroParent)
        {
            Destroy(child.gameObject);
        }
        collectHeroList.Clear();

        foreach (Transform child in enemyParent)
        {
            Destroy(child.gameObject);
        }
        enemyList.Clear();
    }

    private void createPlayer()
    {
        HeroPresenter hero = Instantiate(heroPrefab, Vector3.zero, Quaternion.identity);
        var randomStat = spawnStat.RandomHeroStat(1);
        hero.Init(randomStat.hp, randomStat.atk);
        PlayerPresenter.Instance.AddFirstHero(hero);
    }

    private void createObstacle()
    {
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < setupMapSpawn.obstacle2x2; i++)
        {
            spawnPosition = randomPosition(2, 2);
            if (spawnPosition == Vector3.one * 99) break;
            GameObject obstacle = Instantiate(obstacle2x2Prefab, spawnPosition, Quaternion.identity, obstacleParent);
            obstacleList.Add(obstacle);
        }

        for (int i = 0; i < setupMapSpawn.obstacle2x1; i++)
        {
            spawnPosition = randomPosition(2, 1);
            if (spawnPosition == Vector3.one * 99) break;
            GameObject obstacle = Instantiate(obstacle2x1Prefab, spawnPosition, Quaternion.identity, obstacleParent);
            obstacleList.Add(obstacle);
        }

        for (int i = 0; i < setupMapSpawn.obstacle1x2; i++)
        {
            spawnPosition = randomPosition(1, 2);
            if (spawnPosition == Vector3.one * 99) break;
            GameObject obstacle = Instantiate(obstacle1x2Prefab, spawnPosition, Quaternion.identity, obstacleParent);
            obstacleList.Add(obstacle);
        }

        for (int i = 0; i < setupMapSpawn.obstacle1x1; i++)
        {
            spawnPosition = randomPosition();
            if (spawnPosition == Vector3.one * 99) break;
            GameObject obstacle = Instantiate(obstacle1x1Prefab, spawnPosition, Quaternion.identity, obstacleParent);
            obstacleList.Add(obstacle);
        }
    }

    private void createCollectHero(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = randomPosition();
            if (spawnPosition == Vector3.one * 99) break;
            HeroPresenter hero = Instantiate(heroPrefab, spawnPosition, Quaternion.identity, collectHeroParent);
            collectHeroList.Add(hero);
            hero.ChangeDirection((DirectionType)Random.Range(0, 4));
            var randomStat = spawnStat.RandomHeroStat(heroLevel);
            hero.Init(randomStat.hp, randomStat.atk);
        }
        heroLevel++;
    }

    public void SpawnNewCollectHero()
    {
        createCollectHero(spawnRate.GetHeroSpawnAmount());
    }

    private void createEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = randomPosition();
            if (spawnPosition == Vector3.one * 99) break;
            EnemyPresenter enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyParent);
            enemyList.Add(enemy);
            enemy.ChangeDirection((DirectionType)Random.Range(0, 4));
            var randomStat = spawnStat.RandomEnemyStat(enemyLevel);
            enemy.Init(randomStat.hp, randomStat.atk);
        }
        enemyLevel++;
    }

    public void SpawnNewEnemy()
    {
        createEnemy(spawnRate.GetEnemySpawnAmount());
    }


    public Vector3 randomPosition(int sizeX = 1, int sizeZ = 1)
    {
        Vector3 position = new Vector3();
        int x = 0;
        while (x < 100)
        {
            int startPositioX = Random.Range(0, gridX - sizeX + 1);
            int startPositioZ = Random.Range(0, gridZ - sizeZ + 1);
            position.x = startPositioX + StartPositionX;
            position.z = startPositioZ + StartPositionZ;
            Collider[] hit = Physics.OverlapBox(position, new Vector3(sizeX / 2f, 0.5f, sizeZ / 2f), Quaternion.identity, layerMaskCheckObject.value);
            if (hit.Count() == 0)
            {
                break;
            }
            x++;
        }
        if (x == 100)
            return position = Vector3.one * 99;

        return position;
    }



    public Quaternion randomRotation()
    {
        int randomIndex = Random.Range(0, 4);
        Quaternion rotaion = Quaternion.Euler(0, randomIndex * 90, 0);

        return rotaion;
    }


    private void createGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 position = new Vector3(x + StartPositionX, 0f, z + StartPositionZ);
                Instantiate(floor, position, Quaternion.identity, mapParent);
            }
        }

        for (int x = 0; x <= gridX; x++)
        {
            Vector3 positionWallBottom = new Vector3(x + StartPositionX, 0.5f, StartPositionZ - 1);
            Instantiate(wall, positionWallBottom, Quaternion.identity, mapParent);
            Vector3 positionWallTop = new Vector3(x + StartPositionX - 1, 0.5f, gridZ + StartPositionZ);
            Instantiate(wall, positionWallTop, Quaternion.identity, mapParent);
        }

        for (int z = 0; z <= gridZ; z++)
        {
            Vector3 positionWallLeft = new Vector3(StartPositionX - 1, 0.5f, z + StartPositionZ - 1);
            Instantiate(wall, positionWallLeft, Quaternion.identity, mapParent);
            Vector3 positionWallRight = new Vector3(gridX + StartPositionX, 0.5f, z + StartPositionZ);
            Instantiate(wall, positionWallRight, Quaternion.identity, mapParent);
        }
    }

    public void RemoveCollectHero(HeroPresenter herePresenter)
    {
        collectHeroList.Remove(herePresenter);
    }

    public void RemoveEnemy(EnemyPresenter herePresenter)
    {
        enemyList.Remove(herePresenter);
    }
}
