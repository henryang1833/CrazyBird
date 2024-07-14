using UnityEngine;

public class Element : MonoBehaviour {
    public float speed;
    public Vector3 direction = Vector3.zero;
    public float power = 1;
    public SIDE side;
    public float lifeTime = 10f;
    bool isBooming = false;

	void Start ()
    {
        Destroy(this.gameObject, lifeTime);
	}
	

	void Update ()
    {
        OnUpdate();
	}

    public virtual void OnUpdate()
    {
        if (isBooming)
            return;
        this.transform.position += speed * Time.deltaTime * direction;
        if (!GameUtil.Instance.InScreen(this.transform.position))  //Singleton<GameUtil>.Instance.InScreen(this.transform.position);       
            Destroy(this.gameObject);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.side != collision.gameObject.GetComponent<Unit>().side)
        {
            GetComponent<Animator>().SetTrigger("Boom");
            Destroy(this.gameObject,0.2f);
            isBooming = true;
        }
    }
}
