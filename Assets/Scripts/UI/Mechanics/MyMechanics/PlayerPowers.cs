using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    private List<GameObject> children = new List<GameObject>();
    [SerializeField]
    public Vector3 offset;
    [SerializeField]


    string powerName;
    int numChildren = 1;
    public List<GameObject> GetChildren(GameObject obj)
    {
        Debug.Log("GetChildren");
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in obj.transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }

    //* utility function to get a list of ALL children of a given game object *//*
    public List<GameObject> GetAllChildren(GameObject obj)
    {
        List<GameObject> children = GetChildren(obj);
        for (var i = 0; i < children.Count; i++)
        {
            List<GameObject> moreChildren = GetChildren(children[i]);
            for (var j = 0; j < moreChildren.Count; j++)
            {
                children.Add(moreChildren[j]);
            }
        }
        return children;
    }

    //* utility function to get a list of ALL children of a given game object with a particular NAME *//*
    public List<GameObject> FindChildrenWithName(string nam, GameObject obj)
    {
        List<GameObject> children = GetAllChildren(obj);
        List<GameObject> results = new List<GameObject>();
        for (var i = 0; i < children.Count; i++)
        {
            if (children[i].name == nam)
                results.Add(children[i].gameObject);
        }
        return results;
    }
    private void AddChild(GameObject newChild)
    {
        var obj = Instantiate(newChild, transform);

        // change the name of the object, you may wish to use something different 
        // to denote the different powerups 
        

        var Type = newChild.GetComponent<PowerUp>();

        var powerType = Type.GetPowerType();

        
        obj.name = powerName;

        // remove all powerup component scripts from the clone 
        // otherwise you will have an infinite loop and it will crash your PC
        Destroy(obj.GetComponent<PowerUp>());

        // how many children are already attached to the player?
        // you may wish to use a specific powerup name to see how many powerups are already applied
        // hint: think of only having 1 type of powerup shown, maybe you need to do something here or before
        

        // set the position of the child based on how many already exist
        obj.transform.localPosition = offset * numChildren;

        // set the scale to be small
        obj.transform.localScale /= 5f;

        numChildren++;
    }


    public void TouchPowerUp(PowerUp power, string name)
    {

        Debug.Log(power.PType);

        powerName = name;

        AddChild(power.gameObject);

    }

    public void FlipDirection()
    {
        foreach (Transform child in gameObject.transform)
        {

        }
    }
}
