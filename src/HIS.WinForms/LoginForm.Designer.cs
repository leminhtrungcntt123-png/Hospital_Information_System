namespace HIS.WinForms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        headerPanel = new Panel();
        logoPanel = new Panel();
        logoLabel = new Label();
        lblBrand = new Label();
        lblBrandSubtitle = new Label();
        lblFacility = new Label();
        cardPanel = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        chkShowPassword = new CheckBox();
        lblError = new Label();
        btnLogin = new Button();
        lblDemoHint = new Label();
        lblFooter = new Label();
        headerPanel.SuspendLayout();
        logoPanel.SuspendLayout();
        cardPanel.SuspendLayout();
        SuspendLayout();
        // 
        // headerPanel
        // 
        headerPanel.BackColor = Color.FromArgb(18, 53, 91);
        headerPanel.Controls.Add(logoPanel);
        headerPanel.Controls.Add(lblBrand);
        headerPanel.Controls.Add(lblBrandSubtitle);
        headerPanel.Controls.Add(lblFacility);
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Location = new Point(0, 0);
        headerPanel.Name = "headerPanel";
        headerPanel.Size = new Size(560, 148);
        headerPanel.TabIndex = 0;
        // 
        // logoPanel
        // 
        logoPanel.BackColor = Color.White;
        logoPanel.Controls.Add(logoLabel);
        logoPanel.Location = new Point(48, 42);
        logoPanel.Name = "logoPanel";
        logoPanel.Size = new Size(56, 56);
        logoPanel.TabIndex = 0;
        // 
        // logoLabel
        // 
        logoLabel.Dock = DockStyle.Fill;
        logoLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        logoLabel.ForeColor = Color.FromArgb(18, 53, 91);
        logoLabel.Location = new Point(0, 0);
        logoLabel.Name = "logoLabel";
        logoLabel.Size = new Size(56, 56);
        logoLabel.TabIndex = 0;
        logoLabel.Text = "H";
        logoLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblBrand
        // 
        lblBrand.AutoSize = true;
        lblBrand.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblBrand.ForeColor = Color.White;
        lblBrand.Location = new Point(120, 40);
        lblBrand.Name = "lblBrand";
        lblBrand.Size = new Size(88, 50);
        lblBrand.TabIndex = 1;
        lblBrand.Text = "HIS";
        // 
        // lblBrandSubtitle
        // 
        lblBrandSubtitle.AutoSize = true;
        lblBrandSubtitle.Font = new Font("Segoe UI", 9.5F);
        lblBrandSubtitle.ForeColor = Color.FromArgb(184, 200, 216);
        lblBrandSubtitle.Location = new Point(124, 88);
        lblBrandSubtitle.Name = "lblBrandSubtitle";
        lblBrandSubtitle.Size = new Size(198, 21);
        lblBrandSubtitle.TabIndex = 2;
        lblBrandSubtitle.Text = "Hệ thống quản lý bệnh viện";
        // 
        // lblFacility
        // 
        lblFacility.AutoSize = true;
        lblFacility.Font = new Font("Segoe UI", 8F);
        lblFacility.ForeColor = Color.FromArgb(146, 171, 192);
        lblFacility.Location = new Point(48, 116);
        lblFacility.Name = "lblFacility";
        lblFacility.Size = new Size(256, 19);
        lblFacility.TabIndex = 3;
        lblFacility.Text = "Bệnh viện Đa khoa Trung tâm  •  Phiên bản 2.5.1";
        // 
        // cardPanel
        // 
        cardPanel.BackColor = Color.White;
        cardPanel.Controls.Add(lblTitle);
        cardPanel.Controls.Add(lblSubtitle);
        cardPanel.Controls.Add(lblUsername);
        cardPanel.Controls.Add(txtUsername);
        cardPanel.Controls.Add(lblPassword);
        cardPanel.Controls.Add(txtPassword);
        cardPanel.Controls.Add(chkShowPassword);
        cardPanel.Controls.Add(lblError);
        cardPanel.Controls.Add(btnLogin);
        cardPanel.Location = new Point(44, 180);
        cardPanel.Name = "cardPanel";
        cardPanel.Size = new Size(472, 404);
        cardPanel.TabIndex = 1;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(23, 32, 51);
        lblTitle.Location = new Point(40, 26);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(256, 35);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Đăng nhập hệ thống";
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = Color.FromArgb(102, 115, 134);
        lblSubtitle.Location = new Point(40, 64);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(328, 20);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Vui lòng nhập tài khoản nhân sự để làm việc";
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblUsername.ForeColor = Color.FromArgb(23, 32, 51);
        lblUsername.Location = new Point(40, 102);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(112, 20);
        lblUsername.TabIndex = 2;
        lblUsername.Text = "Tên đăng nhập";
        // 
        // txtUsername
        // 
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 11F);
        txtUsername.ForeColor = Color.FromArgb(23, 32, 51);
        txtUsername.Location = new Point(40, 126);
        txtUsername.Name = "txtUsername";
        txtUsername.PlaceholderText = "Nhập tên đăng nhập";
        txtUsername.Size = new Size(392, 32);
        txtUsername.TabIndex = 3;
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPassword.ForeColor = Color.FromArgb(23, 32, 51);
        lblPassword.Location = new Point(40, 176);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(74, 20);
        lblPassword.TabIndex = 4;
        lblPassword.Text = "Mật khẩu";
        // 
        // txtPassword
        // 
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 11F);
        txtPassword.ForeColor = Color.FromArgb(23, 32, 51);
        txtPassword.Location = new Point(40, 200);
        txtPassword.Name = "txtPassword";
        txtPassword.PlaceholderText = "Nhập mật khẩu";
        txtPassword.Size = new Size(392, 32);
        txtPassword.TabIndex = 5;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // chkShowPassword
        // 
        chkShowPassword.AutoSize = true;
        chkShowPassword.Font = new Font("Segoe UI", 8.75F);
        chkShowPassword.ForeColor = Color.FromArgb(102, 115, 134);
        chkShowPassword.Location = new Point(40, 244);
        chkShowPassword.Name = "chkShowPassword";
        chkShowPassword.Size = new Size(130, 24);
        chkShowPassword.TabIndex = 6;
        chkShowPassword.Text = "Hiện mật khẩu";
        chkShowPassword.UseVisualStyleBackColor = true;
        // 
        // lblError
        // 
        lblError.Font = new Font("Segoe UI", 8.75F);
        lblError.ForeColor = Color.FromArgb(197, 61, 70);
        lblError.Location = new Point(40, 276);
        lblError.Name = "lblError";
        lblError.Size = new Size(392, 44);
        lblError.TabIndex = 7;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(22, 102, 177);
        btnLogin.Cursor = Cursors.Hand;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(26, 112, 184);
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(40, 324);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(392, 46);
        btnLogin.TabIndex = 8;
        btnLogin.Text = "Đăng nhập";
        btnLogin.UseVisualStyleBackColor = false;
        // 
        // lblDemoHint
        // 
        lblDemoHint.Font = new Font("Segoe UI", 8F);
        lblDemoHint.ForeColor = Color.FromArgb(102, 115, 134);
        lblDemoHint.Location = new Point(44, 596);
        lblDemoHint.Name = "lblDemoHint";
        lblDemoHint.Size = new Size(472, 20);
        lblDemoHint.TabIndex = 2;
        lblDemoHint.Text = "Tài khoản demo: admin / His@12345   •   td.tranthivan / His@12345";
        lblDemoHint.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblFooter
        // 
        lblFooter.Font = new Font("Segoe UI", 7.5F);
        lblFooter.ForeColor = Color.FromArgb(146, 158, 174);
        lblFooter.Location = new Point(44, 620);
        lblFooter.Name = "lblFooter";
        lblFooter.Size = new Size(472, 18);
        lblFooter.TabIndex = 3;
        lblFooter.Text = "© 2026 HIS  •  Bệnh viện Đa khoa Trung tâm";
        lblFooter.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // LoginForm
        // 
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(238, 242, 246);
        ClientSize = new Size(560, 660);
        Controls.Add(headerPanel);
        Controls.Add(cardPanel);
        Controls.Add(lblDemoHint);
        Controls.Add(lblFooter);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        ShowIcon = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "HIS - Đăng nhập hệ thống";
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        logoPanel.ResumeLayout(false);
        cardPanel.ResumeLayout(false);
        cardPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel headerPanel;
    private Panel logoPanel;
    private Label logoLabel;
    private Label lblBrand;
    private Label lblBrandSubtitle;
    private Label lblFacility;
    private Panel cardPanel;
    private Label lblTitle;
    private Label lblSubtitle;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private CheckBox chkShowPassword;
    private Label lblError;
    private Button btnLogin;
    private Label lblDemoHint;
    private Label lblFooter;
}
