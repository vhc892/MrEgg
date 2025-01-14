using UnityEngine;
using Spine.Unity;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class PlayerCloneController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private KnockEffect knockEffectPrefab;

    private SkeletonAnimation anim;
    string[] runAnimation = { "run", "run2", "run3" };
    private Dictionary<string, float> walkSoundIntervals = new Dictionary<string, float>
    {
        { "run", 0.4f },
        { "run2", 0.33f },
        { "run3", 0.25f }
    };
    private float lastWalkSoundTime;



    void Awake()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
    }

    void Start()
    {
        if (anim != null)
        {
            string randomAnimation = runAnimation[Random.Range(0, runAnimation.Length)];
            anim.AnimationState.SetAnimation(0, randomAnimation, true);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (walkSoundIntervals.TryGetValue(anim.AnimationName, out float currentInterval))
        {
            if (Time.time - lastWalkSoundTime >= currentInterval)
            {
                AudioManager.Instance.PlaySFX("Walking");
                lastWalkSoundTime = Time.time;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Border")
        {
            if (knockEffectPrefab != null)
            {
                Vector3 effectPosition = transform.position + new Vector3(0, 0.5f, 0);

                KnockEffect knockEffect = Instantiate(knockEffectPrefab, effectPosition, Quaternion.identity);
                knockEffect.PlayKnockAnimation();
            }
            AudioManager.Instance.PlaySFX("Pop");
            Destroy(this.gameObject);
        }
    }
}