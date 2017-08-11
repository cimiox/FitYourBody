using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShop
{
    TextAsset ItemsDB { get; set; }
    List<Item> Items { get; set; }
    string Path { get; set; }
    List<Cell> Cells { get; set; }
    void Activate(string animation);
}
