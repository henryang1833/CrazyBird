using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Element {

    public Transform target;
    private bool running = false;
    public GameObject fxExplod;

    public override void OnUpdate()
    {
        if (!running)
            return;
        if (target != null)
        {
            Vector3 dir = (target.position - this.transform.position);
            if(dir.magnitude < 0.1)
            {
                this.Explod();
            }

            this.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
            this.transform.position += speed * Time.deltaTime * dir.normalized;
        }
    }

    public void Launch()
    {
        running = true;
    }

    public void Explod()
    {
        Destroy(this.gameObject);
        // Instantiate(fxExplod, this.transform.position, Quaternion.identity);  没讲怎么创建的粒子特效
        if (target != null)
        {
            Player p = target.GetComponent<Player>();
            p.Damage(this.power);
        }
    }
}
