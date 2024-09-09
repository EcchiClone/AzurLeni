using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
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
    public CharacterBase baseData = new CharacterBase();
    public UserCharacter userData = new UserCharacter();
    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;
    private string name;
    private int hp;
    private int damage;
    private float moveSpeed;

    Transform moveTargetT;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void InitializeCharacter()
    {
        spriteRenderer.sprite = Resources.Load<Sprite>($"Images/Character/{baseData.imgFullPath}");
        name = baseData.name;
        hp = baseData.baseHp + userData.hpPlus;
        damage = baseData.baseDamage + userData.damagePlus;

        side = UnitSide.User;
        state = UnitState.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case UnitState.Idle:
                state = UnitState.Search; break;
            case UnitState.Search:
                moveTargetT = Game.Field.SearchCloseEntity(transform, new UnitSide[] {UnitSide.Neutral, UnitSide.Enemy});
                if (moveTargetT != null)
                {
                    state = UnitState.Move;
                }
                break;
            case UnitState.Move:
                MoveTowardsTarget();
                break;
        }
    }

    private void MoveTowardsTarget()
    {
        if (moveTargetT == null) return;

        Vector3 direction = (moveTargetT.position - transform.position).normalized;

        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveTargetT.position) < 0.1f)
        {
            state = UnitState.Attack;
        }
    }
}
