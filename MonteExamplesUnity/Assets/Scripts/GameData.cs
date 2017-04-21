using UnityEngine;

public class GameData : MonoBehaviour {
	//Defaults for the player indx and selected AI agent
	static public int playerIndex = 0;
	static public int selectedAgent = 2;

	//File paths for the model and settings.
	static public string[] modelFiles = {"Assets/Monte/TicTacToe_Example.model","Assets/Monte/OrderChaos_Example.model","Assets/Monte/Hex_Example.model"};
	static public string[] settingsFiles = {"Assets/Monte/TicTacToeSettings.xml","Assets/Monte/OrderAndChaosSettings.xml","Assets/Monte/HexSettings.xml"};

	//This just stores the player index so it can be passed from the front end to the game
	public void setPlayerIndex(int indx) { playerIndex = indx; }

	//Set which agent the game is using
	public void setAgentIndex(int indx) { selectedAgent = indx; }

	//Set the model file path for TicTakToe
	public void setTTTModelPath(string path) { modelFiles[0] = path; }

	//Set the model file path for Order and Chaos
	public void setOCModelPath(string path) { modelFiles[1] = path; }

	//Set the model file path for Hex
	public void setHexModelPath(string path) { modelFiles[2] = path; }

	//Set the settings path for TicTacToe
	public void setTTTSettingsPath(string path) { settingsFiles[0] = path; }

	//Set the settings path for Order and Chaos
	public void setOCSettingsPath(string path) { settingsFiles[1] = path; }

	//Set the settings path for Hex
	public void setHexSettingsPath(string path) { settingsFiles[2] = path; }
}