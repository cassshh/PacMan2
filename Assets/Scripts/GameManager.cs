﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PacBoard boardScript;
    public static bool startWorking = false;

    // Use this for initialization
    void Awake()
    {
        boardScript = GetComponent<PacBoard>();
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
