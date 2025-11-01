using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BreakableBlocGlass : MonoBehaviour 
{
    public Rigidbody2D rigidBody;

    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Color OkayColor = new Color(0.529f, 0.808f, 0.922f);
    [SerializeField] private Color WeakColor = new Color(0.071f, 0.569f, 1f);

    [SerializeField] private bool _broken;
    [SerializeField] private float pointDeVie = 10f;
    [SerializeField] private float MaxPointDeVie = 10f;

    private void Start()
    {
        visual = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        pointDeVie = MaxPointDeVie;
        visual.color = OkayColor;
        rigidBody.mass = 2f;
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
        if (collision.gameObject.GetComponent<BreakableBlocGlass>())
        {
            var bloc = collision.gameObject.GetComponent<BreakableBlocGlass>();
            bloc.TakeDamage(bloc.rigidBody.linearVelocity.magnitude);
        }
        else
        {
            TakeDamage(rigidBody.linearVelocity.magnitude);
        }
    }
}
