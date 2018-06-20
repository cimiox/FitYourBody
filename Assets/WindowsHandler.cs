using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class WindowsHandler : MonoBehaviour
{
    private static WindowsHandler instance;
    public static WindowsHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WindowsHandler>();

                if (instance == null)
                {
                    instance = new GameObject("WindowsHandler").AddComponent<WindowsHandler>();
                }
            }

            return instance;
        }
    }

    public ObservableCollection<Window> Windows { get; } = new ObservableCollection<Window>();

    private void Awake()
    {
        Window.OnWindowEnabledOrDisabled += Window_OnWindowEnabledOrDisabled;
    }

    private void Window_OnWindowEnabledOrDisabled(Window window, bool isEnabled)
    {
        if (isEnabled)
        {
            Windows.Add(window);
        }
        else
        {
            if (Windows.Contains(window))
            {
                Windows.Remove(window);
            }
        }
    }
}
