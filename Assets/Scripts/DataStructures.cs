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
    public List<UserCharacter> character;
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

[System.Serializable]
public class CharacterBase
{
    public int id;
    public string name;
    public string name_en;
    public string desc;
    public string region;
    public string job;
    public int rarity;
    public int baseHp;
    public int baseDamage;

    public string imgFullPath;
    public string imgPanelPath;
    public string imgIconPath;

    [System.NonSerialized]
    public Sprite img_full;
    [System.NonSerialized]
    public Sprite img_panel;
    [System.NonSerialized]
    public Sprite img_icon;

    public void LoadSprites()
    {
        img_full = LoadSpriteFromPath(imgFullPath);
        //img_panel = LoadSpriteFromPath(imgPanelPath);
        //img_icon = LoadSpriteFromPath(imgIconPath);
    }

    private Sprite LoadSpriteFromPath(string _path)
    {
        return Resources.Load<Sprite>($"Images/Character/{_path}");
    }
}
[System.Serializable]
public class UserCharacter
{
    public int id;

    public int level = 0;
    public int exp = 0;
    public string memo = "";
    public int hpPlus = 0;
    public int damagePlus = 0;
    public int equip = 0;
}

[System.Serializable]
public class GameData
{
    public string version;
    public int totalCharacterCount;

    public List<CharacterBase> character;

    // 캐릭터 베이스 정보
    // 기타 게임 데이터
}