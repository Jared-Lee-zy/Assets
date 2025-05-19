using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    int health = 100;

    int score = 0;

    bool canInteract = false;

    MatchaBehaviour currentMatcha;


    void OnInteract()
    {
        if(canInteract)
        {
            Debug.Log("Interacting...");
            currentMatcha.collect(this);
            Debug.Log("Score: "+score);
            canInteract = false;
            currentMatcha = null;
        }
    }

    public void ModifyScore(int amount)
    {
        score += amount;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " entered the trigger");
        if(other.gameObject.CompareTag("Matcha"))
        {
            Debug.Log("Matcha detected");
            canInteract = true;
            currentMatcha = other.gameObject.GetComponent<MatchaBehaviour>();
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
