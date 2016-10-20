using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HighScoreUI : MonoBehaviour {

	public Button playBtn,homeBtn;
	public string gameScene;
	public string mainMenu;


	// Use this for initialization
	void Start () {
		playBtn.GetComponent<Button>().onClick.AddListener(() => { PlayBtn(); });    //play
		homeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeBtn(); });    //rate
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void PlayBtn()
	{
        GameManager.instance.isGameOver = false;
        #if UNITY_5_3 || UNITY_5_3_OR_NEWER
        SceneManager.LoadScene(gameScene);
		#else
		Application.LoadLevel(gameScene);
		#endif
	}


	void HomeBtn()
	{
        GameManager.instance.isGameOver = false;
        SceneManager.LoadScene(mainMenu);
	}
}
