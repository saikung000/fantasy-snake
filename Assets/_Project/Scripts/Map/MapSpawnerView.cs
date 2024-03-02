using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSpawnerView : MonoSingleton<MapSpawnerView>
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
    public int heroLevel = 1;
    public int enemyLevel = 1;
    private List<HeroPresenter> collectHeroList = new List<HeroPresenter>();
    private List<EnemyPresenter> enemyList = new List<EnemyPresenter>();
    private List<GameObject> obstacleList = new List<GameObject>();

    private int StartPositionX => -gridX / 2;
    private int StartPositionZ => -gridZ / 2;

    void Start()
    {
        createGrid();
    }

    public void SetupGame()
    {
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
        for (int i = 0; i < setupMapSpawn.obstacle2x2; i++)
        {
            GameObject obstacle = Instantiate(obstacle2x2Prefab, randomPosition(), randomRotation(), obstacleParent);
            obstacleList.Add(obstacle);
        }

        for (int i = 0; i < setupMapSpawn.obstacle2x1; i++)
        {
            GameObject obstacle = Instantiate(obstacle2x1Prefab, randomPosition(), randomRotation(), obstacleParent);
            obstacleList.Add(obstacle);
        }

        for (int i = 0; i < setupMapSpawn.obstacle2x1; i++)
        {
            GameObject obstacle = Instantiate(obstacle1x2Prefab, randomPosition(), randomRotation(), obstacleParent);
            obstacleList.Add(obstacle);
        }

        for (int i = 0; i < setupMapSpawn.obstacle1x1; i++)
        {
            GameObject obstacle = Instantiate(obstacle1x1Prefab, randomPosition(), randomRotation(), obstacleParent);
            obstacleList.Add(obstacle);
        }
    }

    private void createCollectHero(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            HeroPresenter hero = Instantiate(heroPrefab, randomPosition(), Quaternion.identity, collectHeroParent);
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
            EnemyPresenter enemy = Instantiate(enemyPrefab, randomPosition(), Quaternion.identity, enemyParent);
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

    public Vector3 randomPosition()
    {
        int randomIndex = Random.Range(0, gridX * gridZ);
        Vector3 position = new Vector3();
        position.x = randomIndex % gridZ + StartPositionX;
        position.z = randomIndex / gridZ + StartPositionZ;
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
