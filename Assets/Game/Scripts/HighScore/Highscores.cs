using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour {

	const string privateCode = "xwuy6MA6J0mbC0rUNCv_6wkRAQBRsmCEqDqIroV6MV8g";
	const string publicCode = "5805a5788af60306c09fb413";
	const string webURL = "http://dreamlo.com/lb/";

	PlayerScoreList playerScoreList;
	public Highscore[] highscoresList;
	static Highscores instance;
	
	void Awake() {
		playerScoreList = GetComponent<PlayerScoreList> ();
		instance = this;
	}

	public static void AddNewHighscore(string username, int score) {
		instance.StartCoroutine(instance.UploadNewHighscore(username,score));
	}

	IEnumerator UploadNewHighscore(string username, int score) {
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if (string.IsNullOrEmpty(www.error)) {
			print ("Upload Successful");
			DownloadHighscores();
		}
		else {
			print ("Error uploading: " + www.error);
		}
	}

	public void DownloadHighscores() {
		StartCoroutine("DownloadHighscoresFromDatabase");
	}

	IEnumerator DownloadHighscoresFromDatabase() {
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;
		
		if (string.IsNullOrEmpty (www.error)) {
			FormatHighscores (www.text);
			playerScoreList.OnHighscoresDownloaded(highscoresList);
		}
		else {
			print ("Error Downloading: " + www.error);
		}
	}

	void FormatHighscores(string textStream) {
		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i <entries.Length; i ++) {
			string[] entryInfo = entries[i].Split(new char[] {'|'});
			string username = entryInfo[0];

			string[] playerInfo = username.Split(new string[] {"PDMWY"},System.StringSplitOptions.None);
		
			string name ="", team ="";
			if (playerInfo.Length > 1) {
				name = playerInfo [0];
				team = playerInfo [1];
			} else {
				name = username;
			}

		
			int score = int.Parse(entryInfo[1]);

			highscoresList[i] = new Highscore(name,team,score);
			print (highscoresList[i].name + ": " + highscoresList[i].team + " -> " + highscoresList[i].score);
		}
	}

}

public struct Highscore {
	public string name;
	public string team; 
	public int score;

	public Highscore(string _name,string _team, int _score) {
		name = _name;
		team = _team;
		score = _score;
	}

}
