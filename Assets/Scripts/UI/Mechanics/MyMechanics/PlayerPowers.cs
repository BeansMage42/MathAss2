using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    private List<GameObject> children = new List<GameObject>();
    private Stack<GameObject> childrenStack = new Stack<GameObject>();
    [SerializeField]
    public Vector3 offset;
    [SerializeField]
    private GameObject attatchPoint;
    private SpringJoint2D joint;

    string powerName;
    int numChildren = 1;

    private bool flipped;

   // [SerializeField] private ApplyPowers powerScript;
    public List<GameObject> GetChildren(GameObject obj)
    {
       // Debug.Log("GetChildren");
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
    public List<GameObject> FindChildrenWithName(string nam)
    {
        List<GameObject> children = GetAllChildren(gameObject);
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
        var obj = Instantiate(newChild, attatchPoint.transform);
        if (numChildren == 1)
        {
            joint = gameObject.AddComponent<SpringJoint2D>();
            joint.connectedBody = obj.GetComponent<Rigidbody2D>();
        }
        else
        {
            joint = obj.AddComponent<SpringJoint2D>();
            joint.connectedBody = childrenStack.Peek().GetComponent<Rigidbody2D>();
            
        }
        AddToStack(obj);
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

    public void AddToStack(GameObject obj)
    {
        childrenStack.Push(obj);
       // Debug.Log(childrenStack.Peek().name);
    }

    public void TouchPowerUp(PowerUp power, string name)
    {

        Debug.Log(power.PType);

        powerName = name;

        AddChild(power.gameObject);

    }

    public void FlipDirection(int direction)
    {
        /*Debug.Log("flip" + direction);

        if(direction == 1 && flipped)
        {
            Debug.Log("right");
            flipped = false;
            attatchPoint.transform.position = attatchPoint.transform.position + new Vector3(2, 0, 0);
            attatchPoint.transform.localScale = new Vector3 (-attatchPoint.transform.localScale.x, attatchPoint.transform.localScale.y, attatchPoint.transform.localScale.z);
           
        }
        if(direction == 2 && !flipped)
        {
            Debug.Log("left");
            flipped = true;
            attatchPoint.transform.localScale = new Vector3(-attatchPoint.transform.localScale.x, attatchPoint.transform.localScale.y, attatchPoint.transform.localScale.z);
            attatchPoint.transform.position = attatchPoint.transform.position - new Vector3(2, 0, 0);
        }*/
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (childrenStack.Count > 0)
            {


                var obj = childrenStack.Pop();
                numChildren--;
                Debug.Log(obj.name);
                Destroy(obj);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("click");
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = worldPosition - transform.position;
            var distance = dir.magnitude;
             dir = dir.normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(dir.x, dir.y), distance);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "clickable" && childrenStack.Count > 0)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    UsePower(hit.collider.gameObject);
                }
            }
            Debug.DrawRay(transform.position, new Vector2(dir.x, dir.y), Color.green);
        
    }
}
    public GameObject GetTopPower()
    {

        return childrenStack.Peek();
    }

    private void UsePower(GameObject target)
    {
        ApplyPowers powerApply;
        numChildren--;
        
        if(target.GetComponent<ApplyPowers>() == null)
        {
           target.AddComponent<ApplyPowers>();
        }
        
            powerApply = target.GetComponent<ApplyPowers>();
        
        
        var nextPow = childrenStack.Pop();
        powerApply.AddPower(nextPow.name);
        
        
        Destroy(nextPow);
    }
    /*public int GetPowerUp(string nam)
    {
       return FindChildrenWithName(nam).Count;
    }*/

}
