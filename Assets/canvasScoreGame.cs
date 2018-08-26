using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasScoreGame : MonoBehaviour {

    public GameObject personagem;
    int encontroPersonagem;
    GameObject controlaCenario;
    int encontroCenario;

    public Text pontosTxt;
    public float pontos;
    public float pontosAux;

    public Text tempoTxt;
    public float tempo;
    public RuntimeAnimatorController tempoAcabando;

    public Button pauseBtn;
    int pause;

    //float inverteTempo;
    // Use this for initialization
    void Start () {
        encontroPersonagem = 0;
        encontroCenario = 0;

        pause = 1;
    }


    // Update is called once per frame
    void Update () {
        
        timeOver();

        if (GameObject.Find("personagem") && encontroPersonagem == 0)
        {

            personagem = GameObject.Find("personagem");
            encontroPersonagem = 1;
        }
        else {
            encontroPersonagem = 3; //o jogo acabou
            pontosTxt.text = "Pontos: " + pontos.ToString("F0");
        }

        if (encontroPersonagem == 1) {
            pontos = personagem.GetComponent<personagemFisica>().pontos;

            if ( pontosAux < pontos)
            {
                pontosAux = pontosAux + (Time.deltaTime * 500);

            }

            pontosTxt.text = "Pontos: " + pontosAux.ToString("F0");
        }
        else {
            encontroPersonagem = 0;
            pontosTxt.text = "Pontos: " + pontosAux.ToString("F0");
        }


        if (GameObject.Find("motorPlataformas")) {
            controlaCenario = GameObject.Find("motorPlataformas");
            encontroCenario = 1;

        }

        if (encontroCenario == 1 && encontroPersonagem == 1)
        {
            tempo = 100 - controlaCenario.GetComponent<ControlaCenario>().tempoFase;
            tempoTxt.text = "Tempo: " + tempo.ToString("F2");
        }else {
            encontroCenario = 0;
        }
    }

    public void ClickPause(){
        if (pause == 0) {
            pauseBtn.GetComponent<Image>().color = new Color32(0x2E, 0xEC, 0x00, 0xFF);
            Time.timeScale = 1;
            pause = 1;

            GameObject.Find("One shot audio").GetComponent<AudioSource>().Play();
        }
        else{
            Time.timeScale = 0;
            pauseBtn.GetComponent<Image>().color = new Color32(0xDF, 0x02, 0xC1, 0xFF);
            GameObject.Find("One shot audio").GetComponent<AudioSource>().Pause();
            pause = 0;
        }
    }

    public void ClickReload(){
        SceneManager.LoadScene("projeto_1", LoadSceneMode.Single);
    }
    
    void timeOver()
    {
        if (tempo < 15)
        {
            tempoTxt.GetComponent<Animator>().enabled = true;
            tempoTxt.GetComponent<Animator>().runtimeAnimatorController = tempoAcabando;
        }
        else {
            tempoTxt.GetComponent<Animator>().enabled = false;
            tempoTxt.GetComponent<Text>().color = new Color32(0xE0, 0x01, 0xC3, 0xFF); 
        }
    }
}
