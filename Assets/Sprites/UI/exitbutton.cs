using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitbutton : MonoBehaviour {

	public void exitButtonClicked()
	{
		Application.Quit();
		Debug.Log ("Exit Button Clicked");
	}
}
