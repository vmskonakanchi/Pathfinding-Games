using UnityEngine;

public class Noise : MonoBehaviour
{
    private MeshRenderer noiseImage;
    [SerializeField, Range(0, 256)] private int height;
    [SerializeField, Range(0, 256)] private int width;
    [SerializeField, Range(0, 100)] private int scale;
    [SerializeField, Range(0, 500)] private int offsetX;
    [SerializeField, Range(0, 500)] private int offsetY;

    private void Start()
    {
        noiseImage = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        noiseImage.material.mainTexture = SampleTexture();
    }

    private Texture SampleTexture()
    {
        Texture2D sample = new Texture2D(width, height);
        sample.filterMode = FilterMode.Point;
        sample.wrapMode = TextureWrapMode.Clamp;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                sample.SetPixel(x, y, Perlinnoise(x, y));
            }
        }
        sample.Apply();
        return sample;
    }
    private Color Perlinnoise(int x, int y)
    {
        float xCord = (float)(x + offsetX) / width * scale;
        float yCord = (float)(y + offsetY) / height * scale;

        float sample = Mathf.PerlinNoise(xCord, yCord);
        Color noiseColor = new Color(sample, sample, sample);
        return noiseColor;
    }
}

