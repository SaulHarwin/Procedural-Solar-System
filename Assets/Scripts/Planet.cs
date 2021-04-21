using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    planetData planetData;

    public static Transform player;

    [Range(2,240)]
    public int resolution = 240;
    public float radius;
    public float amplitude;
    public float frequency;
    public int octaves;
    public float lacinarity;
    public float persistance;
    int numFaces;
    int numFacesPerDirection;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    
    public static Dictionary<int, float> detailLevelDistances = new Dictionary<int, float>() {
        {0, Mathf.Infinity},
        {1, 60f},
        {2, 25f},
        {3, 10f},
        {4, 4f},
        {5, 1.5f},
        {6, 0.7f},
        {7, 0.3f},
        {8, 0.1f},
    };

	private void Start() {
        numFacesPerDirection = (int)((radius / 10000)*(radius / 10000));
        numFaces = numFacesPerDirection * 6;

        planetData.resolution = resolution;
        planetData.radius = radius;
        planetData.amplitude = amplitude;
        planetData.frequency = frequency;
        planetData.octaves = octaves;
        planetData.lacinarity = lacinarity;
        planetData.persistance = persistance;
        planetData.numFaces = numFaces;
        Initialize();
        GenerateMesh();
	}

	void Initialize() {
        // if (meshFilters == null || meshFilters.Length == 0) {
            meshFilters = new MeshFilter[planetData.numFaces];
        // }
        terrainFaces = new TerrainFace[planetData.numFaces];
        
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < planetData.numFaces; i++) {
            if (meshFilters[i] == null) {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.transform.localPosition = new Vector3(0, 0, 0);
                meshObj.transform.localScale = new Vector3(1,1,1);
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, directions[i], planetData);
        }
    }

    void GenerateMesh() {
        foreach (TerrainFace face in terrainFaces) {
            face.ConstructMesh();
        }
    }
}
