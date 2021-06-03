using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsMove : MonoBehaviour
{
    private float MoveSpeed ;
    private List<BirdsMove> totalBoids = new List<BirdsMove>();
    public BirdsManager birdsManage;
    void Start()
    {
        MoveSpeed = Random.Range(birdsManage.MovementSpeed,birdsManage.MovementSpeed);
    }
    void Update()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        Applyforce();
    }
    public void Applyforce()
    {
        totalBoids = birdsManage.allPrefabs;
        float maxdistance = 50f;
        Vector3 GroupCenter = Vector3.zero;
        Vector3 Avoid = Vector3.zero;
        int GroupSize = 0;
        float GlobalSpeed = 0.01f;
        foreach (var Current in totalBoids)
        {
            if (Current != this.gameObject)
            {
                float distance = Vector3.Distance(Current.transform.position, this.transform.position);
                //if ((distance > 0) & (distance < maxdistance))
                if (distance <= maxdistance)
                {
                    GroupCenter += Current.transform.position;
                    GroupSize++;
                    if (distance < 1.0f)
                    {
                        Avoid += (this.transform.position - Current.transform.position);
                    }
                    BirdsMove flockSpeed = Current.GetComponent<BirdsMove>();
                    GlobalSpeed = GlobalSpeed * flockSpeed.MoveSpeed;
                }
            }
        }
        if (GroupSize > 0)
        {
            Vector3 Goalpos = birdsManage.GoalPos - this.transform.position;
            GroupCenter = GroupCenter / GroupSize + Goalpos;
            Vector3 pos = GroupCenter + Avoid;
            Vector3 direction = pos - this.transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion rotPos = Quaternion.LookRotation(direction);
                Quaternion rot = Quaternion.Slerp(this.transform.rotation, rotPos, 0.2f * Time.deltaTime);
                transform.rotation = rot;
            }
        }
    }
}
