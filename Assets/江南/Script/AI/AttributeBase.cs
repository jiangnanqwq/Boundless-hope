
using UnityEngine;
using UnityEngine.AI;

public class AttributeBase : MonoBehaviour
{
    public float MaxHP= 100;
    protected float currentHP = 100;
    

    public float speed = 5;

    [HideInInspector] public AnimatorState state;

    public NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        state = AnimatorState.idle;
    }
    public Vector3 GetMoveDirection()
    {
        //Vector3 targetPosition = agent.steeringTarget;
        //Vector3 currentPosition = transform.position;
        //Vector3 direction = (targetPosition - currentPosition).normalized;
        //return direction;
        if (agent.hasPath)
        {
            return agent.desiredVelocity.normalized;
        }
        return Vector2.right;
    }
    public void SetTheDestination(Vector2 point)
    {
        agent.speed = speed;
        agent.SetDestination(point);
        transform.localEulerAngles = Vector3.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    public static bool RandomPoint(Vector2 center, float range, out Vector2 result)
    {
        for (int i = 0; i < 30; ++i)
        {
            Vector2 randomPoint = center + Random.insideUnitCircle * range;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector2.zero;
        return false;
    }
}
