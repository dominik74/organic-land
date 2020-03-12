using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager {

    public delegate void CommandHandler(string val, string val2);

    private Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();

    public ConsoleView consoleView;

    class CommandRegistration
    {
        public string command;
        public CommandHandler handler;
        public string help;

        public CommandRegistration(string command, CommandHandler handler, string help)
        {
            this.command = command;
            this.handler = handler;
            this.help = help;
        }
    }


    public CommandManager()
    {
        RegisterCommand("help", Help, "Lists all available commands.");
        RegisterCommand("restart", Restart, "Restarts the level.");
    }

	void RegisterCommand(string command, CommandHandler handler, string help)
    {
        commands.Add(command, new CommandRegistration(command, handler, help));
    }

    public void RunCommand(string command)
    {
        string value = "";
        string value2 = "";

        // Cmd split
        if (command.Contains(" "))
        {
            string[] cmdWords = command.Split(' ');
            if(cmdWords.Length > 3)
            {
                AppendLogLine(string.Format("<color=red>Unable to run command {0}, unsupported amount of parameters.</color>", command));
                return;
            }

            command = cmdWords[0];
            value = cmdWords[1];
            if (cmdWords.Length > 2)
                value2 = cmdWords[2];
        }

        CommandRegistration reg = null;
        if(!commands.TryGetValue(command, out reg))
        {
            AppendLogLine(string.Format("<color=red>Unable to run command {0}, type 'help' for list.</color>", command));
        }
        else
        {
            if (reg.handler == null)
                AppendLogLine(string.Format("<color=red>Unable to run command {0}, handler was null.</color>", command));
            else
                reg.handler(value, value2);
        }
        UIController.instance.SetScreen("pnlConsole", false);
    }

    void AppendLogLine(string text)
    {
        Debug.Log(text);
        consoleView.AppendLogLine(text);
    }

    #region Command Handlers
    void Help(string val, string val2)
    {
        foreach (KeyValuePair<string, CommandRegistration> item in commands)
        {
            AppendLogLine(string.Format("{0} - {1}", item.Key, item.Value.help));
        }
    }

    void Restart(string val, string val2)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    #endregion

}
