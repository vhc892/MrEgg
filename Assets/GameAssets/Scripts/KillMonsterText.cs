using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMonsterText : MonoBehaviour
{
    [SerializeField] private KnockEffect knockEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Debug.Log("trigger");
            if (knockEffectPrefab != null)
            {
                Vector3 effectPosition = collision.transform.position + new Vector3(0, 0.5f, 0);

                KnockEffect knockEffect = Instantiate(knockEffectPrefab, collision.transform.position, Quaternion.identity);
                knockEffect.PlayKnockAnimation();
            }
            AudioManager.Instance.PlayLoopingSFX("Pop");
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}