using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFinish : MonoBehaviour
{
    [Header("UIs / Game Object")]
    [SerializeField] private GameObject GameOverUI = null;
    [SerializeField] private GameObject levelCompleteUI = null;
    [SerializeField] private GameObject TutorialUI = null;
    [SerializeField] private GameObject PlayerUI = null;
    [SerializeField] GameObject Campos = null;

    [Header("Texts")]
    [SerializeField] private Text time = null;
    [SerializeField] private Text accuracy = null;
    [SerializeField] private Text lastHp = null;
    [SerializeField] private Text TotalScore = null;

    [Header("Audio")]
    [SerializeField] private AudioClip _levelFinished = null;
    [SerializeField] private AudioClip _levelFailed = null;


    private GameObject MainCamera;
    private AudioSource MusicSource;
    private GameObject Player;
    private GameTimer gameTimer;
    private bool canCompleted;
    private float currentSceneNumber;
    private float startTime;
    private float startHp;

    //EnemyCounter
    private EnemyCounter enemyCounter;
    // Start is called before the first frame update
    void Start()
    {
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        gameTimer = GameObject.FindGameObjectWithTag("UIManager").GetComponent<GameTimer>();
        canCompleted = true;
        if (currentSceneNumber == 1)
        {
            activateTutorial();
        }
        else if (currentSceneNumber == 2 || currentSceneNumber == 4) canCompleted = false;
        GameOverUI.SetActive(false);
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(false);
        }
        Campos.SetActive(true);
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyCounter = GameObject.FindGameObjectWithTag("UIManager").GetComponent<EnemyCounter>();
        startTime = gameTimer.startTime;
        startHp = Player.GetComponent<Player>().HitPoint;
        MusicSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (enemyCounter.enemyCount<=0 && canCompleted)
         {
            Invoke("LevelComplete", 1f);
         }
    }

    // Update is called once per frame
    public void GameOver()
    {
        MusicSource.Pause();
        MusicSource.clip = _levelFailed;
        MusicSource.loop = false;
        MusicSource.Play();
        Time.timeScale = 0f;
        //AudioListener.pause = true;
        GameOverUI.SetActive(true);
        Campos.SetActive(false);
        Player.GetComponent<Player>().enabled = false;
    }

    public void LevelComplete()
    {
        MusicSource.Pause();
        MusicSource.clip = _levelFinished;
        MusicSource.loop = false;
        MusicSource.Play();
        Time.timeScale = 0f;
        //AudioListener.pause = true;
        levelCompleteUI.SetActive(true);
        float level = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetFloat("Level", level);
        float ftime = gameTimer.startTime;
        int minutes = Mathf.FloorToInt(ftime / 60F);
        int seconds = Mathf.FloorToInt(ftime - minutes * 60);

        string text = string.Format("{0:00}:{1:00}", minutes, seconds);
        time.text = text;
        float acc = Player.GetComponent<Player>().hitBullet/ Player.GetComponent<Player>().firedBullet;
        float tmp = acc*100;
        tmp = Mathf.FloorToInt(tmp);
        accuracy.text = tmp.ToString()+'%';
        float lHP = Player.GetComponent<Player>().HitPoint;
        lastHp.text = lHP.ToString();
        countTotalPoint(ftime, acc, lHP);
        //Campos.SetActive(false);
        //Player.GetComponent<Player>().enabled = false;
    }
    public void activateTutorial()
    {
        TutorialUI.SetActive(true);
        PlayerUI.SetActive(false);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        //Player.GetComponent<Player>().enabled = false;
        Campos.SetActive(false);
    }
    public void deactivateTutorial()
    {
        TutorialUI.SetActive(false);
        PlayerUI.SetActive(true);
    }
    public void countTotalPoint(float time, float accuracy, float lHP)
    {
        float totScore = ((time / startTime)*2000) + (accuracy*2000) + ((lHP/startHp)*1000);
        totScore = Mathf.FloorToInt(totScore);
        TotalScore.text = totScore.ToString();
    }
}
