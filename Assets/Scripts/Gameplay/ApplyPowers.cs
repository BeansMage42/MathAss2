using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPowers : MonoBehaviour
{
    // Start is called before the first frame update
    private bool translate, rotate, reverseSpeed, reverseDirection;
    private int translateC, rotateC;
    private float speed;
    private float rotateSpeed = 0;
    private Vector3 rotateDir = new Vector3(0, 0, 10);
    private float[,] rotation;
    private float curRot;
   // private float []
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, 0,0) * Time.deltaTime;
        curRot = transform.localRotation.eulerAngles.z;
        if(rotate)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + (rotateSpeed * Time.deltaTime)));
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

        }
    }
}
