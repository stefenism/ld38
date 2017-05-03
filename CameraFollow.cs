using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{


    public float xMargin = 3f;
    public float yMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    //Zoom Vars
    public float minZoom=1;
    public float maxZoom=12;
    private Vector3 cameraTarget;
    private float OSize;
    private float zoom_CurrentVelocity;

    private float damp = 0.3f; 
    private Vector3 m_CurrentVelocity;

    public GameObject player;
    public GameObject planet;
    private Transform camera;
    private Camera cameraRef;
    public Vector3 maxCameraPosition;
    public Vector3 minCameraPosition;
    private bool bounds = true;

    private Quaternion newRotation;
    private Vector3 rotationDirection;
    private float rotateSpeed = 2000f;

    // Use this for initialization
    void Start()
    {
        //player = GameObject.FindWithTag("Focus").transform;
        camera = GetComponent<Transform>();
        cameraRef = GetComponent<Camera>();
        OSize = cameraRef.orthographicSize;
    }

    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) > xMargin;
    }

    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - player.transform.position.y) > yMargin;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        TrackPlayer();

        if (bounds)
        {
            cameraTarget =Vector3.Lerp(player.transform.position, planet.transform.position, ((cameraRef.orthographicSize - minZoom) / (maxZoom - minZoom))-.1f);

            Vector3 newPosition = new Vector3(minCameraPosition.x + cameraTarget.x, minCameraPosition.y + cameraTarget.y, camera.position.z);
            //camera.position = new Vector3. (Mathf.Clamp(camera.position.x, minCameraPosition.x + player.position.x, maxCameraPosition.x + player.position.x),
            camera.position = Vector3.SmoothDamp(transform.position, newPosition, ref m_CurrentVelocity, damp); 
            //Mathf.Clamp(camera.position.y, minCameraPosition.y + player.position.y, maxCameraPosition.y + player.position.y),
            //Mathf.Clamp(camera.position.z, minCameraPosition.z, maxCameraPosition.z));
        }

        CheckBounds();
    }

    void Update()
    {
        RotateCamera();
        ScaleCameraInput();
        

    }

    void ScaleCameraInput()
    {
        OSize = OSize + (-3 * Input.GetAxis("Mouse ScrollWheel"));
        if (OSize <= minZoom)
        {
            OSize = minZoom;
        }
        else if (OSize >= maxZoom)
        {
            OSize = maxZoom;
        }

        cameraRef.orthographicSize = Mathf.SmoothDamp(cameraRef.orthographicSize,OSize,ref zoom_CurrentVelocity,damp);

    }


    void RotateCamera()
    {




        rotationDirection = (planet.transform.position - transform.position);//.position).normalized;
        float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);

        //newRotation = Quaternion.LookRotation(rotationDirection);
        //newRotation = Vector3.Angle(transform.rotation,rotationDirection);//Quaternion.Euler(0f,0f,player.transform.rotation.z);

        //transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z);
        //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);//.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
        //transform.rotation = Quaternion.Euler(0,0,rot_z);

        /*
        newRotation = Quaternion.Euler(player.transform.rotation.x,player.transform.rotation.y, player.transform.rotation.z);
        Debug.Log("newrotation: " + newRotation);
        Debug.Log("playerrotation: " + player.transform.localRotation);

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, new Quaternion(0f,0f,Mathf.Round(player.transform.rotation.z),1), rotateSpeed * Time.deltaTime);
        */
    }

    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.transform.position.x, xSmooth * Time.deltaTime);
        }

        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, player.transform.position.y, ySmooth * Time.deltaTime);
        }

        Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, .1f);
    }

    void CheckBounds()
    {
        if (transform.position.x > maxXAndY.x)
        {
            transform.position = new Vector3(maxXAndY.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x < minXAndY.x)
        {
            transform.position = new Vector3(minXAndY.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y > maxXAndY.y)
        {
            transform.position = new Vector3(transform.position.x, maxXAndY.y, transform.position.z);
        }
        if (transform.position.y < minXAndY.y)
        {
            transform.position = new Vector3(transform.position.x, minXAndY.y, transform.position.z);
        }
    }
}
