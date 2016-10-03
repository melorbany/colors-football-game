using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string iOSURL = "";
    public string ANDROIDURL = "";
    public string fbPage = ""; //use "fb://page/pageID" instead of http:// eg:- ("fb://page/315797608481737")
    public string moreGames;

    private AudioSource sound;

    public Text bestScore;
    [SerializeField]
    private Sprite[] soundBtnSprites; //1 for off and 0 for on
    public Button playBtn, leaderboardBtn, rateBtn, fbLikeBtn, soundBtn, moreGamesBtn, noAdsBtn, slideBtn;
    public string gameScene;

    [SerializeField]
    private Animator slideButtonAnim;

    private bool hidden;
    private bool canTouchSlideButton;

    // Use this for initialization
    void Start()
    {
        bestScore.text = "" + GameManager.instance.hiScore;
        canTouchSlideButton = true;
        hidden = true;
        sound = GetComponent<AudioSource>();
        playBtn.GetComponent<Button>().onClick.AddListener(() => { PlayBtn(); });    //play
        rateBtn.GetComponent<Button>().onClick.AddListener(() => { RateBtn(); });    //rate
        noAdsBtn.GetComponent<Button>().onClick.AddListener(() => { NoAdsBtn(); });    //noAds
        
        leaderboardBtn.GetComponent<Button>().onClick.AddListener(() => { LeaderboardBtn(); });    //leaderboard
        fbLikeBtn.GetComponent<Button>().onClick.AddListener(() => { FBlikeBtn(); });    //facebook
        soundBtn.GetComponent<Button>().onClick.AddListener(() => { SoundBtn(); });    //sound
        moreGamesBtn.GetComponent<Button>().onClick.AddListener(() => { MoregamesBtn(); });    //more games
        slideBtn.GetComponent<Button>().onClick.AddListener(() => { SlideBtn(); });    //slide

        if (GameManager.instance.isMusicOn)
        {
            //MusicController.instance.PlayBgMusic();
            soundBtn.transform.GetChild(0).GetComponent<Image>().sprite = soundBtnSprites[0];

        }
        else
        {
            //MusicController.instance.StopBgMusic();
            soundBtn.transform.GetChild(0).GetComponent<Image>().sprite = soundBtnSprites[1];

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayBtn()
    {
        sound.Play();
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
        SceneManager.LoadScene(gameScene);
#else
        Application.LoadLevel(gameScene);
#endif
    }

    void RateBtn()
    {
        sound.Play();
#if UNITY_IPHONE
		Application.OpenURL(iOSURL);
#endif

#if UNITY_ANDROID
        Application.OpenURL(ANDROIDURL);
#endif
        GameManager.instance.showRate = false;
        GameManager.instance.Save();
    }

    void SoundBtn()
    {
        sound.Play();

        if (GameManager.instance.isMusicOn)
        {
            soundBtn.transform.GetChild(0).GetComponent<Image>().sprite = soundBtnSprites[1];
            //MusicController.instance.StopBgMusic();
            GameManager.instance.isMusicOn = false;
            GameManager.instance.Save();
        }
        else
        {
            soundBtn.transform.GetChild(0).GetComponent<Image>().sprite = soundBtnSprites[0];
            //MusicController.instance.PlayBgMusic();
            GameManager.instance.isMusicOn = true;
            GameManager.instance.Save();

        }
    }

    void FBlikeBtn()
    {
        sound.Play();
        Application.OpenURL(fbPage);
    }

    void LeaderboardBtn()
    {
        sound.Play();
    }

    void MoregamesBtn()
    {
        sound.Play();
        Application.OpenURL(moreGames);
    }

    void NoAdsBtn()
    {
        sound.Play();
        //Purchaser.instance.BuyNoAds();
    }

    void SlideBtn()
    {
        sound.Play();
        StartCoroutine(DisableSlideBtnWhilePlayingAnimation());
    }

    IEnumerator DisableSlideBtnWhilePlayingAnimation()
    {
        if (canTouchSlideButton)
        {
            if (hidden)
            {
                canTouchSlideButton = false;
                slideButtonAnim.Play("SlideIn");
                hidden = false;
                yield return new WaitForSeconds(1.2f);
                canTouchSlideButton = true;

            }
            else
            {
                canTouchSlideButton = false;
                slideButtonAnim.Play("SlideOut");
                hidden = true;
                yield return new WaitForSeconds(1.2f);
                canTouchSlideButton = true;

            }

        }
    }

}