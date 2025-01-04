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

    public ObjPoolInfor star;
    public ObjPoolInfor plusText;

    public void InitPool()
    {
        int total = 10;
        ObjPool[] stars = new ObjPool[total];
        ObjPool[] plusTexts = new ObjPool[total];
        for (int i = 0; i < total; i++)
        {
            stars[i] = star.GetPrefabInstance();
            plusTexts[i] = plusText.GetPrefabInstance();
        }
        foreach (ObjPool item in stars)
        {
            item.ReturnToPool();
        }
        foreach (ObjPool item in plusTexts)
        {
            item.ReturnToPool();
        }
    }
}
