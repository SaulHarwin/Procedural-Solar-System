using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace{

    Noise noise = new Noise();
    planetData planetData;

    public Mesh mesh;
    Vector3 localUp; // Which way the planet is facing 
    Vector3 axisA;   // Perpendicular to localUp
    Vector3 axisB;   // Perpendicular to localUp and axisA

    int resolution;
    Vector3[] vertices;
    int[] triangles;

    public TerrainFace(Mesh mesh, Vector3 localUp, planetData planetData) {
        this.planetData = planetData;
        this.resolution = planetData.resolution;
        this.mesh = mesh;
        this.localUp = localUp;
        axisA = new Vector3(localUp.y, localUp.z, localUp.x); 
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh() {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;
        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                int i = x + y * resolution;
                Sphere(vertices, i, x, y);
                triIndex = Triangles(triangles, triIndex, i, x, y);
            }
        }
        UpdateMesh(vertices, triangles);
    }

    private void Sphere(Vector3[] vertices, int i, int x, int y) {
        Vector2 percent = new Vector2(x, y) / (resolution - 1); 
        Vector3 pointOnUnitCube = (localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB);
        Vector3 pointOnUnitSphere = pointOnUnitCube.normalized * planetData.radius;
        pointOnUnitSphere *= (1 + SebNoise(pointOnUnitSphere));
        vertices[i] = pointOnUnitSphere;
    }

    private float SebNoise(Vector3 point) {
        float noiseValue = 0;
        float frequency = planetData.frequency;
        float amplitude = planetData.amplitude;
        for (int o = 0; o < planetData.octaves; o++, amplitude *= planetData.persistance, frequency *= planetData.lacinarity) {
            noiseValue += ((noise.Evaluate(point * frequency) + 1) * .5f) * amplitude;
        }
        return noiseValue;
    }

    private int Triangles(int[] triangles, int triIndex, int i, int x, int y) {
        if (x != resolution - 1 && y != resolution - 1) {
            triangles[triIndex] = i;
            triangles[triIndex + 1] = i + resolution + 1;
            triangles[triIndex + 2] = i + resolution;
            triangles[triIndex + 3] = i;
            triangles[triIndex + 4] = i + 1;
            triangles[triIndex + 5] = i + resolution + 1;
            triIndex += 6;
        }
        return triIndex;
    }

    private void UpdateMesh(Vector3[] vertices, int[] triangles) {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}

public struct planetData {
    public int resolution;
    public float radius;
    public float amplitude;
    public float frequency;
    public int octaves;
    public float lacinarity;
    public float persistance;
    public int numFaces;
}