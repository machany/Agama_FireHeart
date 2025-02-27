using UnityEngine;
using UnityEngine.AI;

namespace Agama.Scripts.Test
{
    public class NavTest : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private NavMeshPath path;
        private int currentPathIndex = 0;
        private Rigidbody2D rb;
        public float speed = 3f;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            path = new NavMeshPath();
            InvokeRepeating("CalculatePath", 0f, 1f); // 1�ʸ��� ��� ����
        }

        [ContextMenu("Mover")]
        private void Move()
        {
            transform.GetComponent<NavMeshAgent>().SetDestination(target.position   );
        }

        private void Update()
        {
            if (path.corners.Length == 0) return;

            Vector2 targetPos = path.corners[currentPathIndex];
            Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

            rb.linearVelocity = direction * speed;

            if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            {
                currentPathIndex++;
                if (currentPathIndex >= path.corners.Length)
                {
                    rb.linearVelocity = Vector2.zero; // ��� ����
                }
            }
        }

        private void CalculatePath()
        {
            if (target != null)
            {
                if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
                {
                    currentPathIndex = 0;
                }
            }
        }
    }
}