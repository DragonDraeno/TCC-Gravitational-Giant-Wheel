using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSide : MonoBehaviour {

    [SerializeField] private GameObject archUp;
    [SerializeField] private GameObject archDown;

    private Animator aimator;

    private void Awake()
    {
        aimator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            aimator.SetBool("run", true);
        }
    }

    public void Change() {
        if (archUp.activeSelf == true) {
            archUp.SetActive(false);
            archDown.SetActive(true);
        } else {
            archUp.SetActive(true);
            archDown.SetActive(false);
        }

        aimator.SetBool("run", false);
    }
}
