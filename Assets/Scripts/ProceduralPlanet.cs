using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralPlanet : MonoBehaviour {
    Vector3[] heightMap;
    int[] faceTriangles; 

    public int chunkSize;

    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    private void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    
    private void Start() {
        MakePlanet();
        UpdateMesh();
    }

    private void MakePlanet() {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        TerrainGeneration();

        // for (int i = 0; i < 6; i++) {
            int i = 0;
            MakeFace(i);
        // }
    }


    private void MakeFace(int dir) {
        vertices.AddRange(TerrainGeneration());
        triangles.AddRange(GenerateTriangles());
        // vertices.AddRange(PlanetMeshData.faceVertices(dir));
        // int vCount = vertices.Count;

        // triangles.Add(vCount - 4);
        // triangles.Add(vCount - 4 + 1);
        // triangles.Add(vCount - 4 + 2);
        // triangles.Add(vCount - 4);
        // triangles.Add(vCount - 4 + 2);
        // triangles.Add(vCount - 4 + 3);
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
        faceTriangles = new int[(chunkSize) * (chunkSize) * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < chunkSize; z ++) {
            for (int x = 0; x < chunkSize; x ++) {
                faceTriangles[tris + 0] = vert + 0;
                faceTriangles[tris + 1] = vert + (chunkSize) + 1;
                faceTriangles[tris + 2] = vert + 1;
                faceTriangles[tris + 3] = vert + 1;
                faceTriangles[tris + 4] = vert + (chunkSize) + 1;
                faceTriangles[tris + 5] = vert + (chunkSize) + 2;
                vert++;
                tris += 6;
            }
            vert++;
        };
        return faceTriangles;
    }

    private void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}
