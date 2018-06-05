using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningOnEditorTest {
    public static bool IsRunningOnEditor()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.OSXEditor)
            return true;
        else
            return false;

    }
}
