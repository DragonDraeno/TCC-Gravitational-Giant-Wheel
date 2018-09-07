using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TouchControlsKit;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private ControlGame controlGame;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject UpBtn;
    [SerializeField] private GameObject DownBtn;

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

    bool backStabUp;
    bool backStabDown;
    float timerBackStab;

    void Start()
    {
        UpBtn.SetActive(false);
        DownBtn.SetActive(false);

        limitaVelMax = 30;
        limitaVelMin = -30;

        limitaVelMaxUp = 10;
        limitaVelMinDown = -10;

        speedBase = 30;
        speedUpOrDown = 10;
        speedInWallColide = 800;

        coinBonus = 5;

        upOrDown = true;
        backStabUp = false;
        backStabDown = false;
        timerBackStab = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovimentarLados();// faz o movimento
        MovimentarVertical();
        DecrementVelocity();
        LimitVelocity();
        BackStab();
        ChangeSpriteUpOrDown();

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
           /* Vector3 currentAngle = new Vector3( Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
                                        Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
                                        Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime)
                                        );

            transform.eulerAngles = currentAngle;*/
   
            tempoOuloGigante = 0;
            personagemAnimacao.SetInteger("pulo", 1);

            limitaVelMaxUp += 10;
            limitaVelMinDown -= 10;
        }

        if (collision.gameObject.tag == "ArchUp")
        {
            limitaVelMaxUp += 5;
            limitaVelMinDown = -10;
            limitaVelMax += limitaVelMax;
            limitaVelMin += limitaVelMin;

            if (upOrDown == true)
            {
                print("esta andando corretamente para cima");
            }
            else {
                print("esta andando para o lado contrario");
                backStabUp = true;
                upOrDown = true;
                limitaVelMinDown = -limitaVelMaxUp;
            }
        }
        
        if (collision.gameObject.tag == "ArchDown")
        {
            limitaVelMinDown -= 5;
            limitaVelMaxUp = 10;

            limitaVelMax += limitaVelMax;
            limitaVelMin += limitaVelMin;

            if (upOrDown == false)
            {
                print("esta andando corretamente para baixo");
            }
            else
            {
                print("esta andando para o lado contrario");
                backStabDown = true;
                upOrDown = false;
                limitaVelMaxUp = Mathf.Abs(limitaVelMinDown);
            }
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
        
        if (PlayerPrefs.GetInt("ButtonOrToutch") == 0)
        {
            if (TCKInput.GetAction("hightBtn", EActionEvent.Press))//dir
            {
                resetSideVelocity = true;
                GetComponent<Rigidbody>().AddForce(new Vector3(5000 * Time.deltaTime, 0, 0));
            }

            if (TCKInput.GetAction("leftBtn", EActionEvent.Press))//esq
            {
                resetSideVelocity = false;
                GetComponent<Rigidbody>().AddForce(new Vector3(-5000 * Time.deltaTime, 0, 0));
            }
        }
        else {
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
        if (PlayerPrefs.GetInt("ButtonOrToutch") == 0)
        {
            if (TCKInput.GetAction("ChangeUp", EActionEvent.Up))
            {
                upOrDown = true;
            }else
            if (TCKInput.GetAction("ChangeDown", EActionEvent.Up))
            {
                upOrDown = false;
            }
        }
        else
        {
            float y = TCKInput.GetAxis("Joystick", EAxisType.Vertical);

            if (y > 0.001)
            {
                upOrDown = true;
            }
            else if (y < -0.001)
            {
                upOrDown = false;
            }
        }

        if ((upOrDown == true || backStabUp == true) && backStabDown == false)
        {
            transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), limitaVelMaxUp * Time.deltaTime);
        }
        else if((upOrDown == false || backStabDown == true) && backStabUp == false)
        {
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

    private void BackStab()
    {
        if (backStabUp == true) {
            timerBackStab -= Time.deltaTime;
            if (timerBackStab <= 0 && upOrDown == false)
            {
                backStabUp = false;
                timerBackStab = 1;
            }
        }

        if (backStabDown == true)
        {
            timerBackStab -= Time.deltaTime;
            if (timerBackStab <= 0 && upOrDown == true)
            {
                backStabDown = false;
                timerBackStab = 1;
            }
        }
    }

    private void ChangeSpriteUpOrDown()
    {
        if (upOrDown == false)
        {
            DownBtn.SetActive(false);
            UpBtn.SetActive(true);
        }
        else {
            UpBtn.SetActive(false);
            DownBtn.SetActive(true);
        }
    }
}
