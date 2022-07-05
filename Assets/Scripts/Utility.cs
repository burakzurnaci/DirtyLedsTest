using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
/*
 * Complete the functions below.  
 * For sure, they don't belong in the same class. This is just for the test so ignore that.
 */
public static class Utility
{
	public static GameObject[] GetObjectsWithName(string name)
	{
		/*
		 * 
		 *	Return all objects in the scene with the specified name. Don't think about performance, do it in as few lines as you can. 
		 * 
		 */
		var scene = SceneManager.GetActiveScene();
		var gameObjects = scene.GetRootGameObjects();
		return gameObjects.Where(obj => obj.name.Equals(name)).ToArray();
	}
	public static bool CheckCollision(Ray ray, float maxDistance, int layer)
	{
		/*
		 * 
		 *	Perform a raycast using the ray provided, only to objects of the specified 'layer' within 'maxDistance' and return if something is hit. 
		 * 
		 */
		
		//Actually we dont need hit var because we dont need to know what is ray hit. 
		RaycastHit hit;
		LayerMask layerMask =  (1 << layer); 
		return Physics.Raycast (ray, out hit, maxDistance, layerMask);
	}
	public static List<Vector2> GeneratePoints(int size, Vector2 sampleRegionSize, int numSamplesBeforeRejection)
	{
		/*
	 * Generate 'size' number of random points, making sure they are distributed as evenly as possible (Trying to achieve maximum distance between every neighbor).
	 * Boundary corners are (0, 0) and (1, 1). (Point (1.2, 0.45) is not valid because it's outside the boundaries. )
	 * Is there a known algorithm that achieves this?
	 */
		// We can use PoissonDiscSampling algorithm for random points distributed as evenly as possible
		// find point radius from sampleRegionSize and numberSize.
		//You can test it using TestGenerate script with 0.01f displayRadius, and generate 'size' number of random points.
		var radius = Mathf.Sqrt(sampleRegionSize.x * sampleRegionSize.y / size  * Mathf.PI)/2;
		var cellsize = radius / Mathf.Sqrt(2);
		int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellsize), Mathf.CeilToInt(sampleRegionSize.y / cellsize)];
		var points = new List<Vector2>();
		var spawnPoints = new List<Vector2>();
		spawnPoints.Add(sampleRegionSize / 2);
		while (spawnPoints.Count > 0)
		{
			var spawnIndex = Random.Range(0, spawnPoints.Count);
			var spawnCentre = spawnPoints[spawnIndex];
			var candidateAccepted = false;
			//Test numSamplesBeforeRejection times until accepted candidate points are valid. 
			for (var i = 0; i < numSamplesBeforeRejection; i++)
			{
				var angle = Random.value * Mathf.PI * 2;
				var dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
				var candidate = spawnCentre + dir * Random.Range(radius, 2 * radius);
				if (!IsValid(candidate, sampleRegionSize, cellsize, radius, points, grid)) continue;
				points.Add(candidate);
				spawnPoints.Add(candidate);
				grid[(int) (candidate.x / cellsize), (int) (candidate.y / cellsize)] = points.Count;
				candidateAccepted = true;
				break;
			}
			if (!candidateAccepted)
			{
				spawnPoints.RemoveAt(spawnIndex);
			}
		}
		return points;
	}
	//Looking for accepted candidate points which are not in accepted points' radius area.
	static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points,
			int[,] grid)
		{
			if (!(candidate.x >= 0) || !(candidate.x < sampleRegionSize.x) || !(candidate.y >= 0) ||
			    !(candidate.y < sampleRegionSize.y)) return false;
			var cellX = (int) (candidate.x / cellSize);
			var cellY = (int) (candidate.y / cellSize);
			var searchStartX = Mathf.Max(0,cellX - 2);
			var searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
			var searchStartY = Mathf.Max(0,cellY - 2);
			var searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

			for (var x = searchStartX; x <= searchEndX; x++)
			{
				for (var y = searchStartY; y <= searchEndY; y++)
				{
					var pointIndex = grid[x,y] - 1;
					if (pointIndex == -1) continue;
					var sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
					if (sqrDst < radius*radius)
					{
						return false;
					}
				}
			}
			return true;
		}
	
	public static Texture2D GenerateTexture(int width, int height, Color color)
	{
		/*
		 * Create a Texture2D object of specified 'width' and 'height', fill it with 'color' and return it. Do it as performant as possible.
		 */
		var texture = new Texture2D (width, height);
		var pixels = texture.GetPixels();
		for(var i = 0; i < pixels.Length; i++)
		{
			pixels[i] = color;
		}
		texture.SetPixels(pixels);
		texture.Apply();
		return texture;
	}
}