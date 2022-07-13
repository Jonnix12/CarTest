#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class RaceManager : MonoBehaviour
{
    [SerializeField] private CheckPointSystem _checkPointSystem;
    [SerializeField] private TimeTrackSystem _timeTrackSystem;
    [SerializeField] private PositionSystem _positionSystem;

    [SerializeField] private int _numberOfLaps;

    private string[,] _endList;
    
    private List<Idisable> _carInputs;
    private List<CarRaceManager> _cars;

    public string[,] EndList
    {
        get { return _endList; }
    }
    
    public IReadOnlyList<CarRaceManager> Cars
    {
        get { return _cars; }
    }

    private void Awake()
    {
        CarRaceManager[] tempCars;
        tempCars = FindObjectsOfType<CarRaceManager>();

        _cars = new List<CarRaceManager>();

        for (int i = 0; i < tempCars.Length; i++)
        {
            _cars.Add(tempCars[i]);
        }

        List<CarCheckPointHelper> tempCarCheckPointHelpers = new List<CarCheckPointHelper>();

        List<TimeTrack> tempCarTrackTime = new List<TimeTrack>();

        _carInputs = new List<Idisable>();

        for (int i = 0; i < _cars.Count; i++)
        {
            if (_cars[i].gameObject.TryGetComponent(out CarCheckPointHelper tempCheckPointHelper))
            {
                tempCarCheckPointHelpers.Add(tempCheckPointHelper);
            }

            if (_cars[i].gameObject.TryGetComponent(out TimeTrack tempTimeTrack))
            {
                tempCarTrackTime.Add(tempTimeTrack);
            }

            if (_cars[i].gameObject.TryGetComponent(out Idisable carInput))
            {
                _carInputs.Add(carInput);
            }
        }

        CarCheckPointHelper[] tempArrayCheckPointHelpers = tempCarCheckPointHelpers.ToArray();
        TimeTrack[] tempArrayTimeTracks = tempCarTrackTime.ToArray();

        _checkPointSystem.InitSystem(tempArrayCheckPointHelpers, _numberOfLaps);
        _positionSystem.InitSystem(tempArrayCheckPointHelpers);
        _timeTrackSystem.InitSystem(tempArrayTimeTracks);
        StartRace();
    }

    public void EndRace()
    {
        
    }

    private void StartRace()
    {
        _timeTrackSystem.StartRace(_carInputs.ToArray());
    }
}