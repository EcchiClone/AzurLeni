using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public Equipment(string name, string texture, int quantity, int maxStackSize)
        : base(name, texture, quantity, maxStackSize)
    {
    }

    public void Equip()
    {
        Debug.Log($"{Name} equipped.");
    }

    public void Unequip()
    {
        Debug.Log($"{Name} unequipped.");
    }
}