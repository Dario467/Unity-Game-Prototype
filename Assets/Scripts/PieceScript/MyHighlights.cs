using System.Collections.Generic;
using UnityEngine;

public class MyHighlights : MonoBehaviour
{
    private Stack<GameObject> stHighLights;

    void Awake()
    {
        stHighLights = new Stack<GameObject>();
    }

    public void MyHighLightCreate(Vector3Int cellpos, Color color, float opacity = 0.3921569f)
    {
        GameObject hLInstance = HighLightManager.Instance.HighLightCreate(cellpos, color, opacity);
        stHighLights.Push(hLInstance);
    }

    public void MyHighLightsDestroy()
    {
        while (stHighLights.Count > 0)
        {
            GameObject hLObj = stHighLights.Pop();
            if (hLObj != null)
            {
                Destroy(hLObj);
            }
        }
    }
    
    
}
