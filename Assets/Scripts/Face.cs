using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class Face : MonoBehaviour {
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles; 
    public int chunkSize;

    Vector3[] heightMap;

    private void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    
    private void Start() {
        MakePlanet();
        UpdateMesh();
    }

    private void MakePlanet() {

        // TerrainGeneration();

        // for (int i = 0; i < 6; i++) {
            int i = 0;
            MakeFace(i);
        // }
    }


    private void MakeFace(int dir) {
        vertices  = TerrainGeneration();
        triangles = GenerateTriangles();
    }

    private Vector3[] TerrainGeneration() {
        heightMap = new Vector3[((chunkSize)+1) * ((chunkSize)+1)];
        int i = 0;
        for (int x = 0; x <= chunkSize; x ++) {
            for (int z = 0; z <= chunkSize; z ++) {
                heightMap[i] = new Vector3(x, 0, z);
                i ++;
            }
        }
        return heightMap;
    }

    private void PivotFaces(Vector3[] face) {
        Quaternion rotation = Quaternion.Euler(0, 0, 90);
        for (int i = 0; i < face.Length; i++) {
            face[i] = rotation * (face[i] - new Vector3(0,0,0)) + new Vector3(0,0,0);
        }            
    }

    private int[] GenerateTriangles() {
        triangles = new int[(chunkSize) * (chunkSize) * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < chunkSize; z ++) {
            for (int x = 0; x < chunkSize; x ++) {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + (chunkSize) + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + (chunkSize) + 1;
                triangles[tris + 5] = vert + (chunkSize) + 2;
                vert++;
                tris += 6;
            }
            vert++;
        };
        return triangles;
    }

    private void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
