using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    Button btn;
    Vector3 upScale = new Vector3(1.4f, 1.4f, 1);
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Anim);
    }
    private void Anim()
    {
        LeanTween.scale(gameObject, upScale, 0.2f);
        LeanTween.scale(gameObject, Vector3.one, 0.2f).setDelay(0.05f);
    }
}
