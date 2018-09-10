using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRespawnMoviment : MonoBehaviour {

    [SerializeField] private Animator[] animator;
    private float timer;
    private float cooldown;
    private int archId;
	// Use this for initialization
	void Start () {
        timer = 0;
        cooldown = 10.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (archId < animator.Length) {

            timer += Time.deltaTime;

            if (cooldown <= timer) {

                animator[archId].SetBool("run", true);

                archId++;
                timer = 0;
                cooldown *= 2;
            }
        }
	}
}
