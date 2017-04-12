using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;

public class Worker : MovingObject
{
    private const int UP = 0;
    private const int RIGHT = 1;
    private const int DOWN = 2;
    private const int LEFT = 3;

    private int currentDir = 0;
    private int stepsLeft = 0;

    private int c = 0;

    private bool start = false;
    private bool hasMoved = false;

    public GameObject spawnItem;
    public GameObject spawnOnDeath;

    public void StartWorker(int dir)
    {
        currentDir = dir;
        stepsLeft = GetSteps();
        start = true;
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (c >= 20)
            {
                c = 0;
            }
            if (c == 0)
            {
                if (stepsLeft <= 0)
                {
                    NewDirection();
                }
                Move(currentDir);
                stepsLeft--;
            }
            c++;
        }
    }

    private void NewDirection()
    {
        int newDir = Random.Range(0, 4);
        while (currentDir == newDir || GetOpposite(currentDir) == newDir)
        {
            newDir = Random.Range(0, 4);
        }
        currentDir = newDir;
        stepsLeft = GetSteps();
    }

    private int GetOpposite(int dir)
    {
        switch (dir)
        {
            case UP:
                return DOWN;
            case RIGHT:
                return LEFT;
            case DOWN:
                return UP;
            case LEFT:
                return RIGHT;
        }
        return -1;
    }

    private void Move(int dir)
    {
        int x;
        int y;
        DirToPos(dir, out x, out y);

        AttemptMove<Wall>(x, y);
    }

    private int GetSteps()
    {
        int s = Random.Range(2, 8);
        if (s % 2 != 0) return s + 1;
        return s;
    }

    private void DirToPos(int dir, out int x, out int y)
    {
        x = 0;
        y = 0;
        switch (dir)
        {
            case UP:
                y = 1;
                break;
            case RIGHT:
                x = 1;
                break;
            case DOWN:
                y = -1;
                break;
            case LEFT:
                x = -1;
                break;
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
        if (canMove)
        {
            gameObject.SetActive(false);
            int r = Random.Range(0, 5);
            if (r == 0)
            {
                Instantiate(spawnOnDeath, gameObject.transform.position,
                Quaternion.identity);
            }
            else
            {
                Instantiate(spawnItem, gameObject.transform.position, Quaternion.identity);
            }
            return;
        }
        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();
        if (!canMove && hitComponent != null)
        {
            if (!hasMoved) hasMoved = true;
            OnCantMove(hitComponent);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Worker" && hasMoved)
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall wall = component as Wall;
        Vector2 pos = wall.transform.position;
        wall.RemoveWall();

        Instantiate(spawnItem, gameObject.transform.position, Quaternion.identity);

        StartCoroutine(SmoothMovement(pos));
    }
}