using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnerView : MonoBehaviour
{
    public int gridX = 16, gridZ = 16;
    public GameObject floor;
    public GameObject wall;

    void Start()
    {
        createGrid();
    }

    private void createGrid()
    {
        int startPositionX = -gridX / 2;
        int startPositionZ = -gridZ / 2;
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 position = new Vector3(x + startPositionX, 0f, z + startPositionZ);
                Instantiate(floor, position, Quaternion.identity, transform);
            }
        }


        for (int x = 0; x <= gridX; x++)
        {
            Vector3 positionWallBottom = new Vector3(x + startPositionX, 0.5f, startPositionZ - 1);
            Instantiate(wall, positionWallBottom, Quaternion.identity, transform);
            Vector3 positionWallTop = new Vector3(x + startPositionX - 1, 0.5f, gridZ + startPositionZ);
            Instantiate(wall, positionWallTop, Quaternion.identity, transform);
        }

        for (int z = 0; z <= gridZ; z++)
        {
            Vector3 positionWallLeft = new Vector3(startPositionX -1 , 0.5f, z  + startPositionZ - 1);
            Instantiate(wall, positionWallLeft, Quaternion.identity, transform);
            Vector3 positionWallRight = new Vector3(gridX + startPositionX, 0.5f, z + startPositionZ);
            Instantiate(wall, positionWallRight, Quaternion.identity, transform);
        }
    }

}
