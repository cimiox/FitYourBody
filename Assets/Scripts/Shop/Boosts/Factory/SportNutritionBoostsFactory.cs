using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportNutritionBoostsFactory : IBoostsFactory
{
    public GameObject BoostGameObject
    {
        get
        {
            return Resources.Load<GameObject>("Shop/Boost");
        }
    }

    public GameObject CreateBoost(Boost boost, Transform parent)
    {
        GameObject obj = Object.Instantiate(BoostGameObject, parent);
        obj.GetComponent<SportNutritionBoostBehaviour>().Boost = boost;

        return obj;
    }

    public GameObject CreateBoost(Boost boost)
    {
        return CreateBoost(boost, null);
    }
}
