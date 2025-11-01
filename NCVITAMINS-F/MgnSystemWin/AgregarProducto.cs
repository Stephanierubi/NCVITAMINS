using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MgnSystemWin.Model;
using MgnSystemWin.Model.DB;
namespace MgnSystemWin
{   
    public partial class AgregarProducto : Form
    {
        DB_A54362_mngEntities dbContext = new DB_A54362_mngEntities();
        public AgregarProducto()
        {
            InitializeComponent();

            var tprecentacion = (from a in dbContext.CatalogDetail.Where(z => z.CatalogId == 4)
                                 select new ComboClassCustom
                                 {
                                     value = a.Id.ToString(),
                                     Text = a.Name
                                 }).ToList();

            var proveedor = (from a in dbContext.Proveedor
                                 select new ComboClassCustom
                                 {
                                     value = a.ID.ToString(),
                                     Text = a.Nombre
                                 }).ToList();
            var tventa = (from a in dbContext.CatalogDetail.Where(z => z.CatalogId == 5)
                                 select new ComboClassCustom
                                 {
                                     value = a.Id.ToString(),
                                     Text = a.Name
                                 }).ToList();
            var talmacen = (from a in dbContext.CatalogDetail.Where(z => z.CatalogId == 3)
                          select new ComboClassCustom
                          {
                              value = a.Id.ToString(),
                              Text = a.Name
                          }).ToList();

            var tlab = (from a in dbContext.CatalogDetail.Where(z => z.CatalogId == 6)
                            select new ComboClassCustom
                            {
                                value = a.Id.ToString(),
                                Text = a.Name
                            }).ToList();

            ChangeCombo(comboBox1, tprecentacion);
            ChangeCombo(comboBox2, proveedor);
            ChangeCombo(comboBox3, tventa);
            ChangeCombo(comboBox4, talmacen);
            ChangeCombo(comboBox5, tlab);
        }

        public static void ChangeCombo(ComboBox x, List<ComboClassCustom> lista)
        {
            x.DataSource = new BindingSource(lista, null);
            x.DisplayMember = "Text";
            x.ValueMember = "value";           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          var sd=  ((ComboClassCustom)comboBox1.SelectedItem).value;
               Producto x = new Producto();
            x.Marca = textBox1.Text;
            x.DescripcionEspecifica = textBox2.Text;
            x.DescripcionGeneral = textBox3.Text;
            x.FechaVencimiento = dateTimePicker1.Value;
            x.TipoPrecentacionID = int.Parse(((ComboClassCustom)comboBox1.SelectedItem).value);
            x.ProveedorID = int.Parse(((ComboClassCustom)comboBox2.SelectedItem).value);
            x.TipoVentaID = int.Parse(((ComboClassCustom)comboBox3.SelectedItem).value);
            x.Almacen = int.Parse(((ComboClassCustom)comboBox4.SelectedItem).value);
            x.Laboratorio = int.Parse(((ComboClassCustom)comboBox5.SelectedItem).value);
            x.CantidadPrecentacion = double.Parse(textBox6.Text);
            x.CantidadDisponible = double.Parse(textBox4.Text);
            x.PrecioCompra = double.Parse(textBox5.Text);
            x.CreatedBy = Program.IdUsuario;
            x.ModifiedBy = Program.IdUsuario;
            x.CreatedOn = DateTime.Now;
            x.ModifiedOn = DateTime.Now;

            dbContext.Producto.Add(x);
            dbContext.SaveChanges();

            var information = (from a in dbContext.Producto.Where(z=>z.CantidadDisponible>0) select a).ToList();

            dtgData.DataSource = information;
            dtgData.Refresh();
        }

        private void txtBusquedaLab_TextChanged(object sender, EventArgs e)
        {
            if (txtBusquedaLab.Text.Length > 0)
            {
                var information = (from a in dbContext.Producto.Where(z => z.Marca.Contains(txtBusquedaLab.Text)) select a).ToList();

                dtgData.DataSource = information;
                dtgData.Refresh();
            }
            else
            {
                var information = (from a in dbContext.Producto select a).ToList();

                dtgData.DataSource = information;
                dtgData.Refresh();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
