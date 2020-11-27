using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    // Main Menu Game Objects
    public GameObject MainMenu;
    public Button MMPlayButton;

    public PlayerController pc;



    private void Start()
    {
        MMPlayButton.onClick.AddListener(OnButtonAClicked);
        pc.bagMoving = true;
    }
    private void OnDestroy()
    {
        MMPlayButton.onClick.RemoveListener(OnButtonAClicked);
    }
    private void OnButtonAClicked()
    {
        MainMenu.SetActive(false);
        pc.bagMoving = false;
        // Allow Movement
    }
    private void OnButtonBClicked()
    {
        
    }

}
