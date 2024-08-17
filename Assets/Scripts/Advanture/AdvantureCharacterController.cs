using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
public enum CharacterStance
{

}
public struct CharacterState
{

}
public class AdvantureCharacterController : UnitController
{
    public CharacterBase baseData;
    public UserCharacter userData;

    public SpriteRenderer spriteRenderer;
    private string name;
    private int hp;
    private int damage;
    
    void Start()
    {
        
    }
    public void InitializeCharacter()
    {
        spriteRenderer.sprite = Resources.Load<Sprite>($"Images/Character/{baseData.imgFullPath}");
        name = baseData.name;
        hp = baseData.baseHp + userData.hpPlus;
        damage = baseData.baseDamage + userData.damagePlus;
    }
}
