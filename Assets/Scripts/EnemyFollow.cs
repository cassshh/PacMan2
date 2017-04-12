using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    private GameObject Player;
    public float speed;

    void Start()
    {
        this.GetComponent<Rigidbody2D>().freezeRotation = true;
        Player = GameObject.Find("Player");
        if (!Player)
        {
            Player = GameObject.Find("Player(Clone)");
        }
    }

    void Update()
    {
        if (!GameManager.startWorking) return;
        if (Player)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }
    }

}
