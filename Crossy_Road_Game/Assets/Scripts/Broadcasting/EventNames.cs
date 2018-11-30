using UnityEngine;
using System.Collections;

/*
 * Holder for event names
 * Created By: NeilDG
 */ 
public class EventNames {

    public const string ON_PAUSE_NAME = "ON_PAUSE";
    public const string ON_RESUME_NAME = "ON_RESUME";
    public const string ON_REPLAY_NAME = "ON_REPLAY";
    public const string ON_TIMER_DONE = "ON_TIMER";

    public class FinalGameEvents
    {
        public const string ON_PLAYER_MOVE_FORWARD = "ON_PLAYER_MOVE_FORWARD";
        public const string ON_CHANGE_DIRECTIONAL_LIGHT = "ON_CHANGE_DIRECTIONAL_LIGHT";
        public const string ON_UPDATE_SCORE = "ON_UPDATE_SCORE";
        public const string ON_SEND_CURRENT_STEPS = "ON_SEND_CURRENT_STEPS";
        public const string ON_PLAYER_EXPLOD_FROM_CAR = "ON_PLAYER_EXPLOD_FROM_CAR";
        public const string ON_PLAYER_SPLASH_FROM_WATER = "ON_PLAYER_SPLASH_FROM_WATER";
        public const string ON_GAME_END = "ON_GAME_END";

        public const string PARAM_PLAYER_SCORE = "PARAM_PLAYER_SCORE";
        public const string PARAM_PLAYER_X_POSITION = "PARAM_PLAYER_X_POSITION";

    }


    public class FinalGameAudioEvents
    {
        public const string ON_GRASS_SOUND = "ON_GRASS_SOUND";
        public const string ON_HOPPING_SOUND = "ON_HOPPING_SOUND";
        public const string ON_ROAD_SOUND = "ON_ROAD_SOUND";
        public const string ON_LOG_SOUND = "ON_LOG_SOUND";
        public const string ON_DEATH_SOUND = "ON_DEATH_SOUND";
        public const string ON_DAY_PHASE = "ON_DAY_PHASE";
        public const string ON_SPLASH_SOUND = "ON_SPLASH_SOUND";
        public const string ON_CRASH_SOUND = "ON_CRASH_SOUND";
    }

}







