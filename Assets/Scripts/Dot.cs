using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MovingObject {

	// Use this for initialization
    protected override void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnCantMove<T>(T component)
    {
        throw new System.NotImplementedException();
    }
}
