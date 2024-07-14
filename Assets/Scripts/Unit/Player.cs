using UnityEngine;
using System.Collections;
public class Player : Unit
{
    public float invincibleTime = 3f;
    public float timer = 0f;

    public override void OnUpdate()
    {
        if (this.death)
            return;
        timer += Time.deltaTime;
        DoUserInput();
    }

    private void DoUserInput()
    {
        Vector2 pos = this.transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        this.transform.position = pos;

        if (Input.GetButton("Fire1"))
            this.Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.death)
            return;

        if (this.IsInvincible)
            return;
        Item item = collision.gameObject.GetComponent<Item>();
        Element bullet = collision.gameObject.GetComponent<Element>();
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (item != null)
            item.Use(this);
        if (bullet == null&&enemy == null)        
            return;
        if (bullet != null && bullet.side == SIDE.ENEMY)
        {
            this.HP -= bullet.power;
            if (this.HP <= 0)
                this.Die();
        }

        if (enemy != null)
        {
            this.HP = 0;
            this.Die();
        }
    }

    public void Rebirth()
    {
        StartCoroutine(DoRebirth());
    }

    IEnumerator DoRebirth()
    {
        yield return new WaitForSeconds(2f);
        timer = 0;
        Init();
        Fly();
    }

    public bool IsInvincible
    {
        get
        {
            return timer < this.invincibleTime;
        }
    }
}
