using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoostsFactory
{
    GameObject BoostGameObject { get; }

    GameObject CreateBoost(Boost boost);
    GameObject CreateBoost(Boost boost, Transform parent);
}
