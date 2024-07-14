using System.Collections;
using UnityEngine;
public class Boss : Enemy {
    public GameObject missileTemplate;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform battery;
    public Unit target; 
    Missile missile = null;
    protected float fireTimer2 = 0;
    protected float fireTimer3 = 0;
    public float fireRate2 = 5f;
    public float UltCD =10f;

    public override void OnStart()
    {
        this.Fly();
        StartCoroutine(Enter());   
    }

    IEnumerator Enter()
    {
        this.transform.position = new Vector3(15, 0, 0);
        yield return MoveTo(new Vector3(5, 0, 0));
        yield return DoAttack();
    }

    IEnumerator UltraAttack()
    {
        yield return MoveTo(new Vector3(5, 5, 0));
        yield return FireMissile();
        yield return MoveTo(new Vector3(5, 0, 0));
    }


    IEnumerator DoAttack()
    {
        while (true)
        {
            fireTimer2 += Time.deltaTime;
            fireTimer3 += Time.deltaTime;
            Fire();
            Fire2();
            if (fireTimer3 > UltCD)
            {
                fireTimer3 = 0;
                yield return UltraAttack();

            }
            yield return null;
        }
    }
    IEnumerator MoveTo(Vector3 pos) {
        while (true)
        {
            Vector3 dir = pos - this.transform.position;
            if (dir.magnitude < 0.1)            
                break;          
            this.transform.position += dir.normalized * speed * Time.deltaTime;
            yield return null;
        }
    }

    public override void OnUpdate()
    {
        if (target != null)
        {
            Vector3 dir = (target.transform.position - battery.position).normalized;
            battery.rotation = Quaternion.FromToRotation(Vector3.left, dir);
        }
    }

    public void OnMissileLoad()
    {
        GameObject go = Instantiate(missileTemplate,firePoint3);
        missile =  go.GetComponent<Missile>();
        missile.target = this.target.transform;
    }

    public void OnMissleLaunch()
    {
        if (missile == null)
            return;
        missile.transform.SetParent(null);
        missile.Launch();
    } 

    IEnumerator FireMissile()
    {
        this.animator.SetTrigger("Skill");
        yield return new WaitForSeconds(3f);   
    }

    void Fire2()
    {
        if(fireTimer2>1f/fireRate2)
        {
            GameObject go = Instantiate(bulletTemplate, firePoint2.position, battery.rotation);
            Element bullet = go.GetComponent<Element>();
            bullet.direction = (target.transform.position - firePoint2.position).normalized;
            fireTimer2 = 0f;
        }
    }
}
