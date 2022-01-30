using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInfoHolder
{

    public static bool isClient = true;
    public static string serverAddr = "";

    public static Action onClientConnect;
}
