using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Recorder.OutputPath;


public class MobController : UnitController
{
    [SerializeField] private int hp = 10;
    private int maxHp = 10;
    public event Action isDamaged;

    private VisualElement UIRoot;

    private VisualElement hpBar;
    private VisualElement hpFill;

    private void OnEnable()
    {
        Game.Field.AddEntityTransform(side, transform);
    }
    void Start()
    {
        InitializeUI();

        isDamaged += UpdateHpBar;

        StartCoroutine(DamageOverTime());
    }
    void InitializeUI()
    {
        uiDocument = GetComponent<UIDocument>();
        UIRoot = uiDocument.rootVisualElement;
        hpBar = UIRoot.Q<VisualElement>("HpBar");
        hpFill = hpBar.Q<VisualElement>("HpFill");
        UpdateHpBar();
    }
    public void UpdateHpBar()
    {
        float hpPercent = (float)hp / maxHp;
        hpFill.style.width = new Length(hpPercent * 100, LengthUnit.Percent);
    }
    private IEnumerator DamageOverTime()
    {
        while (hp > 0)
        {
            Damaged(1);
            yield return new WaitForSeconds(1f);
        }
    }
    public void Damaged(int _value)
    {
        hp -= _value;
        isDamaged?.Invoke();
    }

    void Update()
    {
        UpdateHpBarPosition();
    }

    void UpdateHpBarPosition()
    {
        // 월드 위치에서 화면 위치로 변환
        //float rootWidth = uiDocument.rootVisualElement.worldTransform.GetPosition();
        print(uiDocument.rootVisualElement.worldTransform.GetPosition());
        float rootHeight = UIRoot.resolvedStyle.height;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-1, 1, 0)); // 몹의 머리 위에 배치하도록 오프셋을 추가합니다.
        
        // 화면 위치를 UI Toolkit의 좌표계로 변환
        hpBar.style.left = screenPosition.x;
        hpBar.style.top = Screen.height - screenPosition.y; // UI Toolkit의 Y축은 아래로 증가하므로, Y 좌표를 반대로 설정합니다.
    }
}
