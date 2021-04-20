namespace AmpShell.Games
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary> Represents a DOSBox Config File. </summary>
    public class DOSBoxConfigFile
    {
        private readonly List<string> configFileContent = new List<string>();

        private readonly string autoExecSection = string.Empty;

        public DOSBoxConfigFile(string configFilePath)
        {
            if (StringExt.IsNullOrWhiteSpace(configFilePath) || File.Exists(configFilePath) == false)
            {
                return;
            }

            this.configFileContent = File.ReadAllLines(configFilePath).Select(x => x.ToUpperInvariant()).ToList();

            int index = this.configFileContent.LastIndexOf("[AUTOEXEC]");
            if (index != -1)
            {
                var rangeStart = index + 1;
                var rangeEnd = Math.Abs(index - (this.configFileContent.Count - 1));
                var section = this.configFileContent.GetRange(rangeStart, rangeEnd);
                section.RemoveAll(x => StringExt.IsNullOrWhiteSpace(x) || x.ToUpperInvariant().Trim().StartsWith("REM"));
                this.autoExecSection = string.Join(string.Empty, section.ToArray());
            }
        }

        public bool IsAutoExecSectionUsed() => StringExt.IsNullOrWhiteSpace(this.autoExecSection) == false && Regex.Split(this.autoExecSection, "\r\n|\r|\n").Any(x => StringExt.IsNullOrWhiteSpace(x) == false);
    }
}