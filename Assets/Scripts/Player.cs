using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{

    public int pointsPerDot = 10;

    //private Animator animator;
    //private int score;

	// Use this for initialization
	protected override void Start ()
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
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int) (Input.GetAxisRaw("Horizontal"));
        vertical = (int) (Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Dot>(horizontal, vertical);
        }
    }


    protected override void OnCantMove<T>(T component)
    {
        //score += pointsPerDot;
    }
}
