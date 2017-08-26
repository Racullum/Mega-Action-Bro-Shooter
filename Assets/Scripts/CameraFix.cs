using UnityEngine;

[ExecuteInEditMode]
public class CameraFix : MonoBehaviour
{

    [Range(1, 4)]
    public int pixelScale = 1;
    public GameObject player;

    private int pixelsPerUnit = 100;
    private float halfScreen = 0.5f;


    private Camera _camera;

    private Vector3 playerTransform;
    private float cameraTransformX;
    private float cameraTransformY;
    private Vector3 cameraVector;
    private Vector3 playerVector;
    public float cameraSpeed;

    float timeToLerp = 1;
    float timeLerped = 0.0f;



    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void FixedUpdate()
    {
        // getting the camera
        // note: this could be done in Start()
        // however when running in Editor Mode you might get a null Camera here, this is mostly for helping newbies
      
        //_camera.orthographicSize = Screen.height * ((halfScreen / pixelsPerUnit) / pixelScale);
        playerTransform = GameObject.Find("Player").transform.position;
        cameraTransformX = transform.position.x;
        cameraTransformY = transform.position.y;
        cameraVector = new Vector3(cameraTransformX, cameraTransformY, -10);
        playerVector = new Vector3(playerTransform.x, playerTransform.y, -10);
        // transform.position = Vector3.Lerp(cameraVector, playerVector, cameraSpeed * Time.deltaTime);
        timeLerped += Time.deltaTime;
        transform.position = Vector3.Lerp(cameraVector, playerVector, timeLerped/timeToLerp);
        
    }
}