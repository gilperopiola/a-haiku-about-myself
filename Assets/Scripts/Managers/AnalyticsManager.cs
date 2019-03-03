using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class AnalyticsManager {
    public static void SendEvent(string name, Dictionary<string, object> dictionary) {
        Analytics.CustomEvent(name, dictionary);
    }
}
