using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public static FollowCamera _instance;

    [HideInInspector]
    public float interpVelocity;

    [HideInInspector]
    public float minDistance;

    [HideInInspector]
    public float followDistance;

    //  private Transform target;
    [HideInInspector]
    public Vector3 offset;

    private Vector3 targetPos;

    public float smoothing;

    [HideInInspector]
    public bool isDead;

    [HideInInspector]
    public bool isFollow;

    [HideInInspector]
    // The target we are following
    public Transform target;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        isFollow = false;

        //change distance camera
        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[10] = 15;
        camera.layerCullDistances = distances;
    }

    private void ScreenScale()
    {
    }

    public void Setup()
    {
        if (target == null)
        {
            var targObj = GameObject.FindWithTag("Player");
            if(targObj)
            target = targObj.transform;
            //  target = GameObject.FindWithTag("OcSen").transform;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isFollow)
        {
            if (target)
            {
                // isDead = false;
                Vector3 posNoZ = transform.position;
                posNoZ.z = target.transform.position.z;

                Vector3 targetDirection = (target.transform.position - posNoZ);

                interpVelocity = targetDirection.magnitude * smoothing;

                targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

                transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

                if (isDead)
                {
                    transform.position = targetPos + offset;
                }
            }
        }
    }
}