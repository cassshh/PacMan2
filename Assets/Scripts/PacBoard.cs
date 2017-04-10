using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class PacBoard : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            min = minimum;
            max = maximum;
        }
    }

    public int columns = 10;
    public int rows = 10;
    public Count workerCount = new Count(3, 7);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] workerTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns; x++)
        {
            for (int y = 1; y < rows; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x <= columns + 1; x++)
        {
            for (int y = -1; y <= rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns + 1 || y == -1 || y == rows + 1)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int random = Random.Range(0, gridPositions.Count);
        Vector3 position = gridPositions[random];
        int x = (int) position.x;
        int y = (int) position.y;
        if (x % 2 != 0 || y % 2 != 0)
        {
            return RandomPosition();
        }
        gridPositions.RemoveAt(random);
        return position;
    }

    void LayoutAtRandom(GameObject[] tiles, int min, int max)
    {
        int random = Random.Range(min, max + 1);

        for (int i = 0; i < random; i++)
        {
            Vector3 position = RandomPosition();
            GameObject tile = tiles[Random.Range(0, tiles.Length)];
            Instantiate(tile, position, Quaternion.identity);
        }
    }

    void FillRestOfGrid(GameObject[] tiles)
    {
        foreach (Vector3 gridPosition in gridPositions)
        {
            GameObject overlay = tiles[Random.Range(0, tiles.Length)];
            overlay.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
            Instantiate(overlay, gridPosition, Quaternion.identity);
        }
    }

    public void SetupScene()
    {
        BoardSetup();
        InitList();
        LayoutAtRandom(workerTiles, workerCount.minimum, workerCount.maximum);
        FillRestOfGrid(wallTiles);
    }


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}