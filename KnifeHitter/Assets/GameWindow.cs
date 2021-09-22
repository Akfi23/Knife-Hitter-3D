using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GameWindow : Window
{
    [SerializeField] private Image _knifeImage;
    [SerializeField] private CanvasGroup _knifePanel;
    [SerializeField] private List<Image> _knifeIcons=new List<Image>();
    [SerializeField] private Text _appleHitsText;

    private void OnEnable()
    {
        GameManager.Instance.NewTargetSpawned += FormKnifePanel;
        ScoreCounter.Instance.OnAppleScored += SetAppleText;
        Knife.TargetHitted += CollorIfHit;
    }

    private void OnDisable()
    {       
        Knife.TargetHitted -= CollorIfHit;
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance.NewTargetSpawned -= FormKnifePanel;
        ScoreCounter.Instance.OnAppleScored -= SetAppleText;
    }

    private void Awake()
    {
        base.Awake();

        InitializeIcons();
        FormKnifePanel(GameManager.Instance.ScoreToWin);
    }

    private void Start()
    {
        _appleHitsText.text = ScoreCounter.Instance.AppleHits.ToString();
    }

    private void SetAppleText(int score)
    {
        _appleHitsText.text = score.ToString();
    }
    
    private void FormKnifePanel(int count)
    {
        DeactivateIcons();

        ActivateIcons(count);
    }

    private void CollorIfHit()
    {
        Image result = _knifeIcons.First(enemy => enemy.color == Color.white);
        result.color = Color.black;
    }

    private void InitializeIcons() 
    {
        for (int i = 0; i < 9; i++)
        {
            Image newKnifeImage = Instantiate(_knifeImage, _knifePanel.transform);
            newKnifeImage.gameObject.SetActive(false);
            _knifeIcons.Add(newKnifeImage);
        }
    }

    private void DeactivateIcons() 
    {
        foreach (var icon in _knifeIcons)
        {
            icon.gameObject.SetActive(false);
        }
    }

    private void ActivateIcons(int count) 
    {
        for (int i = 0; i < count; i++)
        {
            _knifeIcons[i].color = Color.white;
            _knifeIcons[i].gameObject.SetActive(true);
        }
    }
    
}
