using UnityEngine;
using UnityEngine.UI;

public class ReplayEnding : MonoBehaviour
{
    public ActManager _actManager;

    public Button CloseButton;
    public GameObject EndingsUI;

    public GameObject ChickPeaEnding;
    public GameObject BeanManWinEnding;
    public GameObject LinaBeanEnding;
    public GameObject PeanutTwinEnding;
    public GameObject GrannySmithEnding;
    public GameObject FireHydrantEnding;
    public GameObject SlimSausageWinning;
    public GameObject BeanManLeavesBagged;
    public GameObject BeanManLeavesTown;
    public GameObject GreenBenEnding;
    public GameObject BirthdayCakeEnding;
    public GameObject BeanManUncoolEnding;

    public Button Button_ChickPeaEnding;
    public Button Button_BeanManWinEnding;
    public Button Button_LinaBeanEnding;
    public Button Button_PeanutTwinEnding;
    public Button Button_GrannySmithEnding;
    public Button Button_FireHydrantEnding;
    public Button Button_SlimSausageWinning;
    public Button Button_BeanManLeavesBagged;
    public Button Button_BeanManLeavesTown;
    public Button Button_GreenBenEnding;
    public Button Button_BirthdayCakeEnding;
    public Button Button_BeanManUncoolEnding;

    private Color LightEndingcolor = new Color(1f, 1f, 1f);
    private Color DarkEndingColor = new Color(.2f, .2f, .2f);


    private void OnEnable()
    {
        CloseButton.onClick.AddListener(CloseEndingsReplay);

        Button_ChickPeaEnding.image.color = DarkEndingColor;
        Button_BeanManWinEnding.image.color = DarkEndingColor;
        Button_LinaBeanEnding.image.color = DarkEndingColor;
        Button_PeanutTwinEnding.image.color = DarkEndingColor;
        Button_GrannySmithEnding.image.color = DarkEndingColor;
        Button_FireHydrantEnding.image.color = DarkEndingColor;
        Button_SlimSausageWinning.image.color = DarkEndingColor;
        Button_BeanManLeavesBagged.image.color = DarkEndingColor;
        Button_BeanManLeavesTown.image.color = DarkEndingColor;
        Button_GreenBenEnding.image.color = DarkEndingColor;
        Button_BirthdayCakeEnding.image.color = DarkEndingColor;
        Button_BeanManUncoolEnding.image.color = DarkEndingColor;

        foreach (GameObject ending in _actManager._endingsManager.endingsSeenList)
        {
            if (ending.name == ChickPeaEnding.name)
            {
                Button_ChickPeaEnding.image.color = LightEndingcolor;
                Button_ChickPeaEnding.onClick.AddListener(ChickPeaEndingClicked);
            }

            if (ending.name == BeanManWinEnding.name)
            {
                Button_BeanManWinEnding.image.color = LightEndingcolor;
                Button_BeanManWinEnding.onClick.AddListener(BeanManWinEndingClicked);
            }

            if (ending.name == LinaBeanEnding.name)
            {
                Button_LinaBeanEnding.image.color = LightEndingcolor;
                Button_LinaBeanEnding.onClick.AddListener(LinaBeanEndingClicked);
            }

            if (ending.name == PeanutTwinEnding.name)
            {
                Button_PeanutTwinEnding.image.color = LightEndingcolor;
                Button_PeanutTwinEnding.onClick.AddListener(PeanutTwinEndingClicked);
            }

            if (ending.name == GrannySmithEnding.name)
            {
                Button_GrannySmithEnding.image.color = LightEndingcolor;
                Button_GrannySmithEnding.onClick.AddListener(GrannySmithEndingClicked);
            }
            if (ending.name == FireHydrantEnding.name)
            {
                Button_FireHydrantEnding.image.color = LightEndingcolor;
                Button_FireHydrantEnding.onClick.AddListener(FireHydrantEndingClicked);
            }
            if (ending.name == SlimSausageWinning.name)
            {
                Button_SlimSausageWinning.image.color = LightEndingcolor;
                Button_SlimSausageWinning.onClick.AddListener(SlimSausageWinningClicked);
            }
            if (ending.name == BeanManLeavesBagged.name)
            {
                Button_BeanManLeavesBagged.image.color = LightEndingcolor;
                Button_BeanManLeavesBagged.onClick.AddListener(BeanManLeavesBaggedClicked);
            }
            if (ending.name == BeanManLeavesTown.name)
            {
                Button_BeanManLeavesTown.image.color = LightEndingcolor;
                Button_BeanManLeavesTown.onClick.AddListener(BeanManLeavesTownClicked);
            }
            if (ending.name == GreenBenEnding.name)
            {
                Button_GreenBenEnding.image.color = LightEndingcolor;
                Button_GreenBenEnding.onClick.AddListener(GreenBenEndingClicked);
            }
            if (ending.name == BirthdayCakeEnding.name)
            {
                Button_BirthdayCakeEnding.image.color = LightEndingcolor;
                Button_BirthdayCakeEnding.onClick.AddListener(BirthdayCakeEndingClicked);
            }
            if (ending.name == BeanManUncoolEnding.name)
            {
                Button_BeanManUncoolEnding.image.color = LightEndingcolor;
                Button_BeanManUncoolEnding.onClick.AddListener(BeanManUncoolEndingClicked);
            }
        }

    }

