using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandom : MonoBehaviour
{

    public LayerMask wall;
    public float speed;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;

    // Use this for initialization
    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
        myBody.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * (myWidth / 400) +
                              Vector2.up * (myHeight / 400);
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down * 0.6f);
        RaycastHit2D playerDown = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down * 0.6f, wall);

        Debug.DrawLine(lineCastPos, lineCastPos - Vector2.down * 0.6f);
        RaycastHit2D playerUp = Physics2D.Linecast(lineCastPos, lineCastPos - Vector2.down * 0.6f, wall);

        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.6f);
        RaycastHit2D playerLeft = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.6f, wall);

        Debug.DrawLine(lineCastPos, lineCastPos + myTrans.right.toVector2() * 0.6f);
        RaycastHit2D playerRight = Physics2D.Linecast(lineCastPos, lineCastPos + myTrans.right.toVector2() * 0.6f, wall);

        Vector2 myVel = myBody.velocity;
        myVel.x = 0;
        myVel.y = 0;
        int randomDirection = 0;

        if (playerUp.collider || playerDown.collider || playerLeft.collider || playerRight.collider)
        {
            randomDirection = Random.Range(1, 5);
            if (playerDown.collider && randomDirection != 3)
            {
                myBody.transform.position += Vector3.up* 11f *Time.deltaTime;
                myVel.y += 0.1f;
                myVel.x = 0;
            }
            if (playerUp.collider && randomDirection != 4)
            {
                myBody.transform.position += Vector3.down * 11f * Time.deltaTime;
                myVel.y += -0.1f;
                myVel.x = 0;
            }
            if (playerLeft.collider && randomDirection != 1)
            {
                myBody.transform.position += Vector3.right * 11f * Time.deltaTime;
                myVel.x += 0.1f;
                myVel.y = 0;
            }
            if (playerRight.collider && randomDirection != 2)
            {
                myBody.transform.position += Vector3.left * 11f * Time.deltaTime;
                myVel.x += -0.1f;
                myVel.y = 0;
            }
        }

        switch (randomDirection)
        {
            case 1: //left
                myVel.y = 0;
                myVel.x = -myTrans.right.x * speed;
                myBody.velocity = myVel;
                break;
            case 2: //right
                myVel.y = 0;
                myVel.x = myTrans.right.x * speed;
                myBody.velocity = myVel;
                break;
            case 3: //down
                myVel.x = 0;
                myVel.y = -Vector2.down.y * speed;
                myBody.velocity = myVel;
                break;
            case 4: //up
                myVel.x = 0;
                myVel.y = Vector2.down.y * speed;
                myBody.velocity = myVel;
                break;
        }
    }
}