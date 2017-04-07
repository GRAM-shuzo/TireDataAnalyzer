using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using TireDataAnalyzer.UserControls.TreeViewNodes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TTCDataUtils;
using TireDataAnalyzer.UserControls;
namespace TireDataAnalyzer
{
    public static class StaticFunctions
    {

        public static ProjectTree.ProjectTreeNode ClipBoad;

        public static bool IsNotValidValue(double d)
        {
            return double.IsInfinity(d) || double.IsNaN(d);
        }

        public static List<ToolStripItem> TemplateMenu(
            EventHandler eCopy, 
            EventHandler eDelete, 
            EventHandler eRename, 
            EventHandler eProperty
            )
        {
            var copy = new ToolStripMenuItem("コピー(&C)", null, eCopy, Keys.C | Keys.Control);

            var delete = new ToolStripMenuItem("削除(&D)", null, eDelete, Keys.D | Keys.Control);

            var rename = new ToolStripMenuItem("名前の変更(&M)", null, eRename, Keys.M | Keys.Control);

            var property = new ToolStripMenuItem("プロパティ(&P)", null, eProperty, Keys.P | Keys.Control);

            var list = new List<ToolStripItem>();
            list.Add(copy);
            list.Add(delete);
            list.Add(rename);
            list.Add(new ToolStripSeparator());
            list.Add(property);
            return list;
        }

        public static ZipArchiveEntry GetEntity(string Path, ZipArchive archive, bool clear = false)
        {
            ZipArchiveEntry entry;
            if(clear == true)
            {
                if ((entry = archive.GetEntry(Path)) != null)
                {
                    entry.Delete();
                }
                entry = archive.CreateEntry(Path);
                return entry;
            }
            if ((entry = archive.GetEntry(Path)) != null)
            {
                return entry;
            }
            entry = archive.CreateEntry(Path);
            return entry;
        }

        public static string GetUniqueName(List<string> names, string name)
        {


            bool unique;
            int i = 1;
            string result;
            do
            {
                unique = true;
                result = i == 1 ? name : name + i.ToString();
                foreach (var value in names)
                {
                    unique &= (result != value);
                }
                ++i;
            } while (!unique);
            return result;
        }

        public static string GetUniqueName(TreeView view, string name)
        {


            bool unique;
            int i = 1;
            string result;
            do
            {
                unique = true;
                result = i == 1 ? name : name + i.ToString();
                foreach (MyTreeNode node in view.Nodes)
                {
                    unique &= GetUniqueName(node, result);
                }
                ++i;
            } while (!unique);
            return result;
        }
        private static bool GetUniqueName(MyTreeNode node, string name)
        {
            bool unique = true;
            foreach (MyTreeNode child in node.Nodes)
            {
                if (child.ImplName != name)
                {
                    unique &= GetUniqueName(child, name);
                }
                else
                {
                    return false;
                }
            }
            return unique;
        }

        public static void AddPropertyPage(UserControls.PropertyPage.PropertyPage property)
        {         
            var newpage = new TabPage();
            newpage.Controls.Add(property);

            property.Dock = DockStyle.Fill;
            
            MainWindow.MainTabControl.TabPages.Add(newpage);
            property.SetTabControl(MainWindow.MainTabControl);
            MainWindow.MainTabControl.SelectedTab = newpage;
        }

        public static bool IsAlreadyAdded(UserControls.PropertyPage.PropertyPage property)
        {
            foreach (TabPage page in MainWindow.MainTabControl.TabPages)
            {
                if (page.Contains(property))
                {
                    MainWindow.MainTabControl.SelectedTab = page;
                    return true;
                }
            }
            return false; 
        }

        public static T DeepCopy<T>(T target)
        {

            T result;
            using (MemoryStream mem = new MemoryStream())
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(mem, target);
                mem.Position = 0;
                result = (T)b.Deserialize(mem);
            }
            return result;

        }

        public static TireDataViewer DeepCopyTest(TireDataViewer target)
        {

            TireDataViewer result;
            using (MemoryStream mem = new MemoryStream())
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(mem, target);
                mem.Position = 0;
                result = (TireDataViewer)b.Deserialize(mem);
            }
            return result;

        }
        
        public static MyTreeNode GetTreeView(ProjectTree.ProjectTreeNode node)
        {
            return MainWindow.Instance.GetTreeView(node);
        }

        public static TTCDataUtils.TireMagicFormula GetDefaultMFValue()
        {
            return null;
        }

        public static bool IsNumeric(string stTarget)
        {
            double dNullable;

            return double.TryParse(
                stTarget,
                System.Globalization.NumberStyles.Any,
                null,
                out dNullable
            );
        }

        public static bool IsNInt(string stTarget)
        {
            int dNullable = -1;
            bool i = int.TryParse(
                stTarget,
                System.Globalization.NumberStyles.Any,
                null,
                out dNullable
            );
            return i && dNullable >= 1;
        }

        public static TireDataMaxmin TireDataMaxminMerge(TireDataMaxmin lh, TireDataMaxmin rh)
        {
            var value = new TireDataMaxmin();
            foreach(TireDataColumn column in Enum.GetValues(typeof(TireDataColumn)))
            {
                if (column == TireDataColumn.NT) continue;
                value.Max[column] = Math.Max(lh.Max[column], rh.Max[column]);
                value.Min[column] = Math.Min(lh.Min[column], rh.Min[column]);
            }
            return value;
        }
    }
}
