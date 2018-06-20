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
        obj.GetComponent<BoostBehaviour>().Boost = boost;
        obj.GetComponent<BoostBehaviour>().BoostImage.sprite = boost.Properties.Sprite;

        if (boost is SportNutritionBoost)
        {
            (boost as SportNutritionBoost).OnEndBoost += () =>
            {
                Object.Destroy(obj.gameObject);
                BoostDatabase.Boosts.Remove(boost);
                BoostDatabase.Save();
            };
        }
        

        return obj;
    }

    public GameObject CreateBoost(Boost boost)
    {
        return CreateBoost(boost, null);
    }
}
