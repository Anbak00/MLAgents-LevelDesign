using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneMap : MonoBehaviour
{
    public float cell_size = 1;

    private float pixelsPerUnit = 4f;
    private Vector3 start = new Vector3();
    private Vector3 end = new Vector3();    
    private Vector2Int size = new Vector2Int();

    private SpriteRenderer renderer;
    private Texture2D pheromoneMap;
    private Color[,] colors;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        size.x = 288;
        size.y = 160;
        pheromoneMap = new Texture2D(size.x, size.y);
        pheromoneMap.wrapMode = TextureWrapMode.Repeat;
        pheromoneMap.filterMode = FilterMode.Point;
        //pheromoneMap.filterMode = FilterMode.Point;
        //pheromoneMap.mipMapBias = 0f;

        colors = new Color[size.x, size.y];

        for (int i = 0; i < 288; i++)
        {
            for (int j = 0; i < 160; i++)
            {
                colors[i, j] = new Color(0f, 1f, 0f, 1f);
            }
        }
        
        pheromoneMap.Apply();
        renderer.sprite = Sprite.Create(pheromoneMap, new Rect(0f, 0f, size.x, size.y), new Vector2(0f, 0f), pixelsPerUnit);
    }
    
    // Update is called once per frame
    void Update()
    {
        DrawGridLine();
    }

    private void LateUpdate()
    {
        //페로몬 감소
        for (int i = 0; i < 288; i++)
        {
            for (int j = 0; j < 160; j++)
            {
                colors[i, j].b -= Time.deltaTime * 0.01f;
                colors[i, j].g -= Time.deltaTime * 0.01f;
                colors[i,j].b = (colors[i,j].b < 0f) ? 0f : colors[i, j].b;
                colors[i, j].g = (colors[i, j].g < 0f) ? 0f : colors[i, j].g;
                pheromoneMap.SetPixel(i, j, colors[i,j]);
                
            }
        }
        pheromoneMap.Apply();
    }

    public void SetGreenByPos(Vector2 pos,float g)
    {
        colors[(int)(pos.x * pixelsPerUnit), (int)(pos.y * pixelsPerUnit)].g = g;
    }
    public void SetBlueByPos(Vector2 pos, float b)
    {
        colors[(int)(pos.x * pixelsPerUnit), (int)(pos.y * pixelsPerUnit)].b = b;
    }

    public void AddGreenByPos(Vector2 pos, float g)
    {
        colors[(int)(pos.x * pixelsPerUnit), (int)(pos.y * pixelsPerUnit)].g += g;
    }
    public void AddBlueByPos(Vector2 pos, float b)
    {
        colors[(int)((pos.x * pixelsPerUnit) %size.x), (int)((pos.y * pixelsPerUnit) %size.y)].b += b;
    }

    public float GetGreenByPos(Vector2 pos)
    {
        return colors[(int)(((pos.x + size.x)  * pixelsPerUnit) %size.x), (int)(((pos.y + size.y) * pixelsPerUnit) %size.y)].g;
    }
    public float GetBlueByPos(Vector2 pos)
    {
        return colors[(int)(((pos.x + size.x) * pixelsPerUnit) % size.x), (int)(((pos.y + size.y) * pixelsPerUnit) % size.y)].b;
    }

    private void DrawGridLine()
    {
        for (int i = 0; i < 288; i++)
        {
            start.x = -1000f;
            end.x = 1000f;

            start.y = i * cell_size;
            end.y = i * cell_size;
            Debug.DrawLine(start + transform.position, end + transform.position, Color.black);

            start.y = -1000f;
            end.y = 1000f;
            start.x = i * cell_size;
            end.x = i * cell_size;
            Debug.DrawLine(start + transform.position, end + transform.position, Color.black);
        }
    }
}
