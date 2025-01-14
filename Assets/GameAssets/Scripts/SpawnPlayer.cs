using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPlayer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject playerClone;
    private Animator anim;
    private bool canSpawn = true;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        anim.Play("DoorShake");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (canSpawn)
            StartCoroutine(HandleDoorAndSpawn());
    }

    private IEnumerator HandleDoorAndSpawn()
    {
        canSpawn = false;
        anim.Play("DoorOpen");
        AudioManager.Instance.PlaySFX("OpenDoor");

        yield return new WaitForSeconds(GetAnimationLength("DoorOpen"));

        GameObject spawnedPlayer = Instantiate(playerClone, transform.position, Quaternion.identity.normalized);
        spawnedPlayer.transform.parent = transform.parent;


        anim.Play("DoorClose");

        yield return new WaitForSeconds(GetAnimationLength("DoorClose"));
        canSpawn = true;
        anim.Play("DoorShake");
    }

    private float GetAnimationLength(string animationName)
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0f;
    }
}