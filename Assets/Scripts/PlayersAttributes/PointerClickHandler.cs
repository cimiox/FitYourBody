using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerClickHandler : MonoBehaviour, IPointerClickHandler
{
    public delegate void Intialized();
    public static event Intialized OnIntialized;

    public static Muscle Muscle { get; set; }
    private static GameObject ClickHandler { get; set; }
    public GameObject ExpCircle { get; set; }

    private void Start()
    {
        ExpCircle = Resources.Load<GameObject>("ExpCircle");
        ClickHandler = gameObject;
        ClickHandler.SetActive(false);
    }

    public static void Intialize(Muscle muscle)
    {
        OnIntialized?.Invoke();
        ClickHandler.SetActive(true);
        Muscle = muscle;
    }

    public static void Close()
    {
        OnIntialized?.Invoke();
        ClickHandler.SetActive(false);
        Muscle = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Muscle.Click();

        var radius = UnityEngine.Random.Range(2.0f, 4.0f);
        var expCircle = Instantiate(ExpCircle, eventData.position, Quaternion.identity, PlayerAttributes.ExperienceSlider.transform);

        expCircle.transform.localScale = new Vector3(radius, radius, 0);
        expCircle.GetComponent<ExperienceCircleController>().StartMove();
        //TODO: CHANGE COLOR
        //expCircle.GetComponent<Image>().color = PlayerAttributes.ExperienceSlider;
    }
}
