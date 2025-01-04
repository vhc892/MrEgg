using UnityEngine;
using UnityEngine.UIElements;

public class KeyDropBox : MonoBehaviour
{
    [SerializeField] private KnockEffect knockEffectPrefab;
    [SerializeField] private Key key;
    public float fallSpeedThreshold = 5f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private float previousVelocityY;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        previousVelocityY = rb.velocity.y;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("drop");
        Debug.Log(previousVelocityY);
        if (Mathf.Abs(previousVelocityY) > fallSpeedThreshold)
        {
            TriggerDisable();
            KnockEffect knockEffect = Instantiate(knockEffectPrefab, transform.position, Quaternion.identity);
            knockEffect.transform.localScale *= 2f;
            knockEffect.PlayKnockAnimation();
        }
    }

    private void TriggerDisable()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;

        key.gameObject.SetActive(true);
    }
}
