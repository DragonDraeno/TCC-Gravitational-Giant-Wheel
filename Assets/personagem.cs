using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personagem : MonoBehaviour {

    public bool plataformaColidindo;

    GameObject cenario;
    float tempoPulo;

    float andar = 20;
    bool ladoAndando;

    float forcaPulo;
    float forcaQueda;

    // Use this for initialization
    void Start () {
        cenario = GameObject.Find("cenario");
        forcaPulo = 30;
        forcaQueda = 3;
    }
	
	// Update is called once per frame
	void Update () {
        //movimento personagem-----------------------------------------------------------------

        transform.position = new Vector3(transform.position.x, 0, 0);
        if (Input.GetAxis("Horizontal") > 0) {//dir
            transform.Translate(1 * andar * Time.deltaTime, transform.position.y, transform.position.y);
            transform.Rotate(10, transform.rotation.y, transform.rotation.z);
            ladoAndando = true;
        }

        if (Input.GetAxis("Horizontal") < 0)//esq
        {
            transform.Translate(-1 * andar * Time.deltaTime, transform.rotation.y, transform.rotation.y);
            transform.Rotate(-10, transform.rotation.y, transform.rotation.z);
            ladoAndando = false;
        }

        if (ladoAndando == true && Input.GetAxis("Horizontal") == 0)
        {
            transform.Translate(1 * andar * Time.deltaTime, transform.position.y, transform.position.y);
            transform.Rotate(10, transform.rotation.y, transform.rotation.z);
        }
        if (ladoAndando == false && Input.GetAxis("Horizontal") == 0) { 
            transform.Translate(-1 * andar * Time.deltaTime, transform.position.y, transform.position.y);
            transform.Rotate(-10, transform.rotation.y, transform.rotation.z);
        }
        //----------------------------------------------------------------------

        if (Input.GetAxis("Vertical") > 0 && plataformaColidindo == true)
        {
            //GetComponent<Rigidbody>().AddForce(0, 2000, 0);
            plataformaColidindo = false;
            
        }

        ////PowerUp------------------------------------------------
        //if (Input.GetAxis("Vertical") < 0 && powerUp < 6)
        //{
        //    //GetComponent<Rigidbody>().AddForce(0, 2000, 0);
        //    powerUp += powerUp * Time.deltaTime;

        //}
        //else {
        //    powerUp = 1;
        //}

        //if (powerUp >= 5) {
        //    tempoPulo += Time.deltaTime;
        //    if (tempoPulo < 0.7f)
        //    {

        //        forcaPulo -= forcaPulo * Time.deltaTime;
        //        cenario.transform.Rotate(-forcaPulo * Time.deltaTime, 0, 0);

        //    }
        //    else
        //    {
        //        forcaQueda += forcaQueda * Time.deltaTime;
        //        cenario.transform.Rotate(forcaQueda * Time.deltaTime, 0, 0);
        //        if (forcaQueda > 9.9)
        //        {
        //            forcaQueda = 10;
        //            powerUp = 1;
        //        }
        //    }

            
        //}

        //print(powerUp);

        //-------------------------------------------------------------------------------

        if (CenarioRodar() == true)//faz o cenario rodar quand o personagem nao esa no chão
        {

            tempoPulo += Time.deltaTime;
            if (tempoPulo < 0.7f)
            {
                
                forcaPulo -= forcaPulo * Time.deltaTime; 
                cenario.transform.Rotate(-forcaPulo * Time.deltaTime, 0, 0);
                
            }
            else
            {
                forcaQueda += forcaQueda * Time.deltaTime;
                cenario.transform.Rotate(forcaQueda * Time.deltaTime, 0, 0);
                if (forcaQueda > 9.9)
                {
                    forcaQueda = 10;
                }
            }
            
        }
        else {
            tempoPulo = 0;
        }

    }

    private bool CenarioRodar()
    {//retorna se o personagem esta ou nao no chão
        if (plataformaColidindo == false)
        {
            return true;

        }
        else {
            return false;
        }
        

    }

    private void OnCollisionStay(Collision collision)
    {


        //Raycast preve 1 unidade de medida para baixo do perssonagem fazendo com que ele nao fique parado na parte 
        //inferior nem lateral das lataformas, somente na parte superior.
        if (collision.gameObject.tag == "Plataforma" && Physics.Raycast(transform.position, -Vector3.up, 1) == true)
        {
            plataformaColidindo = true;
            forcaPulo = 15;
            forcaQueda = 3;
            
        }
        else {
            plataformaColidindo = false;
            //GetComponent<Rigidbody>().AddForce(0, -10, 0);
        }
        //print(collision.gameObject.tag);
    }
}
