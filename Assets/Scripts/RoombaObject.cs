using UnityEngine;

public class RoombaObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundLayer = ~0;

    [Header("Wander")]
    [SerializeField] private float wanderRadius = 15f;
    [SerializeField] private float wanderInterval = 3f;
    [SerializeField] private Vector3 areaCenter = Vector3.zero;

    [Header("Flee")]
    [SerializeField] private float fleeDistance = 8f;
    [SerializeField] private float fleeSpeedMultiplier = 1.8f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float heightRayOffset = 5f;

    private Rigidbody rb;
    private Vector3 targetPoint;
    private float wanderTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        targetPoint = transform.position;
    }

    private void Start()
    {
        PickNewWanderPoint();
    }

    private void FixedUpdate()
    {
        bool isFleeing = player != null && Vector3.Distance(transform.position, player.position) < fleeDistance;

        if (isFleeing)
        {
            Vector3 awayDir = (transform.position - player.position);
            awayDir.y = 0f;
            targetPoint = ClampToArea(transform.position + awayDir.normalized * wanderRadius);
            wanderTimer = 0f; // force a fresh wander point once it stops fleeing
        }
        else
        {
            wanderTimer -= Time.fixedDeltaTime;
            if (wanderTimer <= 0f || Vector3.Distance(transform.position, targetPoint) < 0.5f)
            {
                PickNewWanderPoint();
            }
        }

        MoveTowards(targetPoint, isFleeing ? moveSpeed * fleeSpeedMultiplier : moveSpeed);
    }

    private void PickNewWanderPoint()
    {
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        Vector3 point = areaCenter + new Vector3(randomCircle.x, 0f, randomCircle.y);
        targetPoint = ClampToArea(point);
        wanderTimer = wanderInterval;
    }

    private Vector3 ClampToArea(Vector3 point)
    {
        Vector3 offset = point - areaCenter;
        offset = Vector3.ClampMagnitude(offset, wanderRadius);
        return areaCenter + offset;
    }

    private void MoveTowards(Vector3 destination, float speed)
    {
        Vector3 flatTarget = SampleGroundHeight(destination);
        Vector3 toTarget = flatTarget - transform.position;
        toTarget.y = 0f;

        if (toTarget.sqrMagnitude > 0.0001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(toTarget.normalized, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime));
        }

        Vector3 moveDir = toTarget.normalized * speed * Time.fixedDeltaTime;
        Vector3 nextPos = transform.position + moveDir;
        nextPos.y = SampleGroundHeight(nextPos).y;

        rb.MovePosition(nextPos);
    }

    // Terrain height varies, so cast down each step to keep the body glued to uneven ground.
    private Vector3 SampleGroundHeight(Vector3 point)
    {
        Vector3 rayOrigin = new Vector3(point.x, transform.position.y + heightRayOffset, point.z);
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, heightRayOffset * 2f, groundLayer))
        {
            return new Vector3(point.x, hit.point.y, point.z);
        }
        return point;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(areaCenter, wanderRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fleeDistance);
    }
}
