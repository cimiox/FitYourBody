using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(CustomToggle))]
public class ShowCutomToggle : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CustomToggle component = (CustomToggle)target;

        if (component.isOn)
        {
            component.GraphicIsOff.enabled = false;
        }
        else
            component.GraphicIsOff.enabled = true;
    }
}