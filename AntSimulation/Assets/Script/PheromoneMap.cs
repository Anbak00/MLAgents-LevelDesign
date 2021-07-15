using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneMap : MonoBehaviour
{
    public float size = 1;

    private Vector3 start = new Vector3();
    private Vector3 end = new Vector3();    

    private SpriteRenderer renderer;
    private Texture2D pheromoneMap;
    private Color[,] colors;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        
        pheromoneMap = new Texture2D(288, 160);
        pheromoneMap.wrapMode = TextureWrapMode.Repeat;
        //pheromoneMap.filterMode = FilterMode.Point;
        //pheromoneMap.mipMapBias = 0f;

        colors = new Color[288, 160];

        for (int i = 0; i < 288; i++)
        {
            for (int j = 0; i < 160; i++)
            {
                colors[i, j] = new Color(0f, 1f, 0f, 1f);
            }
        }
        
        pheromoneMap.Apply();
        renderer.sprite = Sprite.Create(pheromoneMap, new Rect(0f, 0f, 288, 160), new Vector2(0f, 0f), 4f);
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
                colors[i, j].b -= Time.deltaTime * 0.02f;
                colors[i, j].g -= Time.deltaTime * 0.02f;
                pheromoneMap.SetPixel(i, j, colors[i,j]);
            }
        }
        pheromoneMap.Apply();
    }

    public void SetGreenByPos(Vector2 pos,float g)
    {
        colors[(int)(pos.x * 4f), (int)(pos.y * 4f)].g = g;
    }
    public void SetBlueByPos(Vector2 pos, float b)
    {
        colors[(int)(pos.x * 4f), (int)(pos.y * 4f)].b = b;
    }

    public float GetGreenByPos(Vector2 pos)
    {
        return colors[(int)(pos.x * 4f), (int)(pos.y * 4f)].g;
    }
    public float GetBlutByPos(Vector2 pos)
    {
        return colors[(int)(pos.x * 4f), (int)(pos.y * 4f)].b;
    }

    private void DrawGridLine()
    {
        for (int i = 0; i < 288; i++)
        {
            start.x = -1000f;
            end.x = 1000f;

            start.y = i * size;
            end.y = i * size;
            Debug.DrawLine(start + transform.position, end + transform.position, Color.black);

            start.y = -1000f;
            end.y = 1000f;
            start.x = i * size;
            end.x = i * size;
            Debug.DrawLine(start + transform.position, end + transform.position, Color.black);
        }
    }
}
