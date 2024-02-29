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
    [SerializeField] private HeroView heroPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject obstacle1x1Prefab;
    [SerializeField] private GameObject obstacle1x2Prefab;
    [SerializeField] private GameObject obstacle2x1Prefab;
    [SerializeField] private GameObject obstacle2x2Prefab;

    public List<HeroView> collectHeroList = new List<HeroView>();
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> obstacleList = new List<GameObject>();

    private int StartPositionX => -gridX / 2;

    private int StartPositionZ => -gridZ / 2;

    void Start()
    {
        createGrid();
    }

    public void SetupGame()
    {
        createPlayer();
        createObstacle();
        createCollectHero();
        createEnemy();
    }

    private void createPlayer()
    {
        HeroView heroView = Instantiate(heroPrefab, Vector3.zero, Quaternion.identity);
        PlayerPresenter.Instance.AddHero(heroView);
    }

    private void createObstacle()
    {
        foreach (GameObject child in obstacleParent)
        {
            Destroy(child);
        }
        obstacleList.Clear();

        for (int i = 0; i < setupMapSpawn.obstacle1x1; i++)
        {
            GameObject obstacle = Instantiate(obstacle1x1Prefab, randomPosition(), randomRotation(), obstacleParent);
            obstacleList.Add(obstacle);
        }
    }

    private void createCollectHero()
    {

        foreach (GameObject child in collectHeroParent)
        {
            Destroy(child);
        }
        collectHeroList.Clear();

        for (int i = 0; i < setupMapSpawn.collectHero; i++)
        {
            HeroView heroView = Instantiate(heroPrefab, randomPosition(), randomRotation(), collectHeroParent);
            collectHeroList.Add(heroView);
        }
    }

    private void createEnemy()
    {
        foreach (GameObject child in enemyParent)
        {
            Destroy(child);
        }
        enemyList.Clear();


        for (int i = 0; i < setupMapSpawn.enemy; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, randomPosition(), randomRotation(), enemyParent);
            enemyList.Add(enemy);
        }
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


    public void RemoveCollectHero(HeroView heroView)
    {
        collectHeroList.Remove(heroView);
    }

    public void RemoveEnemy()
    {

    }
}
