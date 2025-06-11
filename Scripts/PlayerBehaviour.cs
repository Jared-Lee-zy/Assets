using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    int score = 0;

    int deathCount = 0;

    MatchaBehaviour currentMatcha;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI interactText;

    [SerializeField]
    TextMeshProUGUI deathCountText;

    [SerializeField]
    Sprite keycardImage;

    [SerializeField]
    Image keycardUIImage;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    Transform respawnPoint;

    [SerializeField]
    float fireStrength = 0f;

    [SerializeField]
    float interactionDistance = 5f;

    float interactCooldown = 0.2f;
    float lastInteractTime = -1f;

    private float lifetime = 2f;

    bool hasKeycard = false;

    public void PickupKeycard()
    {
        hasKeycard = true;
        Debug.Log("Keycard picked up!");

        if (keycardUIImage != null && keycardImage != null)
        {
            keycardUIImage.sprite = keycardImage;
            keycardUIImage.enabled = true;
        }

    }

    public bool Haskeycard()
    {
        return hasKeycard;
    }

    public void UseKeycard()
    {
        hasKeycard = false;

        if (keycardUIImage != null)
        {
            keycardUIImage.enabled = false;
        }
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        scoreText.text = "SCORE: " + score.ToString();
    }


    void OnFire()

    {
        Debug.Log("Fire...");
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);

        Vector3 fireForce = spawnPoint.forward * fireStrength;

        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);

        Destroy(newProjectile, lifetime);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathCountText.text = "DEATHS: 0";
        scoreText.text = "SCORE: " + score.ToString();
        interactText.enabled = false;

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

        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * interactionDistance, Color.red);

        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, interactionDistance))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            // Matcha Interaction
            if (hitObject.CompareTag("Matcha"))
            {
                MatchaBehaviour matcha = hitObject.GetComponent<MatchaBehaviour>();
                if (matcha != null)
                {
                    if (currentMatcha != matcha)
                    {
                        if (currentMatcha != null)
                            currentMatcha.Unhighlight();

                        currentMatcha = matcha;
                        currentMatcha.Highlight();
                    }

                    isLookingAtInteractable = true;

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
                    interactText.text = "Press E to pick up Keycard";
                    interactText.enabled = true;
                    isLookingAtInteractable = true;

                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {
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
                    interactText.text = "Press E to Unlock Door";
                    interactText.enabled = true;
                    isLookingAtInteractable = true;
                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {
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
                    interactText.text = "Press E to Open/Close Door";
                    interactText.enabled = true;
                    isLookingAtInteractable = true;

                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {
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
                    interactText.text = "Press E to talk to fridge?";
                    interactText.enabled = true;
                    isLookingAtInteractable = true;

                    if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime > interactCooldown)
                    {
                        lastInteractTime = Time.time;
                        fridge.Interact();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coffee"))
        {
            Debug.Log("Player entered trigger");

            deathCount++;
            deathCountText.text = "DEATHS: " + deathCount;

            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }

            transform.position = respawnPoint.position;

            if (cc != null)
            {
                cc.enabled = true;
            }

            Debug.Log("Player respawned at: " + transform.position);
        }   
    }
}
