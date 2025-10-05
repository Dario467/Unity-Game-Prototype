using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightManager : MonoBehaviour
{
    public static HighLightManager Instance { get; private set; }
    [SerializeField] private GameObject hLight;
    private Color yellow;
    private Color green;

    public Color YELLOW => yellow;
    public Color GREEN => green;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ColorUtility.TryParseHtmlString("#F3FF00", out yellow);
            ColorUtility.TryParseHtmlString("#67FF36", out green);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public GameObject HighLightCreate(Vector3Int cellpos, Color color, float opacity = 0.3921569f)
    {
        // Then get the exact center of that cell
        Vector3 centerPos = GridManager.Instance.GetCellCenterWorldFunc(cellpos);
        GameObject hLInstance = Instantiate(hLight, centerPos, Quaternion.identity);

        // Color asignation
        SpriteRenderer hLSpriteRender = hLInstance.GetComponent<SpriteRenderer>();
        Color newColor = color;
        newColor.a = opacity;
        hLSpriteRender.color = newColor;

        // Add to the stack
        return hLInstance;
    }

}
