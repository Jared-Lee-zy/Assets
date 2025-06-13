using TMPro;
using UnityEngine;

public class EndFridgeBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    TextMeshProUGUI header;
    [SerializeField]
    TextMeshProUGUI endScore;
    [SerializeField]
    TextMeshProUGUI endDeathCount;

    [SerializeField]
    AudioSource endSound;
    public void Interact(PlayerBehaviour player)
    {
        if (endScreen != null)
        {
            endSound.Play();
            endScreen.SetActive(true);
            header.text = "Congratulations!";
            endScore.text = "Matcha Collected: " + player.GetScore();
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
