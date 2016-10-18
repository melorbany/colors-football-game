using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ArabicSupport;

public class PlayerScoreList : MonoBehaviour {

	public GameObject playerScoreEntryPrefab;

	ScoreManager scoreManager;

	int lastChangeCounter;

	// Use this for initialization
	void Start () {
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		lastChangeCounter = scoreManager.GetChangeCounter();

		if(scoreManager == null) {
			Debug.LogError("You forgot to add the score manager component to a game object!");
			return;
		}


		//		for (int i = 0; i < 5; i++) {
		//			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
		//		}

		//		if(scoreManager.GetChangeCounter() == lastChangeCounter) {
		//			// No change since last update!
		//			Debug.Log("Player Score Update 2"+ scoreManager.GetChangeCounter());
		//			return;
		//		}

		lastChangeCounter = 0;


		while(this.transform.childCount > 0) {
			Transform c = this.transform.GetChild(0);
			c.SetParent(null);  // Become Batman
			Destroy (c.gameObject);
		}



		for (int i = 0; i < 5; i++) {

			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(this.transform,false);

			if (i == 3) {
				go.transform.GetComponent<Image> ().color = Color.grey;
			}
			Debug.Log (ArabicFixer.Fix ("آهلاوي وافتخر", true, true));
			go.transform.Find ("Name").GetComponent<Text> ().text = ArabicFixer.Fix ("آهلاوي وافتخر", true, true);
			go.transform.Find ("Team").GetComponent<Text> ().text = ArabicFixer.Fix ("النادي الاهلي المصري", true, true);
			go.transform.Find ("Score").GetComponent<Text> ().text = ArabicFixer.Fix ("7987", true, false);
		}

		return;

		string[] names = scoreManager.GetPlayerNames("kills");

		foreach(string name in names) {

			Debug.Log("Player Score Update 3" + name + "Length : " + names.Length );

			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(this.transform);
			go.transform.Find ("Name").GetComponent<Text>().text = name;
			go.transform.Find ("Team").GetComponent<Text>().text = scoreManager.GetScore(name, "kills").ToString();
			go.transform.Find ("Score").GetComponent<Text>().text = scoreManager.GetScore(name, "deaths").ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(scoreManager == null) {
			Debug.LogError("You forgot to add the score manager component to a game object!");
			return;
		}


		return;


//
//
////		for (int i = 0; i < 5; i++) {
////			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
////		}
//
////		if(scoreManager.GetChangeCounter() == lastChangeCounter) {
////			// No change since last update!
////			Debug.Log("Player Score Update 2"+ scoreManager.GetChangeCounter());
////			return;
////		}
//
//		lastChangeCounter = scoreManager.GetChangeCounter();
//
//		while(this.transform.childCount > 0) {
//			Transform c = this.transform.GetChild(0);
//			c.SetParent(null);  // Become Batman
//			Destroy (c.gameObject);
//		}
//
//
//		string[] names = scoreManager.GetPlayerNames("kills");
//		
//		foreach(string name in names) {
//
//			Debug.Log("Player Score Update 3" + name + "Length : " + names.Length );
//
//			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
//			go.transform.SetParent(this.transform);
//			go.transform.Find ("Name").GetComponent<Text>().text = name;
//			go.transform.Find ("Team").GetComponent<Text>().text = scoreManager.GetScore(name, "kills").ToString();
//			go.transform.Find ("Score").GetComponent<Text>().text = scoreManager.GetScore(name, "deaths").ToString();
//		}
	}
}
