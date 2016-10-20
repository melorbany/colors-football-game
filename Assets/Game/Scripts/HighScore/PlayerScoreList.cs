using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ArabicSupport;

public class PlayerScoreList : MonoBehaviour {

	public GameObject playerScoreEntryList;
	public GameObject playerScoreEntryPrefab;
	Highscores highscoresManager;


	int lastChangeCounter;

	// Use this for initialization
	void Start () {

		highscoresManager = GetComponent<Highscores>();
       /*
		Highscores.instance.AddNewHighscore("محمد العرباني", 52);
		Highscores.instance.AddNewHighscore("محمد صث", 23);
		Highscores.instance.AddNewHighscore("محمد شس", 120);

     Debug.Log(highscoresManager);
     */

        StartCoroutine("RefreshHighscores");
		while(playerScoreEntryList.transform.childCount > 0) {
			Transform c = playerScoreEntryList.transform.GetChild(0);
			c.SetParent(null);  // Become Batman
			Destroy (c.gameObject);
		}

		for (int i = 0; i < 1; i++) {

            //Debug.Log("loading");

            GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(playerScoreEntryList.transform,false);
			go.transform.Find ("Name").GetComponent<Text> ().text = ArabicFixer.Fix ("جاري التحميل", true, true);
			go.transform.Find ("Team").GetComponent<Text> ().text ="";
			go.transform.Find ("Score").GetComponent<Text> ().text = "";
		}

   
	}
	
	// Update is called once per frame
	void Update () {
	}


	public void OnHighscoresDownloaded(Highscore[] highscoreList) {

		while(playerScoreEntryList.transform.childCount > 0) {
			Transform c = playerScoreEntryList.transform.GetChild(0);
			c.SetParent(null);  // Become Batman
			Destroy (c.gameObject);
		}

		for (int i = 0; i < highscoreList.Length && i < 5; i++) {

			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(playerScoreEntryList.transform,false);

			if (i == 3) {
				go.transform.GetComponent<Image> ().color = Color.grey;
			}
			Debug.Log (highscoreList[i].name +" - >"+ArabicFixer.Fix (highscoreList[i].name, true, true));

            go.transform.Find("Name").GetComponent<Text>().text = highscoreList[i].name;
            go.transform.Find("Team").GetComponent<Text>().text = highscoreList[i].team;
            go.transform.Find ("Score").GetComponent<Text> ().text = highscoreList[i].score.ToString();
		}

	}


	IEnumerator RefreshHighscores() {
		while (true) {
			highscoresManager.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}
}
