using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    private Color backgroundColor = new Color(0.796f, 0.8f, 0.792f, 0.49f);
    private Color backgroundColorWithoutAlpha = new Color(0, 0, 0, 0);

    [Header("Sprites")]
    [SerializeField]
    private Sprite openMenu;
    [SerializeField]
    private Sprite closeMenu;

    [Header("Buttons")]
    [SerializeField]
    private Button menuButton;

    [Header("Others")]
    [SerializeField]
    private GameObject ButtonsPanel;

    private void Start()
    {
        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(Open);
        gameObject.GetComponent<Image>().color = backgroundColorWithoutAlpha;
        ButtonsPanel.SetActive(false);
    }

    public void Open()
    {
        gameObject.GetComponent<Image>().color = backgroundColor;

        menuButton.GetComponent<Image>().sprite = openMenu;
        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(Close);

        ButtonsPanel.SetActive(true);
    }

    public void Close()
    {
        gameObject.GetComponent<Image>().color = backgroundColorWithoutAlpha;
        menuButton.GetComponent<Image>().sprite = closeMenu;
        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(Open);

        ButtonsPanel.SetActive(false);
    }
}
