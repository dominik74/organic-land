using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleView : MonoBehaviour {

    public InputField inputField;
    public Text outputText;

    public static bool isActive;

    private List<string> logHistory = new List<string>();
    private int historyLimit = 10;
    private int currentLogHistoryPos;

    private CommandManager commandManager = new CommandManager();

    public static ConsoleView instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        commandManager.consoleView = this;
        outputText.text = "";
    }

    private void OnEnable()
    {
        inputField.ActivateInputField();
        isActive = true;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    public void SendCommand()
    {
        if(inputField.text != string.Empty)
        {
            commandManager.RunCommand(inputField.text);
            AddToHistory(inputField.text);
            inputField.text = string.Empty;
        }
        inputField.ActivateInputField();
    }

    public void AppendLogLine(string text)
    {
        if (outputText.text != string.Empty)
            outputText.text += string.Format("\n{0}", text);
        else
            outputText.text = text;
    }

    public void GoToPreviousCommand()
    {
        if(logHistory.Count - (currentLogHistoryPos + 1) >= 0)
        {
            currentLogHistoryPos++;
            inputField.text = logHistory[logHistory.Count - currentLogHistoryPos];
        }
    }

    void AddToHistory(string item)
    {
        if(logHistory.Count < historyLimit)
            logHistory.Add(item); // TODO: Finish
        currentLogHistoryPos = 0;
    }

}
