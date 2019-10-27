using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Dropdown dropdown;

    public void ClickStart()
    {
        FindingPath.CanGoBoard(dropdown.value + 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            Object[] e = Resources.FindObjectsOfTypeAll(typeof(Arrow));

            foreach (Arrow arrow in e)
            {
                arrow.gameObject.SetActive(!arrow.gameObject.activeSelf);
            }
        }
    }
}