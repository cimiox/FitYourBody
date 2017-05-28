using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    TextAsset ItemsDB { get; set; }
    List<Item> Items { get; set; }
    void Init();
}
