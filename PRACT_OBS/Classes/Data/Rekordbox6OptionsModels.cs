using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PRACT_OBS.Classes.Data
{
    public class Option
    {
        [JsonPropertyName("db-path")]
        public string DbPath { get; set; }
        [JsonPropertyName("dp")]
        public string Dp { get; set; }
        public string Port { get; set; }
        [JsonPropertyName("analysis-data-root-path")]
        public string AnalysisDataRootPath { get; set; }
        [JsonPropertyName("settings-root-path")]
        public string SettingsRootPath { get; set; }
        [JsonPropertyName("lang-path")]
        public string LangPath { get; set; }
    }
    public class Connectivity
    {
        public string url { get; set; }
    }
    public class ClockServer
    {
        public string[] urls { get; set; }
    }
    public class Defaults
    {
        public string mode { get; set; }
        public Connectivity connectivity { get; set; }
        [JsonPropertyName("clock_server")]
        public ClockServer ClockServer { get; set; }
    }

    public class Rekordbox6Options
    {
        public Option options { get; set; }
        public Defaults defaults { get; set; }
        
    }

}
