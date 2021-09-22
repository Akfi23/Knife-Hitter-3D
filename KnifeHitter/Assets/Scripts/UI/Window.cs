using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public WindowType windowType;
    protected UIManager uiManager;


    protected void Awake()
    {
        uiManager = GetComponentInParent<UIManager>();
    }
}
