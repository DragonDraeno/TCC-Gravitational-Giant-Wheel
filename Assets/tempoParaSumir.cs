using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempoParaSumir : MonoBehaviour {

    float tempo;

	// Use this for initialization
	void Start () {
        tempo = 2;

    }
	
	// Update is called once per frame
	void Update () {
        tempo += Time.deltaTime * 1f;
        if (tempo >= 5) {
            Destroy(gameObject);
        }
    }
}
