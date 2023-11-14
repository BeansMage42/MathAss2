using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPowers : MonoBehaviour
{
    // Start is called before the first frame update
    private bool translate, rotate, reverseSpeed, reverseDirection, shoot;
    private int translateC, rotateC;
    private float speed;
    private float speedPolarity = 1;
    private float rotateSpeed = 0;
    private float rotatePolarity = 1;
    private Vector3 rotateDir = new Vector3(0, 0, 10);
    private float[,] rotation = { { 90, 90},{90,90} };
    private float curRot;
    bool startShot = true;
    private float[,] scalingVector ={ { 2, 0 }, { 0, 2 } };
    private Vector3 mouseOrigin;
    private Vector3 newMouse;
    
   // private float []
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shoot)
        {


            transform.position += new Vector3(speed * speedPolarity, 0, 0) * Time.deltaTime;
            curRot = transform.localRotation.eulerAngles.z;
            if (rotate)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + (rotateSpeed * Time.deltaTime * rotatePolarity)));
            }
        }

        if (shoot)
        {
            
            
            if (Input.GetMouseButtonUp(0))
            {
                newMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("released");

                var velocity = mouseOrigin - newMouse;
                velocity = Vector3.ClampMagnitude(velocity, 5);


               transform.position +=  new Vector3(velocity.x, velocity.y, 0) ;
                //GetComponent<Rigidbody2D>().AddForce(new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * newSpeed );

                shoot = false;
            }
        }
        


    }

    

    public void AddPower(string powertype)
    {
        Debug.Log(powertype);
        switch (powertype)
        {
            case "TranslatePower":
                translate = true;
                speed++;
                break;
            case "RotatePower":
                rotate = true;
                rotateSpeed += 10;
                break;
            case "ScalePower":


                /*Vector3 origPos = transform.position;
                transform.position -= transform.localScale;*/

                transform.localScale = DotTranformations("Scale", scalingVector);
                //transform.position = origPos + transform.localPosition;
                
                break;
            case "ReverseSpeedPower":
                speedPolarity *= -1;
                break;
            case "ReverseRotatePower":
                Debug.Log("reverse polarity");
                rotatePolarity *= -1;
                
                break;
            case "ShootPower":
                shoot = true;
                    mouseOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;

        }
    }


    private Vector3 DotTranformations(string type, float[,] transformations)
    {
        Vector3 transformer = new Vector3(0,0,0);
        if(type == "Scale")
        {
            transformer = gameObject.transform.localScale;
        }
        return new Vector3((transformer.x * transformations[0, 0]) + (transformer.y * transformations[0, 1]), (transformer.x * transformations[1, 0]) + (transformer.y * transformations[1, 1]), transformer.z);
    }
}
