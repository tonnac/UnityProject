using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Board current;

    public Board Current { get => current; }

    private Board _target;

    private Board moveObject;

    private List<Board> pathList;

    static public Move move;

    [SerializeField]
    private float damp;

    [SerializeField]
    private float epsilon;

    // Start is called before the first frame update
    private void Start()
    {
        move = this;
        //Image image = moveObject.GetComponent<Image>();
        //// 1, -2
        //// Debug.Log($"Viewport : {ViewportPosition}");
        //// Debug.Log($"WorldObject_ScreenPosition : {WorldObject_ScreenPosition}");
        //// Debug.Log($"asd : {CanvasRect.transform.position}");

        //Debug.Log($"Position: {image.transform.position}");
        //Debug.Log($"Width: {image.rectTransform.rect.width}");
        //Debug.Log($"Height: {image.rectTransform.rect.height}");
        //Debug.Log($"LocalPosition: {image.rectTransform.localPosition}");
        //Debug.Log($"Local to World: {Camera.main.ScreenToWorldPoint(image.rectTransform.position)}");

        //this.transform.position = image.transform.position;
        //Vector3 v = transform.position;
        //v.z = 0;
        //transform.position = v;
        SetPosition(current.transform.position);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (_target)
        {
            Vector2 speed = Vector2.zero;
            SetPosition(Vector2.SmoothDamp(transform.position, _target.transform.position, ref speed, damp));

            Vector2 currentVec = new Vector2(transform.position.x, transform.position.y);
            Vector2 movVec = new Vector2(_target.transform.position.x, _target.transform.position.y);

            if ((currentVec - movVec).magnitude < epsilon)
            {
                current = _target;
                if (_target != moveObject)
                {
                    int idx = pathList.IndexOf(_target);
                    _Move(pathList[idx + 1]);
                }
                else
                {
                    moveObject = null;
                    _target = null;
                }
            }
        }
    }

    private void _Move(Board target)
    {
        _target = target;
        animator.Play("New Animation", -1, 0);
    }

    public void SetTarget(Board target)
    {
        if (null == moveObject && target != current)
        {
            moveObject = target;
            pathList = FindingPath.GetPath(current, target);

            _Move(pathList[0]);
        }
    }

    public void SetPosition(Vector3 pos)
    {
        Vector3 vec = new Vector3(pos.x, pos.y, 0);
        transform.position = vec;
    }
}