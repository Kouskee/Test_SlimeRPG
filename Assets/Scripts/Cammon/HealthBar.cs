using UnityEngine;

public class HealthBar : MonoBehaviour
{
    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    Camera mainCamera;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        AlignCamera();
    }

    private void AlignCamera()
    {
        if (mainCamera == null) return;

        var camXform = mainCamera.transform;
        var forward = transform.position - camXform.position;
        forward.Normalize();
        var up = Vector3.Cross(forward, camXform.right);
        transform.rotation = Quaternion.LookRotation(forward, up);
    }

    public void UpdateParams(float hp)
    {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", hp);
        meshRenderer.SetPropertyBlock(matBlock);
    }
}