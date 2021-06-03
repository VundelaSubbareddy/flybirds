using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsManager : MonoBehaviour
{
    public GameObject prefab;
    public List<BirdsMove> allPrefabs = new List<BirdsMove>();
   // public float Seperationdis = 2f;
    public Vector3 SpawnPos = new Vector3(2,0,2);
    public Vector3 GoalPos;
    public int NumberofBirds = 10;
    public float MovementSpeed = 1f;
    private void Start()
    {
        for (int j = 0; j < NumberofBirds; j ++)
        {
            var random = Random.insideUnitSphere;
            Vector3 pos = new Vector3(random.x * SpawnPos.x , random.y *SpawnPos.y ,random.z * SpawnPos.z);
            pos = pos + random;
            var g = (GameObject)Instantiate(prefab, pos , Quaternion.identity);
            BirdsMove b = g.GetComponent<BirdsMove>();
            allPrefabs.Add(b);
            b.birdsManage = this;
        }
        GoalPos = this.transform.position;
    }
    private void Update()
    {
        var random = Random.insideUnitSphere;
        Vector3 pos = new Vector3(random.x * SpawnPos.x, random.y * SpawnPos.y, random.z * SpawnPos.z);
        GoalPos = pos;
    }
}
