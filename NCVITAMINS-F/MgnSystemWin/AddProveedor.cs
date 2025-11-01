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
    public partial class AddProveedor : Form
    {
        DB_A54362_mngEntities dbContext = new DB_A54362_mngEntities();
        public AddProveedor()
        {
            InitializeComponent();
        }

        private void txtBusquedaLab_TextChanged(object sender, EventArgs e)
        {
            if (txtBusquedaLab.Text.Length > 0)
            {
                var information = (from a in dbContext.Proveedor.Where(z => z.Nombre.Contains(txtBusquedaLab.Text)) select a).ToList();

                dtgData.DataSource = information;
                dtgData.Refresh();
            }
            else
            {
                var information = (from a in dbContext.Proveedor select a).ToList();

                dtgData.DataSource = information;
                dtgData.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Proveedor x = new Proveedor();

            x.Nombre = txtProveedor.Text;
            x.Telefono = txtTelefono.Text;
            x.Direccion = txtDireccion.Text;
            x.CreatedBy = Program.IdUsuario;
            x.ModifiedBy = Program.IdUsuario;
            x.CreatedOn = DateTime.Now;
            x.ModifiedOn = DateTime.Now;

            dbContext.Proveedor.Add(x);
            dbContext.SaveChanges();

            var information = (from a in dbContext.Proveedor select a).ToList();

            dtgData.DataSource = information;
            dtgData.Refresh();
        }
    }
}
