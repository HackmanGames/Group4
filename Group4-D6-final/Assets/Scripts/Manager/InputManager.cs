using UnityEngine;

public class InputManager : Singleton<InputManager> {
    int inputAvalible_index = 0;
    bool inputAvalible {
        get{
            return inputAvalible_index <= 0;
        }
    }

    public bool IsEnable(){
        return inputAvalible;
    }

    public void Disable(){
        inputAvalible_index ++;
    }

    public void Enable(){
        inputAvalible_index --;
    }

    public bool GetKeyDown(KeyCode keyCode)
    {
        return inputAvalible && Input.GetKeyDown(keyCode);
    }
    
    public bool GetKeyDown(string name)
    {
        return inputAvalible && Input.GetKeyDown(name);
    }

    public bool GetKeyUp(KeyCode keyCode)
    {
        return inputAvalible && Input.GetKeyUp(keyCode);
    }

    public bool GetKeyUp(string name)
    {
        return inputAvalible && Input.GetKeyUp(name);
    }

    public bool GetKey(KeyCode keyCode)
    {
        return inputAvalible && Input.GetKey(keyCode);
    }

    public bool GetKey(string name)
    {
        return inputAvalible && Input.GetKey(name);
    }
}