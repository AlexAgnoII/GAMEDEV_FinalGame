using UnityEngine;
using System.Collections;

/*
 * Holder for event names
 * Created By: NeilDG
 */ 
public class EventNames {
	public const string ON_UPDATE_SCORE = "ON_UPDATE_SCORE";
	public const string ON_CORRECT_MATCH = "ON_CORRECT_MATCH";
	public const string ON_WRONG_MATCH = "ON_WRONG_MATCH";
	public const string ON_INCREASE_LEVEL = "ON_INCREASE_LEVEL";
	public const string ON_PICTURE_CLICKED = "ON_PICTURE_CLICKED";

    public class FinalGameEvents
    {
        public const string ON_PLAYER_MOVE_FORWARD = "ON_PLAYER_MOVE_FORWARD";
        public const string PARAM_PLAYER_X_POSITION = "PARAM_PLAYER_X_POSITION";
        public const string ON_CHANGE_DIRECTIONAL_LIGHT = "ON_CHANGE_DIRECTIONAL_LIGHT";
        public const string ON_UPDATE_SCORE = "ON_UPDATE_SCORE";
        public const string PLAYER_SCORE = "PLAYER_SCORE";
        public const string ON_DIFFICULTY_CHANGE = "ON_DIFFICULTY_CHANGE"; //for gamescript > terrainGenerator to make the game harder.
        public const string ON_SEND_CURRENT_STEPS = "ON_SEND_CURRENT_STEPS"; //for playerscript > gamescript telling him to change difficulty



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

	public class ARBluetoothEvents {
		public const string ON_START_BLUETOOTH_DEMO = "ON_START_BLUETOOTH_DEMO";
		public const string ON_RECEIVED_MESSAGE = "ON_RECEIVED_MESSAGE";
	}
	public class ARPhysicsEvents {
		public const string ON_FIRST_TARGET_SCAN = "ON_FIRST_TARGET_SCAN";
		public const string ON_FINAL_TARGET_SCAN = "ON_FINAL_TARGET_SCAN";
	}
	public class ExtendTrackEvents {
		public const string ON_TARGET_SCAN = "ON_TARGET_SCAN";
		public const string ON_TARGET_HIDE = "ON_TARGET_HIDE";
		public const string ON_SHOW_ALL = "ON_SHOW_ALL";
		public const string ON_HIDE_ALL = "ON_HIDE_ALL";
		public const string ON_DELETE_ALL = "ON_DELETE_ALL";
	}
	public class X01_Events {
		public const string ON_FIRST_SCAN = "ON_FIRST_SCAN";
		public const string ON_FINAL_SCAN = "ON_FINAL_SCAN";
		public const string EXTENDED_TRACK_ON_SCAN = "EXTENDED_TRACK_ON_SCAN";
		public const string EXTENDED_TRACK_REMOVED = "EXTENDED_TRACK_REMOVED";
	}
	public class X22_Events {
		public const string ON_FIRST_SCAN = "ON_FIRST_SCAN";
		public const string ON_FINAL_SCAN = "ON_FINAL_SCAN";
		public const string EXTENDED_TRACK_ON_SCAN = "EXTENDED_TRACK_ON_SCAN";
		public const string EXTENDED_TRACK_REMOVED = "EXTENDED_TRACK_REMOVED";
	}
	public class S18_Events {
		public const string ON_FIRST_SCAN = "FIRST_TARGET_SCAN";
		public const string ON_FINAL_SCAN = "ON_FINAL_SCAN";
	}
}







