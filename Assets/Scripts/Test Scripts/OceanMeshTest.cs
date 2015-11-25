using UnityEngine;
using System.Collections;

public class OceanMeshTest : MonoBehaviour 
{

	public Vector4 steepness, amplitude, frequency, speed, dirAB, dirCD;

	Mesh m;
	Vector3[] verts;
	
	
	public float GetOceanHeightAt(Vector2 coord)
	{
		/*
		Vector4 AB = new Vector4 (steepness.x * amplitude.x * dirAB.x,
		                         steepness.x * amplitude.x * dirAB.y,
		                         steepness.y * amplitude.y * dirAB.z,
		                         steepness.y * amplitude.y * dirAB.w);
		
		Vector4 CD = new Vector4 (steepness.z * amplitude.z * dirCD.x,
		                         steepness.z * amplitude.z * dirCD.y,
		                         steepness.w * amplitude.w * dirCD.z,
		                         steepness.w * amplitude.w * dirCD.w);
		                         */
		
		
		Vector4 dots = new Vector4 (Vector2.Dot (new Vector2 (dirAB.x, dirAB.y), coord),
		                           Vector2.Dot (new Vector2 (dirAB.z, dirAB.w), coord),
		                           Vector2.Dot (new Vector2 (dirCD.x, dirCD.y), coord),
		                           Vector2.Dot (new Vector2 (dirCD.z, dirCD.w), coord));
		
		Vector4 dotABCD = new Vector4 (dots.x * frequency.x,
		                              dots.y * frequency.y,
		                              dots.z * frequency.z,
		                              dots.w * frequency.w);
		
		float time = Time.time;
		
		Vector4 sin = new Vector4(Mathf.Sin (dotABCD.x + time * speed.x),
		                          Mathf.Sin (dotABCD.y + time * speed.y),
		                          Mathf.Sin (dotABCD.z + time * speed.z),
		                          Mathf.Sin (dotABCD.w + time * speed.w));
		
		return Vector4.Dot (sin, amplitude);
	}


	Vector3 GetGerstnerDisplacement(Vector3 coord)
	{

		Vector2 horiz = new Vector2 (coord.x, coord.z);

		Vector4 AB = new Vector4 (steepness.x * amplitude.x * dirAB.x,
		                         steepness.x * amplitude.x * dirAB.y,
		                         steepness.y * amplitude.y * dirAB.z,
		                         steepness.y * amplitude.y * dirAB.w);
		
		Vector4 CD = new Vector4 (steepness.z * amplitude.z * dirCD.x,
		                         steepness.z * amplitude.z * dirCD.y,
		                         steepness.w * amplitude.w * dirCD.z,
		                         steepness.w * amplitude.w * dirCD.w);
		                         
		
		
		Vector4 dots = new Vector4 (Vector2.Dot (new Vector2 (dirAB.x, dirAB.y), horiz),
		                            Vector2.Dot (new Vector2 (dirAB.z, dirAB.w), horiz),
		                            Vector2.Dot (new Vector2 (dirCD.x, dirCD.y), horiz),
		                            Vector2.Dot (new Vector2 (dirCD.z, dirCD.w), horiz));
		
		Vector4 dotABCD = new Vector4 (dots.x * frequency.x,
		                               dots.y * frequency.y,
		                               dots.z * frequency.z,
		                               dots.w * frequency.w);
		
		float time = Time.time;
		
		Vector4 sin = new Vector4(Mathf.Sin (dotABCD.x + time * speed.x),
		                          Mathf.Sin (dotABCD.y + time * speed.y),
		                          Mathf.Sin (dotABCD.z + time * speed.z),
		                          Mathf.Sin (dotABCD.w + time * speed.w));

		Vector4 cos = new Vector4(Mathf.Cos(dotABCD.x + time * speed.x),
		                          Mathf.Cos(dotABCD.y + time * speed.y),
		                          Mathf.Cos(dotABCD.z + time * speed.z),
		                          Mathf.Cos(dotABCD.w + time * speed.w));

		coord = new Vector3(Vector4.Dot(cos,new Vector4(AB.x,AB.z,CD.x,CD.z)),
		                    Vector4.Dot (sin, amplitude),
		                    Vector4.Dot(cos,new Vector4(AB.y,AB.w,CD.y,CD.w)));

		return coord;
	}

