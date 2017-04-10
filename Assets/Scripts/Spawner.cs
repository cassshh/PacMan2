using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    private List<int> dirs = new List<int>{UP,RIGHT,DOWN,LEFT};
    public GameObject worker;
    public GameObject[] ghosts;

    private const int UP = 0;
    private const int RIGHT = 1;
    private const int DOWN = 2;
    private const int LEFT = 3;

    // Use this for initialization
    void Start ()
	{
        GameObject taskExec = GameObject.Find("TaskObject");
        TaskScript taskScript = taskExec.GetComponent<TaskScript>();

        int workers = Random.Range(2, 5);
	    for (int i = 0; i < workers; i++)
	    {
	        int rndDir = Random.Range(0, dirs.Count);
	        int dir = dirs[rndDir];
            dirs.RemoveAt(rndDir);

            taskScript.ScheduleTask(new Task(delegate
            {
                GameObject obj = Instantiate(worker, GetPosition(dir), Quaternion.identity);

                ((Worker)obj.GetComponent(typeof(Worker))).StartWorker(dir);
            }));
        }

	    Instantiate(ghosts[Random.Range(0, ghosts.Length)], gameObject.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private Vector3 GetPosition(int dir)
    {
        Vector3 pos = gameObject.transform.position;
        switch (dir)
        {
            case UP:
                pos.y++;
                break; 
            case RIGHT:
                pos.x++;
                break;
            case DOWN:
                pos.y--;
                break;
            case LEFT:
                pos.x--;
                break;
        }
        return pos;
    }
}
