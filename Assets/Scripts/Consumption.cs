using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Consumption : Item
{
    public Consumption(string name, string texture, int quantity, int maxStackSize)
        : base(name, texture, quantity, maxStackSize)
    {
    }

    public void Use()
    {
        if (Quantity > 0)
        {
            Quantity--;
            Debug.Log($"{Name} used. Remaining: {Quantity}");
        }
        else
        {
            Debug.Log($"{Name} is out of stock.");
        }
    }
}