using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomToggle : Toggle
{
    public Image GraphicIsOff;

    protected override void Awake()
    {
        ChangeImage(isOn);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        ChangeImage(isOn);
    }

    private void ChangeImage(bool state)
    {
        switch (state)
        {
            case true:
                GraphicIsOff.enabled = false;
                break;
            case false:
                GraphicIsOff.enabled = true;
                break;
        }
    }
}
