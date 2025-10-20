using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BreakableBloc : MonoBehaviour 
{
    public Rigidbody2D rigidBody;

    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Color OkayColor;
    [SerializeField] private Color WeakColor;

    [SerializeField] private bool _broken;
    [SerializeField] private float pointDeVie = 10f;
    [SerializeField] private float MaxPointDeVie = 10f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        pointDeVie = MaxPointDeVie;
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
        if (collision.gameObject.GetComponent<BreakableBloc>())
        {
            var bloc = collision.gameObject.GetComponent<BreakableBloc>();
            bloc.TakeDamage(bloc.rigidBody.linearVelocity.magnitude);
        }
        else
        {
            TakeDamage(rigidBody.linearVelocity.magnitude);
        }
    }
}
