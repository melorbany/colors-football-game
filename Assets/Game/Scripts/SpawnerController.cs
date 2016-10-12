using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour {

    public static SpawnerController instance;

    [SerializeField]
    private GameObject[] spawnPoints;  //ref to all spawn points in the scene
    private AudioSource sound;         //variable where we will assign audio source
    public AudioClip[] swingClips;     //audio clips are assigned here
    public float timeReduce;              //amount by which time is reduce
    public float timeDecreaseMileStone;   //mile stone after which time is reduce
    private float timeMileStoneCount;     //total mile stones achieved

    public float minTime;                 //time limit between each spawn
    public float time;
    int lastI = 0;                      //ref to last spawn point

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        sound = GetComponent<AudioSource>();
        timeMileStoneCount = timeDecreaseMileStone;

        //if game is not over we selct the animal and start a coroutine
        if (GameManager.instance.isGameOver == false)
        {
            SelectAnimal();
        }

        StartCoroutine(WaitForNextSpawn());
    }
	
	// Update is called once per frame
	void Update ()
    {
        IncreaseDiff();
    }

    IEnumerator WaitForNextSpawn()
    {
        float timeVal = time;

        if (GameManager.instance.currentScore <= 10)
        {
            timeVal = time;
        }
        else if (GameManager.instance.currentScore > 10 /*&& GameManager.instance.currentScore <= 15*/)
        {
            int i = Random.Range(0, 3);

            if (i >= 0 && i < 2)
            {
                timeVal = time;
            }
            else
            {
                timeVal = 0.8f;
            }
        }

        yield return new WaitForSeconds(timeVal);
       
        if (GameManager.instance.isGameOver == false)
        {
            SelectAnimal();
        }
        else
        {
            //Debug.Log("GameOver");
        }

        StartCoroutine(WaitForNextSpawn());
    }

    //methode which selects animal and spawn position
    void SelectAnimal()
    {
        int i = Random.Range(0, spawnPoints.Length);
        while (i == lastI)
        {
            i = Random.Range(0, spawnPoints.Length);
        }
        

        if (i == 0)
        {
            //we get the object from object pooling
            GameObject newRed = ObjectPooling.instance.GetRed();
            //changes its transform
            newRed.transform.position = spawnPoints[i].transform.position;
            //change its rotation
            newRed.transform.rotation = this.transform.rotation;
            //get the script attached to that oject
            AnimalController code = newRed.GetComponent<AnimalController>();
            //make the object active
            newRed.SetActive(true);
            //apply the force
            code.applyForce = true;
            //play the sound
            sound.PlayOneShot(swingClips[0]);
        }
        else if (i == 1)
        {
            GameObject newBlue = ObjectPooling.instance.GetBlue();
            newBlue.transform.position = spawnPoints[i].transform.position;
            newBlue.transform.rotation = this.transform.rotation;
            AnimalController code = newBlue.GetComponent<AnimalController>();
            newBlue.SetActive(true);
            code.applyForce = true;
            sound.PlayOneShot(swingClips[1]);
        }
        else if (i == 2)
        {
            GameObject newGreen = ObjectPooling.instance.GetGreen();
            newGreen.transform.position = spawnPoints[i].transform.position;
            newGreen.transform.rotation = this.transform.rotation;
            AnimalController code = newGreen.GetComponent<AnimalController>();
            newGreen.SetActive(true);
            code.applyForce = true;
            sound.PlayOneShot(swingClips[2]);
        }
        else if (i == 3)
        {
            GameObject newYellow = ObjectPooling.instance.GetYellow();
            newYellow.transform.position = spawnPoints[i].transform.position;
            newYellow.transform.rotation = this.transform.rotation;
            AnimalController code = newYellow.GetComponent<AnimalController>();
            newYellow.SetActive(true);
            code.applyForce = true;
            sound.PlayOneShot(swingClips[3]);
        }

        lastI = i;

    }

    //with increase in score we decrease the spawn time between 2 objects
    void IncreaseDiff()
    {
        if (GameManager.instance.currentScore > timeMileStoneCount)
        {
            timeMileStoneCount += timeDecreaseMileStone;
            timeDecreaseMileStone += 5f; //well this set the new mile stone which the score
            time -= timeReduce;

            if (time < minTime)
            {
                time = minTime;
            }

        }
    }


}
