using System.Globalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AmpShell.DOSBox
{
    /// <summary>
    /// Represents a DOSBox Config File
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

            configFileContent = File.ReadAllLines(configFilePath).Select(x => x.ToUpper(CultureInfo.CurrentCulture)).ToList();
        }

        public bool IsAutoExecSectionUsed()
        {
            return string.IsNullOrWhiteSpace(AutoExecSection) == false;
        }

        private string AutoExecSection
        {
            get
            {
                int index = configFileContent.LastIndexOf("[AUTOEXEC]");
                if (index != -1)
                {
                    var range = new Tuple<int, int>(index + 1, Math.Abs(index - (configFileContent.Count - 1)));
                    var section = configFileContent.GetRange(range.Item1, range.Item2);
                    section.RemoveAll(x => string.IsNullOrEmpty(x) || x[0] == '#');
                    return string.Join("", section);
                }
                return string.Empty;
            }
        }
    }
}