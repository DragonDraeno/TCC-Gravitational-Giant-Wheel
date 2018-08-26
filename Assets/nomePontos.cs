using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nomePontos : MonoBehaviour {

    public GameObject menuInicial;
    public string nome;
    int encontraNome;

    // Use this for initialization
    void Start () {
        nome = "";
        encontraNome = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (menuInicial.GetComponent<controlaMenuInicial>().nomeTxt != "" && encontraNome == 0) {
            nome = menuInicial.GetComponent<controlaMenuInicial>().nomeTxt;
            encontraNome = 1;
        }

    }


}
