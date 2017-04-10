using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public LayerMask PlayerMask;
    public float speed;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;

    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
        myBody.freezeRotation = true;
    }

    void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * (myWidth / 400)+ Vector2.up * (myHeight / 400);
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down * 5f);
        RaycastHit2D playerDown = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down * 5f, PlayerMask);

        Debug.DrawLine(lineCastPos, lineCastPos - Vector2.down * 5f);
        RaycastHit2D playerUp = Physics2D.Linecast(lineCastPos, lineCastPos - Vector2.down * 5f, PlayerMask);
        
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * 5f);
        RaycastHit2D playerLeft = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * 5f, PlayerMask);

        Debug.DrawLine(lineCastPos, lineCastPos + myTrans.right.toVector2() * 5f);
        RaycastHit2D playerRight = Physics2D.Linecast(lineCastPos, lineCastPos + myTrans.right.toVector2() * 5f, PlayerMask);
        
        if ((playerDown.collider != null && playerDown.collider.tag == "Player") || (playerUp.collider != null && playerUp.collider.tag == "Player") || (playerLeft.collider != null && playerLeft.collider.tag == "Player") || (playerRight.collider != null && playerRight.collider.tag == "Player"))
        {
            Vector2 myVel = myBody.velocity;

            if (playerDown.collider != null && playerDown.collider.tag == "Player")
            {
                myVel.x = 0;
                myVel.y = Vector2.down.y * speed;
                myBody.velocity = myVel;
            }
            else if ((playerUp.collider != null && playerUp.collider.tag == "Player"))
            {
                myVel.x = 0;
                myVel.y = -Vector2.down.y * speed;
                myBody.velocity = myVel;
            }
            else if ((playerLeft.collider != null && playerLeft.collider.tag == "Player"))
            {
                myVel.y = 0;
                myVel.x = -myTrans.right.x * speed;
                myBody.velocity = myVel;
            }
            else if ((playerRight.collider != null && playerRight.collider.tag == "Player"))
            {
                myVel.y = 0;
                myVel.x = myTrans.right.x * speed;
                myBody.velocity = myVel;
            }
        }
    }
}