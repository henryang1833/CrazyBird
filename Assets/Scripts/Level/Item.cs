using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public int AddHP = 50;
    public GameObject bullet;
    public float lifeTime = 30;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(0, -1f * Time.deltaTime, 0));
	}

    internal void Use(Unit target)
    {
        target.AddHP(this.AddHP);
        Destroy(this.gameObject);
    }
}
