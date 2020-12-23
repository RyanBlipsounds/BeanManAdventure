﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayEnding : MonoBehaviour
{
    public ActManager _actManager;

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


    private void Start()
    {
        Button_ChickPeaEnding.onClick.AddListener(ChickPeaEndingClicked);
        Button_BeanManWinEnding.onClick.AddListener(BeanManWinEndingClicked);
        Button_LinaBeanEnding.onClick.AddListener(LinaBeanEndingClicked);
        Button_PeanutTwinEnding.onClick.AddListener(PeanutTwinEndingClicked);
        Button_GrannySmithEnding.onClick.AddListener(GrannySmithEndingClicked);
        Button_FireHydrantEnding.onClick.AddListener(FireHydrantEndingClicked);
        Button_SlimSausageWinning.onClick.AddListener(SlimSausageWinningClicked);
        Button_BeanManLeavesBagged.onClick.AddListener(BeanManLeavesBaggedClicked);
        Button_BeanManLeavesTown.onClick.AddListener(BeanManLeavesTownClicked);
        Button_GreenBenEnding.onClick.AddListener(GreenBenEndingClicked);
        Button_BirthdayCakeEnding.onClick.AddListener(BirthdayCakeEndingClicked);
        Button_BeanManUncoolEnding.onClick.AddListener(BeanManUncoolEndingClicked);
    }



    private void OnDestroy()
    {
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


    private void ChickPeaEndingClicked()
    {
        _actManager.LoadGraphic(ChickPeaEnding);
    }

    private void BeanManWinEndingClicked()
    {
        _actManager.LoadGraphic(BeanManWinEnding);
    }

    private void LinaBeanEndingClicked()
    {
        _actManager.LoadGraphic(LinaBeanEnding);
    }

    private void PeanutTwinEndingClicked()
    {
        _actManager.LoadGraphic(PeanutTwinEnding);
    }

    private void GrannySmithEndingClicked()
    {
        _actManager.LoadGraphic(GrannySmithEnding);
    }

    private void FireHydrantEndingClicked()
    {
        _actManager.LoadGraphic(FireHydrantEnding);
    }

    private void SlimSausageWinningClicked()
    {
        _actManager.LoadGraphic(SlimSausageWinning);
    }

    private void BeanManLeavesBaggedClicked()
    {
        _actManager.LoadGraphic(BeanManLeavesBagged);
    }

    private void BeanManLeavesTownClicked()
    {
        _actManager.LoadGraphic(BeanManLeavesTown);
    }

    private void GreenBenEndingClicked()
    {
        _actManager.LoadGraphic(GreenBenEnding);
    }

    private void BirthdayCakeEndingClicked()
    {
        _actManager.LoadGraphic(BirthdayCakeEnding);
    }

    private void BeanManUncoolEndingClicked()
    {
        _actManager.LoadGraphic(BeanManUncoolEnding);
    }
}
