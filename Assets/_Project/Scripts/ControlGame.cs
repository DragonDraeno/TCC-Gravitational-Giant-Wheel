using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlGame : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private GameObject btnGameReload;
    [SerializeField] private GameObject btnBackToMenu;
	[SerializeField] private GameObject panelRefeshAndHome;

	[SerializeField] private GameObject panelButtons;
	[SerializeField] private GameObject panelTouch;

    [SerializeField] GameObject popUpChoseControll;
    [SerializeField] private Toggle ButtonTgl;
    [SerializeField] private Toggle TouchTgl;

    private float timer;
    private bool pause;

    private bool begginer;

    public float Timer {get{ return timer; }set{ timer = value; }}

    private void Awake()
    {
        Time.timeScale = 1;
        timerTxt.text = timer.ToString();
        timer = 120;
        pause = false;
        panelRefeshAndHome.SetActive(false);
        
        ButtonTgl.onValueChanged.AddListener(delegate { IsBegginer(); });
        TouchTgl.onValueChanged.AddListener(delegate { IsBegginer(); });

        if (PlayerPrefs.GetInt("isBegginer") != 1)
        {
            begginer = true;
        }
        else {
            begginer = false;
        }

        if (begginer == true)
        {
            Time.timeScale = 0;
            popUpChoseControll.SetActive(true);
            PlayerPrefs.SetInt("isBegginer", 1);
        }
        else {
            if (PlayerPrefs.GetInt("ButtonOrToutch") == 0) {
                panelButtons.SetActive(true);
                panelTouch.SetActive(false);
            }
            else {
                panelButtons.SetActive(false);
                panelTouch.SetActive(true);
            }

            popUpChoseControll.SetActive(false);
        }
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
            panelRefeshAndHome.SetActive(true);
            popUpChoseControll.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pause = false;
            panelRefeshAndHome.SetActive(false);
            popUpChoseControll.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void IsBegginer()
    {
        if (ButtonTgl.isOn == true) {
            panelButtons.SetActive(true);
            panelTouch.SetActive(false);
            PlayerPrefs.SetInt("ButtonOrToutch", 0);
        }

        if (TouchTgl.isOn == true) {
            panelTouch.SetActive(true);
            panelButtons.SetActive(false);
            PlayerPrefs.SetInt("ButtonOrToutch", 1);
        }

        if (panelRefeshAndHome.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }

        popUpChoseControll.SetActive(false);
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
