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
    public partial class AddAlmacen : Form
    {
        DB_A54362_mngEntities dbContext = new DB_A54362_mngEntities();
        public AddAlmacen()
        {
            InitializeComponent();
        }

        private void txtBusquedaLab_TextChanged(object sender, EventArgs e)
        {
            if (txtBusquedaLab.Text.Length > 0)
            {
                var information = (from a in dbContext.CatalogDetail.Where(z => z.CatalogId == 3 && z.Name.Contains(txtBusquedaLab.Text)) select a).ToList();

                dtgData.DataSource = information;
                dtgData.Refresh();
            }
            else
            {
                var information = (from a in dbContext.CatalogDetail.Where(z => z.CatalogId == 3) select a).ToList();

                dtgData.DataSource = information;
                dtgData.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CatalogDetail catalogDetail = new CatalogDetail();

            catalogDetail.Name = txtName.Text;
            catalogDetail.Description = txtDescription.Text;
            catalogDetail.CatalogId = 3;
            catalogDetail.IsActive = true;
            catalogDetail.CreatedBy = Program.IdUsuario;
            catalogDetail.ModifiedBy = Program.IdUsuario;
            catalogDetail.CreatedOn = DateTime.Now;
            catalogDetail.ModifiedOn = DateTime.Now;

            dbContext.CatalogDetail.Add(catalogDetail);
            dbContext.SaveChanges();

            var information = (from a in dbContext.CatalogDetail.Where(z => z.CatalogId == 3) select a).ToList();

            dtgData.DataSource = information;
            dtgData.Refresh();
        }
    }
}
