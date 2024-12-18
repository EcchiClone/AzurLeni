using System.Collections;
using UnityEngine;

public class Material : Item
{
    public Material(string name, string texture, int quantity, int maxStackSize)
        : base(name, texture, quantity, maxStackSize)
    {
    }
}