using System.Collections;
using UnityEngine;

public class ButtonSequence : MonoBehaviour
{
    public GameObject[] buttons;
    public float animationDelay = 0.3f;

    private void Start()
    {
        StartSequence();
    }

    private IEnumerator ActivateButtons()
    {
        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                button.SetActive(true); 
                yield return new WaitForSeconds(animationDelay); 
            }
        }
    }
    public void DeactivateButtons()
    {
        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                button.SetActive(false);
            }
        }
    }

    public void StartSequence()
    {
        StartCoroutine(ActivateButtons());
    }
}
