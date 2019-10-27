using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Array;
using UnityEngine;
using System;

internal class BoardParent
{
    public Board board;
    public BoardParent parentBoard;
    public uint G;
    public uint H;
}

public static class FindingPath
{
    private static List<BoardParent> boardparent = new List<BoardParent>();

    private static List<Board> canGoList = null;

    public static List<Board> GetPath(Board current, Board target)
    {
        boardparent.Clear();
        List<BoardParent> openpath = new List<BoardParent>();
        List<BoardParent> closepath = new List<BoardParent>();

        List<Board> boards = current.GetAllPaths();
        BoardParent board = null;

        boards.ForEach(elem =>
        {
            board = new BoardParent { board = elem, parentBoard = new BoardParent { board = current, parentBoard = null } };
            boardparent.Add(board);
            openpath.Add(board);
        });

        while (openpath.Count >= 1)
        {
            board = openpath.First();
            openpath.ForEach(elem =>
            {
                if (elem.G + elem.H < board.G + board.H)
                {
                    board = elem;
                }
            });

            if (board.board == target)
            {
                break;
            }

            //open에 주위 노드 넣고 close에 현재거 넣음
            closepath.Add(board);
            openpath.Remove(board);

            foreach (Board board1 in board.board.GetAllPaths())
            {
                bool isBreak = false;
                uint distance = (uint)Mathf.FloorToInt(Vector2.Distance(board1.transform.position, target.transform.position));

                foreach (BoardParent cp in closepath)
                {
                    if (cp.board == board1)
                    {
                        isBreak = true;
                        break;
                    }
                }

                if (isBreak)
                {
                    continue;
                }

                foreach (BoardParent cp in openpath)
                {
                    if (cp.board == board1)
                    {
                        if (cp.G + cp.H > board.G + 1 + distance)
                        {
                            cp.G = board.G;
                            cp.H = distance;
                            isBreak = true;
                            break;
                        }
                    }
                }

                if (isBreak)
                {
                    continue;
                }

                uint g = board.G + 1;
                uint h = (uint)Mathf.FloorToInt(Vector2.Distance(board1.transform.position, target.transform.position));
                var added = new BoardParent { board = board1, parentBoard = board, G = g, H = h };
                boardparent.Add(added);
                openpath.Insert(0, added);
            }
        }

        List<Board> path = new List<Board>();

        do
        {
            path.Add(board.board);
            board = board.parentBoard;
        } while (board.parentBoard != null);

        List<Board> temp = new List<Board>();

        ForEach(path.ToArray(), elem =>
        {
            temp.Insert(0, elem);
        });

        return temp;
    }

    private static List<Board> GetPathByG(Board current, int G)
    {
        List<Board> list = current.GetAllPaths();
        List<Board> retList = new List<Board>();
        list.ForEach(elem => retList.Add(GetPath1(elem, G, 1)));

        return retList;
    }

    private static Board GetPath1(Board current, int targetG, int currentG)
    {
        if (currentG == targetG)
        {
            return current;
        }

        Board board = current.GetMainStream();
        return GetPath1(board, targetG, currentG + 1);
    }

    public static void CanGoBoard(int value)
    {
        canGoList = GetPathByG(Move.move.Current, value);
        canGoList.ForEach(elem => elem.Arrow.gameObject.SetActive(true));
    }

    public static void ActivefalseCanList()
    {
        if (null != canGoList)
        {
            canGoList.ForEach(elem => elem.Arrow.gameObject.SetActive(false));
            canGoList.Clear();
        }
    }
}