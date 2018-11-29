using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEfx : MonoBehaviour {

    [SerializeField] private AudioSource starColision;
    [SerializeField] private AudioSource airShootColision;
    [SerializeField] private AudioSource arcImp;
    [SerializeField] private AudioSource arc;
    [SerializeField] private AudioSource arcReflect;

    [SerializeField] private AudioSource clickButon;
    [SerializeField] private AudioSource transictionPanel;
    [SerializeField] private AudioSource glitch;

    private float volumeEfx;

    public AudioSource StarColision { get { return starColision; } set { starColision = value; } }
    public AudioSource AirShootColision { get { return airShootColision; } set { airShootColision = value; } }
    public AudioSource Arc { get { return arc; } set { arc = value; } }
    public AudioSource ArcReflect { get { return arcReflect; } set { arcReflect = value; } }

    public AudioSource ClickButon { get { return clickButon; } set { clickButon = value; } }
    public AudioSource TransictionPanel { get { return transictionPanel; } set { transictionPanel = value; } }
    public AudioSource Glitch { get { return glitch; } set { glitch = value; } }
    public AudioSource ArcImp { get { return arcImp; } set { arcImp = value; } }


    private void Awake()
    {
        volumeEfx = PlayerPrefs.GetFloat("sondVolPP");

        StarColision.volume = volumeEfx;
        AirShootColision.volume = volumeEfx;
        ArcImp.volume = volumeEfx;
        Arc.volume = volumeEfx;
        ArcReflect.volume = volumeEfx;

        ClickButon.volume = volumeEfx;
        TransictionPanel.volume = volumeEfx;
        Glitch.volume = volumeEfx;
    }
}
