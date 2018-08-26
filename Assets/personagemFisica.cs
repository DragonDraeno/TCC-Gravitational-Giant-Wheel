using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personagemFisica : MonoBehaviour
{

    public Animator personagemAnimacao;
    public Animator cameraAnimacao;
    bool plataformaColidindo;
    bool clickUp;
    bool clickDown;
    float tempoPulo;
    float tempDownDash;
    JointMotor motor;
    float targetVelocity;
    float force;
    float velocidade;
    bool ladoAtaque; //pagar o primeiro momento de um lado e zerar a velocidade do outro para nao ter dlay no movimento <- ou ->
    // Use this for initialization
    float limitaVelMax;
    float limitaVelMin;

    public GameObject pegaPonto;
    public float pontos;// publica para se usada no canvas

    float tempoOuloGigante;

    public int du;
    public int dd;
    public float dTimer;

    void Start()
    {
        plataformaColidindo = false;
        GetComponent<HingeJoint>().useMotor = true;
        motor = GetComponent<HingeJoint>().motor;
        limitaVelMax = 30;
        limitaVelMin = -30;
        pontos = 0;

        du = 0;
        dd = 0;
        dTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movimentar();// faz o movimento
        Pular();// da força e velcidade para o motor
        Dash();// da um burst no movimento indicado quando no ar

        if (personagemAnimacao.GetInteger("pulo") == 1) {

            tempoOuloGigante += 1 * Time.deltaTime;
            if (tempoOuloGigante >= 3) {
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

            tempoPulo = 0;
            clickUp = false;
            plataformaColidindo = true;

            print("animação");
            tempoOuloGigante = 0;
            personagemAnimacao.SetInteger("pulo", 1);

            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            motor.targetVelocity = 200000;
            motor.force = 200000;
            GetComponent<HingeJoint>().motor = motor;
        }

    }

    private void OnTriggerStay(Collider collision)
    {

        //print(collision.tag);

        if (collision.gameObject.tag == "Moeda")
        {

            if (collision.gameObject.name == "Moeda1")
            {
                pontos += 50;

            }
            else if (collision.gameObject.name == "Moeda2")
            {

                pontos += 100;

            }
            else if (collision.gameObject.name == "Moeda3")
            {
                pontos += 200;
            }

            Instantiate(pegaPonto, transform.position, Quaternion.identity);

            //collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "arShoot")
        {

            GetComponent<Rigidbody>().AddForce(new Vector3(800, 0, 0));

        }

        if (collision.gameObject.tag == "arShootEsq")
        {

            GetComponent<Rigidbody>().AddForce(new Vector3(-800, 0, 0));

        }

        if (collision.gameObject.tag == "plataformaMovel")
        {
            print("martelada");
            //GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 5000));
            //GetComponent<HingeJoint>().connectedBody = null;
            //GetComponent<HingeJoint>().connectedAnchor = new Vector3(GetComponent<HingeJoint>().connectedAnchor.x*20, GetComponent<HingeJoint>().connectedAnchor.y * 20, GetComponent<HingeJoint>().connectedAnchor.z * 20);
        }

    }

    private void OnCollisionStay(Collision collision)
    {

        if (Pulo() == 0)
        {

            if (collision.gameObject.tag == "Plataforma" || collision.gameObject.tag == "PlataformaGiratoria" || collision.gameObject.tag == "PlataformaPulaPula")
            {
                plataformaColidindo = true;

            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Plataforma" || collision.gameObject.tag == "PlataformaGiratoria")
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);

            limitaVelMax = 30;
            limitaVelMin = -30;
        }

        if (collision.gameObject.tag == "PlataformaPulaPula")
        {

            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, +40, GetComponent<Rigidbody>().velocity.z);

            limitaVelMax = 30;
            limitaVelMin = -30;
        }

    }

    private void OnCollisionExit(Collision collision)
    {

        plataformaColidindo = false;

    }

    private int Pulo()//verifica quando pular
    {

        //Raycast preve 3 unidade de medida do perssonagem fazendo com que ele nao fique parado na parte 
        //inferior nem lateral das plataformas, somente na parte superior.

        float z, y;

        y = transform.position.y;
        z = transform.position.z;

        int pulo = -1;
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), 3) == true)
        {
            if (y >= 0 && z >= 200)
            {
                pulo = 1;

            }
            else if (y <= 0 && z >= 200)
            {
                pulo = 1;

            }
            else
            {
                pulo = 0;
            }
            //print("-y");
        }
        if (Physics.Raycast(transform.position, new Vector3(0, 0, -1), 3) == true)
        {
            if (y <= 0 && z >= 200)
            {
                pulo = 1;

            }
            else if (y <= 0 && z <= 200)
            {
                pulo = 1;

            }
            else
            {
                pulo = 0;
            }
            //print("-z");

        }
        if (Physics.Raycast(transform.position, new Vector3(0, 1, 0), 3) == true)
        {
            if (y >= 0 && z <= 200)
            {
                pulo = 1;
            }
            else
            if (y <= 0 && z <= 200)
            {
                pulo = 1;
            }
            else
            {
                pulo = 0;
            }
            //print("+y");
        }
        if (Physics.Raycast(transform.position, new Vector3(0, 0, 1), 3) == true)
        {
            if (y >= 0 && z >= 200)
            {
                pulo = 1;
            }
            else
            if (y >= 0 && z <= 200)
            {
                pulo = 1;
            }
            else
            {
                pulo = 0;
            }
            // print("+z");
        }

        return pulo;
    }

    private void Movimentar()
    {
        velocidade = GetComponent<Rigidbody>().velocity.x;

        //movimentos laterais-----------------------------------
        if (Input.GetAxis("Horizontal") > 0)
        {//dir
            ladoAtaque = true;
            //GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            //transform.Translate(1 * andar * Time.deltaTime, transform.position.y, transform.position.y);
            GetComponent<Rigidbody>().AddForce(new Vector3(500, 0, 0));
            //transform.Rotate(10, transform.rotation.y, transform.rotation.z);

        }

        if (Input.GetAxis("Horizontal") < 0)//esq
        {
            //GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            ladoAtaque = false;
            GetComponent<Rigidbody>().AddForce(new Vector3(-500, 0, 0));
            //transform.Translate(-1 * andar * Time.deltaTime, transform.rotation.y, transform.rotation.y);
            //transform.Rotate(-10, transform.rotation.y, transform.rotation.z);
        }

        //limitar movimento laterais---------------------------------------------------------------------------
        if (ladoAtaque == true && velocidade < 0)
        {// 0 minimo velocidade
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }
        if (ladoAtaque == false && velocidade > 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }

        if (velocidade >= limitaVelMax)
        { //30 max velocidade
            GetComponent<Rigidbody>().velocity = new Vector3(30, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }

        if (velocidade <= limitaVelMin)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-30, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }
        //-----------------------------------------------------------------------------------------------------

        //print(velocidade);
    }
    private void Pular()
    {
        tempoPulo += Time.deltaTime;

        if (plataformaColidindo == true && Input.GetAxis("Vertical") > 0)
        {
            tempoPulo = 0;
            clickUp = true;
            plataformaColidindo = false;
        }

        if (clickUp == true)
        {

            if (tempoPulo < 0.5f)
            {
                motor.targetVelocity = 24000;
                motor.force = 24000;
                GetComponent<HingeJoint>().motor = motor;
            }
            else if (tempoPulo >= 0.5f && tempoPulo < 1.2f)
            {
                motor.targetVelocity = -24000;
                motor.force = 24000;
                GetComponent<HingeJoint>().motor = motor;
            }

        }

        if (tempoPulo >= 1.2f)
        {
            tempoPulo = 0;
            clickUp = false;
            motor.targetVelocity = -1000;
            motor.force = 1000;
            GetComponent<HingeJoint>().motor = motor;
        }
    }

    private void Dash()
    {
        if (plataformaColidindo == false)
        {
            //cima-Dash------------------------------------------------------------------------------
            if (Input.GetAxis("Vertical") > 0 && du == 0 && Input.GetKeyDown(KeyCode.Space))
            {
                du = 1;
                dd = 0;
                dTimer = 0;

                motor.targetVelocity = 400000;
                motor.force = 400000;
                GetComponent<HingeJoint>().motor = motor;
            }
            else
            if (du == 1)
            {
                dTimer += 1 * Time.deltaTime;

                motor.targetVelocity = -1000;
                motor.force = 1000;
                GetComponent<HingeJoint>().motor = motor;
                
                if (dTimer >= 5)//cd dash
                {
                    du = 0;
                    dTimer = 0;
                }
            }
            ////-------------------------------------------------------------------------------------------

            ////baixo-Dash------------------------------------------------------------------------------
            if (Input.GetAxis("Vertical") < 0 && dd == 0 && Input.GetKeyDown(KeyCode.Space))
            {
                dd = 1;
                du = 0;
                dTimer = 0;

                motor.targetVelocity = -400000;
                motor.force = 400000;
                GetComponent<HingeJoint>().motor = motor;
            }
            else
            if (dd == 1)
            {
                dTimer += 1 * Time.deltaTime;

                motor.targetVelocity = -1000;
                motor.force = 1000;
                GetComponent<HingeJoint>().motor = motor;

                if (dTimer >= 5)//cd dash
                {
                    dd = 0;
                    dTimer = 0;
                }

            }

        }else//reseta variaveis quando colidirem com plataformas
        {
            dd = 0;
            du = 0;
            dTimer = 0;
        }
    }

}
