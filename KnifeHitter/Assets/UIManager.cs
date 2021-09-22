using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum WindowType 
{
    MainMenuWindow,
    GameWindow,
    LoseWindow
}


public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<Window> uiWindows;
    [SerializeField] private Window lastActiveWindow;

    protected override void Awake()
    {
        base.Awake();

        uiWindows = GetComponentsInChildren<Window>().ToList();
        uiWindows.ForEach(w => w.gameObject.SetActive(false));
        SwitchWindows(WindowType.MainMenuWindow);
    }

    public void SwitchWindows(WindowType type) 
    {
        if (lastActiveWindow != null) 
        {
            lastActiveWindow.gameObject.SetActive(false);
        }

        Window desiredWindow = uiWindows.Find(w => w.windowType == type);

        if (desiredWindow != null)
        {
            desiredWindow.gameObject.SetActive(true);
            lastActiveWindow = desiredWindow;
        }
        else
        {
            Debug.LogWarning("Desired window not found! Check activeSelf status");
        }
    }
}
