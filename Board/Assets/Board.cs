using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Board : MonoBehaviour
{
    [SerializeField]
    protected Board mainTarget;

    virtual public List<Board> GetAllPaths() => new List<Board> { mainTarget };

    public Board GetMainStream() => mainTarget;

    public Arrow Arrow { get => arrow; }

    private Arrow arrow;

    private void Awake()
    {
        arrow = GetComponentInChildren<Arrow>();
        arrow.gameObject.SetActive(false);
        arrow._Board = this;
    }
}