namespace DBControl
{
    partial class DatosConexion
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.server = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.database = new System.Windows.Forms.TextBox();
            this.conectar = new System.Windows.Forms.Button();
            this.cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Servidor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Usuario";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Pasword";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Base de datos";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(260, 49);
            this.label5.TabIndex = 0;
            this.label5.Text = "No se ha podido establecer conexion con alguna base de datos. Introdusca los dato" +
                "s de conexión para continuar.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // server
            // 
            this.server.Location = new System.Drawing.Point(97, 64);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(175, 20);
            this.server.TabIndex = 1;
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(97, 90);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(175, 20);
            this.user.TabIndex = 1;
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(97, 116);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '♠';
            this.pass.Size = new System.Drawing.Size(175, 20);
            this.pass.TabIndex = 1;
            // 
            // database
            // 
            this.database.Location = new System.Drawing.Point(97, 142);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(175, 20);
            this.database.TabIndex = 1;
            // 
            // conectar
            // 
            this.conectar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.conectar.Location = new System.Drawing.Point(116, 177);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(75, 23);
            this.conectar.TabIndex = 2;
            this.conectar.Text = "Conectar";
            this.conectar.UseVisualStyleBackColor = true;
            // 
            // cancelar
            // 
            this.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelar.Location = new System.Drawing.Point(197, 177);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(75, 23);
            this.cancelar.TabIndex = 2;
            this.cancelar.Text = "Cancelar";
            this.cancelar.UseVisualStyleBackColor = true;
            // 
            // DatosConexion
            // 
            this.AcceptButton = this.conectar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelar;
            this.ClientSize = new System.Drawing.Size(284, 212);
            this.Controls.Add(this.cancelar);
            this.Controls.Add(this.conectar);
            this.Controls.Add(this.database);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.user);
            this.Controls.Add(this.server);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DatosConexion";
            this.ShowInTaskbar = false;
            this.Text = "Datos de conexion Conexion";
            this.Load += new System.EventHandler(this.DatosConexion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox server;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.TextBox database;
        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.Button cancelar;
    }
}