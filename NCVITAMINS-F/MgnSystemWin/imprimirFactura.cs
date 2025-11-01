using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MgnSystemWin.Model.DB;
using Microsoft.Reporting.WinForms;

namespace MgnSystemWin
{
    public partial class imprimirFactura : Form
    {
        DB_A54362_mngEntities dbContext = new DB_A54362_mngEntities();
        int factura;
        public imprimirFactura(int idFactura)
        {
            InitializeComponent();
            factura = idFactura;
        }

        private void imprimirFactura_Load(object sender, EventArgs e)
        {
            var data = dbContext.GetFactura(factura).ToList();
            this.reportViewer1.Reset();
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory + "ssrsExport.rdlc";
            ReportDataSource reportDataSource = new ReportDataSource();
            // Must match the DataSet in the RDLC
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = data;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
