using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//https://gamedevacademy.org/complete-guide-to-procedural-level-generation-in-unity-part-1/



[System.Serializable]
public class TerrainType {
    public string name;
    public float height;
    public Color color;
    public int index;
}

public class TileGeneration : MonoBehaviour {
 

    [SerializeField]
    NoiseGeneration noiseMapGeneration;
 
    [SerializeField]
    private MeshRenderer tileRenderer;
 
    [SerializeField]
    private MeshFilter meshFilter;
 
    [SerializeField] 
    private MeshCollider meshCollider;
 
    [SerializeField]
    private float mapScale;
    
    [SerializeField]
    private TerrainType[] heightTerrainTypes;
 
    [SerializeField]
    private float heightMultiplier;
    
    [SerializeField]
    private AnimationCurve heightCurve;

    [SerializeField]
    private NoiseGeneration.Wave[] waves;

    private void UpdateMeshVertices(float[,] heightMap) {
        int tileDepth = heightMap.GetLength (0);
        int tileWidth = heightMap.GetLength (1);
 
        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
 
        // iterate through all the heightMap coordinates, updating the vertex index
        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < tileDepth; zIndex++) {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++) {
                float height = heightMap [zIndex, xIndex];
 
                Vector3 vertex = meshVertices [vertexIndex];
                // change the vertex Y coordinate, proportional to the height value
                meshVertices[vertexIndex] = new Vector3(vertex.x, height * this.heightMultiplier, vertex.z);
 
                // change the vertex Y coordinate, proportional to the height value. The height value is evaluated by the heightCurve function, in order to correct it.
                meshVertices[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);
                vertexIndex++;
            }
        }
 
        // update the vertices in the mesh and update its properties
        this.meshFilter.mesh.vertices = meshVertices;
        this.meshFilter.mesh.RecalculateBounds ();
        this.meshFilter.mesh.RecalculateNormals ();
        // update the mesh collider
        this.meshCollider.sharedMesh = this.meshFilter.mesh;
    }

    void Start() {
        GenerateTile ();
    }
 
    public TileData GenerateTile() {
        // calculate tile depth and width based on the mesh vertices
        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        int tileWidth = tileDepth;
 
        // calculate the offsets based on the tile position
        float offsetX = -this.gameObject.transform.position.x;
        float offsetZ = -this.gameObject.transform.position.z;


        // calculate the offsets based on the tile position
        float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap(tileDepth, tileWidth, this.mapScale, offsetX, offsetZ, waves);
 
        // build a Texture2D from the height map
        TerrainType[,] chosenHeightTerrainTypes = new TerrainType[tileDepth, tileWidth];
        Texture2D heightTexture = BuildTexture (heightMap, this.heightTerrainTypes, chosenHeightTerrainTypes);

       
        this.tileRenderer.material.mainTexture = heightTexture;
        UpdateMeshVertices(heightMap);
        
        TileData tileData = new TileData (heightMap,  chosenHeightTerrainTypes, this.meshFilter.mesh);

        return tileData;
    }
 
    private Texture2D BuildTexture(float[,] heightMap, TerrainType[] terrainTypes, TerrainType[,] chosenTerrainTypes) {
        int tileDepth = heightMap.GetLength (0);
        int tileWidth = heightMap.GetLength (1);

        Color[] colorMap = new Color[tileDepth * tileWidth];
        for (int zIndex = 0; zIndex < tileDepth; zIndex++) {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++) {
                // transform the 2D map index is an Array index
                int colorIndex = zIndex * tileWidth + xIndex;
                float height = heightMap [zIndex, xIndex];
                // choose a terrain type according to the height value
                TerrainType terrainType = ChooseTerrainType (height, terrainTypes);
                // assign the color according to the terrain type
                colorMap[colorIndex] = terrainType.color;

                // save the chosen terrain type
                chosenTerrainTypes [zIndex, xIndex] = terrainType;
            }
        }

        // create a new texture and set its pixel colors
        Texture2D tileTexture = new Texture2D (tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels (colorMap);
        tileTexture.Apply ();

        return tileTexture;
    }

    TerrainType ChooseTerrainType(float height, TerrainType[] terrainTypes) {
        // for each terrain type, check if the height is lower than the one for the terrain type
        foreach (TerrainType terrainType in terrainTypes) {
            // return the first terrain type whose height is higher than the generated one
            if (height < terrainType.height) {
                return terrainType;
            }
        }
        return terrainTypes [terrainTypes.Length - 1];
    }
}


// class to store all data for a single tile
public class TileData {
    public float[,]  heightMap;
    //public float[,]  heatMap;
    //public float[,]  moistureMap;
    public TerrainType[,] chosenHeightTerrainTypes;
    //public TerrainType[,] chosenHeatTerrainTypes;
    //public TerrainType[,] chosenMoistureTerrainTypes;
    //public Biome[,] chosenBiomes;
    public Mesh mesh;
 
    public TileData(float[,]  heightMap, TerrainType[,] chosenHeightTerrainTypes, Mesh mesh) {
        this.heightMap = heightMap;
        //this.heatMap = heatMap;
        //this.moistureMap = moistureMap;
        this.chosenHeightTerrainTypes = chosenHeightTerrainTypes;
        //this.chosenHeatTerrainTypes = chosenHeatTerrainTypes;
        //this.chosenMoistureTerrainTypes = chosenMoistureTerrainTypes;
        //this.chosenBiomes = chosenBiomes;
        this.mesh = mesh;
    }
}