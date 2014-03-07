using UnityEngine;
using System.Collections;

public class SetActiveGO : MonoBehaviour
{
    public GameObject SelectedGameObject;

    public void ToggleSetActive()
    {
        SelectedGameObject.SetActive(!SelectedGameObject.activeSelf);
    }
}