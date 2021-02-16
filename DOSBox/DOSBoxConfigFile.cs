namespace AmpShell.DOSBox
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a DOSBox Config File.
    /// </summary>
    public class DOSBoxConfigFile
    {
        private readonly List<string> configFileContent = new List<string>();

        private readonly string autoExecSection = string.Empty;

        public DOSBoxConfigFile(string configFilePath)
        {
            if (string.IsNullOrWhiteSpace(configFilePath) || File.Exists(configFilePath) == false)
            {
                return;
            }

            this.configFileContent = File.ReadAllLines(configFilePath).Select(x => x.ToUpper(CultureInfo.CurrentCulture)).ToList();
            int index = this.configFileContent.LastIndexOf("[AUTOEXEC]");
            if (index != -1)
            {
                var range = new Tuple<int, int>(index + 1, Math.Abs(index - (this.configFileContent.Count - 1)));
                var section = this.configFileContent.GetRange(range.Item1, range.Item2);
                section.RemoveAll(x => string.IsNullOrEmpty(x) || x[0] == '#');
                this.autoExecSection = string.Join(string.Empty, section);
            }
        }

        public bool IsAutoExecSectionUsed() => string.IsNullOrWhiteSpace(this.autoExecSection) == false && Regex.Split(this.autoExecSection, "\r\n|\r|\n").Any(x => string.IsNullOrWhiteSpace(x) == false && x.ToUpper().Trim().StartsWith("REM") == false);
    }
}