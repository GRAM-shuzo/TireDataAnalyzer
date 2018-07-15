using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTCDataUtils;

namespace TireDataAnalyzer.UserControls.PropertyPage
{
    public partial class TireDataSelectorWithViewer : UserControl
    {
        public TireDataSelectorWithViewer()
        {
            InitializeComponent();
        }

        static public string[] LegendTexts = { "SelectedData", "RemovedData" };

        DataSelectorProperty property;
        Table Table;
        public MultiTireDataViewer Viewer { get { return DataViewer; } } 



        
        public void Initialize(TireDataSetSelector selector, Table table, DataSelectorProperty p )
        {
            property = p;
            Table = table;
            selector.ExtractedDataChanged += OnExtractedDataChanged;
            Selector.Initialize(selector, table,p);
            Selector.SelectedAreaChanged += delegate (TireDataSetSelector s)
            {
                ShowGraph(s);
            };

            SetDataList(selector);


        }


        public void OnExtractedDataChanged(TireDataSetSelector selector, Table table)
        {
            if(table == Table)
            {
                ShowGraph(selector);
            }
        }

        public void SetDataList(TireDataSetSelector selector)
        {
            List<TireData>[] dataLists = { selector.ExtractedData(Table), selector.NotExtractedData(Table) };
            for (int i = 0; i < 2; ++i)
            {
                if (dataLists[i].Count != 0)
                {
                    if (property.PointToDraw() != -1)
                    {
                        int lastIndex = Math.Min(property.PointToDraw(), dataLists[i].Count - 1);
                        dataLists[i] = dataLists[i].OrderBy(j => Guid.NewGuid()).ToList().GetRange(0, lastIndex);
                        
                    }
                    
                }
                DataViewer.numPoints = property.PointToDraw();
                DataViewer.SetDataList(dataLists[i],Table,  LegendTexts[i]);
                
            }
        }

        public void ShowGraph(TireDataSetSelector selector)
        {
            if(selector != null )
            {
                SetDataList(selector);
            }
            DataViewer.DrawAllGraph();
        }
    }
}
