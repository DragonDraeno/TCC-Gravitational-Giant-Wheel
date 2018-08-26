using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class controlaMenuInicial : MonoBehaviour {

    public GameObject instanciaJogo;
    public GameObject instanciaPersonagem;
    public GameObject plataformaGiratoria;
    public GameObject MusicaFundo;

    GameObject clone;

    public GameObject canvasPontos;

    public Text nome;
    public string nomeTxt;

    // Use this for initialization
    void Start () {
        nomeTxt = "";
        //gameObject.SetActive(false);
        clone = Instantiate(MusicaFundo);
        clone.name = "MusicaFundoMenu";

        clone = Instantiate(instanciaJogo, new Vector3(-9.68f, 0, 0), Quaternion.identity);
        clone.name = "fundoMenuIicial";

        clone = Instantiate(plataformaGiratoria, new Vector3(0, 0, 1000), Quaternion.identity);
        clone.name = "plataformaGiratoria";

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickPlay()
    {

        nomeTxt = nome.GetComponent<Text>().text;

        //deleta o menu de fundo
        Destroy(GameObject.Find("cenario"));
        Destroy(GameObject.Find("motorPlataformas"));
        Destroy(GameObject.Find("fundoMenuIicial"));
        Destroy(GameObject.Find("plataformaGiratoria"));
        Destroy(GameObject.Find("MusicaFundoMenu"));
        Destroy(GameObject.Find("One shot audio"));
        
        //instacia um mapa e um personagem
        gameObject.GetComponent<Canvas>().targetDisplay = 3;

        Instantiate(instanciaJogo, new Vector3(-9.68f,0,0), Quaternion.identity);

        clone = Instantiate(instanciaPersonagem, new Vector3(0, -200, 200), new Quaternion(0,0,0,0));
        //clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z +100);
        //clone.transform.Rotate(90, 0, 0);
        clone.name = "personagem";

        clone = Instantiate(plataformaGiratoria, new Vector3(0, 0, 0), Quaternion.identity);
        clone.name = "plataformaGiratoria";

        canvasPontos.SetActive(true);

        clone = Instantiate(MusicaFundo);
        clone.name = "MusicaFundoJogo";

    }
}
