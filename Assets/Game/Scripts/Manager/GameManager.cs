using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/// <summary>
/// This script helps in saving and loading data in device
/// </summary>
public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private GameData data;

    //data which is not stored on device but refered while game is on
    public bool isGameOver = false;
    public int currentScore;

    //data to store on device
    public bool isGameStartedFirstTime;
    public bool isMusicOn;
    public int hiScore, points, textureStyle;
    public bool canShowAds;//when noAds is false we show ads and when its true we dont show it
    public bool showRate;
    public bool[] textureUnlocked;
    //ref to the background music
    //private AudioSource audio;

    void Awake()
    {
        MakeInstance();
        InitializeVariables();

        //if you increase or decrease the shop button shance values here also
        //textureUnlocked = new bool[4];
        //textureUnlocked[0] = true;
        //for (int i = 1; i < textureUnlocked.Length; i++)
        //{
        //    textureUnlocked[i] = false;
        //}
        //Save();
        //Load();

    }

    void Start()
    {
        
    }

    void MakeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //we initialize variables here
    void InitializeVariables()
    {
        //first we load any data is avialable
        Load();
        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }
        if (isGameStartedFirstTime)
        {
            //when game is started for 1st time on device we set the initial values
            isGameStartedFirstTime = false;
            hiScore = 0;
            points = 0;
            textureStyle = 0;
            textureUnlocked = new bool[4];
            textureUnlocked[0] = true;
            for (int i = 1; i < textureUnlocked.Length; i++)
            {
                textureUnlocked[i] = false;
            }
            isMusicOn = true;
            canShowAds = true;
            showRate = true;
            data = new GameData();

            //storing data
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setIsMusicOn(isMusicOn);
            data.setHiScore(hiScore);
            data.setPoints(points);
            data.setTexture(textureStyle);
            data.setTextureUnlocked(textureUnlocked);
            data.setCanShowAds(canShowAds);
            data.setShowRate(showRate);
            Save();
            Load();
        }
        else
        {
            //getting data
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getIsMusicOn();
            hiScore = data.getHiScore();
            points = data.getPoints();
            textureStyle = data.getTexture();
            textureUnlocked = data.getTextureUnlocked();
            canShowAds = data.getCanShowAds();
            showRate = data.getShowRate();      
        }
    }

    void Update()
    {//here we control the background music
        //if (isGameOver == false && audio.isPlaying == false)
        //{
        //    audio.Play();
        //}
        //else if (isGameOver == true)
        //{
        //    audio.Stop();
        //}
    }

    //method to save data
    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameInfo.dat");
            if (data != null)
            {
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setHiScore(hiScore);
                data.setPoints(points);
                data.setTexture(textureStyle);
                data.setTextureUnlocked(textureUnlocked);
                data.setIsMusicOn(isMusicOn);
                data.setCanShowAds(canShowAds);
                data.setShowRate(showRate);
                bf.Serialize(file, data);
            }
        }
        catch (Exception e)
        { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    //method to load data
    public void Load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameInfo.dat", FileMode.Open);//here we get saved file
            data = (GameData)bf.Deserialize(file);
        }
        catch (Exception e) { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    private int hiScore, points, textureStyle;
    private bool[] textureUnlocked;
    private bool canShowAds;
    private bool showRate;

    //is game started 1st time
    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirstTime()
    {
        return isGameStartedFirstTime;
    }

    //ads
    public void setCanShowAds(bool canShowAds)
    {
        this.canShowAds = canShowAds;
    }

    public bool getCanShowAds()
    {
        return canShowAds;
    }

    //rate
    public void setShowRate(bool showRate)
    {
        this.showRate = showRate;
    }

    public bool getShowRate()
    {
        return showRate;
    }

    //music
    public void setIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool getIsMusicOn()
    {
        return isMusicOn;
    }

    //hi score 
    public void setHiScore(int hiScore)
    {
        this.hiScore = hiScore;
    }

    public int getHiScore()
    {
        return hiScore;
    }

    //points 
    public void setPoints(int points)
    {
        this.points = points;
    }

    public int getPoints()
    {
        return points;
    }

    //textureStyle 
    public void setTexture(int textureStyle)
    {
        this.textureStyle = textureStyle;
    }

    public int getTexture()
    {
        return textureStyle;
    }

    //texture unlocked
    public void setTextureUnlocked(bool[] textureUnlocked)
    {
        this.textureUnlocked = textureUnlocked;
    }

    public bool[] getTextureUnlocked()
    {
        return textureUnlocked;
    }
}