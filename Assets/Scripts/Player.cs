using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MovingObject
{
    private bool move = true;
    public int pointsPerDot = 10;

    //private Animator animator;
    //private int score;

    // Use this for initialization
    protected override void Start()
    {
        //animator = GetComponent<Animator>();

        //score = 0;

        base.Start();
    }

    private void OnDisable()
    {
        //Set score
    }

    private void Update()
    {
        if (!GameManager.startWorking) return;
        if (move)
        {
            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            if (horizontal != 0)
            {
                vertical = 0;
            }

            if (horizontal != 0 || vertical != 0)
            {
                move = false;
                AttemptMove<Wall>(horizontal, vertical);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGERD");
        if (other.tag == "Dot")
        {
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "BigDot")
        {
            other.gameObject.SetActive(false);
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
        if (!canMove) move = true;
        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();
        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    protected override IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
        move = true;
    }

    protected override void OnCantMove<T>(T component)
    {
        //throw new System.NotImplementedException();
    }
}
