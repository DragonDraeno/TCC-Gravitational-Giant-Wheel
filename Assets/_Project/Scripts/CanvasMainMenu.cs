using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasMainMenu : MonoBehaviour {

    [SerializeField] private Button playBtn;
    [SerializeField] private Button configBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Slider musicVolSldr;
    [SerializeField] private Slider sondVolSldr;

    public GameObject MainMenuPanel;
    public GameObject configPanel;

    [SerializeField] private float musicVol;
    [SerializeField] private float sondVol;

    [SerializeField] private GameObject loadingFld;
    [SerializeField] private Slider loadingSldr;
    AsyncOperation asyncOpp;

    int firstGame;

    private void Awake()
    {
        Time.timeScale = 1;
        playBtn.onClick.AddListener(btnPlay);
        configBtn.onClick.AddListener(btnConfig);
        exitBtn.onClick.AddListener(btnExit);
        backBtn.onClick.AddListener(btnBack);
        musicVolSldr.onValueChanged.AddListener(delegate { sldrMusicVol(); });
        sondVolSldr.onValueChanged.AddListener(delegate { sldrSondVol(); });
    }

    void Start () {
        MainMenuPanel.SetActive(true);
        configPanel.SetActive(false);
        loadingFld.SetActive(false);
        loadConfig();
    }

    public void btnPlay() {
        MainMenuPanel.SetActive(false);
        configPanel.SetActive(false);

        loadingFld.SetActive(true);
        StartCoroutine(nowLoadingAsync());
    }

    IEnumerator nowLoadingAsync() {

        asyncOpp = SceneManager.LoadSceneAsync("Fase00");

        while (!asyncOpp.isDone) {

            float loadingCount = Mathf.Clamp01(asyncOpp.progress / 0.9f);
            loadingSldr.value = loadingCount;
            
            yield return null;
        }
    }

    void loadConfig()
    {
        if (PlayerPrefs.GetInt("firstGamePP") == 0)
        {
            firstGame = 1;
            PlayerPrefs.SetInt("firstGamePP", firstGame);

            musicVolSldr.GetComponent<Slider>().value = 10;
            musicVol = musicVolSldr.GetComponent<Slider>().value;

            sondVolSldr.GetComponent<Slider>().value = 10;
            sondVol = sondVolSldr.GetComponent<Slider>().value;
        }
        else
        {
            musicVolSldr.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicVolPP");
            musicVol = musicVolSldr.GetComponent<Slider>().value;

            sondVolSldr.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sondVolPP");
            sondVol = sondVolSldr.GetComponent<Slider>().value;
        }
    }

    public void btnConfig()
    {
        MainMenuPanel.SetActive(false);
        configPanel.SetActive(true);
    }

    public void sldrMusicVol()
    {
        musicVol = musicVolSldr.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("musicVolPP", musicVol);
    }

    public void sldrSondVol()
    {
        sondVol = sondVolSldr.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("sondVolPP", sondVol);

    }

    public void btnBack()
    {
        MainMenuPanel.SetActive(true);
        configPanel.SetActive(false);
    }

    public void btnExit()
    {
        Application.Quit();
    }
}
