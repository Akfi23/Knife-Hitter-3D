using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWIndow : Window
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Text _recordText;
    private void Start()
    {
        _recordText.text = " Best Record: " + ScoreCounter.Instance.RecordLvl.ToString();
    }

    public void Restart() 
    {
        uiManager.SwitchWindows(WindowType.GameWindow);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
