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
    public partial class Login : Form
    {
        DB_A54362_mngEntities dbContext = new DB_A54362_mngEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            var usuario = (from a in dbContext.Usuarios.Where(z => z.Usuario == txtUser.Text && z.Contrasenia == txtPass.Text) select a).FirstOrDefault();

            if(usuario != null && usuario.Id > 0)
            {
                Program.IdUsuario = usuario.Id;
                this.Hide();
                Form1 principal = new Form1();
                principal.Show();

            }
            else
            {
                lblError.Text = "Usuario o contraseña errada!";
            }
        }
    }
}
