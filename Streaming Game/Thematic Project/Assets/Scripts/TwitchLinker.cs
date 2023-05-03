using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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
    public UnityEvent<string> Spawn;

    TcpClient twitch;
    StreamReader reader;
    StreamWriter writer;

    const string URL = "irc.chat.twitch.tv";
    const int PORT = 6667;

    string user = "BrainDeadCrashTester";
    string oAuth = "oauth:o1ocblxdj31vmm7iqc38i09oe8rtnt";

    [SerializeField] InputField inputField;

    float PingCounter = 0;

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
        PingCounter += Time.deltaTime;
        if (PingCounter > 60)
        {
            writer.WriteLine("PING " + URL);
            writer.Flush();
            PingCounter = 0;
        }
        if (!twitch.Connected)
            ConnectToTwitch();
        if (twitch.Available > 0) //if tcp has new data
        {
            string message = reader.ReadLine(); //read the message
            if (message.Contains("JOIN"))
            {
                DontDestroyOnLoad(this.gameObject);
                SceneManager.LoadScene("Game");
            }
            if (message.Contains("PRIVMSG"))
            {
                int splitPoint = message.IndexOf("!");
                string chatter = message.Substring(1, splitPoint - 1);

                splitPoint = message.IndexOf(":", 1);
                string pureChatMessage = message.Substring(splitPoint + 1);

                if (pureChatMessage.Substring(0, 7) == "balloon")
                    Spawn?.Invoke(chatter);
            }

            Debug.Log(message);
        }
    }
}

