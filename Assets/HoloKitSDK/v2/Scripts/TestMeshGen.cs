using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestMeshGen : MonoBehaviour {

    public int xSize, ySize;
    public Mesh mesh;

    public Vector3[] vertices;

    public Camera mCam;
    public Vector3 gtopleft;
    public Vector3 gtopright;
    public Vector3 gbotright;
    public Vector3 gbotleft;
    [Range(0f, 1f)]
    public float xDistAmount = 1f;
    [Range(0f, 1f)]
    public float yDistAmount = 1f;

    private void Awake()
    {
        //Generate();
    }

    private void Update()
    {
        Generate();
    }

    private void Generate()
    {
        gtopleft = mCam.ViewportToWorldPoint(new Vector3(0f, 1f, 1f));
        gtopright = mCam.ViewportToWorldPoint(new Vector3(1f, 1f, 1f));
        gbotright = mCam.ViewportToWorldPoint(new Vector3(1f, 0f, 1f));
        gbotleft = mCam.ViewportToWorldPoint(new Vector3(0f, 0f, 1f));

        float distw = Vector3.Distance(gtopleft, gtopright);
        float disth = Vector3.Distance(gtopleft, gbotleft);
        float stepw = distw / xSize;
        float steph = disth / ySize;

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector3[] verticesDistort = new Vector3[vertices.Length];
        Vector2[] uv = new Vector2[vertices.Length];

        float fovLeft = Mathf.Tan(-60f * Mathf.Deg2Rad);
        float fovRight = Mathf.Tan(60f * Mathf.Deg2Rad);
        int i = 0;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(((float) x * stepw) - (distw * 0.5f), (float) y * steph - (disth * 0.5f));

                float xlerp = Mathf.Lerp(fovLeft, fovRight, (float)x / (xSize - 1));
                float ylerp = Mathf.Lerp(fovLeft, fovRight, (float)y / (ySize - 1));

                float d = Mathf.Sqrt(xlerp * xlerp + ylerp * ylerp);
                float r = distortion.distortInv(d);
                float p = xlerp * r / d;
                float q = ylerp * r / d;
                //return new Rect(left, bottom, right - left, top - bottom);
                //u = (p - noLensFrustum[0]) / (noLensFrustum[2] - noLensFrustum[0]);
                //v = (q - noLensFrustum[3]) / (noLensFrustum[1] - noLensFrustum[3]);
                //float u = (p - 0f) / (-1f - 0f);
                //float v = (q - 1f) / (0f - 1f);
                float u = p/2f;
                float v = q/2f;

                verticesDistort[i] = new Vector3(u, v, 0f);

                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                i++;
            }
        }
        mesh.vertices = verticesDistort;
        mesh.uv = uv;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private Distortion distortion = new Distortion { Coef = new[] { 0.5f, 0.5f } };

    [System.Serializable]
    public struct Distortion
    {
        private float[] coef;
        public float[] Coef
        {
            get
            {
                return coef;
            }
            set
            {
                if (value != null)
                {
                    coef = (float[])value.Clone();
                }
                else
                {
                    coef = null;
                }
            }
        }

        public float distort(float r)
        {
            float r2 = r * r;
            float ret = 0;
            for (int j = coef.Length - 1; j >= 0; j--)
            {
                ret = r2 * (ret + coef[j]);
            }
            return (ret + 1) * r;
        }

        public float distortInv(float radius)
        {
            // Secant method.
            float r0 = 0;
            float r1 = 1;
            float dr0 = radius - distort(r0);
            while (Mathf.Abs(r1 - r0) > 0.0001f)
            {
                float dr1 = radius - distort(r1);
                float r2 = r1 - dr1 * ((r1 - r0) / (dr1 - dr0));
                r0 = r1;
                r1 = r2;
                dr0 = dr1;
            }
            return r1;
        }
    }
}
