using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    public Toggle SFX;
    public Toggle Music;
    public Toggle VO;
    public Button Credits;
    public Button Exit;


    // Start is called before the first frame update
    private void Start()
    {
        Exit.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        Exit.onClick.RemoveListener(ExitGame);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
