using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System.IO;

/// <summary> Bots account data
/// Username: BrainDeadCrashTester
/// Password: TwitchGameBeCrazy
/// OAuth: oauth:o1ocblxdj31vmm7iqc38i09oe8rtnt
/// </summary>

public class TwitchLinker : MonoBehaviour
{
    TcpClient twitch;
    StreamReader reader;
    StreamWriter writer;

    const string URL = "irc.chat.twitch.tv";
    const int PORT = 6667;

    string user = "BrainDeadCrashTester";
    string oAuth = "oauth:o1ocblxdj31vmm7iqc38i09oe8rtnt";

    [SerializeField] InputField inputField;

    private void ConnectToTwitch()
    {
        twitch = new TcpClient(URL, PORT);
        reader = new StreamReader(twitch.GetStream());
        writer = new StreamWriter(twitch.GetStream());
    }

    private void Awake()
    {
        ConnectToTwitch();
    }

    public void JoinStream()
    {
        //get input
        string channel = inputField.text;
        //use bot account to join twitch
        writer.WriteLine("PASS " + oAuth);
        writer.WriteLine("NICK " + user.ToLower());
        writer.WriteLineAsync("JOIN #" + channel.ToLower());
        writer.Flush();
    }


    // Update is called once per frame
    void Update()
    {
        if (twitch.Available > 0) //if tcp has new data
        {
            string message = reader.ReadLine(); //read the message
            if (message.Contains("JOIN"))
            {
                DontDestroyOnLoad(this.gameObject);
                SceneManager.LoadScene("Game");
            }
            Debug.Log(message);


        }
    }
}

