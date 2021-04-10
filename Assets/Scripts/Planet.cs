using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    planetData planetData;

    [Range(2,240)]
    public int resolution = 240;
    public float radius;
    public float amplitude;
    public float frequency;
    public int octaves;
    public float lacinarity;
    public float persistance;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    
	private void Start() {
        planetData.resolution = resolution;
        planetData.radius = radius;
        planetData.amplitude = amplitude;
        planetData.frequency = frequency;
        planetData.octaves = octaves;
        planetData.lacinarity = lacinarity;
        planetData.persistance = persistance;
        
        Initialize();
        GenerateMesh();
	}

	void Initialize() {
        if (meshFilters == null || meshFilters.Length == 0) {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {
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
