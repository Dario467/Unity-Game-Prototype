using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }
    private Selectable current;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void TrySelect(Selectable newSel)
    {
        if (current == newSel)
        {
            return;
        }
        if (current != null)
        {
            current.Deselect();
        }
        Debug.Log("Se selecciono algo");
        current = newSel;
        current.Select();
    }
    
    public void TryDeselect(Selectable newSel)
    {
        if (current == newSel)
        {
            current.Deselect();
            current = null;
        }
    }

    public Selectable GetCurrentSelection()
    {
        return current;
    }
}
