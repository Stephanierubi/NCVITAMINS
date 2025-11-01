using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MgnSystemWin.Model.DB;

namespace MgnSystemWin
{
    public partial class Factura : Form
    {
        DB_A54362_mngEntities dbContext = new DB_A54362_mngEntities();
        public Factura()
        {
            InitializeComponent();
        }

        private void txtBusquedaLab_TextChanged(object sender, EventArgs e)
        {
            var information = dbContext.Producto.Where(z => z.CodigoBarra.Contains(txtBusquedaLab.Text) && z.CantidadDisponible>0).FirstOrDefault();
            if(information != null)
            {
                label4.Text = information.Marca;
                label5.Text = information.PrecioVenta.ToString();
                label6.Text = information.FechaVencimiento.Value.ToString("dd/MM/yy");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Producto> x = new List<Producto>();
            
            var information = dbContext.Producto.Where(z => z.CodigoBarra.Contains(txtBusquedaLab.Text)).FirstOrDefault();            
            if (information != null)
            {
                if (dtgData.Rows.Count > 0)
                {
                    label3.Text = (double.Parse(label3.Text) + double.Parse(textBox1.Text)).ToString();
                    information.CantidadPrecentacion = (double.Parse(textBox1.Text) * information.PrecioVenta);
                    for (int index = 0; index < dtgData.SelectedRows.Count; index++)
                    {
                        var selectedRow = dtgData.SelectedRows[index];
                        var pilot = (Producto)selectedRow.DataBoundItem;

                        x.Add(pilot);
                    }
                    x.Add(information);
                }
                else
                {
                    label3.Text = (double.Parse(label3.Text) + (double.Parse(textBox1.Text)*information.PrecioVenta)).ToString();
                    information.CantidadPrecentacion = (double.Parse(textBox1.Text) * information.PrecioVenta);
                    dtgData.DataSource = information;
                    dtgData.Refresh();
                }
                
            }
            
            }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Producto> x = new List<Producto>();
            for (int index = 0; index < dtgData.SelectedRows.Count; index++)
            {
                var selectedRow = dtgData.SelectedRows[index];
                var pilot = (Producto)selectedRow.DataBoundItem;

                x.Add(pilot);
            }

            Model.DB.Factura fa = new Model.DB.Factura();
            fa.Fecha = DateTime.Now;
            fa.CreatedBy = Program.IdUsuario;
            fa.ModifiedBy = Program.IdUsuario;
            fa.CreatedOn = DateTime.Now;
            fa.ModifiedOn = DateTime.Now;
            dbContext.Factura.Add(fa);
            dbContext.SaveChanges();
            List<FacturaDetalle> fdet = new List<FacturaDetalle>();
            FacturaDetalle fd = new FacturaDetalle();
            foreach (Producto item in x)
            {
                fd = new FacturaDetalle();
                fd.ProductoID = item.ID;
                fd.Valor = item.PrecioVenta;
                fd.FacturaID = fa.ID;
                fd.Cantidad = item.CantidadPrecentacion;
                fd.IVA = 0;
                fdet.Add(fd);
            }
            dbContext.FacturaDetalle.AddRange(fdet);
            dbContext.SaveChanges();

            imprimirFactura fc= new imprimirFactura(fa.ID);
            fc.MdiParent = this;
            // Display the new form.
            fc.Show();
        }
    }
}
