namespace PizzaExpressClient
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.picBx = new System.Windows.Forms.PictureBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lstBx = new System.Windows.Forms.ListBox();
            this.cmbBxCmd = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBx)).BeginInit();
            this.SuspendLayout();
            // 
            // picBx
            // 
            this.picBx.Image = global::PizzaExpressClient.Properties.Resources.Pizza;
            this.picBx.Location = new System.Drawing.Point(12, 12);
            this.picBx.Name = "picBx";
            this.picBx.Size = new System.Drawing.Size(189, 160);
            this.picBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBx.TabIndex = 0;
            this.picBx.TabStop = false;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(12, 309);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(189, 20);
            this.txtId.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 351);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(189, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(12, 390);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(189, 20);
            this.txtPrice.TabIndex = 3;
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(12, 429);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(189, 20);
            this.txtCategory.TabIndex = 4;
            // 
            // lstBx
            // 
            this.lstBx.FormattingEnabled = true;
            this.lstBx.HorizontalScrollbar = true;
            this.lstBx.Location = new System.Drawing.Point(207, 16);
            this.lstBx.Name = "lstBx";
            this.lstBx.ScrollAlwaysVisible = true;
            this.lstBx.Size = new System.Drawing.Size(228, 433);
            this.lstBx.TabIndex = 5;
            // 
            // cmbBxCmd
            // 
            this.cmbBxCmd.FormattingEnabled = true;
            this.cmbBxCmd.Location = new System.Drawing.Point(12, 178);
            this.cmbBxCmd.Name = "cmbBxCmd";
            this.cmbBxCmd.Size = new System.Drawing.Size(189, 21);
            this.cmbBxCmd.TabIndex = 6;
            this.cmbBxCmd.SelectedIndexChanged += new System.EventHandler(this.cmbBxCmd_SelectedIndexChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(12, 205);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(189, 68);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "Manda";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(12, 413);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 8;
            this.lblCategory.Text = "Categoria";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(12, 374);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(39, 13);
            this.lblPrice.TabIndex = 9;
            this.lblPrice.Text = "Prezzo";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 335);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Nome";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(12, 293);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(16, 13);
            this.lblId.TabIndex = 11;
            this.lblId.Text = "Id";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 461);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.cmbBxCmd);
            this.Controls.Add(this.lstBx);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.picBx);
            this.Name = "Form1";
            this.Text = "Pizza Express";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBx;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.ListBox lstBx;
        private System.Windows.Forms.ComboBox cmbBxCmd;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblId;
    }
}

