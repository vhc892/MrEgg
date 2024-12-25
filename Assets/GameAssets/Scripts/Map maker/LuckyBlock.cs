using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyBlock : MonoBehaviour
{
    [SerializeField] GameObject original;
    [SerializeField] GameObject reveal;
    [SerializeField] GameObject broken;

    [SerializeField] int numberOfHits = 1;
    bool isBroken = false;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        original.SetActive(true);
        reveal.SetActive(false);
        broken.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHit()
    {
        if (isBroken)
        {
            return;
        }
        switch (numberOfHits)
        {
            case 1:
                reveal.SetActive(true);
                original.SetActive(false);
                break;
            case 0:
                broken.SetActive(true);
                reveal.SetActive(false);
                original.SetActive(false);
                isBroken = true;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.y > transform.position.y - 0.5f)
            {
                return;
            }
            Debug.Log("Player hit the block");
            OnHit();
            numberOfHits--;
        }
    }

}
