using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTemplate : MonoBehaviour {

    GameObject contanier;
    GameObject contanierPlataformas;

    GameObject clone;
    GameObject cloneAux;

    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject modelWallJumJump;
    [SerializeField] private GameObject modelCoin;
    [SerializeField] private GameObject airShoot;

    public Transform from;
    public Transform to;
    private float RotationsVel;
    //public GameObject plataformaImpulsiona;
    float step;//n° peace
    int r;//raio

    float xx, yy;//posoção inicial x e y

    int plataformaEspacos;//espaços para montar as plaaformas que precisam de mais autura. sendo assim, deve-se nao fazer algumas das plataformas normais.

    public Vector3 targetAngle;

    private Vector3 currentAngle;

    // Use this for initialization
    void Start()
    {

        currentAngle = transform.eulerAngles;

        RotationsVel = 1;

        plataformaEspacos = 0;

        step = 2 * Mathf.PI / 60; // 80 é o numero de obj que tera no cenatio para uma volta
        r = 200; // raio

        xx = 0;
        yy = 0;
        //---------------------------------------------------------------------------------

        contanier = new GameObject();
        contanier.name = "cenario";

        contanierPlataformas = new GameObject();
        contanierPlataformas.name = "motorPlataformas";
        contanierPlataformas.tag = "Plataforma";

        //--------------------------------------------------------------------------------

        for (float theta = 0; theta < 2 * Mathf.PI; theta += step)
        {
            xx = r * Mathf.Cos(theta);
            yy = r * Mathf.Sin(theta);

            transform.position = new Vector3(transform.position.x, yy, xx);

            xx = r * Mathf.Cos(theta / 2);
            yy = r * Mathf.Sin(theta / 2);

            transform.localRotation = new Quaternion(transform.localRotation.x, yy, xx, transform.localRotation.w);

            Paredes();
            MoedasEscolhe();

        }
    }

    // Update is called once per frame
    void Update()
    {
        RotationsVel += Time.deltaTime;
        targetAngle = new Vector3(RotationsVel, 0, 0f);
        currentAngle = new Vector3(
        Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
        Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
        Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));

        contanier.transform.eulerAngles = currentAngle;

    }

    private void Paredes()
    {
        //parede esquerda
        if (Random.Range(0, 100) < 3)
        {
            clone = Instantiate(modelWallJumJump, transform.position, transform.localRotation);
            clone.transform.parent = contanier.transform;
            clone.transform.localScale = new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
            clone.transform.position = new Vector3(clone.transform.position.x - 50, clone.transform.position.y, clone.transform.position.z);
            clone.name = "paredePulaPula";
        }
        else
        {
            clone = Instantiate(wall, transform.position, transform.localRotation);
            clone.transform.parent = contanier.transform;
            clone.transform.position = new Vector3(clone.transform.position.x - 50, clone.transform.position.y, clone.transform.position.z);
            clone.name = "parede";

            if (Random.Range(0, 100) < 10)//arshoot na parede esquerda 
            {
                clone = Instantiate(airShoot, transform.position, transform.localRotation);
                clone.transform.parent = contanier.transform;
                clone.transform.Rotate(clone.transform.rotation.x + 90, clone.transform.rotation.y, clone.transform.rotation.z + 90);
                clone.transform.position = new Vector3(clone.transform.position.x - 23, clone.transform.position.y, clone.transform.position.z);
                clone.name = "arShoot";
            }
        }

        //parede direita
        if (Random.Range(0, 100) < 3)
        {
            clone = Instantiate(modelWallJumJump, transform.position, transform.localRotation);
            clone.transform.parent = contanier.transform;
            clone.transform.localScale = new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
            clone.transform.position = new Vector3(clone.transform.position.x + 70, clone.transform.position.y, clone.transform.position.z);
            clone.name = "paredePulaPula";
        }
        else
        {
            clone = Instantiate(wall, transform.position, transform.localRotation);
            clone.transform.parent = contanier.transform;
            clone.transform.position = new Vector3(clone.transform.position.x + 70, clone.transform.position.y, clone.transform.position.z);
            clone.name = "parede2";

            if (Random.Range(0, 100) < 10)//arshoot na parede direita 
            {
                clone = Instantiate(airShoot, transform.position, transform.localRotation);
                clone.transform.parent = contanier.transform;
                clone.transform.Rotate(clone.transform.rotation.x + 270, clone.transform.rotation.y, clone.transform.rotation.z + -90);
                clone.transform.position = new Vector3(clone.transform.position.x + 44, clone.transform.position.y, clone.transform.position.z);
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
 
    /*private void PlataformaImpulsiona()
    {
        clone = Instantiate(plataformaImpulsiona, transform.position, transform.localRotation);
        clone.transform.Rotate(clone.transform.rotation.x, clone.transform.rotation.y, clone.transform.rotation.z);
        clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z + 200);
        clone.transform.localScale = new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
        clone.transform.parent = contanierPlataformas.transform;
        clone.GetComponent<HingeJoint>().connectedBody = contanierPlataformas.GetComponent<Rigidbody>();
        clone.name = "plataformaImpulsiona";
    }*/

    private void MoedasEscolhe()
    {

        clone = Instantiate(modelCoin, transform.position, transform.localRotation);

        clone.transform.rotation = new Quaternion(clone.transform.rotation.x, clone.transform.rotation.y, clone.transform.rotation.z, clone.transform.rotation.w);
        clone.transform.position = new Vector3(Random.Range(-30, 30), clone.transform.position.y, clone.transform.position.z);
        clone.transform.localScale = new Vector3(3 * clone.transform.localScale.x, 3 * clone.transform.localScale.y, clone.transform.localScale.z);
        clone.transform.parent = contanierPlataformas.transform;
        clone.name = "Moedas";
        
    }

}
