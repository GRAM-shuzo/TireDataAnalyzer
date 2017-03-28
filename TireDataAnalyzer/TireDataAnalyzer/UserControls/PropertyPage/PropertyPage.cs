using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TireDataAnalyzer.UserControls.PropertyPage
{
    [Docking(DockingBehavior.Ask)]
    public partial class PropertyPage : UserControl
    {
        public PropertyPage(ProjectTree.ProjectTreeNode node)
        {
            InitializeComponent();
            State = PropertyPageState.NotChanged;
            if (node.Updated == ProjectTree.ProjectTreeNode.UpdateState.NotUpdated)
            {
                DialogResult result = MessageBox.Show("親ノードの変更が反映されていません。更新しますか？）",
                        "確認",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    node.Update();
                }
            }
        }

        public enum PropertyPageState
        {
            Changed,
            NotChanged
        };
        PropertyPageState state;
        public PropertyPageState State
        {
            get { return state; }
            protected set
            {
                state = value;
                SetTabText(MyName);
            }
        }

        string MyName;
        CustomTabControl ctc;
        public void SetTabControl(CustomTabControl c)
        {
            ctc = c;
            ctc.TabClosing += Ctc_TabClosing;
        }

        private void Ctc_TabClosing(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = true;
            Remove();
        }

        public void SetTabText(string name)
        {
            MyName = name;
            var page = Parent as TabPage;
            if (State == PropertyPageState.Changed)
                name = name + "  *";

            if (page != null)
            {
                Parent.Text = name;
            }
        }

        public virtual bool OnOKClick()
        {
            return true;
        }
        public virtual bool OnCancelClick()
        {
            return true;
        }
        public virtual void OnApplyClick()
        {
        }
        public virtual bool OnRemove()
        {
            //変更されている場合
            if (State == PropertyPageState.Changed)
            {
                DialogResult result = MessageBox.Show("内容の変更を反映しますか？",
                        "確認",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    OnApplyClick();
                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Remove()
        {
            if (OnRemove())
            {
                var page = Parent as TabPage;
                var tabcontrol = page.Parent as CustomTabControl;
                if(tabcontrol != null)
                {
                    tabcontrol.TabPages.Remove(page);
                    ctc.TabClosing -= Ctc_TabClosing;
                }
                return true;
            }
            return false;
        }


        private void OKButton_Click(object sender, EventArgs e)
        {
            if (OnOKClick())
            {
                Remove();
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (OnCancelClick())
            {
                Remove();
            }
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            OnApplyClick();
        }
        private PropertyPage()
        {
            InitializeComponent();

        }
    }
}
