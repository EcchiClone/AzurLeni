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

public class UserData
{
    public string uid, name, haveChar, comment;
    public int level, exp;
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