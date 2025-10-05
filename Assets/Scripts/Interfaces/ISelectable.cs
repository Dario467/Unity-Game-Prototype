using UnityEngine;

public interface ISelectable
{
    bool IsSelected { get; }
    void Select();
    void Deselect();
    void ShowSelectVisuals();
}
