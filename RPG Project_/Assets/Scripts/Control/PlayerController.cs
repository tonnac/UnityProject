using System;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            [HideInInspector]
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float maxNavPathLength = 40f;

        private void Awake() 
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if(InteractWithUI()) return;
            if(health.IsDead) 
            {
                SetCursor(CursorType.None);
                return;
            }
            if(InteractWithComponent()) return;
            if(InteractWithMovement()) return;

            SetCursor(CursorType.None);
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RayCastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if(raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RayCastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            //Array.Sort(distances, hits);
            Array.Sort(hits, (x,y) => x.distance.CompareTo(y.distance));
            return hits;
        }

        private bool InteractWithUI()
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);            
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type) return mapping;
            }
            return cursorMappings[0];
        }

        private bool InteractWithMovement()
        {
            // Physics.Raycast(GetMouseRay(), out RaycastHit hit)
            if (RaycastNavMesh(out Vector3 target))
            {
                if(Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }
        private bool RaycastNavMesh(out Vector3 target)
        {
            target = Vector3.zero;
            if (!Physics.Raycast(GetMouseRay(), out RaycastHit hit)) return false;
            if (!NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas)) return false;
            NavMeshPath path = new NavMeshPath();
            target = navMeshHit.position;
            if (!NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path)) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;
            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0f;
            if(path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return total;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}