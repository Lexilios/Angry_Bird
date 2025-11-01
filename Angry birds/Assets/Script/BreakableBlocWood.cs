using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BreakableBlocWood : MonoBehaviour 
{
    public Rigidbody2D rigidBody;

    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Color OkayColor = new Color(0.627f, 0.322f, 0.176f);
    [SerializeField] private Color WeakColor = new Color(0.420f, 0.227f, 0.020f);

    [SerializeField] private bool _broken;
    [SerializeField] private float pointDeVie = 20f;
    [SerializeField] private float MaxPointDeVie = 20f;

    private void Start()
    {
        visual = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        pointDeVie = MaxPointDeVie;
        visual.color = OkayColor;
        rigidBody.mass = 1f;
    }

    void Update()
    {
        if(pointDeVie < 0)
        {
            Destroy(gameObject);
        }
        if (pointDeVie / MaxPointDeVie < 0.5f)
        {
            visual.color = WeakColor;
        }
        else
        {
            visual.color = OkayColor;
        }
    }

    public void TakeDamage(float damage)
    {
        pointDeVie -= damage;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BreakableBlocWood>())
        {
            var bloc = collision.gameObject.GetComponent<BreakableBlocWood>();
            bloc.TakeDamage(bloc.rigidBody.linearVelocity.magnitude);
        }
        else
        {
            TakeDamage(rigidBody.linearVelocity.magnitude);
        }
    }
}
