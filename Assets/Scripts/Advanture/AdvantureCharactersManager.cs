using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AdvantureCharactersManager : MonoBehaviour
{
    public AdvantureCharacterController[] characters;
    public int[] characterNum = { 2, 4, 6 };
    private void Start()
    {
        for(int i = 0; i < characterNum.Length; i++)
        {
            characters[i].baseData = Game.GameData.character[characterNum[i]];
            characters[i].userData = Game.UserData.character[characterNum[i]];

            characters[i].InitializeCharacter();
        }
    }
}
