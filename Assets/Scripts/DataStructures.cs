using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
public enum LogSign
{
    Normal,
    Warning,
    Error,
}
public enum UserState
{
    LoggedIn,
    LoggedOut,
}

[System.Serializable]
public class UserData
{
    public string uid;
    public string name;
    public string comment;
    public int level;
    public int exp;
    public Inventory inventory;
    // (추가예정)소지 캐릭터 정보
    // (추가예정)소지 장비 정보
}

[System.Serializable]
public class Inventory
{
    [JsonProperty("general")]
    public List<InventoryItem> item_general { get; set; }
    [JsonProperty("consumption")]
    public List<InventoryItem> item_consumption { get; set; }
    [JsonProperty("currency")]
    public List<InventoryItem> item_currency { get; set; }
    [JsonProperty("totem")]
    public List<InventoryItem> item_totem { get; set; }
    [JsonProperty("collection")]
    public List<InventoryItem> item_collection { get; set; }
}

[System.Serializable]
public class InventoryItem
{
    public int id { get; set; }
    public int count { get; set; }
    public string memo { get; set; }
}

public class CharacterData
{
    public int id;
    public string name;
    public string desc;
    public Sprite img_full;
    public Sprite img_panel;
    public Sprite img_icon;

    public CharacterStats stats;
}
public class CharacterStats
{
    // From Base GameData
    public int baseHp;
    public int baseDmg;

    // From UserData
    public int level;
    public int exp;

    public int plusHp;
    public int plusDmg;

    public int multipleHp;
    public int multipleDmg;
} 