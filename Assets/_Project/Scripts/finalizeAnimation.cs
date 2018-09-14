using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalizeAnimation : MonoBehaviour {

    private Animator animator;

    public GameObject MainMenuPanel;
    public GameObject configPanel;
    public GameObject loadingFld;
    public ShaderEffect_CorruptedVram shaderShift;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void MainMenuGoToConfig() {
        animator.SetBool("run", false);
        MainMenuPanel.SetActive(false);
        configPanel.SetActive(true);
        shaderShift.enabled = false;
    }

    public void ConfigGoToMainMenu()
    {
        animator.SetBool("runBack", false);
        MainMenuPanel.SetActive(true);
        configPanel.SetActive(false);
        shaderShift.enabled = false;
    }

    public void ExitGame() {
        animator.SetBool("runExit", false);
        Application.Quit();
    }

    public void PlayGame()
    {
        animator.SetBool("runPlay", false);
        MainMenuPanel.SetActive(false);
        configPanel.SetActive(false);
        loadingFld.SetActive(true);
        shaderShift.enabled = false;
    }
}
