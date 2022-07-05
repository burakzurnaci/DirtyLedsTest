using Sirenix.OdinInspector;
using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
/*
 *
 * JobTest scene runs very slow because of the repeated dummy math operation below. Implement the for loop below, using parallelized Unity jobs and Burst compiler to gain performance
 * If the 'count' is too large for your machine to handle, you can decrease it.
 * 
 */
public class JobTest : MonoBehaviour
{
	[SerializeField]
	private bool mUseJob = false;
	[ShowInInspector,Sirenix.OdinInspector.ReadOnly] 
	private int _count = 1000000;
	private float[] _values;
	
	void Start()  
	{
		_values = new float[_count];
	}
	
	void Update()
	{

		if (mUseJob)
		{
			var values = new NativeArray<float>(_count,Allocator.TempJob);
			var toughJob = new ToughJob()
			{
				Values = values
			};
			var jobHandle = toughJob.Schedule(values.Length, 100);
			jobHandle.Complete();

			values.Dispose();
		}
		else
		{
			for (int i = 0; i < _values.Length; i++)
			{
				_values[i] = Mathf.Sqrt(Mathf.Pow(_values[i] + 1.75f, 2.5f + i)) * 5 + 2f;
			}
		}

	}

}
[BurstCompile]
public struct ToughJob : IJobParallelFor
{
	public NativeArray<float> Values;
	public void Execute(int index)
	{
		Values[index] = Mathf.Sqrt(Mathf.Pow(Values[index] + 1.75f, 2.5f +index)) * 5 + 2f;
	}
}