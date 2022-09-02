using UnityEngine;

namespace TeamZero.AppBuildSystem.Editor
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
