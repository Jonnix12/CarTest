using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    
    private List<CheckPoint> _checkPoints;
    private CarCheckPointHelper[] _cars;
    private int _id;

    private const string CHECK_POINT_TAG = "CheckPoint";
    private const int CHECK_POINT_LAYER = 7;
    private const string WALL_TAG = "Wall";
    private const int WALL_LAYER = 6;


    public void InitSystem(CarCheckPointHelper[] cars)
    {
        _checkPoints = new List<CheckPoint>();

        _cars = cars;
        
        foreach (Transform note in transform)
        {
           
            foreach (Transform edgeObject in note)
            {
                if (edgeObject.TryGetComponent<Wall>(out Wall wall))
                {
                    wall.gameObject.tag = WALL_TAG;
                    wall.gameObject.layer = WALL_LAYER;
                }
                
                foreach (Transform checkPoint in edgeObject)
                {
                    if (checkPoint.TryGetComponent<CheckPoint>(out CheckPoint temp))
                    {
                        temp.OnCheckPointTrigger += OnCheckPointTrigger;
                        temp.SetId(_id);
                        temp.gameObject.tag = CHECK_POINT_TAG;
                        temp.gameObject.layer = CHECK_POINT_LAYER;
                        _id++;
                        _checkPoints.Add(temp);
                        //Debug.Log("Add " + temp.name + " From " + transform.name);
                    }
                }
            }
        }
        
        for (int i = 0; i < _cars.Length; i++)
        {
            _cars[i].SetCheckPointCount(_checkPoints.Count);
            _cars[i].SetNextCheckPoint(_checkPoints[0]);
        }
    }
    
    private void OnCheckPointTrigger(CarCheckPointHelper car,CheckPoint checkPointId)
    {
        if (car.NextCheckPoint == checkPointId)
        {
            int nextCheckPointIndex;
            
            if (_checkPoints.IndexOf(checkPointId) + 1 >= _checkPoints.Count)
            {
                nextCheckPointIndex = 0;
            }
            else
            {
                nextCheckPointIndex = _checkPoints.IndexOf(checkPointId) + 1;
            }
            
            car.SetNextCheckPoint(_checkPoints[nextCheckPointIndex]);
            //Debug.Log(car.gameObject.name + " Move to CheckPoint " + checkPointId.name);
        }
        else
        {
            car.PassInCorrectCheckPoint();
            //Debug.LogError("Wrong CheckPoint" + checkPointId.ID);
        }
        
    }

    private void OnDisable()
    {
        foreach (var vaCheckPoint in _checkPoints)
        {
            vaCheckPoint.OnCheckPointTrigger -= OnCheckPointTrigger;
        }
    }

}