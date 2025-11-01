namespace MgnSystemWin
{
    partial class ConsultarProducto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtBusquedaLab = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtgData = new System.Windows.Forms.DataGridView();
            this.getProductoResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.marcaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionEspecificaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionGeneralDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaVencimientoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigoBarraDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidadDisponibleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidadPrecentacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tPrencentacionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tVentaNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.almacenNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.laboratorioNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proveedorNombreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getProductoResultBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBusquedaLab
            // 
            this.txtBusquedaLab.Location = new System.Drawing.Point(186, 13);
            this.txtBusquedaLab.Name = "txtBusquedaLab";
            this.txtBusquedaLab.Size = new System.Drawing.Size(337, 20);
            this.txtBusquedaLab.TabIndex = 29;
            this.txtBusquedaLab.TextChanged += new System.EventHandler(this.txtBusquedaLab_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(67, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Buscar Marca";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(186, 91);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(337, 20);
            this.textBox1.TabIndex = 31;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Buscar Des Especifica";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(186, 65);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(337, 20);
            this.textBox2.TabIndex = 33;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Buscar Des General";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(186, 39);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(337, 20);
            this.textBox3.TabIndex = 35;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Buscar Laboratorio";
            // 
            // dtgData
            // 
            this.dtgData.AllowUserToAddRows = false;
            this.dtgData.AllowUserToDeleteRows = false;
            this.dtgData.AutoGenerateColumns = false;
            this.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.marcaDataGridViewTextBoxColumn,
            this.descripcionEspecificaDataGridViewTextBoxColumn,
            this.descripcionGeneralDataGridViewTextBoxColumn,
            this.fechaVencimientoDataGridViewTextBoxColumn,
            this.codigoBarraDataGridViewTextBoxColumn,
            this.cantidadDisponibleDataGridViewTextBoxColumn,
            this.cantidadPrecentacionDataGridViewTextBoxColumn,
            this.tPrencentacionNameDataGridViewTextBoxColumn,
            this.tVentaNameDataGridViewTextBoxColumn,
            this.almacenNameDataGridViewTextBoxColumn,
            this.laboratorioNameDataGridViewTextBoxColumn,
            this.proveedorNombreDataGridViewTextBoxColumn});
            this.dtgData.DataSource = this.getProductoResultBindingSource;
            this.dtgData.Location = new System.Drawing.Point(12, 135);
            this.dtgData.Name = "dtgData";
            this.dtgData.ReadOnly = true;
            this.dtgData.Size = new System.Drawing.Size(776, 303);
            this.dtgData.TabIndex = 36;
            // 
            // getProductoResultBindingSource
            // 
            this.getProductoResultBindingSource.DataSource = typeof(MgnSystemWin.Model.DB.getProducto_Result);
            // 
            // marcaDataGridViewTextBoxColumn
            // 
            this.marcaDataGridViewTextBoxColumn.DataPropertyName = "Marca";
            this.marcaDataGridViewTextBoxColumn.HeaderText = "Marca";
            this.marcaDataGridViewTextBoxColumn.Name = "marcaDataGridViewTextBoxColumn";
            this.marcaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripcionEspecificaDataGridViewTextBoxColumn
            // 
            this.descripcionEspecificaDataGridViewTextBoxColumn.DataPropertyName = "DescripcionEspecifica";
            this.descripcionEspecificaDataGridViewTextBoxColumn.HeaderText = "DescripcionEspecifica";
            this.descripcionEspecificaDataGridViewTextBoxColumn.Name = "descripcionEspecificaDataGridViewTextBoxColumn";
            this.descripcionEspecificaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripcionGeneralDataGridViewTextBoxColumn
            // 
            this.descripcionGeneralDataGridViewTextBoxColumn.DataPropertyName = "DescripcionGeneral";
            this.descripcionGeneralDataGridViewTextBoxColumn.HeaderText = "DescripcionGeneral";
            this.descripcionGeneralDataGridViewTextBoxColumn.Name = "descripcionGeneralDataGridViewTextBoxColumn";
            this.descripcionGeneralDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fechaVencimientoDataGridViewTextBoxColumn
            // 
            this.fechaVencimientoDataGridViewTextBoxColumn.DataPropertyName = "FechaVencimiento";
            this.fechaVencimientoDataGridViewTextBoxColumn.HeaderText = "FechaVencimiento";
            this.fechaVencimientoDataGridViewTextBoxColumn.Name = "fechaVencimientoDataGridViewTextBoxColumn";
            this.fechaVencimientoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // codigoBarraDataGridViewTextBoxColumn
            // 
            this.codigoBarraDataGridViewTextBoxColumn.DataPropertyName = "CodigoBarra";
            this.codigoBarraDataGridViewTextBoxColumn.HeaderText = "CodigoBarra";
            this.codigoBarraDataGridViewTextBoxColumn.Name = "codigoBarraDataGridViewTextBoxColumn";
            this.codigoBarraDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cantidadDisponibleDataGridViewTextBoxColumn
            // 
            this.cantidadDisponibleDataGridViewTextBoxColumn.DataPropertyName = "CantidadDisponible";
            this.cantidadDisponibleDataGridViewTextBoxColumn.HeaderText = "CantidadDisponible";
            this.cantidadDisponibleDataGridViewTextBoxColumn.Name = "cantidadDisponibleDataGridViewTextBoxColumn";
            this.cantidadDisponibleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cantidadPrecentacionDataGridViewTextBoxColumn
            // 
            this.cantidadPrecentacionDataGridViewTextBoxColumn.DataPropertyName = "CantidadPrecentacion";
            this.cantidadPrecentacionDataGridViewTextBoxColumn.HeaderText = "CantidadPrecentacion";
            this.cantidadPrecentacionDataGridViewTextBoxColumn.Name = "cantidadPrecentacionDataGridViewTextBoxColumn";
            this.cantidadPrecentacionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tPrencentacionNameDataGridViewTextBoxColumn
            // 
            this.tPrencentacionNameDataGridViewTextBoxColumn.DataPropertyName = "TPrencentacionName";
            this.tPrencentacionNameDataGridViewTextBoxColumn.HeaderText = "Tipo Precentacion";
            this.tPrencentacionNameDataGridViewTextBoxColumn.Name = "tPrencentacionNameDataGridViewTextBoxColumn";
            this.tPrencentacionNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tVentaNameDataGridViewTextBoxColumn
            // 
            this.tVentaNameDataGridViewTextBoxColumn.DataPropertyName = "TVentaName";
            this.tVentaNameDataGridViewTextBoxColumn.HeaderText = "Tipo Venta";
            this.tVentaNameDataGridViewTextBoxColumn.Name = "tVentaNameDataGridViewTextBoxColumn";
            this.tVentaNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // almacenNameDataGridViewTextBoxColumn
            // 
            this.almacenNameDataGridViewTextBoxColumn.DataPropertyName = "AlmacenName";
            this.almacenNameDataGridViewTextBoxColumn.HeaderText = "Almacen";
            this.almacenNameDataGridViewTextBoxColumn.Name = "almacenNameDataGridViewTextBoxColumn";
            this.almacenNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // laboratorioNameDataGridViewTextBoxColumn
            // 
            this.laboratorioNameDataGridViewTextBoxColumn.DataPropertyName = "LaboratorioName";
            this.laboratorioNameDataGridViewTextBoxColumn.HeaderText = "Laboratorio";
            this.laboratorioNameDataGridViewTextBoxColumn.Name = "laboratorioNameDataGridViewTextBoxColumn";
            this.laboratorioNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // proveedorNombreDataGridViewTextBoxColumn
            // 
            this.proveedorNombreDataGridViewTextBoxColumn.DataPropertyName = "ProveedorNombre";
            this.proveedorNombreDataGridViewTextBoxColumn.HeaderText = "Proveedor";
            this.proveedorNombreDataGridViewTextBoxColumn.Name = "proveedorNombreDataGridViewTextBoxColumn";
            this.proveedorNombreDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(539, 13);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(164, 20);
            this.textBox4.TabIndex = 37;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // ConsultarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.dtgData);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBusquedaLab);
            this.Controls.Add(this.label10);
            this.Name = "ConsultarProducto";
            this.Text = "ConsultarProducto";
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getProductoResultBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBusquedaLab;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dtgData;
        private System.Windows.Forms.DataGridViewTextBoxColumn marcaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionEspecificaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionGeneralDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaVencimientoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoBarraDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadDisponibleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadPrecentacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tPrencentacionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tVentaNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn almacenNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn laboratorioNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn proveedorNombreDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource getProductoResultBindingSource;
        private System.Windows.Forms.TextBox textBox4;
    }
}