using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenTerrain : MonoBehaviour
{
    public Mesh ChunkMesh { get; set; }
    public MeshFilter mFilter;
    public MeshRenderer mRenderer;
    public MeshCollider mCollider;

    public int sizeX;
    public int sizeY;

    public Material inputMat;
    public Material rendererMat;

    public Texture2D bump;

    private void Awake()
    {
        mRenderer = GetComponent<MeshRenderer>();
        mFilter = GetComponent<MeshFilter>();

        BuildMesh();
        BuildTxr();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTile(Texture2D tile, int x, int y)
    {
        Texture2D txr = (Texture2D)mRenderer.materials[0].GetTexture("_MainTex");
        Color[] pixels = tile.GetPixels(0, 0, 32, 32);
        txr.SetPixels(x * 32, y * 32, 32, 32, pixels);
        txr.Apply();
        mRenderer.materials[0].SetTexture("_MainTex", txr);
    }

    private void BuildMesh()
    {
        ChunkMesh = new Mesh();

        sizeX = 256;
        sizeY = 144;

        int vSizeX = sizeX + 1;
        int vSizeY = sizeY + 1;
        int numVerts = (vSizeX) * (vSizeY);

        int numTiles = sizeX * sizeY;
        int numTris = numTiles * 2;

        Vector3[] verts = new Vector3[numVerts];
        int[] triVertIdxs = new int[numTris * 3];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        for (int y = 0; y < vSizeY; y++)
        {
            for (int x = 0; x < vSizeX; x++)
            {
                float val = bump.GetPixel((int)(x * 7.5f), (int)(y * 7.5f)).r;
                float ival = 1f - bump.GetPixel((int)(x * 7.5f), (int)(y * 7.5f)).r;
                verts[(y * vSizeX) + x] = new Vector3((x / 5f) * 3f, (y / 5f) * 3f, (y / 4.5f + (val * 4.5f)) * 3f);
                normals[(y * vSizeX) + x] = Vector3.back;
                uv[(y * vSizeX) + x] = new Vector2((float)x / (float)sizeX, (float)y / (float)sizeY);
            }
        }

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                int squareIdx = y * sizeX + x;
                int triIdx = squareIdx * 6;

                triVertIdxs[triIdx + 0] = y * vSizeX + x + 0;
                triVertIdxs[triIdx + 1] = y * vSizeX + x + vSizeX + 0;
                triVertIdxs[triIdx + 2] = y * vSizeX + x + vSizeX + 1;

                triVertIdxs[triIdx + 3] = y * vSizeX + x + 0;
                triVertIdxs[triIdx + 4] = y * vSizeX + x + vSizeX + 1;
                triVertIdxs[triIdx + 5] = y * vSizeX + x + 1;
            }
        }

        ChunkMesh.vertices = verts;
        ChunkMesh.normals = normals;
        ChunkMesh.uv = uv;
        ChunkMesh.triangles = triVertIdxs;

        mFilter.mesh = ChunkMesh;
        mCollider.sharedMesh = ChunkMesh;
    }

    void BuildTxr()
    {
        mRenderer.material = new Material(inputMat);
    }
}
