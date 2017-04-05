using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {
	public GameObject LobbyManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void multiplayerOnclick(){
		SceneManager.LoadScene(1);
		LobbyManager.SetActive (true);
	}

	public void menuOnclick(){
		SceneManager.LoadScene(0);
	}
}
