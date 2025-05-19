using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    int health = 100;

    int score = 0;

    bool canInteract = false;

    MatchaBehaviour currentMatcha;

    DoorBehaviour currentDoor;


    void OnInteract()
    {
        if(canInteract)
        {
            if(currentMatcha != null)
            {
                Debug.Log("Interacting with Matcha...");
                currentMatcha.Collect(this);
            }
            else if(currentDoor != null)
            {
                Debug.Log("Interacting with Door...");
                currentDoor.Interact();
            }
        }
    }

    public void ModifyScore(int amount)
    {
        score += amount;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.CompareTag("Matcha"))
        {
            canInteract = true;
            currentMatcha = other.GetComponent<MatchaBehaviour>();
        }
        else if(other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
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
