using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Score : MonoBehaviour {

    public GameObject instanciaJogo;
    public GameObject instanciaJogagor;
    public GameObject plataformaGiratoria;
    GameObject clone;

    public GameObject firebaseInstancia;
    int encontroFirebase = 0;

    public GameObject dadosFirebase;
    public string recebeDados;

    int pega1x;

    public string jogadores;

    int iniciaPegar;
    int paraPegar;

    public string[] nome;
    public string[] pontos;

    public string[] arrayNome;
    public string[] arrayPontos;

    float tempoBusacaDados;

    public Text montaScore;
    public GameObject nomePontos;

    public GameObject resetCanvasVars;
    public Text resetCanvasTxt;

    public Text tittleCanvas;

    public GameObject MusicaFundo;
    // Use this for initialization
    void Start() {
        nome = new string[1];
        pontos = new string[1];

        pega1x = 0;

        iniciaPegar = 0;
        paraPegar = 0;

    }


    // Update is called once per frame
    void Update() {


        if (GetComponent<Canvas>().targetDisplay == 0) {
            tempoBusacaDados += Time.deltaTime;
            recebeDados = dadosFirebase.GetComponent<TextMesh>().text;

        }

        if (tempoBusacaDados > 5) {
            if (GetComponent<Canvas>().targetDisplay == 0 && pega1x == 0 && encontroFirebase == 1)
            {
                Destroy(GameObject.Find("MusicaFundoMenu"));
                Destroy(GameObject.Find("One shot audio"));
                clone = Instantiate(MusicaFundo);
                clone.name = "MusicaFundoMenu";

                for (int i = 0; i < recebeDados.Length; i++) {

                    if (iniciaPegar == 0 && recebeDados[i] == '<' && recebeDados[i + 1] == '/' && recebeDados[i + 2] == 'S' && recebeDados[i + 3] == 'c' && recebeDados[i + 4] == 'o' && recebeDados[i + 5] == 'r' && recebeDados[i + 6] == 'e' && recebeDados[i + 7] == '>') {
                        i = i + 7;

                        iniciaPegar = 1;
                        print("encontrou");
                        pega1x = 2;
                    }

                    if (iniciaPegar == 1 && recebeDados[i] == '\n')
                    {
                        paraPegar++;
                    }

                    if (iniciaPegar == 1 && paraPegar == 1) {
                        jogadores += recebeDados[i];//variavel jogadores tem a lista de todos os jogadores, pontos e tempos
                    }

                }

                print("jogadores: " + jogadores);

                int quantNomes = 0;
                string strAux = "";

                int inicioFimNome = 0;//inicio nome == 0 // fim nome == 1
                int inicioFimPontos = 0;//inicio pontos == 0 // fim pontos == 1


                for (int i = 0; i < jogadores.Length; i++)
                {


                    if (jogadores[i] == '%' && jogadores[i + 1] == '_')
                    {
                        i = i + 2;
                        print("encontrou jogadores nome");
                        inicioFimNome = 1;
                        //strAux += quantNomes+1+"° Lugar - Nome: ";
                    }

                    if (inicioFimNome == 1 && jogadores[i] == '_' && jogadores[i + 1] == '%')
                    {
                        i = i + 2;
                        print("encontrou jogadores fim nome");

                        inicioFimNome = 0;
                        //strAux += " ";
                    }

                    if (jogadores[i] == '#' && jogadores[i + 1] == '_')
                    {
                        i = i + 2;
                        print("encontrou jogadores pontos");
                        inicioFimPontos = 1;
                        //strAux += "Pontos: ";
                    }

                    if (inicioFimPontos == 1 && jogadores[i] == '_' && jogadores[i + 1] == '#')
                    {
                        i = i + 2;
                        print("encontrou jogadores fim pontos");
                        inicioFimPontos = 0;
                        //strAux += " ";

                        quantNomes++;

                        System.Array.Resize(ref nome, quantNomes + 1);
                        System.Array.Resize(ref pontos, quantNomes + 1);

                    }

                    if (inicioFimNome == 1)
                    {
                        strAux += jogadores[i];
                        nome[quantNomes] += jogadores[i];
                    }

                    if (inicioFimPontos == 1)
                    {
                        strAux += jogadores[i];
                        pontos[quantNomes] += jogadores[i];
                    }
                }

                arrayNome = new string[quantNomes];
                arrayPontos = new string[quantNomes];
                string arrayNomeAux = "";
                string arrayPontosAux = "0";
                int saveIt = 0;

                for (int i = 0; i < quantNomes; i++)
                {
                    for (int j = 0; j < quantNomes; j++)
                    {

                        if (pontos[j] != "-500") {
                            if (int.Parse(arrayPontosAux) <= int.Parse(pontos[j]))
                            {

                                arrayNomeAux = nome[j];
                                arrayPontosAux = pontos[j];
                                saveIt = j;

                            }
                        }
                    }
                    if (pontos[saveIt] != "-500")
                    {
                        pontos[saveIt] = "-500";
                        nome[saveIt] = "";
                    }

                    print(arrayPontosAux);
                    arrayNome[i] = arrayNomeAux;
                    arrayPontos[i] = arrayPontosAux;
                    arrayPontosAux = "0";



                    if (nomePontos.GetComponent<nomePontos>().nome == arrayNome[i])
                    {
                        montaScore.GetComponent<Text>().text += "-► ";
                    }
                    montaScore.GetComponent<Text>().text += i + 1 + "° - ";
                    montaScore.GetComponent<Text>().text += arrayNome[i] + ". - ";
                    montaScore.GetComponent<Text>().text += arrayPontos[i];
                    montaScore.GetComponent<Text>().text += "\n";



                }

                //print(strAux);
            }

        }

        if (GetComponent<Canvas>().targetDisplay == 0 && !GameObject.Find("firebase"))
        {
            clone = Instantiate(firebaseInstancia);
            clone.GetComponent<SampleScript>().textMesh = dadosFirebase.GetComponent<TextMesh>();
            clone.name = "firebase";
            encontroFirebase = 1;
        }

    }

    public void ClickPlay()
    {

        //deleta o menu de fundo
        Destroy(GameObject.Find("cenario"));
        Destroy(GameObject.Find("motorPlataformas"));
        Destroy(GameObject.Find("plataformaGiratoria"));
        Destroy(GameObject.Find("personagem"));
        Destroy(GameObject.Find("firebase"));
        dadosFirebase.GetComponent<TextMesh>().text = "";
        jogadores = "";
        iniciaPegar = 0;
        paraPegar = 0;

        System.Array.Resize(ref nome, 1);
        System.Array.Resize(ref pontos, 1);
        nome[0] = "";
        pontos[0] = "";

        System.Array.Resize(ref arrayNome, 0);
        System.Array.Resize(ref arrayPontos, 0);

        montaScore.GetComponent<Text>().text = "";

        resetCanvasVars.GetComponent<canvasScoreGame>().pontosAux = 0;

        gameObject.GetComponent<Canvas>().targetDisplay = 2;
        encontroFirebase = 0;
        pega1x = 0;
        tempoBusacaDados = 0;

        Instantiate(instanciaJogo, new Vector3(-9.68f, 0, 0), Quaternion.identity);

        clone = Instantiate(instanciaJogagor, new Vector3(0, -200, 200), new Quaternion(0, 0, 0, 0));
        clone.name = "personagem";

        clone = Instantiate(plataformaGiratoria, new Vector3(0, 0, 0), Quaternion.identity);
        clone.name = "plataformaGiratoria";

        Destroy(GameObject.Find("MusicaFundoMenu"));
        Destroy(GameObject.Find("One shot audio"));
        clone = Instantiate(MusicaFundo);
        clone.name = "MusicaFundoMenu";

        Time.timeScale = 1;

    }

    public void ClickUp()
    {
        montaScore.transform.position = new Vector3(montaScore.transform.position.x, montaScore.transform.position.y + 30, montaScore.transform.position.z);
        tittleCanvas.transform.position = new Vector3(tittleCanvas.transform.position.x, tittleCanvas.transform.position.y + 30, tittleCanvas.transform.position.z);

    }
    public void ClickDowm()
    {
        montaScore.transform.position = new Vector3(montaScore.transform.position.x, montaScore.transform.position.y - 30, montaScore.transform.position.z);
        tittleCanvas.transform.position = new Vector3(tittleCanvas.transform.position.x, tittleCanvas.transform.position.y - 30, tittleCanvas.transform.position.z);

    }

}