    private void OnDestroy()
    {
        CloseButton.onClick.RemoveListener(CloseEndingsReplay);

        Button_ChickPeaEnding.onClick.RemoveListener(ChickPeaEndingClicked);
        Button_BeanManWinEnding.onClick.RemoveListener(BeanManWinEndingClicked);
        Button_LinaBeanEnding.onClick.RemoveListener(LinaBeanEndingClicked);
        Button_PeanutTwinEnding.onClick.RemoveListener(PeanutTwinEndingClicked);
        Button_GrannySmithEnding.onClick.RemoveListener(GrannySmithEndingClicked);
        Button_FireHydrantEnding.onClick.RemoveListener(FireHydrantEndingClicked);
        Button_SlimSausageWinning.onClick.RemoveListener(SlimSausageWinningClicked);
        Button_BeanManLeavesBagged.onClick.RemoveListener(BeanManLeavesBaggedClicked);
        Button_BeanManLeavesTown.onClick.RemoveListener(BeanManLeavesTownClicked);
        Button_GreenBenEnding.onClick.RemoveListener(GreenBenEndingClicked);
        Button_BirthdayCakeEnding.onClick.RemoveListener(BirthdayCakeEndingClicked);
        Button_BeanManUncoolEnding.onClick.RemoveListener(BeanManUncoolEndingClicked);
    }

    private void CloseEndingsReplay()
    {
        Debug.Log("CLICKING ME");
        EndingsUI.SetActive(false);
    }

    private void ChickPeaEndingClicked()
    {
        _actManager.LoadGraphic(ChickPeaEnding, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.ChickPeaEndingText;
    }

    private void BeanManWinEndingClicked()
    {
        _actManager.LoadGraphic(BeanManWinEnding, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.BeanManWinEndingText;
    }

    private void LinaBeanEndingClicked()
    {
        _actManager.LoadGraphic(LinaBeanEnding, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.LinaBeanEndingText;
    }

    private void PeanutTwinEndingClicked()
    {
        _actManager.LoadGraphic(PeanutTwinEnding, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.PeanutTwinEndingText;

    }

    private void GrannySmithEndingClicked()
    {
        _actManager.LoadGraphic(GrannySmithEnding, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.GrannySmithEndingText;
    }

    private void FireHydrantEndingClicked()
    {
        _actManager.LoadGraphic(FireHydrantEnding, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.FireHydrantEndingText;
    }

    private void SlimSausageWinningClicked()
    {
        _actManager.LoadGraphic(SlimSausageWinning, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.SlimSausageWinningText;
    }

    private void BeanManLeavesBaggedClicked()
    {
        _actManager.LoadGraphic(BeanManLeavesBagged, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.BeanManLeavesBaggedText;
    }

    private void BeanManLeavesTownClicked()
    {
        _actManager.LoadGraphic(BeanManLeavesTown, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.BeanManLeavesCoolText;
    }

    private void GreenBenEndingClicked()
    {
        _actManager.LoadGraphic(GreenBenEnding, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.GreenBenEndingText;
    }

    private void BirthdayCakeEndingClicked()
    {
        _actManager.LoadGraphic(BirthdayCakeEnding, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.BirthdayCakeEndingText;
    }

    private void BeanManUncoolEndingClicked()
    {
        _actManager.LoadGraphic(BeanManUncoolEnding, "event:/Good Ending");
        _actManager.EndingScreenText = _actManager.BeanManLeavesUncoolText;
    }
}
