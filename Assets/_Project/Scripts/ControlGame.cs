using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlGame : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private GameObject btnGameReload;
    [SerializeField] private GameObject btnBackToMenu;

    private float timer;
    private bool pause;
    
    public float Timer {get{ return timer; }set{ timer = value; }}

    private void Awake()
    {
        Time.timeScale = 1;
        timerTxt.text = timer.ToString();
        timer = 120;
        pause = false;
        btnGameReload.SetActive(false);
        btnBackToMenu.SetActive(false);
    }

	void Update () {
        timer -= Time.deltaTime;
        timerTxt.text = "Time: " + Mathf.Floor(timer / 60).ToString("00") + ":" + (timer % 60).ToString("00");

        if (TCKInput.GetAction("PauseBtn", EActionEvent.Down))
        {
            GamePauseBtn();
        }
        if (TCKInput.GetAction("GameReloadBtn", EActionEvent.Down))
        {
            GameReloadLevelBtn();
        }
        if (TCKInput.GetAction("BackToMenuBtn", EActionEvent.Down))
        {
            BackToMenuBtn();
        }
    }

    public void GamePauseBtn() {
        if (pause == false)
        {
            pause = true;
            btnGameReload.SetActive(true);
            btnBackToMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pause = false;
            btnGameReload.SetActive(false);
            btnBackToMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void GameReloadLevelBtn()
    {
        SceneManager.LoadScene("fase00", LoadSceneMode.Single);
    }

    public void BackToMenuBtn()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
