using TMPro;
using UnityEngine;

// Controls the behaviour of the end fridge in the game that displays the end screen with score and death count when interacted with.
public class EndFridgeBehaviour : MonoBehaviour
{
    // Reference to the end screen UI element that is displayed when the end fridge is interacted with.
    [SerializeField]
    GameObject endScreen;
    // References to the UI text element that displays the header.
    [SerializeField]
    TextMeshProUGUI header;
    // References to the UI text element that displays the final score.
    [SerializeField]
    TextMeshProUGUI endScore;
    // References to the UI text element that displays the final death count.
    [SerializeField]
    TextMeshProUGUI endDeathCount;
    // Audio source that plays the end sound when the end fridge is interacted with.
    [SerializeField]
    AudioSource endSound;
    
    // Called when the player interacts with the end fridge.
    public void Interact(PlayerBehaviour player)
    {
        if (endScreen != null)
        {
            // Plays the end sound and displays the end screen with the player's final score and final death count
            endSound.Play();
            endScreen.SetActive(true);
            header.text = "Congratulations!";
            endScore.text = "Matcha Collected: " + player.GetScore() + "/10";
            endDeathCount.text = "Deaths: " + player.GetDeathCount();
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
