using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BreakableBlocStone : MonoBehaviour 
{
    public Rigidbody2D rigidBody;

    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Color OkayColor = new Color(0.4f, 0.4f, 0.4f);
    [SerializeField] private Color WeakColor = new Color(0.2f, 0.2f, 0.2f);

    [SerializeField] private bool _broken;
    [SerializeField] private float pointDeVie = 30f;
    [SerializeField] private float MaxPointDeVie = 30f;

    private void Start()
    {
        visual = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        pointDeVie = MaxPointDeVie;
        visual.color = OkayColor;
        rigidBody.mass = 3f;
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
        if (collision.gameObject.GetComponent<BreakableBlocStone>())
        {
            var bloc = collision.gameObject.GetComponent<BreakableBlocStone>();
            bloc.TakeDamage(bloc.rigidBody.linearVelocity.magnitude);
        }
        else
        {
            TakeDamage(rigidBody.linearVelocity.magnitude);
        }
    }
}
