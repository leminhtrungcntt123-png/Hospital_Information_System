using HIS.WinForms.Controls;

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
        titleBarPanel = new TitleBarPanel();
        logoMark = new Label();
        lblAppTitle = new Label();
        btnMinimizeLogin = new Button();
        btnCloseLogin = new Button();
        brandPanel = new Panel();
        lblBrandKicker = new Label();
        lblBrandTitle = new Label();
        lblBrandDesc = new Label();
        lblDot1 = new Label();
        lblFeature1 = new Label();
        lblDot2 = new Label();
        lblFeature2 = new Label();
        lblDot3 = new Label();
        lblFeature3 = new Label();
        lblVersion = new Label();
        loginHost = new Panel();
        card = new CardPanel();
        lblLoginKicker = new Label();
        lblLoginTitle = new Label();
        lblLoginSubtitle = new Label();
        lblUsernameCaption = new Label();
        pnlUsername = new Panel();
        txtUsername = new TextBox();
        lblPasswordCaption = new Label();
        pnlPassword = new Panel();
        txtPassword = new TextBox();
        btnTogglePassword = new Button();
        lblError = new Label();
        btnLogin = new Button();
        divider = new Panel();
        lblSupport = new Label();
        lblPatientNote = new Label();
        titleBarPanel.SuspendLayout();
        brandPanel.SuspendLayout();
        loginHost.SuspendLayout();
        card.SuspendLayout();
        pnlUsername.SuspendLayout();
        pnlPassword.SuspendLayout();
        SuspendLayout();
        // 
        // titleBarPanel
        // 
        titleBarPanel.Controls.Add(logoMark);
        titleBarPanel.Controls.Add(lblAppTitle);
        titleBarPanel.Controls.Add(btnMinimizeLogin);
        titleBarPanel.Controls.Add(btnCloseLogin);
        titleBarPanel.Dock = DockStyle.Top;
        titleBarPanel.Location = new Point(0, 0);
        titleBarPanel.Margin = new Padding(4, 5, 4, 5);
        titleBarPanel.Name = "titleBarPanel";
        titleBarPanel.Size = new Size(1371, 73);
        titleBarPanel.TabIndex = 0;
        // 
        // logoMark
        // 
        logoMark.BackColor = Color.White;
        logoMark.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
        logoMark.ForeColor = Color.FromArgb(18, 53, 91);
        logoMark.Location = new Point(23, 15);
        logoMark.Margin = new Padding(4, 0, 4, 0);
        logoMark.Name = "logoMark";
        logoMark.Size = new Size(37, 43);
        logoMark.TabIndex = 0;
        logoMark.Text = "H";
        logoMark.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblAppTitle
        // 
        lblAppTitle.AutoSize = true;
        lblAppTitle.BackColor = Color.Transparent;
        lblAppTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblAppTitle.ForeColor = Color.White;
        lblAppTitle.Location = new Point(74, 20);
        lblAppTitle.Margin = new Padding(4, 0, 4, 0);
        lblAppTitle.Name = "lblAppTitle";
        lblAppTitle.Size = new Size(333, 25);
        lblAppTitle.TabIndex = 1;
        lblAppTitle.Text = "HIS  ·  Hệ thống thông tin bệnh viện";
        // 
        // btnMinimizeLogin
        // 
        btnMinimizeLogin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnMinimizeLogin.BackColor = Color.FromArgb(20, 70, 120);
        btnMinimizeLogin.Cursor = Cursors.Hand;
        btnMinimizeLogin.FlatAppearance.BorderSize = 0;
        btnMinimizeLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(32, 95, 155);
        btnMinimizeLogin.FlatStyle = FlatStyle.Flat;
        btnMinimizeLogin.Font = new Font("Segoe UI", 9.5F);
        btnMinimizeLogin.ForeColor = Color.White;
        btnMinimizeLogin.Location = new Point(1240, 0);
        btnMinimizeLogin.Margin = new Padding(4, 5, 4, 5);
        btnMinimizeLogin.Name = "btnMinimizeLogin";
        btnMinimizeLogin.Size = new Size(66, 73);
        btnMinimizeLogin.TabIndex = 2;
        btnMinimizeLogin.Text = "—";
        btnMinimizeLogin.UseVisualStyleBackColor = false;
        // 
        // btnCloseLogin
        // 
        btnCloseLogin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCloseLogin.BackColor = Color.FromArgb(20, 70, 120);
        btnCloseLogin.Cursor = Cursors.Hand;
        btnCloseLogin.FlatAppearance.BorderSize = 0;
        btnCloseLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(197, 61, 70);
        btnCloseLogin.FlatStyle = FlatStyle.Flat;
        btnCloseLogin.Font = new Font("Segoe UI", 10.5F);
        btnCloseLogin.ForeColor = Color.White;
        btnCloseLogin.Location = new Point(1306, 0);
        btnCloseLogin.Margin = new Padding(4, 5, 4, 5);
        btnCloseLogin.Name = "btnCloseLogin";
        btnCloseLogin.Size = new Size(66, 73);
        btnCloseLogin.TabIndex = 3;
        btnCloseLogin.Text = "×";
        btnCloseLogin.UseVisualStyleBackColor = false;
        // 
        // brandPanel
        // 
        brandPanel.BackColor = Color.FromArgb(11, 39, 70);
        brandPanel.Controls.Add(lblBrandKicker);
        brandPanel.Controls.Add(lblBrandTitle);
        brandPanel.Controls.Add(lblBrandDesc);
        brandPanel.Controls.Add(lblDot1);
        brandPanel.Controls.Add(lblFeature1);
        brandPanel.Controls.Add(lblDot2);
        brandPanel.Controls.Add(lblFeature2);
        brandPanel.Controls.Add(lblDot3);
        brandPanel.Controls.Add(lblFeature3);
        brandPanel.Controls.Add(lblVersion);
        brandPanel.Dock = DockStyle.Left;
        brandPanel.Location = new Point(0, 73);
        brandPanel.Margin = new Padding(4, 5, 4, 5);
        brandPanel.Name = "brandPanel";
        brandPanel.Size = new Size(614, 927);
        brandPanel.TabIndex = 1;
        // 
        // lblBrandKicker
        // 
        lblBrandKicker.AutoSize = true;
        lblBrandKicker.BackColor = Color.Transparent;
        lblBrandKicker.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblBrandKicker.ForeColor = Color.FromArgb(140, 180, 220);
        lblBrandKicker.Location = new Point(69, 110);
        lblBrandKicker.Margin = new Padding(4, 0, 4, 0);
        lblBrandKicker.Name = "lblBrandKicker";
        lblBrandKicker.Size = new Size(251, 20);
        lblBrandKicker.TabIndex = 0;
        lblBrandKicker.Text = "HOSPITAL INFORMATION SYSTEM";
        // 
        // lblBrandTitle
        // 
        lblBrandTitle.BackColor = Color.Transparent;
        lblBrandTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
        lblBrandTitle.ForeColor = Color.White;
        lblBrandTitle.Location = new Point(63, 157);
        lblBrandTitle.Margin = new Padding(4, 0, 4, 0);
        lblBrandTitle.Name = "lblBrandTitle";
        lblBrandTitle.Size = new Size(500, 160);
        lblBrandTitle.TabIndex = 1;
        lblBrandTitle.Text = "Chăm sóc người bệnh,\r\nbắt đầu từ dữ liệu.";
        // 
        // lblBrandDesc
        // 
        lblBrandDesc.BackColor = Color.Transparent;
        lblBrandDesc.Font = new Font("Segoe UI", 9.5F);
        lblBrandDesc.ForeColor = Color.FromArgb(198, 214, 230);
        lblBrandDesc.Location = new Point(69, 340);
        lblBrandDesc.Margin = new Padding(4, 0, 4, 0);
        lblBrandDesc.Name = "lblBrandDesc";
        lblBrandDesc.Size = new Size(471, 87);
        lblBrandDesc.TabIndex = 2;
        lblBrandDesc.Text = "Một nền tảng thống nhất cho tiếp đón, khám chữa bệnh và vận hành bệnh viện.";
        // 
        // lblDot1
        // 
        lblDot1.AutoSize = true;
        lblDot1.BackColor = Color.Transparent;
        lblDot1.Font = new Font("Segoe UI", 7F);
        lblDot1.ForeColor = Color.FromArgb(101, 189, 255);
        lblDot1.Location = new Point(69, 477);
        lblDot1.Margin = new Padding(4, 0, 4, 0);
        lblDot1.Name = "lblDot1";
        lblDot1.Size = new Size(17, 19);
        lblDot1.TabIndex = 3;
        lblDot1.Text = "●";
        // 
        // lblFeature1
        // 
        lblFeature1.AutoSize = true;
        lblFeature1.BackColor = Color.Transparent;
        lblFeature1.Font = new Font("Segoe UI", 9.5F);
        lblFeature1.ForeColor = Color.FromArgb(226, 236, 246);
        lblFeature1.Location = new Point(97, 470);
        lblFeature1.Margin = new Padding(4, 0, 4, 0);
        lblFeature1.Name = "lblFeature1";
        lblFeature1.Size = new Size(246, 25);
        lblFeature1.TabIndex = 4;
        lblFeature1.Text = "Tiếp đón & lịch hẹn tập trung";
        // 
        // lblDot2
        // 
        lblDot2.AutoSize = true;
        lblDot2.BackColor = Color.Transparent;
        lblDot2.Font = new Font("Segoe UI", 7F);
        lblDot2.ForeColor = Color.FromArgb(101, 189, 255);
        lblDot2.Location = new Point(69, 543);
        lblDot2.Margin = new Padding(4, 0, 4, 0);
        lblDot2.Name = "lblDot2";
        lblDot2.Size = new Size(17, 19);
        lblDot2.TabIndex = 5;
        lblDot2.Text = "●";
        // 
        // lblFeature2
        // 
        lblFeature2.AutoSize = true;
        lblFeature2.BackColor = Color.Transparent;
        lblFeature2.Font = new Font("Segoe UI", 9.5F);
        lblFeature2.ForeColor = Color.FromArgb(226, 236, 246);
        lblFeature2.Location = new Point(97, 537);
        lblFeature2.Margin = new Padding(4, 0, 4, 0);
        lblFeature2.Name = "lblFeature2";
        lblFeature2.Size = new Size(267, 25);
        lblFeature2.TabIndex = 6;
        lblFeature2.Text = "Hồ sơ bệnh án điện tử an toàn";
        // 
        // lblDot3
        // 
        lblDot3.AutoSize = true;
        lblDot3.BackColor = Color.Transparent;
        lblDot3.Font = new Font("Segoe UI", 7F);
        lblDot3.ForeColor = Color.FromArgb(101, 189, 255);
        lblDot3.Location = new Point(69, 610);
        lblDot3.Margin = new Padding(4, 0, 4, 0);
        lblDot3.Name = "lblDot3";
        lblDot3.Size = new Size(17, 19);
        lblDot3.TabIndex = 7;
        lblDot3.Text = "●";
        // 
        // lblFeature3
        // 
        lblFeature3.AutoSize = true;
        lblFeature3.BackColor = Color.Transparent;
        lblFeature3.Font = new Font("Segoe UI", 9.5F);
        lblFeature3.ForeColor = Color.FromArgb(226, 236, 246);
        lblFeature3.Location = new Point(97, 603);
        lblFeature3.Margin = new Padding(4, 0, 4, 0);
        lblFeature3.Name = "lblFeature3";
        lblFeature3.Size = new Size(327, 25);
        lblFeature3.TabIndex = 8;
        lblFeature3.Text = "Báo cáo vận hành theo thời gian thực";
        // 
        // lblVersion
        // 
        lblVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblVersion.AutoSize = true;
        lblVersion.BackColor = Color.Transparent;
        lblVersion.Font = new Font("Segoe UI", 7.5F);
        lblVersion.ForeColor = Color.FromArgb(146, 171, 192);
        lblVersion.Location = new Point(69, 870);
        lblVersion.Margin = new Padding(4, 0, 4, 0);
        lblVersion.Name = "lblVersion";
        lblVersion.Size = new Size(311, 20);
        lblVersion.TabIndex = 9;
        lblVersion.Text = "Phiên bản 2.5.1   ·   © 2026 Bệnh viện Đa khoa";
        // 
        // loginHost
        // 
        loginHost.BackColor = Color.FromArgb(238, 242, 246);
        loginHost.Controls.Add(card);
        loginHost.Dock = DockStyle.Fill;
        loginHost.Location = new Point(614, 73);
        loginHost.Margin = new Padding(4, 5, 4, 5);
        loginHost.Name = "loginHost";
        loginHost.Size = new Size(757, 927);
        loginHost.TabIndex = 2;
        // 
        // card
        // 
        card.BackColor = Color.White;
        card.Controls.Add(lblLoginKicker);
        card.Controls.Add(lblLoginTitle);
        card.Controls.Add(lblLoginSubtitle);
        card.Controls.Add(lblUsernameCaption);
        card.Controls.Add(pnlUsername);
        card.Controls.Add(lblPasswordCaption);
        card.Controls.Add(pnlPassword);
        card.Controls.Add(lblError);
        card.Controls.Add(btnLogin);
        card.Controls.Add(divider);
        card.Controls.Add(lblSupport);
        card.Controls.Add(lblPatientNote);
        card.Location = new Point(28, 27);
        card.Margin = new Padding(4, 5, 4, 5);
        card.Name = "card";
        card.Padding = new Padding(1, 2, 1, 2);
        card.Size = new Size(700, 863);
        card.TabIndex = 0;
        // 
        // lblLoginKicker
        // 
        lblLoginKicker.AutoSize = true;
        lblLoginKicker.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblLoginKicker.ForeColor = Color.FromArgb(22, 102, 177);
        lblLoginKicker.Location = new Point(105, 67);
        lblLoginKicker.Margin = new Padding(4, 0, 4, 0);
        lblLoginKicker.Name = "lblLoginKicker";
        lblLoginKicker.Size = new Size(167, 20);
        lblLoginKicker.TabIndex = 0;
        lblLoginKicker.Text = "CHÀO MỪNG TRỞ LẠI";
        // 
        // lblLoginTitle
        // 
        lblLoginTitle.AutoSize = true;
        lblLoginTitle.Font = new Font("Segoe UI", 16.5F, FontStyle.Bold);
        lblLoginTitle.ForeColor = Color.FromArgb(23, 32, 51);
        lblLoginTitle.Location = new Point(105, 99);
        lblLoginTitle.Margin = new Padding(4, 0, 4, 0);
        lblLoginTitle.Name = "lblLoginTitle";
        lblLoginTitle.Size = new Size(338, 45);
        lblLoginTitle.TabIndex = 1;
        lblLoginTitle.Text = "Đăng nhập hệ thống";
        // 
        // lblLoginSubtitle
        // 
        lblLoginSubtitle.Font = new Font("Segoe UI", 8.75F);
        lblLoginSubtitle.ForeColor = Color.FromArgb(102, 115, 134);
        lblLoginSubtitle.Location = new Point(105, 169);
        lblLoginSubtitle.Margin = new Padding(4, 0, 4, 0);
        lblLoginSubtitle.Name = "lblLoginSubtitle";
        lblLoginSubtitle.Size = new Size(491, 30);
        lblLoginSubtitle.TabIndex = 2;
        lblLoginSubtitle.Text = "Sử dụng tài khoản nội bộ do phòng CNTT cấp.";
        // 
        // lblUsernameCaption
        // 
        lblUsernameCaption.AutoSize = true;
        lblUsernameCaption.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblUsernameCaption.ForeColor = Color.FromArgb(40, 54, 74);
        lblUsernameCaption.Location = new Point(105, 230);
        lblUsernameCaption.Margin = new Padding(4, 0, 4, 0);
        lblUsernameCaption.Name = "lblUsernameCaption";
        lblUsernameCaption.Size = new Size(128, 23);
        lblUsernameCaption.TabIndex = 3;
        lblUsernameCaption.Text = "Tên đăng nhập";
        // 
        // pnlUsername
        // 
        pnlUsername.BackColor = Color.FromArgb(247, 249, 252);
        pnlUsername.Controls.Add(txtUsername);
        pnlUsername.Location = new Point(105, 258);
        pnlUsername.Margin = new Padding(4, 5, 4, 5);
        pnlUsername.Name = "pnlUsername";
        pnlUsername.Size = new Size(491, 77);
        pnlUsername.TabIndex = 4;
        // 
        // txtUsername
        // 
        txtUsername.BackColor = Color.FromArgb(247, 249, 252);
        txtUsername.BorderStyle = BorderStyle.None;
        txtUsername.Font = new Font("Segoe UI", 10F);
        txtUsername.ForeColor = Color.FromArgb(23, 32, 51);
        txtUsername.Location = new Point(17, 20);
        txtUsername.Margin = new Padding(4, 5, 4, 5);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(457, 27);
        txtUsername.TabIndex = 0;
        // 
        // lblPasswordCaption
        // 
        lblPasswordCaption.AutoSize = true;
        lblPasswordCaption.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblPasswordCaption.ForeColor = Color.FromArgb(40, 54, 74);
        lblPasswordCaption.Location = new Point(105, 377);
        lblPasswordCaption.Margin = new Padding(4, 0, 4, 0);
        lblPasswordCaption.Name = "lblPasswordCaption";
        lblPasswordCaption.Size = new Size(86, 23);
        lblPasswordCaption.TabIndex = 5;
        lblPasswordCaption.Text = "Mật khẩu";
        // 
        // pnlPassword
        // 
        pnlPassword.BackColor = Color.FromArgb(247, 249, 252);
        pnlPassword.Controls.Add(txtPassword);
        pnlPassword.Controls.Add(btnTogglePassword);
        pnlPassword.Location = new Point(105, 405);
        pnlPassword.Margin = new Padding(4, 5, 4, 5);
        pnlPassword.Name = "pnlPassword";
        pnlPassword.Size = new Size(491, 77);
        pnlPassword.TabIndex = 6;
        // 
        // txtPassword
        // 
        txtPassword.BackColor = Color.FromArgb(247, 249, 252);
        txtPassword.BorderStyle = BorderStyle.None;
        txtPassword.Font = new Font("Segoe UI", 10F);
        txtPassword.ForeColor = Color.FromArgb(23, 32, 51);
        txtPassword.Location = new Point(17, 20);
        txtPassword.Margin = new Padding(4, 5, 4, 5);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(374, 27);
        txtPassword.TabIndex = 0;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // btnTogglePassword
        // 
        btnTogglePassword.BackColor = Color.Transparent;
        btnTogglePassword.Cursor = Cursors.Hand;
        btnTogglePassword.FlatAppearance.BorderSize = 0;
        btnTogglePassword.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnTogglePassword.FlatStyle = FlatStyle.Flat;
        btnTogglePassword.Font = new Font("Segoe UI", 8F);
        btnTogglePassword.ForeColor = Color.FromArgb(102, 115, 134);
        btnTogglePassword.Location = new Point(417, 0);
        btnTogglePassword.Margin = new Padding(4, 5, 4, 5);
        btnTogglePassword.Name = "btnTogglePassword";
        btnTogglePassword.Size = new Size(74, 77);
        btnTogglePassword.TabIndex = 1;
        btnTogglePassword.Text = "Hiện";
        btnTogglePassword.UseVisualStyleBackColor = false;
        // 
        // lblError
        // 
        lblError.Font = new Font("Segoe UI", 8.25F);
        lblError.ForeColor = Color.FromArgb(197, 61, 70);
        lblError.Location = new Point(57, 487);
        lblError.Margin = new Padding(4, 0, 4, 0);
        lblError.Name = "lblError";
        lblError.Size = new Size(491, 53);
        lblError.TabIndex = 7;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(22, 102, 177);
        btnLogin.Cursor = Cursors.Hand;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(11, 39, 70);
        btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(15, 79, 141);
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(105, 549);
        btnLogin.Margin = new Padding(4, 5, 4, 5);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(491, 80);
        btnLogin.TabIndex = 8;
        btnLogin.Text = "Đăng nhập";
        btnLogin.UseVisualStyleBackColor = false;
        // 
        // divider
        // 
        divider.BackColor = Color.FromArgb(226, 231, 238);
        divider.Location = new Point(105, 669);
        divider.Margin = new Padding(4, 5, 4, 5);
        divider.Name = "divider";
        divider.Size = new Size(491, 2);
        divider.TabIndex = 9;
        // 
        // lblSupport
        // 
        lblSupport.Font = new Font("Segoe UI", 7.75F);
        lblSupport.ForeColor = Color.FromArgb(102, 115, 134);
        lblSupport.Location = new Point(105, 697);
        lblSupport.Margin = new Padding(4, 0, 4, 0);
        lblSupport.Name = "lblSupport";
        lblSupport.Size = new Size(491, 30);
        lblSupport.TabIndex = 10;
        lblSupport.Text = "Quên mật khẩu? Liên hệ phòng CNTT · (028) 1234 5678";
        // 
        // lblPatientNote
        // 
        lblPatientNote.Font = new Font("Segoe UI", 7.75F);
        lblPatientNote.ForeColor = Color.FromArgb(22, 102, 177);
        lblPatientNote.Location = new Point(105, 727);
        lblPatientNote.Margin = new Padding(4, 0, 4, 0);
        lblPatientNote.Name = "lblPatientNote";
        lblPatientNote.Size = new Size(491, 30);
        lblPatientNote.TabIndex = 11;
        lblPatientNote.Text = "Tài khoản bệnh nhân vui lòng đăng nhập tại cổng web.";
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(1371, 1000);
        Controls.Add(loginHost);
        Controls.Add(brandPanel);
        Controls.Add(titleBarPanel);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.None;
        KeyPreview = true;
        Margin = new Padding(4, 5, 4, 5);
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Đăng nhập - HIS";
        titleBarPanel.ResumeLayout(false);
        titleBarPanel.PerformLayout();
        brandPanel.ResumeLayout(false);
        brandPanel.PerformLayout();
        loginHost.ResumeLayout(false);
        card.ResumeLayout(false);
        card.PerformLayout();
        pnlUsername.ResumeLayout(false);
        pnlUsername.PerformLayout();
        pnlPassword.ResumeLayout(false);
        pnlPassword.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TitleBarPanel titleBarPanel;
    private Label logoMark;
    private Label lblAppTitle;
    private Button btnMinimizeLogin;
    private Button btnCloseLogin;
    private Panel brandPanel;
    private Label lblBrandKicker;
    private Label lblBrandTitle;
    private Label lblBrandDesc;
    private Label lblDot1;
    private Label lblFeature1;
    private Label lblDot2;
    private Label lblFeature2;
    private Label lblDot3;
    private Label lblFeature3;
    private Label lblVersion;
    private Panel loginHost;
    private CardPanel card;
    private Label lblLoginKicker;
    private Label lblLoginTitle;
    private Label lblLoginSubtitle;
    private Label lblUsernameCaption;
    private Panel pnlUsername;
    private TextBox txtUsername;
    private Label lblPasswordCaption;
    private Panel pnlPassword;
    private TextBox txtPassword;
    private Button btnTogglePassword;
    private Label lblError;
    private Button btnLogin;
    private Panel divider;
    private Label lblSupport;
    private Label lblPatientNote;
}
