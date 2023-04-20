using UnityEngine;
using System.Collections;

public class SeamlessBackground : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        Material material = meshRenderer.material;

        Vector2 offset = material.mainTextureOffset;

        offset.y += Time.deltaTime / 10f;

        material.mainTextureOffset = offset;
    }
}
