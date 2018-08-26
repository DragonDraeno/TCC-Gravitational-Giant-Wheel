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

    [SerializeField] private ControlGame controlGame;

    void Start()
    {
        limitaVelMax = 30;
        limitaVelMin = -30;

        limitaVelMaxUp = 30;
        limitaVelMinDown = -30;

        speedBase = 30;
        speedInWallColide = 800;
        
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
            if (tempoOuloGigante >= 3)
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

            limitaVelMaxUp = limitaVelMaxUp + 50;
            limitaVelMinDown = limitaVelMinDown - 50;
            float y = Vector3.Dot(transform.forward, Vector3.Normalize(GetComponent<Rigidbody>().velocity));
            GetComponent<Rigidbody>().AddForce(transform.forward * y * 3000 * Time.deltaTime, ForceMode.Impulse);
        }

        if (collision.gameObject.tag == "ArchUp")
        {
            limitaVelMaxUp += limitaVelMaxUp;
            limitaVelMax += limitaVelMax;
            limitaVelMin += limitaVelMin;
            float y = Vector3.Dot(transform.forward, Vector3.Normalize(GetComponent<Rigidbody>().velocity));
            if (y > 0) {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, Mathf.Sign(-GetComponent<Rigidbody>().velocity.y) * (limitaVelMaxUp * 50) * Time.deltaTime, Mathf.Sign(-GetComponent<Rigidbody>().velocity.z) * (limitaVelMaxUp * 50) * Time.deltaTime), ForceMode.Impulse);
            }
            else {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, Mathf.Sign(GetComponent<Rigidbody>().velocity.y) * (limitaVelMaxUp * 50) * Time.deltaTime, Mathf.Sign(GetComponent<Rigidbody>().velocity.z) * (limitaVelMaxUp * 50) * Time.deltaTime), ForceMode.Impulse);
            }
        }

        if (collision.gameObject.tag == "ArchDown")
        {
            limitaVelMinDown += limitaVelMinDown;
            limitaVelMax += limitaVelMax;
            limitaVelMin += limitaVelMin;
            float y = Vector3.Dot(transform.forward, Vector3.Normalize(GetComponent<Rigidbody>().velocity));
            if (y < 0)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, Mathf.Sign(GetComponent<Rigidbody>().velocity.y) * (limitaVelMinDown * 50) * Time.deltaTime, Mathf.Sign(GetComponent<Rigidbody>().velocity.z) * (limitaVelMinDown * 50) * Time.deltaTime), ForceMode.Impulse);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, Mathf.Sign(-GetComponent<Rigidbody>().velocity.y) * (limitaVelMinDown * 50) * Time.deltaTime, Mathf.Sign(-GetComponent<Rigidbody>().velocity.z) * (limitaVelMinDown * 50) * Time.deltaTime), ForceMode.Impulse);
            }
            //GetComponent<Rigidbody>().AddForce(-(Vector3.Normalize(transform.forward) * (200 * limitaVelMinDown) * Time.deltaTime), ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Moeda")
        {
            controlGame.Timer += 5;
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

        /*if (Input.GetTouch(0).deltaPosition.x > 0)
        {
            resetSideVelocity = true;
            GetComponent<Rigidbody>().AddForce(new Vector3(500, 0, 0));
        } else
        if (Input.GetTouch(0).deltaPosition.x < 0)
        {
            resetSideVelocity = false;
            GetComponent<Rigidbody>().AddForce(new Vector3(-500, 0, 0));
        }*/

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

        GetComponent<Rigidbody>().AddForce(-transform.forward * y * speedBase * Time.deltaTime, ForceMode.Impulse);

        if (GetComponent<Rigidbody>().velocity.y >= limitaVelMaxUp) {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, limitaVelMaxUp, GetComponent<Rigidbody>().velocity.z);
        }
        if (GetComponent<Rigidbody>().velocity.z >= limitaVelMaxUp)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, limitaVelMaxUp);
        }
        if (GetComponent<Rigidbody>().velocity.y <= limitaVelMinDown)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, limitaVelMinDown, GetComponent<Rigidbody>().velocity.z);
        }
        if (GetComponent<Rigidbody>().velocity.z <= limitaVelMinDown)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, limitaVelMinDown);
        }
    }

    private void DecrementVelocity()
    {
        float DecrementSpeed = 10;
        float DecrementSpeedSide = 50;
       
        if (limitaVelMaxUp > speedBase) {
            limitaVelMaxUp -= Time.deltaTime * DecrementSpeed;
        }

        if (limitaVelMinDown < -speedBase) {
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

        Vector3 velocityCurrent = GetComponent<Rigidbody>().velocity;
        float velocityLimiteMax = 150;

        if (velocityCurrent.x > velocityLimiteMax) {
            GetComponent<Rigidbody>().velocity = new Vector3(velocityLimiteMax, velocityCurrent.y, velocityCurrent.z);
        }
        if (velocityCurrent.y > velocityLimiteMax)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(velocityCurrent.x, velocityLimiteMax, velocityCurrent.z);
        }
        if (velocityCurrent.z > velocityLimiteMax)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(velocityCurrent.x, velocityCurrent.y, velocityLimiteMax);
        }
    }
}
