using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cenario : MonoBehaviour {

    GameObject contanier;
    GameObject contanierPlataformas;

    GameObject clone;
    GameObject cloneAux;

    public GameObject parede;
    public GameObject plataforma01;
    public GameObject plataformaGiratoria;
    public GameObject plataformaPulaPula;
    public GameObject modeloParedePulaPula;
    public GameObject modeloMoedas1;
    public GameObject modeloMoedas2;
    public GameObject modeloMoedas3;
    public GameObject arShoot;
    public GameObject plataformaMovel;
    public GameObject plataformaImpulsiona;
    float step;//n° peace
    int r;//raio

    float xx, yy;//posoção inicial x e y

    int plataformaEspacos;//espaços para montar as plaaformas que precisam de mais autura. sendo assim, deve-se nao fazer algumas das plataformas normais.

    JointMotor motorPlataformas;

    int tipoPlataforma;

    // Use this for initialization
    void Start () {
        plataformaEspacos = 0;

        step = 2 * Mathf.PI / 60; // 80 é o numero de obj que tera no cenatio para uma volta
        r = 195; // raio

        xx = 0;
        yy = 0;

        parede = GameObject.Find("modeloParede");
        plataforma01 = GameObject.Find("plataformaModelo");
        plataformaGiratoria = GameObject.Find("plataformaGiratoria");
        plataformaPulaPula = GameObject.Find("plataformaPulaPula");
        modeloParedePulaPula = GameObject.Find("modeloParedePulaPula");
        modeloMoedas1 = GameObject.Find("Moedas1");
        modeloMoedas2 = GameObject.Find("Moedas2");
        modeloMoedas3 = GameObject.Find("Moedas3");
        arShoot = GameObject.Find("armaDeAr");
        plataformaMovel = GameObject.Find("plataformaMovel");
        plataformaImpulsiona = GameObject.Find("plataformaImpulsiona");
        //---------------------------------------------------------------------------------

        contanier = new GameObject();
        contanier.name = "cenario";
        contanier.AddComponent<ControlaCenario>();
        contanier.transform.position = new Vector3(0, 0, 200);//centralizar o pivot
        contanier.AddComponent<Rigidbody>();
        contanier.GetComponent<Rigidbody>().mass = 1000;
        //contanier.GetComponent<Rigidbody>().useGravity = false;

        contanier.AddComponent<HingeJoint>();
        contanier.GetComponent<HingeJoint>().useMotor = true;
        JointMotor motor = contanier.GetComponent<HingeJoint>().motor;
        motor.targetVelocity = 8500;
        motor.force = 8500;
        contanier.GetComponent<HingeJoint>().motor = motor;

        //---------------------------------------------------------------------------------

        contanierPlataformas = new GameObject();
        contanierPlataformas.name = "motorPlataformas";
        contanierPlataformas.tag = "Plataforma";
        contanierPlataformas.AddComponent<ControlaCenario>();
        contanierPlataformas.transform.position = new Vector3(0, 0, 200);//centralizar o pivot
        contanierPlataformas.AddComponent<Rigidbody>();
        //contanierPlataformas.GetComponent<Rigidbody>().mass = 1000;
        ////contanier.GetComponent<Rigidbody>().useGravity = false;

        contanierPlataformas.AddComponent<HingeJoint>();
        //contanierPlataformas.GetComponent<HingeJoint>().useMotor = true;
        //motorPlataformas = contanierPlataformas.GetComponent<HingeJoint>().motor;
        //motorPlataformas.targetVelocity = 8500;
        //motorPlataformas.force = 300000;
        //contanierPlataformas.GetComponent<HingeJoint>().motor = motorPlataformas;

        //--------------------------------------------------------------------------------

        int randTipoPlataforma = Random.Range(0,100);
        

        for (float theta = 0; theta < 2 * Mathf.PI; theta += step)
        {
            xx = r * Mathf.Cos(theta);
            yy = r * Mathf.Sin(theta);

            transform.position = new Vector3(transform.position.x, yy, xx);

            xx = r * Mathf.Cos(theta/2);
            yy = r * Mathf.Sin(theta/2);

            transform.localRotation = new Quaternion(transform.localRotation.x, yy, xx, transform.localRotation.w);

            Paredes();
            MoedasEscolhe();

            if (randTipoPlataforma <= 50) {
                if (randTipoPlataforma <= 5)
                {
                    PlataformaImpulsiona();

                }else{
                    PlataformaNotmal();
                }

                randTipoPlataforma = Random.Range(0, 100);
            }else

            if (randTipoPlataforma > 50 && randTipoPlataforma <= 80)
            {

                if (plataformaEspacos == 2)//2 numero de espaços para cima
                {
                    PlataformaPulaPula();
                }

                plataformaEspacos++;
                if (plataformaEspacos == 3) //+1 para baixo
                {
                    plataformaEspacos = 0;
                    randTipoPlataforma = Random.Range(0, 100);
                }
            }
            else 
            if (randTipoPlataforma > 80 && randTipoPlataforma <= 100)
            {

                if (plataformaEspacos == 3)//2 numero de espaços para cima
                {
                    PlataformaGiratoria();
                }

                plataformaEspacos++;
                if (plataformaEspacos == 4) //+1 para baixo
                {
                    plataformaEspacos = 0;
                    randTipoPlataforma = Random.Range(0, 100);
                }
            }
           
            /*if (randTipoPlataforma < 10)
            {
                PlataformaMovel();

            }*/

        }

        Destroy(gameObject);
        Destroy(plataformaGiratoria);
        /*Destroy(parede); 
        Destroy(plataforma01);
        Destroy(modeloParedePulaPula);
        Destroy(plataformaGiratoria);
        Destroy(plataformaMovel);
        Destroy(plataformaPulaPula);
        Destroy(modeloMoedas1);
        Destroy(modeloMoedas2);
        Destroy(modeloMoedas3);
        Destroy(arShoot);
        Destroy(plataformaMovel);
        Destroy(plataformaImpulsiona);*/
    }

    // Update is called once per frame
    void Update () {
        
    }

    private void Paredes()
    {
        //parede esquerda
        if (Random.Range(0, 100) < 3)
        {
            clone = Instantiate(modeloParedePulaPula, transform.position, transform.localRotation);
            clone.transform.parent = contanier.transform;
            clone.transform.localScale = new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
            clone.transform.position = new Vector3(clone.transform.position.x - 50, clone.transform.position.y, clone.transform.position.z + 200);
            clone.name = "paredePulaPula";
        }
        else {
            clone = Instantiate(parede, transform.position, transform.localRotation);
            clone.transform.parent = contanier.transform;
            clone.transform.position = new Vector3(clone.transform.position.x - 50, clone.transform.position.y, clone.transform.position.z + 200);
            clone.name = "parede";

            if (Random.Range(0, 100) < 10)//arshoot na parede esquerda 
            {
                clone = Instantiate(arShoot, transform.position, transform.localRotation);
                clone.transform.parent = contanier.transform;
                clone.transform.Rotate(clone.transform.rotation.x+90, clone.transform.rotation.y, clone.transform.rotation.z+90);
                clone.transform.position = new Vector3(clone.transform.position.x - 23, clone.transform.position.y, clone.transform.position.z + 200);
                clone.name = "arShoot";
            }
        }

        //arede direita
        if (Random.Range(0, 100) < 3)
        {
            clone = Instantiate(modeloParedePulaPula, transform.position, transform.localRotation);
            clone.transform.parent = contanier.transform;
            clone.transform.localScale = new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
            clone.transform.position = new Vector3(clone.transform.position.x + 70, clone.transform.position.y, clone.transform.position.z + 200);
            clone.name = "paredePulaPula";
        }
        else {
            clone = Instantiate(parede, transform.position, transform.localRotation);
            clone.transform.parent = contanier.transform;
            clone.transform.position = new Vector3(clone.transform.position.x + 70, clone.transform.position.y, clone.transform.position.z + 200);
            clone.name = "parede2";

            if (Random.Range(0, 100) < 10)//arshoot na parede direita 
            {
                clone = Instantiate(arShoot, transform.position, transform.localRotation);
                clone.transform.parent = contanier.transform;
                clone.transform.Rotate(clone.transform.rotation.x+270, clone.transform.rotation.y, clone.transform.rotation.z + -90);
                clone.transform.position = new Vector3(clone.transform.position.x + 44, clone.transform.position.y, clone.transform.position.z + 200);
                clone.tag = "arShootEsq";
                for (int i = 0; i < clone.transform.childCount; i++)
                {
                    if (clone.transform.GetChild(i).transform.name == "ar")
                    {
                        clone.transform.GetChild(i).transform.tag = "arShootEsq";
                    }
                }
                clone.name = "arShoot";
            }
        }

    }

    private void PlataformaNotmal()
    {
        clone = Instantiate(plataforma01, transform.position, transform.localRotation);
        clone.transform.position = new Vector3(Random.Range(-30, 30), clone.transform.position.y, clone.transform.position.z);
        clone.transform.localScale = new Vector3(Random.Range(2, 6) * clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
        clone.transform.parent = contanierPlataformas.transform;
        clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z + 200);
        clone.name = "PlataformaNotmal";
    }

    private void PlataformaGiratoria()
    {
        
        clone = Instantiate(plataformaGiratoria, transform.position, transform.localRotation);
        clone.transform.rotation = new Quaternion(clone.transform.rotation.x, clone.transform.rotation.y, clone.transform.rotation.z, clone.transform.rotation.w);
        clone.transform.position = new Vector3(Random.Range(-20, 20), clone.transform.position.y, clone.transform.position.z + 200);
        clone.transform.localScale = new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
        clone.transform.parent = contanierPlataformas.transform;
        if (Random.Range(0, 100) > 50) {

            motorPlataformas.targetVelocity = -110;
            motorPlataformas.force = 1000;
            clone.GetComponent<HingeJoint>().motor = motorPlataformas;
        }
        clone.GetComponent<HingeJoint>().connectedBody = contanierPlataformas.GetComponent<Rigidbody>();
        clone.name = "PlataformaGiratoria";


    }

    private void PlataformaPulaPula()
    {

        clone = Instantiate(plataformaPulaPula, transform.position, transform.localRotation);
        clone.transform.rotation = new Quaternion(clone.transform.rotation.x, clone.transform.rotation.y, clone.transform.rotation.z, clone.transform.rotation.w);
        clone.transform.position = new Vector3(Random.Range(-25, 25), clone.transform.position.y, clone.transform.position.z + 202);
        clone.transform.localScale = new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
        clone.transform.parent = contanierPlataformas.transform;
        clone.GetComponent<HingeJoint>().connectedBody = contanierPlataformas.GetComponent<Rigidbody>();
        clone.name = "plataformaPulaPula";

    }

    private void PlataformaMovel()
    {
        clone = Instantiate(plataformaMovel, transform.position, transform.localRotation);
        clone.transform.position = new Vector3(0, clone.transform.position.y, clone.transform.position.z + 200);
        clone.transform.localScale = new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
        clone.transform.parent = contanierPlataformas.transform;
        clone.GetComponent<HingeJoint>().connectedBody = contanierPlataformas.GetComponent<Rigidbody>();
        clone.name = "plataformaMovel";
    }

    private void PlataformaImpulsiona()
    {
        clone = Instantiate(plataformaImpulsiona, transform.position, transform.localRotation);
        clone.transform.Rotate(clone.transform.rotation.x, clone.transform.rotation.y, clone.transform.rotation.z);
        clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z + 200);
        clone.transform.localScale = new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
        clone.transform.parent = contanierPlataformas.transform;
        clone.GetComponent<HingeJoint>().connectedBody = contanierPlataformas.GetComponent<Rigidbody>();
        clone.name = "plataformaImpulsiona";
    }

    private void MoedasEscolhe()
    {
        int tamanho = Random.Range(1, 4);

        if (tamanho == 1)
        {
            clone = Instantiate(modeloMoedas1, transform.position, transform.localRotation);
        }
        else if (tamanho == 2)
        {
            clone = Instantiate(modeloMoedas2, transform.position, transform.localRotation);
        }
        else if (tamanho == 3)
        {
            clone = Instantiate(modeloMoedas3, transform.position, transform.localRotation);
        }

        clone.transform.rotation = new Quaternion(clone.transform.rotation.x, clone.transform.rotation.y, clone.transform.rotation.z, clone.transform.rotation.w);
        clone.transform.position = new Vector3(Random.Range(-30, 30), clone.transform.position.y + 10, clone.transform.position.z + 200);
        clone.transform.localScale = new Vector3(tamanho * clone.transform.localScale.x, tamanho * clone.transform.localScale.y, clone.transform.localScale.z);
        clone.transform.parent = contanierPlataformas.transform;

        if (tamanho == 1) {
            clone.name = "Moeda1";
        }else if(tamanho == 2) {
            clone.name = "Moeda2";
        }
        else if(tamanho == 3) {
            clone.name = "Moeda3";
        }
    }

}
