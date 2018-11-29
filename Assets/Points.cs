using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour {
    
    [SerializeField] ControlGame controlGame;
    private BoxCollider starColider;

    private bool personagemColision;
    private float timerToReturn;
    private float timerRuning;

    public bool PersonagemColision { get { return personagemColision; } set { personagemColision = value; } }
    public float TimerToReturn { get { return timerToReturn; } set { timerToReturn = value; } }
    
    void Start () {
        personagemColision = false;
        timerRuning = 0;
        starColider = gameObject.GetComponent<BoxCollider>();
    }

	void Update () {

        if (personagemColision == true) {
            timerRuning += Time.deltaTime;

            if (timerRuning >= timerToReturn)
            {
                print(timerRuning);
                personagemColision = false;
                timerRuning = 0;

                Transform childAux = GetChildOfChild();
                childAux.GetComponent<MeshRenderer>().enabled = true;
                Component halo = childAux.GetComponent("Halo");
                halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
                starColider.enabled = true;
            }
        }
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "personagem")
        {
            if (GetChildOfChild() != null) {
                Transform childAux = GetChildOfChild();
                Component halo = childAux.GetComponent("Halo");
                halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                childAux.GetComponent<MeshRenderer>().enabled = false;
                starColider.enabled = false;
            }
        }

        if (collision.gameObject.name == "personagemx")
        {
            if (GetChildOfChild() != null)
            {
                Transform childAux = GetChildOfChild();
                Component halo = childAux.GetComponent("Halo");
                halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                childAux.GetComponent<MeshRenderer>().enabled = false;
                starColider.enabled = false;
            }
        }
    }

    private Transform GetChildOfChild() {

        foreach (Transform child in transform)
        {    
            foreach (Transform child2 in child.transform)
            {
                return child2;
            }
        }
        return null;
    }

}
