using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTags : MonoBehaviour {

    public const string PLAYER = "PLAYER";


	public class TerrainGroup
    {
        public const string GRASS = "GRASS";

        public const string DEFAULT_ROAD = "ROAD_DEFAULT";
        public const string NORTH_BOUND_ROAD = "NORTH_BOUND_ROAD";
        public const string SOUTH_BOUND_ROAD = "SOUTH_BOUND_ROAD";

        public const string NORTH_BOUND_WATER = "NORTH_BOUND_WATER";
        public const string SOUTH_BOUND_WATER = "SOUTH_BOUND_WATER";


        public const string NORTH_SPAWN = "NORTH_SPAWN";
        public const string SOUTH_SPAWN = "SOUTH_SPAWN";
    }

    public class FixedObstacles
    {
        public const string FIXED_OBSTACLES = "FIXED_OBSTACLES";
    }

    public class MovingObstacles
    {
        public const string VEHICLE = "VEHICLE";
        public const string LOG = "LOG";
    }
}
