﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TireDataAnalyzer.ProjectTree;

namespace TireDataAnalyzer.UserControls.TreeViewNodes
{
    public class TreeViewNode_Project : MyTreeNode
    {
        public TreeViewNode_Project(TreeView tv, Node_Project impl)
            : base(tv,impl)
        {
            Impl = impl;
            ContextMenuStrip.Items.RemoveAt(0); //コピーはできないので削除
            ContextMenuStrip.Items.RemoveAt(0);  //削除もできないので削除
            ContextMenuStrip.Items.Insert(0,
                new ToolStripMenuItem("タイヤデータを追加(&A)", null, AddRawTireData, Keys.A & Keys.Control)
                );
        }

        Node_Project Impl { get; set; }


        void AddRawTireData(object sender, EventArgs e)
        {
            ConfirmIfNotUpdated();
            var rawDataImpl = new Node_RawTireData(
                StaticFunctions.GetUniqueName(this.TreeView, "新規データ"),
                Impl
                );
            var raw = new TreeViewNode_RawTireData(
                this.TreeView,
                rawDataImpl
                );
            this.Nodes.Add(raw);
            this.Expand();
        }

        override protected void OnRename(string name)
        {

        }

        override protected bool OnRemove()
        {
            return false;
        }

        override protected void OnCopy()
        {

        }

        override public void OnProperty()
        {

        }
    }
}