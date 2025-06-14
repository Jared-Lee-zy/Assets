using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Formats.Fbx.Exporter;

// Handles player related behaviours such as score, death count, UI updates and interactions with matcha, keycards, doors, locked door, fridge and end fridge.
public class PlayerBehaviour : MonoBehaviour
{
    // Tracks the player's score
    int score = 0;
    // Returns the current score of the player to be used in other scripts.
    public int GetScore()
    {
        return score;
    }
    // Tracks the number of times the player has died.
    int deathCount = 0;
    // Returns the current death count of the player to be used in other scripts.
    public int GetDeathCount()
    {
        return deathCount;
    }
    // Reference to the MatchaBehaviour script to handle interactions with matcha objects.
    MatchaBehaviour currentMatcha;
    // UI elements to display score, interact text ad death count.
    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI interactText;

    [SerializeField]
    TextMeshProUGUI deathCountText;
    // Keycard UI visuals
    [SerializeField]
    Sprite keycardImage;

    [SerializeField]
    Image keycardUIImage;
    // Projectile prefab and spawn point.
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform spawnPoint;
    // Respawn point for the player when they die.
    [SerializeField]
    Transform respawnPoint;
    // Fire strength for the projjectile.
    [SerializeField]
    float fireStrength = 0f;
    // Maximum distance for interaction raycast with objects.
    [SerializeField]
    float interactionDistance = 5f;
    // Cooldown time for interactions.
    float interactCooldown = 0.2f;
    float lastInteractTime = -1f;
    // Lifetime for the projectile before it gets destroyed.
    private float lifetime = 2f;
    // Tracks whether the player has the keycard.
    bool hasKeycard = false;
    // Called when the player picks up the keycard, updates the UI and sets the hasKeycard flag to true.
    public void PickupKeycard()
    {
        hasKeycard = true;
        Debug.Log("Keycard picked up!");
        // Updates the keycard UI.
        if (keycardUIImage != null && keycardImage != null)
        {
            keycardUIImage.sprite = keycardImage;
            keycardUIImage.enabled = true;
        }

    }
    // Returns whether the player has a keycard or not.
    public bool Haskeycard()
    {
        return hasKeycard;
    }
    // Called when the player uses the keycard. Updates the UI and sets the hasKeycard flag to false.
    public void UseKeycard()
    {
        hasKeycard = false;
        // Hides the keycard UI.
        if (keycardUIImage != null)
        {
            keycardUIImage.enabled = false;
        }
    }
    // Modifies the player's score and updates the score UI.
    public void ModifyScore(int amount)
    {
        score += amount;
        scoreText.text = "MATCHA: " + score.ToString() + "/10";
    }

    // Fires the projectile from the spawn point.
    void OnFire()

    {
        Debug.Log("Fire...");
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);

