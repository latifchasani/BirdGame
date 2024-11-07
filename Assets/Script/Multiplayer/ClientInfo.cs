using UnityEngine;

public static class ClientInfo
{
    public static string Username
    {
        get => PlayerPrefs.GetString("C_Username", string.Empty);
        set => PlayerPrefs.SetString("C_Username", value);
    }

    public static int PlayerId
    {
        get => PlayerPrefs.GetInt("C_PlayerId", 0);
        set => PlayerPrefs.SetInt("C_PlayerId", value);
    }

    public static string LobbyName
    {
        get => PlayerPrefs.GetString("C_LastLobbyName", "");
        set => PlayerPrefs.SetString("C_LastLobbyName", value);
    }
}