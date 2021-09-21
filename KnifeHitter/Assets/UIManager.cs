using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _uiManager;
    public static UIManager Instance
    {
        get
        {
            if (_uiManager == null)
            {
                _uiManager = FindObjectOfType<UIManager>();
            }
            return _uiManager;
        }
    }

    [SerializeField] private List<Window> _windows;

    [SerializeField] private Window _gameWindow;
    [SerializeField] private Window _loseWindow;

    private void Awake()
    {
        _windows.Add(_gameWindow);
        _windows.Add(_loseWindow);
    }

    public void OpenGame() 
    {
        _gameWindow.gameObject.SetActive(true);
        _loseWindow.gameObject.SetActive(false);
    }

    public void OpenLoseWindow() 
    {
        _loseWindow.gameObject.SetActive(true);
        _gameWindow.gameObject.SetActive(false);
    }
}
