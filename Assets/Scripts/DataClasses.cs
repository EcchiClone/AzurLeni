public class PlayerData123
{
    public string uid, name, have_char, comment;
    public int level, exp;
}

public class CharacterData
{
    public int id;
    public string name;
    public string description;
    public string img_full;
    public string img_panel;
    public string img_icon;

    public CharacterStats stats;
}
public class CharacterStats
{
    // From Base GameData
    public int level;
    public int exp;

    public int baseHp;
    public int baseDmg;

    // From UserData
    public int plusHp;
    public int plusDmg;

    public int multipleHp;
    public int multipleDmg;
}