#nullable enable
using UnityEngine;

namespace TeamZero.ApplicationProfile.Building
{
    public class UnityLogReport : IBuildReport
    {
        public static UnityLogReport Create() => new();
        
        private UnityLogReport()
        {
        }
        
        public void AppendLine(string value) => Debug.Log(value);
    }
}
