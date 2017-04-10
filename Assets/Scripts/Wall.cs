using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
	{

	}

    public void RemoveWall()
    {
        gameObject.SetActive(false);
    }
}
