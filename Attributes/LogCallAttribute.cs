using System;

namespace attr.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class LogCallAttribute : Attribute
    {
        public string LogDecoration { get; set; }

        public LogCallAttribute(string logDecoration = "")
        {
            LogDecoration = logDecoration;
        }
    }
}