using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropRule : MonoBehaviour {
    public Item itemTemplate;
    public float dropRatio;

    public void Execute(Vector3 pos)
    {
        if(Random.Range(0,100) < dropRatio)
        {
            Item item = Instantiate<Item>(itemTemplate);
            item.transform.position = pos;
        }
    }
}
