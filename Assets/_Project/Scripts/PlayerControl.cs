using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TouchControlsKit;

public class PlayerControl : MonoBehaviour
{

    public Animator personagemAnimacao;
    public Animator cameraAnimacao;
    
    float speedSide;
    float speedBase;

    bool resetSideVelocity; //pegar o primeiro momento de um lado e zerar a velocidade do outro para nao ter dlay no movimento <- ou ->

    float limitaVelMax;
    float limitaVelMin;

    float limitaVelMaxUp;
    float limitaVelMinDown;

    float speedInWallColide;

    public GameObject pegaPonto;

    float tempoOuloGigante;

    bool upOrDown;
    float speedUpOrDown;

    float coinBonus;

    [SerializeField] private ControlGame controlGame;
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        limitaVelMax = 30;
        limitaVelMin = -30;

        limitaVelMaxUp = 10;
        limitaVelMinDown = -10;

        speedBase = 30;
        speedUpOrDown = 10;
        speedInWallColide = 800;

        coinBonus = 5;

        upOrDown = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovimentarLados();// faz o movimento
        MovimentarVertical();
        DecrementVelocity();
        LimitVelocity();

        if (personagemAnimacao.GetInteger("pulo") == 1)
        {
            tempoOuloGigante += 1 * Time.deltaTime;
            if (tempoOuloGigante >= 4)
            {
                tempoOuloGigante = 0;
                GetComponent<Collider>().enabled = true;
                personagemAnimacao.SetInteger("pulo", 0);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "plataformaImpulsiona")
        {
            tempoOuloGigante = 0;
            personagemAnimacao.SetInteger("pulo", 1);

            limitaVelMaxUp += 10;
            limitaVelMinDown -= 10;
        }

        if (collision.gameObject.tag == "ArchUp")
        {
            limitaVelMaxUp += 5;
            limitaVelMax += limitaVelMax;
            limitaVelMin += limitaVelMin;
        }

        if (collision.gameObject.tag == "ArchDown")
        {
            limitaVelMinDown -= 5;
            limitaVelMax += limitaVelMax;
            limitaVelMin += limitaVelMin;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Moeda")
        {
            if(coinBonus <= 0.01f)
            {
                coinBonus = 0.01f;
            }

            controlGame.Timer += coinBonus;

            coinBonus -= Time.deltaTime;
            Instantiate(pegaPonto, transform.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "arShoot")
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(800, 0, 0));
        }

        if (collision.gameObject.tag == "arShootEsq")
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(-800, 0, 0));
        }

        if (collision.gameObject.tag == "WallH")
        {

            GetComponent<Rigidbody>().AddForce(new Vector3(-speedInWallColide, 0, 0));
        }

        if (collision.gameObject.tag == "WallL")
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(speedInWallColide, 0, 0));
        }
    }

    private void MovimentarLados()
    {
        speedSide = GetComponent<Rigidbody>().velocity.x;

        float x = TCKInput.GetAxis("Joystick", EAxisType.Horizontal);

        if (x > 0)//dir
        {
            resetSideVelocity = true;
            GetComponent<Rigidbody>().AddForce(new Vector3(8000 * x * Time.deltaTime, 0, 0));
        }

         if (x < 0)//esq
         {
             resetSideVelocity = false;
             GetComponent<Rigidbody>().AddForce(new Vector3(8000 * x * Time.deltaTime, 0, 0));
         }

        //limitar movimento laterais---------------------------------------------------------------------------
        if (resetSideVelocity == true && speedSide < 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }
        if (resetSideVelocity == false && speedSide > 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }

        if (speedSide >= limitaVelMax)
        { //30 max velocidade
            GetComponent<Rigidbody>().velocity = new Vector3(limitaVelMax, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }

        if (speedSide <= limitaVelMin)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(limitaVelMin, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }
    }

    private void MovimentarVertical()
    {
        float y = TCKInput.GetAxis("Joystick", EAxisType.Vertical);
     
        if (y > 0.001)
        {
            upOrDown = true;
        }
        else if(y < -0.001){
            upOrDown = false;
        }

        if (upOrDown == true)
        {
            transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), limitaVelMaxUp * Time.deltaTime);
        }
        else {
            transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), limitaVelMinDown * Time.deltaTime);
        }
    }

    private void DecrementVelocity()
    {
        float DecrementSpeed = 2;
        float DecrementSpeedSide = 10;

        if (limitaVelMaxUp >= 10) {
            limitaVelMaxUp -= Time.deltaTime * DecrementSpeed;
        }

        if (limitaVelMinDown <= -10) {
            limitaVelMinDown += Time.deltaTime * DecrementSpeed;
        }

        if (limitaVelMax > speedBase)
        {
            limitaVelMax -= Time.deltaTime * DecrementSpeedSide;
            limitaVelMin += Time.deltaTime * DecrementSpeedSide;
        }
    }

    private void LimitVelocity()
    {
        float velocityLimiteMax = 100;

        if (limitaVelMaxUp >= velocityLimiteMax / 2) {
            limitaVelMaxUp = velocityLimiteMax / 2;
        }
        if (limitaVelMaxUp <= -velocityLimiteMax / 2)
        {
            limitaVelMaxUp = -velocityLimiteMax / 2;
        }
        if (limitaVelMax >= velocityLimiteMax)
        {
            limitaVelMax = velocityLimiteMax;
        }
        if (limitaVelMin <= -velocityLimiteMax)
        {
            limitaVelMin = -velocityLimiteMax;
        }
    }
}
