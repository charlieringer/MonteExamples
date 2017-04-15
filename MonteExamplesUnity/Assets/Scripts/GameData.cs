using UnityEngine;

public class GameData : MonoBehaviour {
	static public int playerIndex = 0;
	static public int selectedAgent = 2;


	static public string[] modelFiles = new string[3];
	static public string[] settingsFiles = {"Monte/DefaultSettings.xml","Monte/DefaultSettings.xml","Monte/DefaultSettings.xml"};

	//This just stores the player index so it can be passed from the front end to the game
	public void setPlayerIndex(int indx)
	{
		playerIndex = indx;
	}

	public void setAgentIndex(int indx)
	{
		selectedAgent = indx;
	}

	public void setTTTModelPath(string path)
	{
		modelFiles[0] = path;
	}

	public void setOCModelPath(string path)
	{
		modelFiles[1] = path;
	}

	public void setHexModelPath(string path)
	{
		modelFiles[2] = path;
	}

	public void setTTTSettingsPath(string path)
	{
		settingsFiles[0] = path;
	}

	public void setOCSettingsPath(string path)
	{
		settingsFiles[1] = path;
	}

	public void setHexSettingsPath(string path)
	{
		settingsFiles[2] = path;
	}
}