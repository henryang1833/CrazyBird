using UnityEngine;

public class Unit : MonoBehaviour {
    public SIDE side;
    public float speed = 5;
    public float fireRate = 5f;
    public Rigidbody2D rigidbodyBird;
    public float HP = 10f;
    public float MaxHP = 10f;
    public Animator animator;
    protected bool death = false;
    
    public Transform firePoint;
    public float Attack;
    public GameObject bulletTemplate;
    protected Vector3 initPos;
    protected bool isFlying = false;
    protected float fireTimer = 0;
    public delegate void DeathNotify(Unit sneder);
    public event DeathNotify OnDeath; //不能出现在父类中，事件不可以直接派生？
    public bool destroyOnDeath = false;
    public int life = 3;

    void Start ()
    {
        this.animator = this.GetComponent<Animator>();
        this.Idle();
        initPos = this.transform.position;
        this.Init();
        OnStart();
    }
	public virtual void OnStart()
    {

    }
	
	void Update ()
    {    
        if (this.death)
            return;
        if (!this.isFlying)
            return;
        fireTimer += Time.deltaTime;
        OnUpdate();
    }

    public virtual void OnUpdate()
    {

    }
    
    public void Init()
    {
        this.Idle();
        this.HP = this.MaxHP;
        this.transform.position = initPos;
        this.death = false;
    }

    public void Fire()
    {
        if (fireTimer > 1f / fireRate)
        {
            GameObject go = Instantiate(bulletTemplate);
            go.transform.position = firePoint.position;
            go.GetComponent<Element>().direction = this.side == SIDE.PLAYER?Vector3.right:Vector3.left;
            fireTimer = 0;
        }
    }
    public void Idle()
    {
        this.rigidbodyBird.simulated = false;
        this.animator.SetTrigger("Idle");
    }

    public void Fly()
    {
        this.rigidbodyBird.simulated = true;
        this.animator.SetTrigger("Fly");
        this.isFlying = true;
    }

    public void Die()
    {
        if (this.death)
            return;
        this.life--;
        this.death = true;
        this.animator.SetTrigger("Die");
        if (this.OnDeath != null)        
            this.OnDeath(this);   
        if (destroyOnDeath)       
            Destroy(this.gameObject, 0.3f);
    }

    public void Damage(float power)
    {
        this.HP -= power;
        if (this.HP <= 0)       
            this.Die();        
    }

    public void AddHP(int hp)
    {
        this.HP += hp;
        if (this.HP > this.MaxHP)
            this.HP = this.MaxHP;
    }
}
