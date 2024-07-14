using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1 : MonoBehaviour {
    public int LevelID;
    public string Name;
    public Boss Boss;
    public List<SpawnRule> Rules = new List<SpawnRule>();
    public float bossTime = 10f;
    public float timeSinceLevelStart = 0;
    public float levelStartTime = 0;
    Boss boss = null;
    public UnityAction<LEVEL_RESULT>  OnLveleEnd; //不能出现在父类中，事件不可以直接派生？
    public enum LEVEL_RESULT
    {
        NONE,
        SUCCESS,
        FAIL
    }
    public LEVEL_RESULT result = LEVEL_RESULT.NONE;

    void Start ()
    {
        StartCoroutine(RunLevel());
        
        this.levelStartTime = Time.realtimeSinceStartup; //推荐放到RunLevel中，播放完过场动画后
    }
	
	void Update ()
    {
        //推荐加个flag，播放完动画后再执行下面的代码
        timeSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;
        if (this.result != LEVEL_RESULT.NONE)
            return;
        if (timeSinceLevelStart > bossTime)
        {
            if (boss == null)
            {
                boss = (Boss)UnitManager.Instance.CreateEnemy(this.Boss.gameObject);
                boss.target = Game.Instance.player;
                boss.OnDeath += Boss_OnDeath;
            }
        }
    }

    private void Boss_OnDeath(Unit sender)
    {
        this.result = LEVEL_RESULT.SUCCESS;
        if (this.OnLveleEnd != null)
            this.OnLveleEnd(this.result);
    }

    IEnumerator RunLevel()
    {
        UIManager.Instance.ShowLevelStart(string.Format("LEVEL {0} {1}", this.LevelID, this.Name));
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < Rules.Count; ++i)
            Instantiate<SpawnRule>(Rules[i]);
    }
}