	public float GetOceanHeightTest(Vector2 coord)
	{
		//first, transform the coord by getting rid of the cos sums in the gerstner displacement
		Vector4 AB = new Vector4 (steepness.x * amplitude.x * dirAB.x,
		                          steepness.x * amplitude.x * dirAB.y,
		                          steepness.y * amplitude.y * dirAB.z,
		                          steepness.y * amplitude.y * dirAB.w);
		
		Vector4 CD = new Vector4 (steepness.z * amplitude.z * dirCD.x,
		                          steepness.z * amplitude.z * dirCD.y,
		                          steepness.w * amplitude.w * dirCD.z,
		                          steepness.w * amplitude.w * dirCD.w);




		
		Vector4 dots = new Vector4(Vector2.Dot(new Vector2(dirAB.x,dirAB.y),coord),
		                           Vector2.Dot(new Vector2(dirAB.z,dirAB.w),coord),
		                           Vector2.Dot(new Vector2(dirCD.x,dirCD.y),coord),
		                           Vector2.Dot(new Vector2(dirCD.z,dirCD.w),coord));
		
		Vector4 dotABCD = new Vector4(dots.x * frequency.x,
		                              dots.y * frequency.y,
		                              dots.z * frequency.z,
		                              dots.w * frequency.w);
		
		float time = Time.time;

		Vector4 cos = new Vector4(Mathf.Cos(dotABCD.x + time * speed.x),
		                          Mathf.Cos(dotABCD.y + time * speed.y),
		                          Mathf.Cos(dotABCD.z + time * speed.z),
		                          Mathf.Cos(dotABCD.w + time * speed.w));


		coord.x -= cos.x * AB.x + cos.y * AB.z + cos.z * CD.x + cos.w * CD.z;
		coord.y -= cos.x * AB.y + cos.y * AB.w + cos.z * CD.y + cos.w * CD.w;

		//now recalculate height with the newly obtained coords
		Vector4 dots2 = new Vector4(Vector2.Dot(new Vector2(dirAB.x,dirAB.y),coord),
		                           Vector2.Dot(new Vector2(dirAB.z,dirAB.w),coord),
		                           Vector2.Dot(new Vector2(dirCD.x,dirCD.y),coord),
		                           Vector2.Dot(new Vector2(dirCD.z,dirCD.w),coord));


		Vector4 dotABCD2 = new Vector4(dots2.x * frequency.x,
		                              dots2.y * frequency.y,
		                              dots2.z * frequency.z,
		                              dots2.w * frequency.w);

		
		Vector4 sin = new Vector4(Mathf.Sin(dotABCD2.x + time * speed.x),
		                          Mathf.Sin(dotABCD2.y + time * speed.y),
		                          Mathf.Sin(dotABCD2.z + time * speed.z),
		                          Mathf.Sin(dotABCD2.w + time * speed.w));



		
		return Vector4.Dot(sin,amplitude);
	}




	// Use this for initialization
	void Start () 
	{
		m = GetComponent<MeshFilter> ().mesh;
		verts = m.vertices;
	}
	
	// Update 
	void Update () 
	{
		Vector3[] newVerts = new Vector3[verts.Length];

		for (int i=0; i<verts.Length; ++i) 
		{

			newVerts[i] = GetGerstnerDisplacement(verts[i] + transform.position) + verts[i];
			//verts[i].y = GetOceanHeightTest(new Vector2(verts[i].x + transform.position.x,verts[i].z + transform.position.z));
		}

		m.vertices = newVerts;

	}
}
