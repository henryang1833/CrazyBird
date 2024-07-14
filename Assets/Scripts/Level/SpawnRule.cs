using UnityEngine;
using UnityEditor;
using System;

public class SpawnRule : MonoBehaviour
{
    public Unit Monster;
    public float InitTime;
    
    public float Period;
    public int MaxNum;

    public int HP;
    public float Attack;

    float timeSinceLevelStart = 0;
    float levelStartTime = 0;
    int num = 0;
    float timer = 0;

    public ItemDropRule dropRuleTemplate;
    ItemDropRule dropRule;
    private void Start()
    {
        this.levelStartTime = Time.realtimeSinceStartup;
        if (dropRuleTemplate != null)
            dropRule = Instantiate<ItemDropRule>(dropRuleTemplate);
    }

    private void Update()
    {
        timeSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;
        if (num >= MaxNum)
            return;
        if (timeSinceLevelStart > InitTime)// 开始刷怪
        {   
            timer += Time.deltaTime;
            if (timer > Period)
            {
                timer = 0;
                Enemy enemy = UnitManager.Instance.CreateEnemy(this.Monster.gameObject);
                enemy.MaxHP = this.HP;
                enemy.Attack = this.Attack;
                enemy.OnDeath += Enemy_OnDeath;
                ++num;
            }
        }
        
    }



    private void Enemy_OnDeath(Unit sender)
    {
        if(dropRule!=null)
        dropRule.Execute(sender.transform.position);
    }
}