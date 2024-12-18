using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Item
{
    public string Name { get; set; }
    public string Texture { get; set; }
    public int Quantity { get; set; }
    public int MaxStackSize { get; set; }

    public Item(string name, string texture, int quantity, int maxStackSize)
    {
        Name = name;
        Texture = texture;
        Quantity = quantity;
        MaxStackSize = maxStackSize;
    }

    // 공통 메서드 정의
    public virtual void Obtain(int amount)
    {
        Quantity += amount;
        if (Quantity > MaxStackSize)
        {
            Quantity = MaxStackSize;
        }
        Debug.Log($"{Name} obtained: {amount}. Total: {Quantity}");
    }

    public virtual void Discard(int amount)
    {
        Quantity -= amount;
        if (Quantity < 0)
        {
            Quantity = 0;
        }
        Debug.Log($"{Name} discarded: {amount}. Total: {Quantity}");
    }
}
public interface IEquipable
{
    void Equip();
    void Unequip();
}

public interface IUsable
{
    void Use();
}