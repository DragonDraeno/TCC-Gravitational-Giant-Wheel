using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mudaParede : MonoBehaviour {

    public GameObject personagem;

    public Material upMatB;
    public Material upMatG;

    public RuntimeAnimatorController upAniB;
    public RuntimeAnimatorController upAniG;

    private void Start()
    {
        personagem = GameObject.Find("personagem");
    }

    void Update () {
        if (lado() == true)
        {
            GetComponent<MeshRenderer>().material = upMatB;
            GetComponent<Animator>().runtimeAnimatorController = upAniG;
        }
        else
        {
            GetComponent<MeshRenderer>().material = upMatG;
            GetComponent<Animator>().runtimeAnimatorController = upAniB;
        }
    }

    bool lado()
    {
        if (personagem)
        {
            if (Vector3.Dot(personagem.transform.forward, Vector3.Normalize(personagem.GetComponent<Rigidbody>().velocity)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else {
            return true;
        }
    }
}
