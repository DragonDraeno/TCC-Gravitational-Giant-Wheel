using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaSomFundo : MonoBehaviour {

    public AudioClip jogoSomFundo;
    public AudioClip menuSomFundo;

    // Use this for initialization
    void Start () {
        if (GameObject.Find("personagem"))
        {
            AudioSource.PlayClipAtPoint(jogoSomFundo, transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(menuSomFundo, transform.position);
        }
    }
	
	// Update is called once per frame
	void Update () {


    }
}
