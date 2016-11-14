using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ArabicSupport;
using SmartLocalization;

public class PlayerScoreList : MonoBehaviour {

	public GameObject playerScoreEntryList;
	public GameObject playerScoreEntryPrefab;
	Highscores highscoresManager;


	int lastChangeCounter;

	// Use this for initialization
	void Start () {

		highscoresManager = GetComponent<Highscores>();
      
        StartCoroutine("RefreshHighscores");
		while(playerScoreEntryList.transform.childCount > 0) {
			Transform c = playerScoreEntryList.transform.GetChild(0);
			c.SetParent(null);  // Become Batman
			Destroy (c.gameObject);
		}

		string language = LanguageManager.Instance.GetSystemLanguageEnglishName ();
		if (LanguageManager.Instance.IsLanguageSupportedEnglishName (language)) {
			LanguageManager.Instance.ChangeLanguage (LanguageManager.Instance.GetDeviceCultureIfSupported ());
		} else {
			LanguageManager.Instance.ChangeLanguage ("en");
		}

		for (int i = 0; i < 1; i++) {

            //Debug.Log("loading");

            GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(playerScoreEntryList.transform,false);


			//LanguageManager.Instance.ChangeLanguage ("ar");


			if (LanguageManager.Instance.GetDeviceCultureIfSupported () != null && 
				LanguageManager.Instance.GetDeviceCultureIfSupported ().languageCode.Equals ("ar")) {

				go.transform.Find ("Name").GetComponent<Text> ().text =  ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("Loading"));
			} else {
				go.transform.Find ("Name").GetComponent<Text> ().text =  LanguageManager.Instance.GetTextValue ("Loading");
			}



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

		for (int i = 0; i < highscoreList.Length && i < 6; i++) {

			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(playerScoreEntryList.transform,false);


			//Debug.Log (GameManager.instance.regUserName + " == " + highscoreList[i].userName + "= ");


			if (string.Compare(GameManager.instance.regUserName,highscoreList[i].userName.Replace ('+', ' '))==0) {
				go.transform.GetComponent<Image> ().color = Color.grey;
			}
			//Debug.Log (highscoreList[i].name +" - >"+ArabicFixer.Fix (highscoreList[i].name, true, true));

			go.transform.Find("Name").GetComponent<Text>().text = highscoreList[i].name.Replace ('+', ' ');
			go.transform.Find("Team").GetComponent<Text>().text = highscoreList[i].team.Replace ('+', ' ');
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
