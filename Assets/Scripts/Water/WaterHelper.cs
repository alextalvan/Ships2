using UnityEngine;
using System.Collections;

public class WaterHelper : MonoBehaviour {

	//shared material and statics will be sued for performance reasons
	private Material _mat;
    private static float _delta = 0.0f;
    private static Vector3 waveScale;

	//this is set by the time synchronizer in order to sync the water shader time with the server time
	public static float Delta
	{
        get { return _delta; }
        set { _delta = value; }
	}


    public static Vector3 WaveScale
    {
        get { return waveScale; }
        set { waveScale = value; }
    }

    // Use this for initialization
    void Start () {
		waveScale = new Vector3 ();
        _mat = GetComponent<Renderer>().sharedMaterial;
		steepness = _mat.GetVector ("_GSteepness");
		amplitude = _mat.GetVector ("_GAmplitude");
		frequency = _mat.GetVector ("_GFrequency");
		speed = _mat.GetVector ("_GSpeed");
		dirAB = _mat.GetVector ("_GDirectionAB");
		dirCD = _mat.GetVector ("_GDirectionCD");
        //waveScale = _mat.GetVector("_WaveScale");

    }
	
	// Update 
	void Update ()
	{
		_mat.SetFloat ("_CustomTime", Time.time + _delta);
		_mat.SetFloat ("_Panner", (Time.time + _delta) * 0.1f);
        //_mat.SetVector("_WaveScale", new Vector4 (waveScale.x, waveScale.y, waveScale.z, 0f));
	}

	//gerstner wave height computation

	static Vector4 steepness, amplitude, frequency, speed, dirAB, dirCD;

	//very slow but accurate sampling of the ocean height used by the shader
	public static float GetOceanHeightAt(Vector2 coord)
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
		
		float time = Time.time + _delta;
		
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









}
