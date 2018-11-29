using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalizeAnimation : MonoBehaviour {

    private Animator animator;

    public GameObject MainMenuPanel;
    public GameObject configPanel;
    public GameObject scorePanel;
    public GameObject credtsPanel;
    public GameObject loadingFld;
    public ShaderEffect_CorruptedVram shaderShift;
    [SerializeField] private AudioSource glitch;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void MainMenuGoToConfig() {
        glitch.Pause();
        animator.SetBool("run", false);
        MainMenuPanel.SetActive(false);
        scorePanel.SetActive(false);
        credtsPanel.SetActive(false);
        configPanel.SetActive(true);
        shaderShift.enabled = false;
    }

    public void MainMenuGoToScore()
    {
        glitch.Pause();
        animator.SetBool("runScore", false);
        MainMenuPanel.SetActive(false);
        configPanel.SetActive(false);
        credtsPanel.SetActive(false);
        scorePanel.SetActive(true);
        shaderShift.enabled = false;
    }

    public void ConfigGoToMainMenu()
    {
        glitch.Pause();
        animator.SetBool("runBack", false);
        MainMenuPanel.SetActive(true);
        configPanel.SetActive(false);
        scorePanel.SetActive(false);
        credtsPanel.SetActive(false);
        shaderShift.enabled = false;
    }

    public void MainMenuGoToCredts()
    {
        glitch.Pause();
        animator.SetBool("runCredits", false);
        MainMenuPanel.SetActive(false);
        configPanel.SetActive(false);
        scorePanel.SetActive(false);
        credtsPanel.SetActive(true);
        loadingFld.SetActive(false);
        shaderShift.enabled = false;
    }

    public void ExitGame() {
        glitch.Pause();
        animator.SetBool("runExit", false);
        Application.Quit();
    }

    public void PlayGame()
    {
        glitch.Pause();
        animator.SetBool("runPlay", false);
        MainMenuPanel.SetActive(false);
        configPanel.SetActive(false);
        scorePanel.SetActive(false);
        credtsPanel.SetActive(false);
        loadingFld.SetActive(true);
        shaderShift.enabled = false;
    }
}
