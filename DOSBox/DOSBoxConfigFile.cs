namespace AmpShell.DOSBox
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a DOSBox Config File.
    /// </summary>
    public class DOSBoxConfigFile
    {
        private readonly List<string> configFileContent = new List<string>();

        public DOSBoxConfigFile(string configFilePath)
        {
            if (string.IsNullOrWhiteSpace(configFilePath) || File.Exists(configFilePath) == false)
            {
                return;
            }

            this.configFileContent = File.ReadAllLines(configFilePath).Select(x => x.ToUpper(CultureInfo.CurrentCulture)).ToList();
        }

        private string AutoExecSection
        {
            get
            {
                int index = this.configFileContent.LastIndexOf("[AUTOEXEC]");
                if (index != -1)
                {
                    var range = new Tuple<int, int>(index + 1, Math.Abs(index - (this.configFileContent.Count - 1)));
                    var section = this.configFileContent.GetRange(range.Item1, range.Item2);
                    section.RemoveAll(x => string.IsNullOrEmpty(x) || x[0] == '#');
                    return string.Join(string.Empty, section);
                }
                return string.Empty;
            }
        }

        public bool IsAutoExecSectionUsed()
        {
            return string.IsNullOrWhiteSpace(this.AutoExecSection) == false;
        }
    }
}