        Vector3 fireForce = spawnPoint.forward * fireStrength;

        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
        // Destroys the projectile after the specified lifetime.
        Destroy(newProjectile, lifetime);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Initializes the player UI elements and sets the initial state of the player.
    void Start()
    {
        deathCountText.text = "DEATHS: 0";
        scoreText.text = "MATCHA: " + score.ToString() + "/10";
        interactText.enabled = false;
        // Hides the keycard UI at the start.
        if (keycardUIImage != null)
        {
            keycardUIImage.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        bool isLookingAtInteractable = false;
        // Visualize the interaction raycast in the scene view for debugging purposes.
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * interactionDistance, Color.red);
        // Cast a ray from the spawn point forward to check for interactable objects.
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, interactionDistance))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            // Matcha Interaction
            if (hitObject.CompareTag("Matcha"))
            {
                MatchaBehaviour matcha = hitObject.GetComponent<MatchaBehaviour>();
                if (matcha != null)
                {
                    // Highlight new matcha and unhighlights the previous matcha.
                    if (currentMatcha != matcha)
                    {
                        if (currentMatcha != null)
                            currentMatcha.Unhighlight();

                        currentMatcha = matcha;
                        currentMatcha.Highlight();
                    }

                    isLookingAtInteractable = true;
                    // Collect matcha if interaction key is pressed and cooldown has passed.
                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {   
                        lastInteractTime = Time.time;
                        matcha.Collect(this);
                        currentMatcha = null;
                    }
                }
            }

            // Keycard Interaction
            else if (hitObject.CompareTag("Keycard"))
            {
                keycardBehaviour keycard = hitObject.GetComponent<keycardBehaviour>();
                if (keycard != null)
                {  
                    // Interact UI for keycard
                    interactText.text = "Press E to pick up Keycard";
                    interactText.enabled = true;
                    isLookingAtInteractable = true;
                    // Pick up keycard if interaction key is ppressed and cooldown has passed.
                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {   
                        // Disable keycard UI after picking up the keycard.
                        lastInteractTime = Time.time;
                        keycard.Collect(this);
                        interactText.enabled = false;
                    }
                }

                else
                {
                    // If the keycardBehaviour component is missing, log a warning
                    Debug.LogWarning("Keycard object found but keycardBehaviour component is missing on: " + hitObject.name);
                }

            }

            // Locked Door Interaction
            else if (hitObject.CompareTag("LockedDoor"))
            {
                LockedDoorBehaviour lockedDoor = hitObject.GetComponent<LockedDoorBehaviour>();
                if (lockedDoor != null)
                {   
                    // Interact UI for locked door.
                    interactText.text = "Keycard Required! Press E to Unlock";
                    interactText.enabled = true;
                    isLookingAtInteractable = true;
                    // Unlock the locked door if the interaction key is pressed and cooldown has passed.
                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {
                        // Disable interact UI after unlocking the door.
                        lastInteractTime = Time.time;
                        lockedDoor.Unlock(this);
                        UseKeycard();
                        interactText.enabled = false;
                    }
                }

                else
                {
                    // If the LockedDoorBehaviour component is missing, log a warning
                    Debug.LogWarning("LockedDoor object found but LockedDoorBehaviour component is missing on: " + hitObject.name);
                }

            }

            // Normal Door Interaction
            else if (hitObject.CompareTag("Door"))
            {
                DoorBehaviour door = hitObject.GetComponent<DoorBehaviour>();
                if (door != null)
                {
                    //Interact UI for normal door.
                    interactText.text = "Press E to Open/Close Door";
                    interactText.enabled = true;
                    isLookingAtInteractable = true;
                    // Open/Close the door if the interaction key is pressed and cooldown has passed.
                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {
                        // Disable interact UI after opening/closing the door.
                        lastInteractTime = Time.time;
                        door.Interact();
                        interactText.enabled = false;
                    }
                }

                else
                {
                    // If the DoorBehaviour component is missing, log a warning
                    Debug.LogWarning("Door object found but DoorBehaviour component is missing on: " + hitObject.name);
                }

            }

            // Fridge Interaction
            else if (hitObject.CompareTag("Fridge"))
            {
                FridgeBehaviour fridge = hitObject.GetComponent<FridgeBehaviour>();
                if (fridge != null)
                {
                    // Interact UI for the fridge.
                    interactText.text = "Press E to talk to Fridge?";
                    interactText.enabled = true;
                    isLookingAtInteractable = true;
                    // Interact with the fridge if the interaction key is pressed and cooldown has passed.
                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {
                        // Disable interact UI after interacting with the fridge.
                        lastInteractTime = Time.time;
                        fridge.Interact();
                        interactText.enabled = false;
                    }
                }
            }

            // End Fridge Innteraction
            else if (hitObject.CompareTag("EndFridge"))
            {
                EndFridgeBehaviour endFridge = hitObject.GetComponent<EndFridgeBehaviour>();
                if (endFridge != null)
                {
                    //Interact UI for the end fridge.
                    interactText.text = "Press E to talk to Fridge of Congratulations!";
                    interactText.enabled = true;
                    isLookingAtInteractable = true;
                    // Interact with the end fridge if the interaction key is pressed and cooldown has passed.
                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {   
                        // Disable interact UI after interacting with the end fridge.
                        lastInteractTime = Time.time;
                        endFridge.Interact(this);
                        interactText.enabled = false;
                    }
                }
            }
        }

        // Clear highlights and UI if not looking at an interactable
        if (!isLookingAtInteractable)
        {
            if (currentMatcha != null)
            {
                currentMatcha.Unhighlight();
                currentMatcha = null;
            }

            interactText.enabled = false;
        }
    }

    bool isRespawning = false;
    // Triggered when the player enters a collider, specifically for respawning when colliding with the "Coffee" object.
    void OnTriggerEnter(Collider other)
    {   
        // Prevents respawning if already in the process of respawning.
        if (isRespawning) return;
        // Check if the player has collided with the "Coffee" hazard.
        if (other.CompareTag("Coffee"))
        {
            Debug.Log("Player entered trigger");

            isRespawning = true;

            // Increment death count and update the death count UI.
            deathCount++;
            deathCountText.text = "DEATHS: " + deathCount;

            // Temporarily disable the CharacterController to safely change the player's position.
            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }

            // Move player to respawn point.
            transform.position = respawnPoint.position;

            // Re-enable the CharacterController after moving the player.
            if (cc != null)
            {
                cc.enabled = true;
            }

            Debug.Log("Player respawned at: " + transform.position);

            // Reset respawn flag after a short delay.
            Invoke(nameof(ResetRespawn), 0.5f);
        }
    }

    // Resets the respawn flag to allow the player to respawn again after a delay.
    void ResetRespawn()
    {
        isRespawning = false;
    }

}
