using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public static event Action<Window, bool> OnWindowEnabledOrDisabled; 

    protected virtual void OnEnable()
    {
        OnWindowEnabledOrDisabled?.Invoke(this, true);
    }

    protected virtual void OnDisable()
    {
        OnWindowEnabledOrDisabled?.Invoke(this, false);
    }
}
