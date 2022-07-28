using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Rig))]
public class WeightAdjuster : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private float headTurnSpeed = 2f;

    private Rig rig;

    private void Awake()
    {
        rig = GetComponent<Rig>();
        rig.weight = 0f;
    }

    private void Update()
    {
        Vector3 direction = transform.position - lookAt.position;
        direction.y = 0f;
        float angle = Vector3.Angle(direction, transform.forward);
        bool isBehind = angle < 110f;

        if (isBehind)
        {
            rig.weight -= headTurnSpeed * Time.deltaTime;
            rig.weight = Mathf.Max(0f, rig.weight);
        }
        else
        {
            rig.weight += headTurnSpeed * Time.deltaTime;
            rig.weight = Mathf.Min(1f, rig.weight);
        }
    }
}
