using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontos : MonoBehaviour {

    //public GameObject personagem;

    int ativa;
    float timerAtivar;
    Vector3 posPrimaria;
    //public bool ativa;
    // Use this for initialization
    void Start () {

        ativa = 0;
        timerAtivar = 0;

        posPrimaria = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if (ativa == 1) {//estrela desativada

            timerAtivar += 1 * Time.deltaTime;
            if (timerAtivar > 60) {//cd para ativar
                transform.position = posPrimaria;
            }
        }

    }
    private void OnTriggerStay(Collider collision)
    {
       // print("collisioncollisioncollisioncollision: " + gameObject.name);
        if (collision.gameObject.name == "personagem")
        {
            
            transform.position = new Vector3(0, 0, 1000);
            ativa = 1;
            timerAtivar = 0;
        }

    }

}
