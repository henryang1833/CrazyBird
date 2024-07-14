using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    List<Enemy> enemies = new List<Enemy>();

    public void Clear()
    {
        this.enemies.Clear();
    }

    public Enemy CreateEnemy(GameObject template)
    {
        if (template == null)
            return null;
        GameObject obj = Instantiate(template, this.transform);
        Enemy p = obj.GetComponent<Enemy>();
        enemies.Add(p);
        return p;
    }
}
