using UnityEngine;

public class MirrorCameraScript : MonoBehaviour
{
    public Transform observerTransform;
    public GameObject mirror;
    public float farClippingDistance = 500f;
    private Camera cam;
    private Mesh mirrorMesh;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        mirrorMesh = mirror.GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {
        Transform mirrorTransform = mirror.transform;
        Vector3 mirrorPosition = mirrorTransform.position;
        Vector3 mirrorNormal = mirrorTransform.up;

        // setting camera position and rotation
        // mirror camera position is simple reflection of observer camera from mirror plane
        // rotation should be this way: z pointing along the mirror normal and y pointing upwards
        Vector3 observerPos = observerTransform.position;
        Vector3 mirrorOffset = 2 * Vector3.Project(mirrorPosition, mirrorNormal);
        Vector3 reflect = Vector3.Reflect(observerPos, mirrorNormal);
        Vector3 position = reflect + mirrorOffset;
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(mirrorNormal, Vector3.up);
        
        
        // setting camera parameters - projection matrix
        // coordinates are calculated in camera transform coordinates
        // and camera transform is always orthogonal z-wise to mirror plane
        // further explanation here http://www.songho.ca/opengl/gl_projectionmatrix.html
        // n - near clip plane distance
        // f - far clip plane distance
        // r - right border of near clip plane distance
        // l - left border of near clip plane distance
        // t - top border of near clip plane distance
        // b - border border of near clip plane distance
        Vector3 mirrorExtents = Vector3.Scale(mirrorMesh.bounds.extents, mirror.transform.localScale);
        // mirror extents z is negative due to flip for switching right-hand coordinate system to left-hand one
        Vector3 mirrorPosInCamSpace = transform.InverseTransformPoint(mirrorPosition);
        float n = mirrorPosInCamSpace.z;
        float f = farClippingDistance;
        float r = -mirrorPosInCamSpace.x + mirrorExtents.x;
        float l = -mirrorPosInCamSpace.x - mirrorExtents.x;
        float t = mirrorPosInCamSpace.y - mirrorExtents.z;
        float b = mirrorPosInCamSpace.y + mirrorExtents.z;
        // this can be optimized by precalculating 2 * n, t - b, r - l, f - n,
        // but for the sake of clarity let it stay this way
        Vector4 column0 = new Vector4((2 * n) / (r - l),  0,                 0,                    0);
        Vector4 column1 = new Vector4(0,                  (2 * n) / (t - b), 0,                    0);
        Vector4 column2 = new Vector4(-(r + l) / (r - l), (t + b) / (t - b), -(f + n) / (f - n),   -1);
        Vector4 column3 = new Vector4(0,                  0,                 -2 * f * n / (f - n), 0);
        cam.projectionMatrix = new Matrix4x4(column0, column1, column2, column3);
    }

}
