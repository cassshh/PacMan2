using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Task();

public class TaskScript : MonoBehaviour {

    private Queue<Task> TaskQueue = new Queue<Task>();
    private object _queueLock = new object();
	
	// Update is called once per frame
	void Update () {
	    lock (_queueLock)
	    {
	        if (TaskQueue.Count > 0)
	        {
	            Task task = TaskQueue.Dequeue();
                task.Invoke();
	        }
	    }
	}

    public void ScheduleTask(Task task)
    {
        lock (_queueLock)
        {
            if (TaskQueue.Count < 100)
            {
                TaskQueue.Enqueue(task);
                Debug.Log("Added Task");
            }
        }
    }
}
