using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ArabicSupport;
using SmartLocalization;

public class InGameGui : MonoBehaviour {

    private AudioSource sound;
    public GameObject gameOn , gameOver;
    public Text score, best, ingameScore, pointText;
	public Text gameOverText,scoreText,highScoreText;

    public Color[] medalCols;
    public Image medal;
    public Button homeBtn, leaderBtn, retryBtn, shareBtn;
	public string mainMenu,leaderScene, accountScene;
    int i = 0;
    bool isScoreUpdatedOnServe = false;
	// Use this for initialization
	void Start ()
    {
		LanguageManager.Instance.ChangeLanguage (LanguageManager.Instance.GetDeviceCultureIfSupported ());
		//LanguageManager.Instance.ChangeLanguage ("ja");

		//Debug.Log (LanguageManager.Instance.GetDeviceCultureIfSupported ().languageCode.Equals("ar"));

		if (LanguageManager.Instance.GetDeviceCultureIfSupported ().languageCode.Equals ("ar")) {
			gameOverText.text = ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("GameOver"));
			scoreText.text = ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("Score"));
			highScoreText.text = ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("HighScore"));
		} else {
			gameOverText.text = LanguageManager.Instance.GetTextValue ("GameOver").ToUpper();
			scoreText.text = LanguageManager.Instance.GetTextValue ("Score").ToUpper();
			highScoreText.text = LanguageManager.Instance.GetTextValue ("HighScore").ToUpper();
		}
	
		sound = GetComponent<AudioSource>();
        GameManager.instance.currentScore = 0;
        ingameScore.text = "" + GameManager.instance.currentScore;
        homeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeBtn(); });    //home
        leaderBtn.GetComponent<Button>().onClick.AddListener(() => { LeaderboardBtn(); });    //leaderboard
        retryBtn.GetComponent<Button>().onClick.AddListener(() => { RetryBtn(); });    //retry
        shareBtn.GetComponent<Button>().onClick.AddListener(() => { ShareBtn(); });    //snapshot

    }

    // Update is called once per frame
    void Update ()
    {
        ingameScore.text = "" + GameManager.instance.currentScore;

        if (GameManager.instance.currentScore >= GameManager.instance.hiScore)
        {
            GameManager.instance.hiScore = GameManager.instance.currentScore;
            GameManager.instance.Save();

			if(!GameManager.instance.isUserRegistered  && GameManager.instance.hiScore > 30)
			{
				//ShowRegisterMessage ();
			}
        }

        if (GameManager.instance.isGameOver)
        {

            score.text = "" + GameManager.instance.currentScore;
            best.text = "" + GameManager.instance.hiScore;
            MedalColor();
            gameOn.SetActive(false);
            gameOver.SetActive(true);

            if (GameManager.instance.currentScore >= 10 && i == 0)
            {
				//ShowRegisterMessage ();

                int point = GameManager.instance.currentScore / 10;
                pointText.text = "+" + point;
                GameManager.instance.points = point;
                GameManager.instance.Save();
                i = 1;
            }


            //Update the list.
			if (!isScoreUpdatedOnServe && GameManager.instance.isUserRegistered
			             && GameManager.instance.regUserName.Length > 1) {
				Highscores.instance.AddNewHighscore (GameManager.instance.regUserName, GameManager.instance.hiScore);
				isScoreUpdatedOnServe = true;
			} else if (!GameManager.instance.isUserRegistered 
				&& GameManager.instance.currentScore >= 20) {
				SceneManager.LoadScene(accountScene);
			}
        }

    }

    void HomeBtn()
    {
        sound.Play();
        GameManager.instance.isGameOver = false;
        SceneManager.LoadScene(mainMenu);
    }

    void RetryBtn()
    {
        sound.Play();
        GameManager.instance.isGameOver = false;
        Application.LoadLevel(Application.loadedLevel);
    }

    void LeaderboardBtn()
    {
        if (GameManager.instance.isUserRegistered)
            SceneManager.LoadScene(leaderScene);
        else
        {
            SceneManager.LoadScene(accountScene);
        }
    }

    void ShareBtn()
    {
        sound.Play();
        //FacebookShare.instance.FBShareLink();
    }

    void MedalColor()
    {
        if (GameManager.instance.currentScore >= 10)
        {
            medal.color = medalCols[0];
        }
        else if (GameManager.instance.currentScore >= 25)
        {
            medal.color = medalCols[1];
        }
        else if (GameManager.instance.currentScore >= 40)
        {
            medal.color = medalCols[2];
        }
        else if (GameManager.instance.currentScore >= 60)
        {
            medal.color = medalCols[3];
        }
        else if (GameManager.instance.currentScore >= 80)
        {
            medal.color = medalCols[4];
        }
    }


}
