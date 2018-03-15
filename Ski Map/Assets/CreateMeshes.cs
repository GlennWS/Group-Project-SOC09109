using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMeshes : MonoBehaviour
{
    public GameObject obj;
    public Texture2D bump;
    public Texture2D mainT;
    public int chunksX;
    public int chunksY;
    public int totY;
    public int totX;

    public int vertsPerChunkX;
    public int vertsPerChunkY;

    public int startX;
    public int startY;

	// Use this for initialization
	void Start ()
    {
        object[] allG = Resources.LoadAll("Art/DevBumpMap");
        object[] all = Resources.LoadAll("Art/Dev16bit255x255Divided");
        for (int y = 0; y < chunksY; y++)
        {
            for (int x = 0; x < chunksX; x++)
            {
                GameObject tmp = Instantiate(obj, new Vector3(((vertsPerChunkX * x) / 2f), ((vertsPerChunkY * y) / 2f), 0f), Quaternion.identity) as GameObject;

                Texture2D greyT = new Texture2D(vertsPerChunkX, vertsPerChunkY);
                Texture2D txrT = new Texture2D(vertsPerChunkX, vertsPerChunkY);

                int iX = startX + x;
                int iY = startY + y;
                Color[] gPs = bump.GetPixels(iX * vertsPerChunkX, iY * vertsPerChunkY, vertsPerChunkX, vertsPerChunkY);
                Color[] mPs = mainT.GetPixels(iX * vertsPerChunkX, iY * vertsPerChunkY, vertsPerChunkX, vertsPerChunkY);
                greyT.SetPixels(gPs);
                txrT.SetPixels(mPs);

                greyT.Apply();
                txrT.Apply();
                //int flippedY = totY - startY;
                //int flippedX = totX - startX;
                //int row = flippedY + y - 1;
                //int col = startX + x;
                //int Idx = 0;
                //Sprite grey = (Sprite)allG[Idx];
                //Sprite txr = (Sprite)all[Idx];

                //Texture2D greyT = (Texture2D)allG[Idx];
                //Texture2D txrT = (Texture2D)allG[Idx];

                tmp.GetComponent<GenTerrain>().BuildTxr();
                tmp.GetComponent<GenTerrain>().mRenderer.material.SetTexture("_MainTex", txrT);
                tmp.GetComponent<GenTerrain>().bump = greyT;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
