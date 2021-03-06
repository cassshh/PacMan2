﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
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
    public GameObject player;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    private GameObject taskExec;
    private TaskScript taskScript;

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
        int x = (int)position.x;
        int y = (int)position.y;
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
        for (int i = 0; i < gridPositions.Count; i++)
        {
            int j = i;
            int count = gridPositions.Count;
            Vector3 gridPosition = gridPositions[i];
            taskScript.ScheduleTask(new Task(delegate
            {
                GameObject overlay = tiles[Random.Range(0, tiles.Length)];
                overlay.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
                Instantiate(overlay, gridPosition, Quaternion.identity);
                if (j == (count - 1))
                {
                    GameManager.startWorking = true;
                }
            }));
        }
    }

    public void SetupScene()
    {
        taskExec = GameObject.Find("TaskObject");
        taskScript = taskExec.GetComponent<TaskScript>();
        taskScript.ScheduleTask(new Task(BoardSetup));
        taskScript.ScheduleTask(new Task(InitList));
        taskScript.ScheduleTask(new Task(delegate
        {
            LayoutAtRandom(workerTiles, workerCount.minimum, workerCount.maximum);
            FillRestOfGrid(wallTiles);
            Instantiate(player, new Vector3(0, 0, 0f), Quaternion.identity);
        }));
    }
}