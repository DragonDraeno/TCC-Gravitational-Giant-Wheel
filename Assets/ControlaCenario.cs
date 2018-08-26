using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControlaCenario : MonoBehaviour {

    //GameObject personagem;
    public float velRot;
    public float tempoFase;
    public GameObject canvasScore;
    public GameObject personagem;

    // Use this for initialization
    void Start () {
        //personagem = GameObject.Find("personagem");
        personagem = GameObject.Find("personagem");
        
        canvasScore = GameObject.Find("CanvasScore");
        canvasScore.GetComponent<Canvas>().targetDisplay = 2;

        tempoFase = 1;

    }

    // Update is called once per frame
    void Update() {
        if (GameObject.Find("menuInicial")) {
            if (GameObject.Find("menuInicial").GetComponent<Canvas>().targetDisplay == 0) {
                tempoFase = 1;
            }
        }
        velRot = 0.5f;
        tempoFase += Time.deltaTime;
        if (tempoFase > 100) { // um minuto == 60
            canvasScore.GetComponent<Canvas>().targetDisplay = 0;
            if (tempoFase > 101){
                Destroy(personagem);
            }
            //SceneManager.LoadScene(0);
        }


        transform.Rotate(velRot * Time.deltaTime, 0, 0);

        //transform.localRotation = new
        //transform.Rotate(-2 * Time.deltaTime, 0, 0);

        //transform.Rotate(Vector3.right, Space.World);
        //transform.position = new Vector3(0,0,200);

    }

}
