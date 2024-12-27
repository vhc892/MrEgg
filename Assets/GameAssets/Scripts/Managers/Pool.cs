using UnityEngine;
using Helper;
public class Pool : MonoBehaviour
{
    public static Pool Instance;
    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }
}
