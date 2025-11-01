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
namespace MgnSystemWin
{
    public partial class ConsultarProducto : Form
    {
        DB_A54362_mngEntities dbContext = new DB_A54362_mngEntities();
        public ConsultarProducto()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            var information =  dbContext.getProducto().Where(z => z.CodigoBarra.Contains(textBox4.Text)).ToList();

            dtgData.DataSource = information;
            dtgData.Refresh();
        }

        public List<getProducto_Result> getLista()
        {
             var information = dbContext.getProducto().ToList();
            if (txtBusquedaLab.Text.Length > 0)
            {
                information = information.Where(z=>z.Marca.Contains(txtBusquedaLab.Text)).ToList();
            }
            if (textBox3.Text.Length > 0)
            {
                information = information.Where(z => z.LaboratorioName.Contains(textBox3.Text)).ToList();
            }
            if (textBox2.Text.Length > 0)
            {
                information = information.Where(z => z.DescripcionGeneral.Contains(textBox2.Text)).ToList();
            }
            if (textBox1.Text.Length > 0)
            {
                information = information.Where(z => z.DescripcionEspecifica.Contains(textBox1.Text)).ToList();
            }
            return information;
        }

        private void txtBusquedaLab_TextChanged(object sender, EventArgs e)
        {
            dtgData.DataSource = getLista();
            dtgData.Refresh();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dtgData.DataSource = getLista();
            dtgData.Refresh();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dtgData.DataSource = getLista();
            dtgData.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dtgData.DataSource = getLista();
            dtgData.Refresh();
        }
    }
}
