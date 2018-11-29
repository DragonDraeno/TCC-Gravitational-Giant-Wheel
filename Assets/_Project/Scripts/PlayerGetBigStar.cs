using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetBigStar : MonoBehaviour {

    private bool isColider = false;
    private float tempoOuloGigante = 0;

    [SerializeField] private Collider playerColider;

    private Collider gameObjectColider;

    private void Start()
    {
        gameObjectColider = this. GetComponent<Collider>();
    }

    void FixedUpdate()
    {

        if (playerColider.enabled == true)
        {
            gameObjectColider.enabled = false;
        }
        else {
            gameObjectColider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        /*if (collision.gameObject.tag == "plataformaImpulsiona")
        {
            GetComponent<Collider>().enabled = true;
            isColider = true;
        }*/
    }
}
