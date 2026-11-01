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
        pnlBrand = new Panel();
        logoBox = new Panel();
        logoLabel = new Label();
        lblBrandName = new Label();
        lblBrandSub = new Label();
        pnlBrandDivider = new Panel();
        lblFeature1 = new Label();
        lblFeature2 = new Label();
        lblFeature3 = new Label();
        lblFacilityName = new Label();
        lblVersion = new Label();
        pnlForm = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        pnlFormDivider = new Panel();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        chkShowPassword = new CheckBox();
        lblError = new Label();
        btnLogin = new Button();
        lblFooter = new Label();
        pnlBrand.SuspendLayout();
        logoBox.SuspendLayout();
        pnlForm.SuspendLayout();
        SuspendLayout();
        // 
        // pnlBrand (Cột trái: Nhận diện thương hiệu & Thông tin hệ thống)
        // 
        pnlBrand.BackColor = Color.FromArgb(18, 53, 91);
        pnlBrand.Controls.Add(logoBox);
        pnlBrand.Controls.Add(lblBrandName);
        pnlBrand.Controls.Add(lblBrandSub);
        pnlBrand.Controls.Add(pnlBrandDivider);
        pnlBrand.Controls.Add(lblFeature1);
        pnlBrand.Controls.Add(lblFeature2);
        pnlBrand.Controls.Add(lblFeature3);
        pnlBrand.Controls.Add(lblFacilityName);
        pnlBrand.Controls.Add(lblVersion);
        pnlBrand.Dock = DockStyle.Left;
        pnlBrand.Location = new Point(0, 0);
        pnlBrand.Name = "pnlBrand";
        pnlBrand.Size = new Size(340, 520);
        pnlBrand.TabIndex = 0;
        // 
        // logoBox
        // 
        logoBox.BackColor = Color.White;
        logoBox.Controls.Add(logoLabel);
        logoBox.Location = new Point(44, 42);
        logoBox.Name = "logoBox";
        logoBox.Size = new Size(52, 52);
        logoBox.TabIndex = 0;
        // 
        // logoLabel
        // 
        logoLabel.Dock = DockStyle.Fill;
        logoLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        logoLabel.ForeColor = Color.FromArgb(18, 53, 91);
        logoLabel.Location = new Point(0, 0);
        logoLabel.Name = "logoLabel";
        logoLabel.Size = new Size(52, 52);
        logoLabel.TabIndex = 0;
        logoLabel.Text = "H";
        logoLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblBrandName
        // 
        lblBrandName.AutoSize = true;
        lblBrandName.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblBrandName.ForeColor = Color.White;
        lblBrandName.Location = new Point(38, 108);
        lblBrandName.Name = "lblBrandName";
        lblBrandName.Size = new Size(84, 50);
        lblBrandName.TabIndex = 1;
        lblBrandName.Text = "HIS";
        // 
        // lblBrandSub
        // 
        lblBrandSub.AutoSize = true;
        lblBrandSub.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblBrandSub.ForeColor = Color.FromArgb(184, 208, 232);
        lblBrandSub.Location = new Point(42, 160);
        lblBrandSub.Name = "lblBrandSub";
        lblBrandSub.Size = new Size(244, 19);
        lblBrandSub.TabIndex = 2;
        lblBrandSub.Text = "HỆ THỐNG QUẢN LÝ BỆNH VIỆN";
        lblBrandSub.UseMnemonic = false;
        // 
        // pnlBrandDivider
        // 
        pnlBrandDivider.BackColor = Color.FromArgb(40, 82, 126);
        pnlBrandDivider.Location = new Point(42, 194);
        pnlBrandDivider.Name = "pnlBrandDivider";
        pnlBrandDivider.Size = new Size(256, 1);
        pnlBrandDivider.TabIndex = 3;
        // 
        // lblFeature1
        // 
        lblFeature1.AutoSize = true;
        lblFeature1.Font = new Font("Segoe UI", 9F);
        lblFeature1.ForeColor = Color.FromArgb(220, 235, 250);
        lblFeature1.Location = new Point(42, 218);
        lblFeature1.Name = "lblFeature1";
        lblFeature1.Size = new Size(254, 20);
        lblFeature1.TabIndex = 4;
        lblFeature1.Text = "✦ Tiếp đón & quản lý khám chữa bệnh";
        lblFeature1.UseMnemonic = false;
        // 
        // lblFeature2
        // 
        lblFeature2.AutoSize = true;
        lblFeature2.Font = new Font("Segoe UI", 9F);
        lblFeature2.ForeColor = Color.FromArgb(220, 235, 250);
        lblFeature2.Location = new Point(42, 254);
        lblFeature2.Name = "lblFeature2";
        lblFeature2.Size = new Size(240, 20);
        lblFeature2.TabIndex = 5;
        lblFeature2.Text = "✦ Lịch khám & điều phối phòng ban";
        lblFeature2.UseMnemonic = false;
        // 
        // lblFeature3
        // 
        lblFeature3.AutoSize = true;
        lblFeature3.Font = new Font("Segoe UI", 9F);
        lblFeature3.ForeColor = Color.FromArgb(220, 235, 250);
        lblFeature3.Location = new Point(42, 290);
        lblFeature3.Name = "lblFeature3";
        lblFeature3.Size = new Size(245, 20);
        lblFeature3.TabIndex = 6;
        lblFeature3.Text = "✦ Phân quyền & bảo mật chuẩn y tế";
        lblFeature3.UseMnemonic = false;
        // 
        // lblFacilityName
        // 
        lblFacilityName.AutoSize = true;
        lblFacilityName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblFacilityName.ForeColor = Color.White;
        lblFacilityName.Location = new Point(42, 435);
        lblFacilityName.Name = "lblFacilityName";
        lblFacilityName.Size = new Size(224, 20);
        lblFacilityName.TabIndex = 7;
        lblFacilityName.Text = "Bệnh viện Đa khoa Trung tâm";
        lblFacilityName.UseMnemonic = false;
        // 
        // lblVersion
        // 
        lblVersion.AutoSize = true;
        lblVersion.Font = new Font("Segoe UI", 8F);
        lblVersion.ForeColor = Color.FromArgb(146, 171, 192);
        lblVersion.Location = new Point(42, 460);
        lblVersion.Name = "lblVersion";
        lblVersion.Size = new Size(218, 19);
        lblVersion.TabIndex = 8;
        lblVersion.Text = "Phiên bản 2.5 • Chuẩn HIS Bộ Y Tế";
        lblVersion.UseMnemonic = false;
        // 
        // pnlForm (Cột phải: Biểu mẫu đăng nhập)
        // 
        pnlForm.BackColor = Color.White;
        pnlForm.Controls.Add(lblTitle);
        pnlForm.Controls.Add(lblSubtitle);
        pnlForm.Controls.Add(pnlFormDivider);
        pnlForm.Controls.Add(lblUsername);
        pnlForm.Controls.Add(txtUsername);
        pnlForm.Controls.Add(lblPassword);
        pnlForm.Controls.Add(txtPassword);
        pnlForm.Controls.Add(chkShowPassword);
        pnlForm.Controls.Add(lblError);
        pnlForm.Controls.Add(btnLogin);
        pnlForm.Controls.Add(lblFooter);
        pnlForm.Dock = DockStyle.Fill;
        pnlForm.Location = new Point(340, 0);
        pnlForm.Name = "pnlForm";
        pnlForm.Size = new Size(500, 520);
        pnlForm.TabIndex = 1;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(23, 32, 51);
        lblTitle.Location = new Point(56, 42);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(172, 41);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Đăng nhập";
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = Color.FromArgb(102, 115, 134);
        lblSubtitle.Location = new Point(56, 86);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(254, 20);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Vui lòng nhập tài khoản để làm việc";
        // 
        // pnlFormDivider
        // 
        pnlFormDivider.BackColor = Color.FromArgb(238, 242, 246);
        pnlFormDivider.Location = new Point(56, 118);
        pnlFormDivider.Name = "pnlFormDivider";
        pnlFormDivider.Size = new Size(388, 1);
        pnlFormDivider.TabIndex = 2;
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblUsername.ForeColor = Color.FromArgb(60, 72, 88);
        lblUsername.Location = new Point(56, 136);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(119, 19);
        lblUsername.TabIndex = 3;
        lblUsername.Text = "TÊN ĐĂNG NHẬP";
        // 
        // txtUsername
        // 
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 11.5F);
        txtUsername.ForeColor = Color.FromArgb(23, 32, 51);
        txtUsername.Location = new Point(56, 158);
        txtUsername.Name = "txtUsername";
        txtUsername.PlaceholderText = "Nhập tên đăng nhập...";
        txtUsername.Size = new Size(388, 33);
        txtUsername.TabIndex = 4;
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblPassword.ForeColor = Color.FromArgb(60, 72, 88);
        lblPassword.Location = new Point(56, 205);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(81, 19);
        lblPassword.TabIndex = 5;
        lblPassword.Text = "MẬT KHẨU";
        // 
        // txtPassword
        // 
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 11.5F);
        txtPassword.ForeColor = Color.FromArgb(23, 32, 51);
        txtPassword.Location = new Point(56, 227);
        txtPassword.Name = "txtPassword";
        txtPassword.PlaceholderText = "Nhập mật khẩu...";
        txtPassword.Size = new Size(388, 33);
        txtPassword.TabIndex = 6;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // chkShowPassword
        // 
        chkShowPassword.AutoSize = true;
        chkShowPassword.Cursor = Cursors.Hand;
        chkShowPassword.Font = new Font("Segoe UI", 8.75F);
        chkShowPassword.ForeColor = Color.FromArgb(102, 115, 134);
        chkShowPassword.Location = new Point(56, 271);
        chkShowPassword.Name = "chkShowPassword";
        chkShowPassword.Size = new Size(130, 24);
        chkShowPassword.TabIndex = 7;
        chkShowPassword.Text = "Hiện mật khẩu";
        chkShowPassword.UseVisualStyleBackColor = true;
        // 
        // lblError
        // 
        lblError.Font = new Font("Segoe UI", 8.5F);
        lblError.ForeColor = Color.FromArgb(197, 61, 70);
        lblError.Location = new Point(56, 298);
        lblError.Name = "lblError";
        lblError.Size = new Size(388, 38);
        lblError.TabIndex = 8;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(22, 102, 177);
        btnLogin.Cursor = Cursors.Hand;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(15, 79, 141);
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(56, 342);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(388, 46);
        btnLogin.TabIndex = 9;
        btnLogin.Text = "ĐĂNG NHẬP VÀO HỆ THỐNG";
        btnLogin.UseVisualStyleBackColor = false;
        // 
        // lblFooter
        // 
        lblFooter.Font = new Font("Segoe UI", 8F);
        lblFooter.ForeColor = Color.FromArgb(146, 158, 174);
        lblFooter.Location = new Point(56, 455);
        lblFooter.Name = "lblFooter";
        lblFooter.Size = new Size(388, 20);
        lblFooter.TabIndex = 10;
        lblFooter.Text = "© 2026 HIS  •  Bệnh viện Đa khoa Trung tâm";
        lblFooter.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // LoginForm
        // 
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(840, 520);
        Controls.Add(pnlForm);
        Controls.Add(pnlBrand);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        ShowIcon = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "HIS - Đăng nhập hệ thống";
        pnlBrand.ResumeLayout(false);
        pnlBrand.PerformLayout();
        logoBox.ResumeLayout(false);
        pnlForm.ResumeLayout(false);
        pnlForm.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlBrand;
    private Panel logoBox;
    private Label logoLabel;
    private Label lblBrandName;
    private Label lblBrandSub;
    private Panel pnlBrandDivider;
    private Label lblFeature1;
    private Label lblFeature2;
    private Label lblFeature3;
    private Label lblFacilityName;
    private Label lblVersion;
    private Panel pnlForm;
    private Label lblTitle;
    private Label lblSubtitle;
    private Panel pnlFormDivider;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private CheckBox chkShowPassword;
    private Label lblError;
    private Button btnLogin;
    private Label lblFooter;
}