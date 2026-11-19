using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DarknessController : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;
    public Material darknessMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (darknessMaterial != null && player != null)
        {
            darknessMaterial.SetVector("_PlayerPos", player.position);
            darknessMaterial.SetFloat("_Radius", radius);
            Graphics.Blit(source, destination, darknessMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
