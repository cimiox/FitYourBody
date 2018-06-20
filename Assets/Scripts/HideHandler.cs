using UnityEngine;

public class HideHandler : MonoBehaviour
{
    private void Start()
    {
        PointerClickHandler.OnIntialized += PointerClickHandler_OnIntialized;
    }

    private void PointerClickHandler_OnIntialized()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
