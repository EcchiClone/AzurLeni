using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MobController : MonoBehaviour
{
    private int hp = 10;
    private int maxHp = 10;
    public event Action isDamaged;

    private VisualElement hpBar;
    private VisualElement hpFill;
    private UIDocument uiDocument;

    void Start()
    {
        uiDocument = gameObject.AddComponent<UIDocument>();

        uiDocument.panelSettings = Resources.Load<PanelSettings>("UI/New Panel Settings");
        uiDocument.visualTreeAsset = new VisualTreeAsset();

        InitializeHpBar();

        UpdateHpBar();

        isDamaged += UpdateHpBar;

        StartCoroutine(DamageOverTime());
    }
    void InitializeHpBar()
    {
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("UI/Adventure/UXML_AdventureUI");
        print(visualTree);
        VisualElement clonedHpBar = visualTree.CloneTree().Q<VisualElement>("MobHpBar");

        var root = uiDocument.rootVisualElement;
        print(root);
        root.Add(clonedHpBar);

        hpBar = clonedHpBar;
        hpFill = hpBar.Q<VisualElement>("HpFill");

        hpBar.style.position = Position.Absolute;
        hpBar.style.top = -50;
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
    public void UpdateHpBar()
    {
        float hpPercent = (float)hp / maxHp;
        hpFill.style.width = new Length(hpPercent * 100, LengthUnit.Percent);
    }
}
