using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreenUI
{
    void Open();
    void Open(object data);
    void Close();
    bool GetActiveState();
}
