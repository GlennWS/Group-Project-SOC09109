using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen2 : MonoBehaviour
{
    public GameObject pre;

    public Material inputMat;
    public Material rendererMat;

    public Texture2D bump;

    public RectInt pixelRect;

    public int vertsX;
    public int vertsY;
    public float factorZ;

    public float ppvX;
    public float ppvY;

    private void Awake()
    {
        //mRenderer = GetComponent<MeshRenderer>();
        //mFilter = GetComponent<MeshFilter>();


        //BuildTxr();
    }

    // Use this for initialization
    void Start()
    {
        //BuildMesh();
        BuildSlope();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GrabTxrs(out Texture2D fullSlope, out Texture2D fullBump)
    {
        Texture2D tmp1 = (Texture2D)inputMat.GetTexture("_MainTex");
        fullSlope = new Texture2D(pixelRect.width, pixelRect.height);
        fullSlope.SetPixels(tmp1.GetPixels(pixelRect.x, pixelRect.y, pixelRect.width, pixelRect.height));
        fullSlope.Apply();

        Texture2D tmp2 = bump;
        fullBump = new Texture2D(pixelRect.width, pixelRect.height);
        fullBump.SetPixels(bump.GetPixels(pixelRect.x, pixelRect.y, pixelRect.width, pixelRect.height));
        fullBump.Apply();
    }

    private void BuildSlope()
    {
        List<GameObject> objects = new List<GameObject>();

        Texture2D slopeTxr;
        Texture2D bumpTxr;

        GrabTxrs(out slopeTxr, out bumpTxr);

        float[,] vals = new float[vertsX, vertsY * 4];

        for (int y = 0; y < vertsY * 4; y++)
        {
            for (int x = 0; x < vertsX; x++)
            {
                vals[x, y] = bumpTxr.GetPixel((int)(x * ppvX), (int)(y * ppvY)).r;
            }
        }
        //float lastY = 0;
        for (int i = 0; i < 4; i++)
        {
            int c = bumpTxr.height / 4;
            GameObject obj = Instantiate(pre, new Vector3(), Quaternion.identity);
            BuildMesh(slopeTxr, bumpTxr, obj, 4, i, c, ref vals);
        }
    }

    private void BuildMesh(Texture2D fullSlope, Texture2D fullBump, GameObject obj, int chunks, int chunk, int chunkS, ref float[,] vals)
    {
        Mesh ChunkMesh = new Mesh();
        MeshFilter mFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer mRenderer = obj.AddComponent<MeshRenderer>();
        MeshCollider mCollider = obj.AddComponent<MeshCollider>();

        int pixelY = chunk * chunkS;
        RectInt txrRect = new RectInt(0, pixelY, fullBump.width, chunkS);
        Texture2D slopeTxr = new Texture2D(fullBump.width, chunkS);
        slopeTxr.SetPixels(fullSlope.GetPixels(txrRect.x, txrRect.y, txrRect.width, txrRect.height));
        slopeTxr.Apply();

        mRenderer.material = new Material(inputMat);
        //rendererMat = mRenderer.material;
        mRenderer.material.SetTexture("_MainTex", slopeTxr);

        Vector3 start = new Vector3(0f, (vertsY * chunk), ((vertsY * chunk)));
        if (chunk > 0)
        {
            start.y -= chunk;
            start.z -= chunk;
        }

        int sizeX = vertsX - 1;
        int sizeY = vertsY - 1;

        int vSizeX = sizeX + 1;
        int vSizeY = sizeY + 1;
        int numVerts = (vSizeX) * (vSizeY);

        int numTiles = sizeX * sizeY;
        int numTris = numTiles * 2;

        Vector3[] verts = new Vector3[numVerts];
        int[] triVertIdxs = new int[numTris * 3];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        //float factorX = inputMat.GetTexture("_MainTex").width / vertsX;
        //float factorY = inputMat.GetTexture("_MainTex").height / vertsY;
        int diff = chunk;
        for (int y = 0; y < vSizeY; y++)
        {
            for (int x = 0; x < vSizeX; x++)
            {
                float val = vals[x, (vertsY * chunk) + y - diff];
                //val = SmoothVal((int)(x * ppvX), (int)(pixelY + (y * ppvY)), fullBump);

                //if (y >= vSizeY - 1 && x == 0)
                //    last = val;
                //else if (y <= 0 && last != 0)
                //    val = last;
                //float ival = 1f - bump.GetPixel((int)(x * factorX), (int)(y * factorY)).r;

                verts[(y * vSizeX) + x] = start + new Vector3(x, y, (val * factorZ) + y);
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
        ChunkMesh.RecalculateNormals();

        mFilter.mesh = ChunkMesh;
        mCollider.sharedMesh = ChunkMesh;
    }

    public float SmoothVal(int X, int Y, Texture2D fullBump)
    {
        List<float> valsX = new List<float>();
        List<float> valsY = new List<float>();

        Vector2Int dims = new Vector2Int(fullBump.width, fullBump.height);

        if (Y + 1 < dims.y - 1)
            valsY.Add(fullBump.GetPixel(X, Y + 1).r);
        else
            valsY.Add(fullBump.GetPixel(X, Y).r);
        if (Y - 1 > 0)
            valsY.Add(fullBump.GetPixel(X, Y - 1).r);
        else
            valsY.Add(fullBump.GetPixel(X, Y).r);

        if (X + 1 < dims.x - 1)
            valsX.Add(fullBump.GetPixel(X + 1, Y).r);
        else
            valsX.Add(fullBump.GetPixel(X, Y).r);
        if (X - 1 > 0)
            valsX.Add(fullBump.GetPixel(X - 1, Y).r);
        else
            valsX.Add(fullBump.GetPixel(X, Y).r);



        float lX = Mathf.Min(valsX[0], valsX[1]) + (Mathf.Abs(valsX[0] - valsX[1]) / 2);
        float lY = Mathf.Min(valsY[0], valsY[1]) + (Mathf.Abs(valsY[0] - valsY[1]) / 2);

        float lerped = Mathf.Min(lX, lY) + (Mathf.Abs(lX - lY) / 2);

        return lerped;
    }

    public void BuildTxr()
    {
        //mRenderer.material = new Material(inputMat);
        //rendererMat = mRenderer.material;
    }
}
