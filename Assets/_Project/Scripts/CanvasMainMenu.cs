using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;

public class CanvasMainMenu : MonoBehaviour
{

    [SerializeField] private Button playBtn;
    [SerializeField] private Button configBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button scoreBtn;
    [SerializeField] private Button credtsBtn;
    [SerializeField] private Button helpUsBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button backScoreBtn;
    [SerializeField] private Button backCredtsBtn;
    [SerializeField] private Slider musicVolSldr;
    [SerializeField] private Slider sondVolSldr;
    
    [SerializeField] private Animator cameraAnimator;

    public GameObject MainMenuPanel;
    public GameObject configPanel;

    [SerializeField] private float musicVol;
    [SerializeField] private float sondVol;

    [SerializeField] private GameObject loadingFld;
    [SerializeField] private Slider loadingSldr;
    AsyncOperation asyncOpp;

    public ShaderEffect_CorruptedVram shaderShift;

    public string placementId = "video";

    private int firstGame;

    [SerializeField] private AudioSource backgroung;

    [SerializeField] private AudioSource glitch;
    [SerializeField] private AudioSource clickBtnSoung;
    [SerializeField] private AudioSource clickSlider;

    private void Awake()
    {
        Time.timeScale = 1;
        playBtn.onClick.AddListener(btnPlay);
        configBtn.onClick.AddListener(btnConfig);
        exitBtn.onClick.AddListener(btnExit);
        scoreBtn.onClick.AddListener(btnScore);
        credtsBtn.onClick.AddListener(btnCredtis);
        helpUsBtn.onClick.AddListener(btnHelpUs);
        backBtn.onClick.AddListener(btnBack);
        backScoreBtn.onClick.AddListener(btnBack);
        backCredtsBtn.onClick.AddListener(btnBack);
        musicVolSldr.onValueChanged.AddListener(delegate { sldrMusicVol(); });
        sondVolSldr.onValueChanged.AddListener(delegate { sldrSondVol(); });
    }

    void Start () {
        Advertisement.Initialize("2826948");

        MainMenuPanel.SetActive(true);
        configPanel.SetActive(false);
        loadingFld.SetActive(false);
        loadConfig();
    }

    public void btnPlay() {
        clickBtnSoung.Play();
        glitch.Play();
        shaderShift.enabled = true;
        cameraAnimator.SetBool("runPlay", true);
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

    public void loadConfig()
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

            backgroung.volume = PlayerPrefs.GetFloat("musicVolPP");

            sondVolSldr.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sondVolPP");
            sondVol = sondVolSldr.GetComponent<Slider>().value;

            clickBtnSoung.volume = PlayerPrefs.GetFloat("sondVolPP");
            glitch.volume = PlayerPrefs.GetFloat("sondVolPP");
            clickSlider.volume = PlayerPrefs.GetFloat("sondVolPP");
        }
    }

    public void btnConfig()
    {
        clickBtnSoung.Play();
        glitch.Play();
        shaderShift.enabled = true;
        cameraAnimator.SetBool("run", true);
    }
    
    public void sldrMusicVol()
    {
        musicVol = musicVolSldr.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("musicVolPP", musicVol);

        if (configPanel.activeSelf == true)
        {
            clickSlider.PlayDelayed(0.3f);
        }

        backgroung.volume = musicVol;
    }

    public void sldrSondVol()
    {
        sondVol = sondVolSldr.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("sondVolPP", sondVol);

        if (configPanel.activeSelf == true) {
            clickSlider.PlayDelayed(0.3f);
        }

        clickBtnSoung.volume = sondVol;
        clickSlider.volume = sondVol;
        glitch.volume = sondVol;
    }

    public void btnScore() {
        clickBtnSoung.Play();
        glitch.Play();
        shaderShift.enabled = true;
        cameraAnimator.SetBool("runScore", true);
        StartCoroutine(ShowAdWhenReady());
    }

    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady("video01"))
            yield return null;

        Advertisement.Show("video01");
    }

    public void btnHelpUs() {
        Application.OpenURL("https://goo.gl/forms/zxodwUmpjXGKqdO33");
    }

    public void btnCredtis() {
        clickBtnSoung.Play();
        glitch.Play();
        shaderShift.enabled = true;
        cameraAnimator.SetBool("runCredits", true);
    }

    public void btnBack()
    {
        clickBtnSoung.Play();
        glitch.Play();
        shaderShift.enabled = true;
        cameraAnimator.SetBool("runBack", true);
    }

    public void btnScoreBack()
    {
        clickBtnSoung.Play();
        glitch.Play();
        shaderShift.enabled = true;
        cameraAnimator.SetBool("runBack", true);
    }

    public void btnExit()
    {
        clickBtnSoung.Play();
        glitch.Play();
        shaderShift.enabled = true;
        cameraAnimator.SetBool("runExit", true);
    }
}
