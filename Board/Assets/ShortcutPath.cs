using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShortcutPath : Board
{
    [SerializeField]
    private Board shortcut;

    public override List<Board> GetAllPaths() => new List<Board> { mainTarget, shortcut };
}