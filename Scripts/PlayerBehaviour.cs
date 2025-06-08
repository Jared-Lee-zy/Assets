using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    int health = 100;

    int score = 0;

    bool canInteract = false;

    MatchaBehaviour currentMatcha;

    DoorBehaviour currentDoor;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform spawnPoint;
    
    [SerializeField]
    float fireStrength = 0f;


    void OnInteract()
    {
        if(canInteract)
        {
            if (currentMatcha != null)
            {
                Debug.Log("Interacting with Matcha...");
                currentMatcha.Collect(this);
                Debug.Log(score);
            }
            else if (currentDoor != null)
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
        if (other.CompareTag("Matcha"))
        {
            canInteract = true;
            currentMatcha = other.GetComponent<MatchaBehaviour>();
            currentMatcha.Highlight();
        }
        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
        }
    }

    void OnTriggetExit(Collider other)
    {
        if (currentMatcha != null)
        {
            if (other.gameObject == currentMatcha.gameObject)
            {
                canInteract = false;
                currentMatcha.Unhighlight();
                currentMatcha = null;
            }
        }
    }

    void OnFire()

    {
        Debug.Log("Fire...");
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);

        Vector3 fireForce = spawnPoint.forward * fireStrength;

        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
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
