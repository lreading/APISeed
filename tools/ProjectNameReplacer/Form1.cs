using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectNameReplacer
{
    public partial class Form1 : Form
    {
        private string _solutionPath = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var res = fbd.ShowDialog();
            if (res == DialogResult.OK)
            {
                var path = fbd.SelectedPath;
                solutionLocationTextBox.Text = path;
                solutionLocationTextBox.Enabled = true;
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            _solutionPath = solutionLocationTextBox.Text;
            var searchPatterns = new HashSet<string>(new[] { ".csproj", ".sln", ".cs", ".config" }, StringComparer.OrdinalIgnoreCase);
            var dirInfo = new DirectoryInfo(_solutionPath);
            var fileInfo = dirInfo.GetFiles("*", SearchOption.AllDirectories)
                .Where(file => searchPatterns.Contains(file.Extension))
                .ToArray();

            foreach (var file in fileInfo)
            {
                UpdateFile(file);
            }
        }

        private void UpdateFile(FileInfo file)
        {
            var lines = File.ReadAllLines(file.FullName);
            var outputSb = new StringBuilder();

            foreach (var line in lines)
            {
                outputSb.AppendLine(line.Replace("Template.", "$ext_safeprojectname$.").Replace("..\\packages\\", "..\\..\\packages\\"));
            }

            File.WriteAllText(file.FullName, outputSb.ToString());
        }
    }
}
