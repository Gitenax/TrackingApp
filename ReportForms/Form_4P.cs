using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public void FormShow()
        {
            ShowDialog();
        }
    }

    public class ReportForm
    {
        static public string GetFormName() { return "Форма 4-П"; }
    }

  private void MainForm_Click(object sender, EventArgs e)
  {
    /*
    #region lol
    FileInfo file;
    foreach (string filename in Directory.GetFiles(Application.StartupPath + @"\Reports"))
    {
      file = new FileInfo(filename);
      if (Path.GetExtension(filename).Equals(".dll"))
      {
        try
        {
          Assembly a = Assembly.LoadFile(filename);
          Type type = a.GetType("ReportForms.ReportForm");
          MethodInfo info = type.GetMethod("GetFormName");
          MainMenu_Forms.DropDownItems.Add(info.Invoke(null, null).ToString()).Click += MainForm_Click;

        }
        catch (Exception Ex)
        {
          MessageBox.Show(Ex.Message);
        }
      }
    }
    #endregion

    string filename = ((ToolStripMenuItem)sender).Text;


      foreach (string file in Directory.GetFiles(Application.StartupPath + @"\Reports"))
      {
          Assembly a = Assembly.LoadFile(file);

          Type formClass = a.GetType("ReportForms.MainForm");
          MethodInfo infoClass = formClass.GetMethod("ShowDialog", new Type[] { });

          Type type = a.GetType("ReportForms.ReportForm");
          MethodInfo info = type.GetMethod("GetFormName");
          if (info.Invoke(null, null).Equals(filename))
          {
              Object instance = Activator.CreateInstance(formClass);
              infoClass.Invoke(instance, null);
          }
      }*/
  }
