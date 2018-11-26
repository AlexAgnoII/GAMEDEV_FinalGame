using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTags : MonoBehaviour {

    public const string PLAYER = "PLAYER";


	public class TerrainGroup
    {
        public const string DEFAULT_ROAD = "ROAD_DEFAULT";
        public const string NORTH_ROAD = "NORTH_ROAD";
        public const string SOUTH_ROAD = "SOUTH_ROAD";
        public const string RIGHT_FORCE = "RIGHT_FORCE";
        public const string LEFT_FORCE = "LEFT_FORCE";
    }

    public class FixedObstacles
    {

    }

    public class MovingObstacles
    {
        public const string VEHICLE = "VEHICLE";
    }
}
