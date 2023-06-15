using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject TutorialUI;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] GameObject Campos;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        ActivateTutorial();
        GameOverUI.SetActive(false);
        levelCompleteUI.SetActive(false);
        Campos.SetActive(true);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        GameOverUI.SetActive(true);
        Campos.SetActive(false);
        Player.GetComponent<Player>().enabled = false;
    }

    public void LevelComplete()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        levelCompleteUI.SetActive(true);
        Campos.SetActive(false);
        Player.GetComponent<Player>().enabled = false;
    }

    public void ActivateTutorial()
    {
        Time.timeScale = 0f;
        PlayerUI.SetActive(false);
        TutorialUI.SetActive(true);
    }
    public void DeactivateTutorial()
    {
        Time.timeScale = 1f;
        TutorialUI.SetActive(false);
        PlayerUI.SetActive(true);
    }
}
