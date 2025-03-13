using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEditor;
using UnityEngine;


public class GifPlayer : MonoBehaviour
{
    public string path;
    public UnityEngine.UI.RawImage unityRawImage;

    public List<Texture2D> textures;
    public bool isPlay = false;
    public int index = 0;
    public float speed = .1f;

    private void Start()
    {
        Load();
    }

    private void Update()
    {
        if (isPlay && textures.Count > 0)
        {
            index++;
            int frameIndex = (int)(index * speed) % textures.Count;
            unityRawImage.texture = textures[frameIndex];
        }
    }

    public void Load()
    {
        index = 0;
        // 释放之前的纹理资源
        foreach (var texture in textures)
        {
            if (texture != null)
            {
                Destroy(texture);
            }
        }
        textures.Clear();

        try
        {
            using (var gifImage = System.Drawing.Image.FromFile(path))
            {
                FrameDimension dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
                int frameCount = gifImage.GetFrameCount(dimension);

                for (int i = 0; i < frameCount; i++)
                {
                    gifImage.SelectActiveFrame(dimension, i);

                    using (Bitmap bitmap = new Bitmap(gifImage.Width, gifImage.Height))
                    {
                        using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
                        {
                            graphics.DrawImage(gifImage, System.Drawing.Point.Empty);
                            Texture2D texture2D = new Texture2D(bitmap.Width, bitmap.Height);

                            for (int x = 0; x < bitmap.Width; x++)
                            {
                                for (int y = 0; y < bitmap.Height; y++)
                                {
                                    System.Drawing.Color color = bitmap.GetPixel(x, y);
                                    texture2D.SetPixel(x, bitmap.Height - 1 - y, new UnityEngine.Color(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f));
                                }
                            }
                            texture2D.Apply();
                            textures.Add(texture2D);
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"加载 GIF 文件时出错: {e.Message}，请检查文件路径是否正确。");
        }
    }

    private void OnDestroy()
    {
        // 在对象销毁时释放纹理资源
        foreach (var texture in textures)
        {
            if (texture != null)
            {
                Destroy(texture);
            }
        }
    }
}

[CustomEditor(typeof(GifPlayer))]
public class GifPlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("选择路径"))
        {
            var gif = target as GifPlayer;
            gif.path = EditorUtility.OpenFilePanel("选择 gif", "", "");
        }
    }
}