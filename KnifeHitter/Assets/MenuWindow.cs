using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : Window
{
    [SerializeField] private Button _playButton;

    public void StartButton() 
    {
        uiManager.SwitchWindows(WindowType.GameWindow);
    }
}
