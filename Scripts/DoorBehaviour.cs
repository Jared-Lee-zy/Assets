using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    bool Open = false;

    public void Interact()
    {
        if (!Open)
        {
            transform.eulerAngles += new Vector3(0, 90f, 0);
            Open = true;
        }
        else
        {
            transform.eulerAngles -= new Vector3(0, 90f, 0);
            Open = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
