using UnityEngine;

public class Enemy : Unit
{
    public float lifeTime = 5f;
    public float minRange;
    public float maxRange;
    public ENEMY_TYPE enemyType;
    float initY = 0;
    
    public override void OnStart()
    {
        Destroy(this.gameObject, lifeTime);
        initY = Random.Range(minRange, maxRange);
        this.transform.localPosition = new Vector3(0, initY, 0);
        this.Fly();
    }

    public override void OnUpdate()
    {
        float y = 0;

        if (this.enemyType == ENEMY_TYPE.SWING_ENEMY)        
            y = Mathf.Sin(Time.time) * 3f;
        
        this.transform.position = new Vector3(transform.position.x-Time.deltaTime * speed, initY + y, 0);
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Element bullet = collision.gameObject.GetComponent<Element>();
        if (bullet == null)
            return;
        if (bullet.side == SIDE.PLAYER)
        {
            this.Damage(bullet.power);
            if(this.HP<=0)
                this.Die();
        }
    }
}
