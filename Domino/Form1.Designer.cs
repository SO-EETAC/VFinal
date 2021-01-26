namespace Domino
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nombreBox = new System.Windows.Forms.TextBox();
            this.nombreLbl = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.contraseñaBox = new System.Windows.Forms.TextBox();
            this.respuestaMicuentaBox = new System.Windows.Forms.TextBox();
            this.usuarioBox = new System.Windows.Forms.ComboBox();
            this.respuestaMicuentaLbl = new System.Windows.Forms.Label();
            this.miCuentaBtn = new System.Windows.Forms.Button();
            this.registrarseRadioBtn = new System.Windows.Forms.RadioButton();
            this.iniciarRadioBtn = new System.Windows.Forms.RadioButton();
            this.contraseñaLbl = new System.Windows.Forms.Label();
            this.usuarioLbl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.duracionBox = new System.Windows.Forms.TextBox();
            this.ConsultaBtn = new System.Windows.Forms.Button();
            this.respuestaConsultaBox = new System.Windows.Forms.TextBox();
            this.ganadorLbl = new System.Windows.Forms.Label();
            this.fechaRadioBtn = new System.Windows.Forms.RadioButton();
            this.duracionRadioBtn = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DesconectarBtn = new System.Windows.Forms.Button();
            this.ConectarBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.InvitarBtn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.holaLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nombreBox);
            this.groupBox1.Controls.Add(this.nombreLbl);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.contraseñaBox);
            this.groupBox1.Controls.Add(this.respuestaMicuentaBox);
            this.groupBox1.Controls.Add(this.usuarioBox);
            this.groupBox1.Controls.Add(this.respuestaMicuentaLbl);
            this.groupBox1.Controls.Add(this.miCuentaBtn);
            this.groupBox1.Controls.Add(this.registrarseRadioBtn);
            this.groupBox1.Controls.Add(this.iniciarRadioBtn);
            this.groupBox1.Controls.Add(this.contraseñaLbl);
            this.groupBox1.Controls.Add(this.usuarioLbl);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11F);
            this.groupBox1.Location = new System.Drawing.Point(45, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 235);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ACCESO A TU CUENTA";
            this.groupBox1.Visible = false;
            // 
            // nombreBox
            // 
            this.nombreBox.Location = new System.Drawing.Point(307, 116);
            this.nombreBox.Name = "nombreBox";
            this.nombreBox.Size = new System.Drawing.Size(208, 30);
            this.nombreBox.TabIndex = 17;
            this.nombreBox.Visible = false;
            // 
            // nombreLbl
            // 
            this.nombreLbl.AutoSize = true;
            this.nombreLbl.Location = new System.Drawing.Point(223, 123);
            this.nombreLbl.Name = "nombreLbl";
            this.nombreLbl.Size = new System.Drawing.Size(78, 23);
            this.nombreLbl.TabIndex = 16;
            this.nombreLbl.Text = "Nombre:";
            this.nombreLbl.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Calibri", 9F);
            this.checkBox1.Location = new System.Drawing.Point(339, 91);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(149, 22);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Mostrar contraseña";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // contraseñaBox
            // 
            this.contraseñaBox.Location = new System.Drawing.Point(307, 58);
            this.contraseñaBox.Name = "contraseñaBox";
            this.contraseñaBox.Size = new System.Drawing.Size(208, 30);
            this.contraseñaBox.TabIndex = 12;
            this.contraseñaBox.UseSystemPasswordChar = true;
            // 
            // respuestaMicuentaBox
            // 
            this.respuestaMicuentaBox.Location = new System.Drawing.Point(88, 192);
            this.respuestaMicuentaBox.Name = "respuestaMicuentaBox";
            this.respuestaMicuentaBox.Size = new System.Drawing.Size(243, 30);
            this.respuestaMicuentaBox.TabIndex = 10;
            // 
            // usuarioBox
            // 
            this.usuarioBox.FormattingEnabled = true;
            this.usuarioBox.Location = new System.Drawing.Point(307, 22);
            this.usuarioBox.Name = "usuarioBox";
            this.usuarioBox.Size = new System.Drawing.Size(208, 30);
            this.usuarioBox.TabIndex = 11;
            this.usuarioBox.SelectedIndexChanged += new System.EventHandler(this.usuarioBox_SelectedIndexChanged);
            // 
            // respuestaMicuentaLbl
            // 
            this.respuestaMicuentaLbl.AutoSize = true;
            this.respuestaMicuentaLbl.Location = new System.Drawing.Point(15, 199);
            this.respuestaMicuentaLbl.Name = "respuestaMicuentaLbl";
            this.respuestaMicuentaLbl.Size = new System.Drawing.Size(67, 23);
            this.respuestaMicuentaLbl.TabIndex = 7;
            this.respuestaMicuentaLbl.Text = "Estado:";
            // 
            // miCuentaBtn
            // 
            this.miCuentaBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.miCuentaBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.miCuentaBtn.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.miCuentaBtn.Location = new System.Drawing.Point(370, 158);
            this.miCuentaBtn.Name = "miCuentaBtn";
            this.miCuentaBtn.Size = new System.Drawing.Size(145, 64);
            this.miCuentaBtn.TabIndex = 6;
            this.miCuentaBtn.Text = "INICIAR SESION";
            this.miCuentaBtn.UseVisualStyleBackColor = false;
            this.miCuentaBtn.Click += new System.EventHandler(this.miCuentaBtn_Click);
            // 
            // registrarseRadioBtn
            // 
            this.registrarseRadioBtn.AutoSize = true;
            this.registrarseRadioBtn.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.registrarseRadioBtn.Location = new System.Drawing.Point(19, 91);
            this.registrarseRadioBtn.Name = "registrarseRadioBtn";
            this.registrarseRadioBtn.Size = new System.Drawing.Size(122, 27);
            this.registrarseRadioBtn.TabIndex = 5;
            this.registrarseRadioBtn.TabStop = true;
            this.registrarseRadioBtn.Text = "Registrarse";
            this.registrarseRadioBtn.UseVisualStyleBackColor = true;
            // 
            // iniciarRadioBtn
            // 
            this.iniciarRadioBtn.AutoSize = true;
            this.iniciarRadioBtn.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.iniciarRadioBtn.Location = new System.Drawing.Point(19, 63);
            this.iniciarRadioBtn.Name = "iniciarRadioBtn";
            this.iniciarRadioBtn.Size = new System.Drawing.Size(135, 27);
            this.iniciarRadioBtn.TabIndex = 4;
            this.iniciarRadioBtn.TabStop = true;
            this.iniciarRadioBtn.Text = "Iniciar sesión";
            this.iniciarRadioBtn.UseVisualStyleBackColor = true;
            this.iniciarRadioBtn.CheckedChanged += new System.EventHandler(this.iniciarRadioBtn_CheckedChanged);
            // 
            // contraseñaLbl
            // 
            this.contraseñaLbl.AutoSize = true;
            this.contraseñaLbl.Location = new System.Drawing.Point(198, 65);
            this.contraseñaLbl.Name = "contraseñaLbl";
            this.contraseñaLbl.Size = new System.Drawing.Size(103, 23);
            this.contraseñaLbl.TabIndex = 3;
            this.contraseñaLbl.Text = "Contraseña:";
            // 
            // usuarioLbl
            // 
            this.usuarioLbl.AutoSize = true;
            this.usuarioLbl.Location = new System.Drawing.Point(226, 29);
            this.usuarioLbl.Name = "usuarioLbl";
            this.usuarioLbl.Size = new System.Drawing.Size(75, 23);
            this.usuarioLbl.TabIndex = 2;
            this.usuarioLbl.Text = "Usuario:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.calendar);
            this.groupBox2.Controls.Add(this.duracionBox);
            this.groupBox2.Controls.Add(this.ConsultaBtn);
            this.groupBox2.Controls.Add(this.respuestaConsultaBox);
            this.groupBox2.Controls.Add(this.ganadorLbl);
            this.groupBox2.Controls.Add(this.fechaRadioBtn);
            this.groupBox2.Controls.Add(this.duracionRadioBtn);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 11F);
            this.groupBox2.Location = new System.Drawing.Point(45, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(536, 381);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CONSULTAR GANADOR";
            this.groupBox2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "segundos";
            // 
            // calendar
            // 
            this.calendar.Location = new System.Drawing.Point(45, 89);
            this.calendar.Name = "calendar";
            this.calendar.TabIndex = 11;
            // 
            // duracionBox
            // 
            this.duracionBox.Location = new System.Drawing.Point(386, 89);
            this.duracionBox.Name = "duracionBox";
            this.duracionBox.Size = new System.Drawing.Size(39, 30);
            this.duracionBox.TabIndex = 10;
            // 
            // ConsultaBtn
            // 
            this.ConsultaBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ConsultaBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ConsultaBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConsultaBtn.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.ConsultaBtn.Location = new System.Drawing.Point(370, 258);
            this.ConsultaBtn.Name = "ConsultaBtn";
            this.ConsultaBtn.Size = new System.Drawing.Size(145, 38);
            this.ConsultaBtn.TabIndex = 8;
            this.ConsultaBtn.Text = "CONSULTAR";
            this.ConsultaBtn.UseVisualStyleBackColor = false;
            this.ConsultaBtn.Click += new System.EventHandler(this.ConsultaBtn_Click);
            // 
            // respuestaConsultaBox
            // 
            this.respuestaConsultaBox.Location = new System.Drawing.Point(122, 333);
            this.respuestaConsultaBox.Name = "respuestaConsultaBox";
            this.respuestaConsultaBox.ReadOnly = true;
            this.respuestaConsultaBox.Size = new System.Drawing.Size(278, 30);
            this.respuestaConsultaBox.TabIndex = 9;
            // 
            // ganadorLbl
            // 
            this.ganadorLbl.AutoSize = true;
            this.ganadorLbl.Location = new System.Drawing.Point(26, 340);
            this.ganadorLbl.Name = "ganadorLbl";
            this.ganadorLbl.Size = new System.Drawing.Size(90, 23);
            this.ganadorLbl.TabIndex = 8;
            this.ganadorLbl.Text = "Ganador : ";
            // 
            // fechaRadioBtn
            // 
            this.fechaRadioBtn.AutoSize = true;
            this.fechaRadioBtn.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.fechaRadioBtn.Location = new System.Drawing.Point(88, 37);
            this.fechaRadioBtn.Name = "fechaRadioBtn";
            this.fechaRadioBtn.Size = new System.Drawing.Size(105, 27);
            this.fechaRadioBtn.TabIndex = 6;
            this.fechaRadioBtn.TabStop = true;
            this.fechaRadioBtn.Text = "Por fecha";
            this.fechaRadioBtn.UseVisualStyleBackColor = true;
            // 
            // duracionRadioBtn
            // 
            this.duracionRadioBtn.AutoSize = true;
            this.duracionRadioBtn.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.duracionRadioBtn.Location = new System.Drawing.Point(370, 37);
            this.duracionRadioBtn.Name = "duracionRadioBtn";
            this.duracionRadioBtn.Size = new System.Drawing.Size(131, 27);
            this.duracionRadioBtn.TabIndex = 5;
            this.duracionRadioBtn.TabStop = true;
            this.duracionRadioBtn.Text = "Por duración";
            this.duracionRadioBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DesconectarBtn);
            this.groupBox3.Controls.Add(this.ConectarBtn);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 11F);
            this.groupBox3.Location = new System.Drawing.Point(902, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 244);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ACCESO AL SERVIDOR";
            // 
            // DesconectarBtn
            // 
            this.DesconectarBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.DesconectarBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DesconectarBtn.Location = new System.Drawing.Point(6, 142);
            this.DesconectarBtn.Name = "DesconectarBtn";
            this.DesconectarBtn.Size = new System.Drawing.Size(172, 38);
            this.DesconectarBtn.TabIndex = 8;
            this.DesconectarBtn.Text = "Desconectar";
            this.DesconectarBtn.UseVisualStyleBackColor = false;
            this.DesconectarBtn.Click += new System.EventHandler(this.DesconectarBtn_Click);
            // 
            // ConectarBtn
            // 
            this.ConectarBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ConectarBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConectarBtn.Location = new System.Drawing.Point(6, 98);
            this.ConectarBtn.Name = "ConectarBtn";
            this.ConectarBtn.Size = new System.Drawing.Size(172, 38);
            this.ConectarBtn.TabIndex = 9;
            this.ConectarBtn.Text = "Conectar";
            this.ConectarBtn.UseVisualStyleBackColor = false;
            this.ConectarBtn.Click += new System.EventHandler(this.ConectarBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.listBox1);
            this.groupBox4.Controls.Add(this.InvitarBtn);
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 11F);
            this.groupBox4.Location = new System.Drawing.Point(587, 268);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(499, 381);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "JUGADORES EN LÍNEA";
            this.groupBox4.UseCompatibleTextRendering = true;
            this.groupBox4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F);
            this.label3.Location = new System.Drawing.Point(46, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(322, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "Para seleccionar +1 jugador, mantén pulsado \"Ctrl\".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9F);
            this.label2.Location = new System.Drawing.Point(46, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(343, 18);
            this.label2.TabIndex = 12;
            this.label2.Text = "Selecciona en la lista los jugadores que quieres invitar.";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Calibri", 15F);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 31;
            this.listBox1.Location = new System.Drawing.Point(33, 37);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(436, 221);
            this.listBox1.TabIndex = 11;
            // 
            // InvitarBtn
            // 
            this.InvitarBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.InvitarBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InvitarBtn.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.InvitarBtn.Location = new System.Drawing.Point(179, 325);
            this.InvitarBtn.Name = "InvitarBtn";
            this.InvitarBtn.Size = new System.Drawing.Size(146, 38);
            this.InvitarBtn.TabIndex = 10;
            this.InvitarBtn.Text = "INVITAR";
            this.InvitarBtn.UseVisualStyleBackColor = false;
            this.InvitarBtn.Click += new System.EventHandler(this.InvitarBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.holaLbl);
            this.groupBox5.Font = new System.Drawing.Font("Calibri", 11F);
            this.groupBox5.Location = new System.Drawing.Point(587, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(309, 244);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "TU PERFIL";
            this.groupBox5.Visible = false;
            // 
            // holaLbl
            // 
            this.holaLbl.AutoSize = true;
            this.holaLbl.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.holaLbl.Location = new System.Drawing.Point(45, 42);
            this.holaLbl.Name = "holaLbl";
            this.holaLbl.Size = new System.Drawing.Size(0, 23);
            this.holaLbl.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1098, 681);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "INICIO";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label respuestaMicuentaLbl;
        private System.Windows.Forms.Button miCuentaBtn;
        private System.Windows.Forms.RadioButton registrarseRadioBtn;
        private System.Windows.Forms.RadioButton iniciarRadioBtn;
        private System.Windows.Forms.Label contraseñaLbl;
        private System.Windows.Forms.Label usuarioLbl;
        private System.Windows.Forms.Button ConsultaBtn;
        private System.Windows.Forms.TextBox respuestaConsultaBox;
        private System.Windows.Forms.Label ganadorLbl;
        private System.Windows.Forms.RadioButton fechaRadioBtn;
        private System.Windows.Forms.RadioButton duracionRadioBtn;
        private System.Windows.Forms.Button DesconectarBtn;
        private System.Windows.Forms.Button ConectarBtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button InvitarBtn;
        private System.Windows.Forms.TextBox respuestaMicuentaBox;
        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.TextBox duracionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox usuarioBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label holaLbl;
        private System.Windows.Forms.TextBox contraseñaBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombreBox;
        private System.Windows.Forms.Label nombreLbl;
    }
}

