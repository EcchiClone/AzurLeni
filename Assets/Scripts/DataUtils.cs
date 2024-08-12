using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataUtils : MonoBehaviour
{
    public static CharacterBase GetCharacterBaseWithID(int _characterNum)
    {
        return DataManager._GameData.character.FirstOrDefault(c => c.id == _characterNum);
    }
    public static UserCharacter GetUserCharacterWithID(int _characterNum)
    {
        return DataManager._UserData.character.FirstOrDefault(c => c.id == _characterNum);
    }
}
