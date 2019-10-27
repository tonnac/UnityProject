using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Arrow : MonoBehaviour, IPointerClickHandler
{
    public Board _Board { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Move.move.SetTarget(_Board);
            FindingPath.ActivefalseCanList();
        }
    }
}