using UnityEngine;
using Spine.Unity;
using UnityEngine.UIElements;

public class PlayerCloneController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private KnockEffect knockEffectPrefab;

    private SkeletonAnimation anim;
    private string runAnimation = "run";

    void Awake()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
    }

    void Start()
    {
        if (anim != null)
        {
            anim.AnimationState.SetAnimation(0, runAnimation, true);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            if (knockEffectPrefab != null)
            {
                Vector3 effectPosition = transform.position + new Vector3(0, 0.5f, 0);

                KnockEffect knockEffect = Instantiate(knockEffectPrefab, effectPosition, Quaternion.identity);
                knockEffect.PlayKnockAnimation();
            }
            Destroy(this.gameObject);
        }
    }
}
