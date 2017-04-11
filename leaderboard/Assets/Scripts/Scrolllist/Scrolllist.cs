using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scrolllist : MonoBehaviour {

	private static Scrolllist instance;
	
	public static Scrolllist Instance
	{
		get { return instance; }
	}
	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this) {
			Destroy (gameObject);
			return;
		}
	}
//____________________________________________________________

	public GameObject ScrollEntry;
	public GameObject ScrollContain;
	public int yourPosition;
	public GameObject LoadingText;
	public bool loading = true;

	void Update () {
	
		if (!loading)
			LoadingText.SetActive (false);
		else
			LoadingText.SetActive (true);
	}

	public void getScrollEntrys()
	{
		foreach (Transform childTransform in ScrollContain.transform) Destroy(childTransform.gameObject);

		int j = 1;
		for (int i=0; i<HSController.Instance.onlineHighscore.Length-1; i++) {
			GameObject ScorePanel;
			ScorePanel = Instantiate (ScrollEntry) as GameObject;
			ScorePanel.transform.parent = ScrollContain.transform;
			ScorePanel.transform.localScale = ScrollContain.transform.localScale;
			Transform ThisScoreName = ScorePanel.transform.Find ("ScoreText");
			Text ScoreName = ThisScoreName.GetComponent<Text> ();
			//
			Transform ThisScorePoints = ScorePanel.transform.Find ("ScorePoints");
			Text ScorePoints = ThisScorePoints.GetComponent<Text> ();
			//
			Transform ThisScorePosition = ScorePanel.transform.Find ("ScorePosition");
			Text ScorePosition = ThisScorePosition.GetComponent<Text> ();

			//first position is yellow
			if (j==1)
			{
				ScoreName.color=Color.yellow;
				ScorePoints.color=Color.yellow;
				ScorePosition.color=Color.yellow;
			}
			ScorePosition.text = j+". ";
			string helpString = "";

			helpString = helpString+HSController.Instance.onlineHighscore [i]+" ";
			i++;

			ScoreName.text = helpString;

			//
			ScorePoints.text = HSController.Instance.onlineHighscore [i];

			if(HSController.Instance.onlineHighscore [i]=="9999")
			{
				ScoreName.color=Color.red;
				ScorePoints.color=Color.red;
				ScorePosition.color=Color.red;
				yourPosition = j;
			}
			j++;

		}

	}
}
