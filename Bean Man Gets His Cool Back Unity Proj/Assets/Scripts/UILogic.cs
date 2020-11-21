using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    // Main Menu Game Objects
    public GameObject MainMenu;
    public Button MMPlayButton;



    private void Start()
    {
        MMPlayButton.onClick.AddListener(OnButtonAClicked);
    }
    private void OnDestroy()
    {
        MMPlayButton.onClick.RemoveListener(OnButtonAClicked);
    }
    private void OnButtonAClicked()
    {
        MainMenu.SetActive(false);
        // Allow Movement
    }
    private void OnButtonBClicked()
    {
        
    }

}
