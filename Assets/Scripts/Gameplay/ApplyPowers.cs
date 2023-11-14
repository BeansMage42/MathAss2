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
    private float[,] rotation;
    private float curRot;
    bool startShot = true;

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
                var newSpeed = velocity.magnitude;
                
               transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * 25 * newSpeed ;
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
                transform.localScale += new Vector3(2, 2, 0);
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
}
