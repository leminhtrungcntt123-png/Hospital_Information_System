using HIS.WinForms.Controls;

namespace HIS.WinForms.Dialogs;

partial class NewReceptionDialog
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

    #region Component Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        dialogShell = new Panel();
        bodyLayout = new TableLayoutPanel();
        cardPatient = new CardPanel();
        lblPatientSection = new Label();
        lblPatientHint = new Label();
        lblLookupCaption = new Label();
        txtLookup = new TextBox();
        btnLookup = new Button();
        lblLookupResult = new Label();
        lnkResetPatient = new LinkLabel();
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblDateOfBirth = new Label();
        dtpDateOfBirth = new DateTimePicker();
        lblGender = new Label();
        cboGender = new ComboBox();
        lblPhone = new Label();
        txtPhone = new TextBox();
        lblNationalId = new Label();
        txtNationalId = new TextBox();
        lblInsurance = new Label();
        txtInsurance = new TextBox();
        lblAddress = new Label();
        txtAddress = new TextBox();
        cardRegistration = new CardPanel();
        lblRegistrationSection = new Label();
        lblRegistrationHint = new Label();
        lblDepartment = new Label();
        cboDepartment = new ComboBox();
        lblDoctor = new Label();
        cboDoctor = new ComboBox();
        lblPriority = new Label();
        cboPriority = new ComboBox();
        lblReason = new Label();
        txtReason = new TextBox();
        lblProcessNote = new Label();
        footerPanel = new Panel();
        footerTopLine = new Panel();
        lblSecurity = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        btnCloseFooter = new Button();
        headerPanel = new Panel();
        headerLine = new Panel();
        iconPanel = new Panel();
        lblDialogIcon = new Label();
        lblDialogTitle = new Label();
        lblDialogSubtitle = new Label();
        btnCloseDialog = new Button();
        dialogShell.SuspendLayout();
        bodyLayout.SuspendLayout();
        cardPatient.SuspendLayout();
        cardRegistration.SuspendLayout();
        footerPanel.SuspendLayout();
        headerPanel.SuspendLayout();
        iconPanel.SuspendLayout();
        SuspendLayout();
        // 
        // dialogShell
        // 
        dialogShell.BackColor = Color.White;
        dialogShell.Controls.Add(bodyLayout);
        dialogShell.Controls.Add(footerPanel);
        dialogShell.Controls.Add(headerPanel);
        dialogShell.Dock = DockStyle.Fill;
        dialogShell.Location = new Point(1, 1);
        dialogShell.Name = "dialogShell";
        dialogShell.Size = new Size(898, 698);
        dialogShell.TabIndex = 0;
        // 
        // bodyLayout
        // 
        bodyLayout.BackColor = Color.White;
        bodyLayout.ColumnCount = 2;
        bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57F));
        bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
        bodyLayout.Controls.Add(cardPatient, 0, 0);
        bodyLayout.Controls.Add(cardRegistration, 1, 0);
        bodyLayout.Dock = DockStyle.Fill;
        bodyLayout.Location = new Point(0, 68);
        bodyLayout.Name = "bodyLayout";
        bodyLayout.Padding = new Padding(16, 14, 16, 14);
        bodyLayout.RowCount = 1;
        bodyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bodyLayout.Size = new Size(898, 562);
        bodyLayout.TabIndex = 1;
        // 
        // cardPatient
        // 
        cardPatient.BackColor = Color.White;
        cardPatient.Controls.Add(lblPatientSection);
        cardPatient.Controls.Add(lblPatientHint);
        cardPatient.Controls.Add(lblLookupCaption);
        cardPatient.Controls.Add(txtLookup);
        cardPatient.Controls.Add(btnLookup);
        cardPatient.Controls.Add(lblLookupResult);
        cardPatient.Controls.Add(lnkResetPatient);
        cardPatient.Controls.Add(lblFullName);
        cardPatient.Controls.Add(txtFullName);
        cardPatient.Controls.Add(lblDateOfBirth);
        cardPatient.Controls.Add(dtpDateOfBirth);
        cardPatient.Controls.Add(lblGender);
        cardPatient.Controls.Add(cboGender);
        cardPatient.Controls.Add(lblPhone);
        cardPatient.Controls.Add(txtPhone);
        cardPatient.Controls.Add(lblNationalId);
        cardPatient.Controls.Add(txtNationalId);
        cardPatient.Controls.Add(lblInsurance);
        cardPatient.Controls.Add(txtInsurance);
        cardPatient.Controls.Add(lblAddress);
        cardPatient.Controls.Add(txtAddress);
        cardPatient.Dock = DockStyle.Fill;
        cardPatient.Location = new Point(16, 14);
        cardPatient.Margin = new Padding(0, 0, 8, 0);
        cardPatient.Name = "cardPatient";
        cardPatient.Padding = new Padding(1);
        cardPatient.Size = new Size(477, 534);
        cardPatient.TabIndex = 0;
        // 
        // lblPatientSection
        // 
        lblPatientSection.AutoSize = true;
        lblPatientSection.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblPatientSection.ForeColor = Color.FromArgb(37, 54, 74);
        lblPatientSection.Location = new Point(14, 10);
        lblPatientSection.Name = "lblPatientSection";
        lblPatientSection.Size = new Size(145, 17);
        lblPatientSection.TabIndex = 0;
        lblPatientSection.Text = "Thông tin bệnh nhân";
        // 
        // lblPatientHint
        // 
        lblPatientHint.AutoSize = true;
        lblPatientHint.Font = new Font("Segoe UI", 8.25F);
        lblPatientHint.ForeColor = Color.FromArgb(122, 135, 152);
        lblPatientHint.Location = new Point(14, 31);
        lblPatientHint.Name = "lblPatientHint";
        lblPatientHint.Size = new Size(224, 13);
        lblPatientHint.TabIndex = 1;
        lblPatientHint.Text = "Thông tin hành chính · * là trường bắt buộc";
        // 
        // lblLookupCaption
        // 
        lblLookupCaption.AutoSize = true;
        lblLookupCaption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblLookupCaption.ForeColor = Color.FromArgb(76, 90, 109);
        lblLookupCaption.Location = new Point(14, 56);
        lblLookupCaption.Name = "lblLookupCaption";
        lblLookupCaption.Size = new Size(130, 14);
        lblLookupCaption.TabIndex = 2;
        lblLookupCaption.Text = "Tra cứu hồ sơ có sẵn";
        // 
        // txtLookup
        // 
        txtLookup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtLookup.BorderStyle = BorderStyle.FixedSingle;
        txtLookup.Font = new Font("Segoe UI", 9.5F);
        txtLookup.Location = new Point(14, 76);
        txtLookup.Name = "txtLookup";
        txtLookup.PlaceholderText = "Nhập số điện thoại, CCCD hoặc mã BN";
        txtLookup.Size = new Size(344, 24);
        txtLookup.TabIndex = 3;
        // 
        // btnLookup
        // 
        btnLookup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLookup.BackColor = Color.White;
        btnLookup.Cursor = Cursors.Hand;
        btnLookup.FlatAppearance.BorderColor = Color.FromArgb(186, 196, 208);
        btnLookup.FlatAppearance.MouseDownBackColor = Color.FromArgb(232, 242, 253);
        btnLookup.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnLookup.FlatStyle = FlatStyle.Flat;
        btnLookup.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        btnLookup.ForeColor = Color.FromArgb(22, 102, 177);
        btnLookup.Location = new Point(366, 75);
        btnLookup.Name = "btnLookup";
        btnLookup.Size = new Size(96, 26);
        btnLookup.TabIndex = 4;
        btnLookup.Text = "Tìm hồ sơ";
        btnLookup.UseVisualStyleBackColor = false;
        // 
        // lblLookupResult
        // 
        lblLookupResult.AutoSize = false;
        lblLookupResult.Font = new Font("Segoe UI", 8.25F);
        lblLookupResult.ForeColor = Color.FromArgb(102, 115, 134);
        lblLookupResult.Location = new Point(14, 108);
        lblLookupResult.Name = "lblLookupResult";
        lblLookupResult.Size = new Size(448, 18);
        lblLookupResult.TabIndex = 5;
        lblLookupResult.Text = "Nhập thông tin để tra cứu hồ sơ đã có, hoặc bỏ trống để tạo bệnh nhân mới.";
        // 
        // lnkResetPatient
        // 
        lnkResetPatient.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lnkResetPatient.AutoSize = true;
        lnkResetPatient.Font = new Font("Segoe UI", 8.25F);
        lnkResetPatient.LinkColor = Color.FromArgb(22, 102, 177);
        lnkResetPatient.Location = new Point(330, 128);
        lnkResetPatient.Name = "lnkResetPatient";
        lnkResetPatient.Size = new Size(132, 13);
        lnkResetPatient.TabIndex = 6;
        lnkResetPatient.TabStop = true;
        lnkResetPatient.Text = "Nhập bệnh nhân mới";
        lnkResetPatient.Visible = false;
        // 
        // lblFullName
        // 
        lblFullName.AutoSize = true;
        lblFullName.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblFullName.ForeColor = Color.FromArgb(76, 90, 109);
        lblFullName.Location = new Point(14, 148);
        lblFullName.Name = "lblFullName";
        lblFullName.Size = new Size(93, 14);
        lblFullName.TabIndex = 7;
        lblFullName.Text = "Họ và tên *";
        // 
        // txtFullName
        // 
        txtFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtFullName.BorderStyle = BorderStyle.FixedSingle;
        txtFullName.Font = new Font("Segoe UI", 9.5F);
        txtFullName.Location = new Point(14, 168);
        txtFullName.Name = "txtFullName";
        txtFullName.PlaceholderText = "Nhập họ và tên bệnh nhân";
        txtFullName.Size = new Size(448, 24);
        txtFullName.TabIndex = 8;
        // 
        // lblDateOfBirth
        // 
        lblDateOfBirth.AutoSize = true;
        lblDateOfBirth.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblDateOfBirth.ForeColor = Color.FromArgb(76, 90, 109);
        lblDateOfBirth.Location = new Point(14, 206);
        lblDateOfBirth.Name = "lblDateOfBirth";
        lblDateOfBirth.Size = new Size(78, 14);
        lblDateOfBirth.TabIndex = 9;
        lblDateOfBirth.Text = "Ngày sinh *";
        // 
        // dtpDateOfBirth
        // 
        dtpDateOfBirth.CustomFormat = "dd/MM/yyyy";
        dtpDateOfBirth.Font = new Font("Segoe UI", 9.5F);
        dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
        dtpDateOfBirth.Location = new Point(14, 226);
        dtpDateOfBirth.Name = "dtpDateOfBirth";
        dtpDateOfBirth.ShowCheckBox = true;
        dtpDateOfBirth.Size = new Size(218, 24);
        dtpDateOfBirth.TabIndex = 10;
        // 
        // lblGender
        // 
        lblGender.AutoSize = true;
        lblGender.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblGender.ForeColor = Color.FromArgb(76, 90, 109);
        lblGender.Location = new Point(244, 206);
        lblGender.Name = "lblGender";
        lblGender.Size = new Size(66, 14);
        lblGender.TabIndex = 11;
        lblGender.Text = "Giới tính *";
        // 
        // cboGender
        // 
        cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGender.Font = new Font("Segoe UI", 9.5F);
        cboGender.FormattingEnabled = true;
        cboGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
        cboGender.Location = new Point(244, 226);
        cboGender.Name = "cboGender";
        cboGender.Size = new Size(218, 25);
        cboGender.TabIndex = 12;
        // 
        // lblPhone
        // 
        lblPhone.AutoSize = true;
        lblPhone.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblPhone.ForeColor = Color.FromArgb(76, 90, 109);
        lblPhone.Location = new Point(14, 264);
        lblPhone.Name = "lblPhone";
        lblPhone.Size = new Size(82, 14);
        lblPhone.TabIndex = 13;
        lblPhone.Text = "Số điện thoại *";
        // 
        // txtPhone
        // 
        txtPhone.BorderStyle = BorderStyle.FixedSingle;
        txtPhone.Font = new Font("Segoe UI", 9.5F);
        txtPhone.Location = new Point(14, 284);
        txtPhone.Name = "txtPhone";
        txtPhone.PlaceholderText = "Ví dụ: 0912345678";
        txtPhone.Size = new Size(218, 24);
        txtPhone.TabIndex = 14;
        // 
        // lblNationalId
        // 
        lblNationalId.AutoSize = true;
        lblNationalId.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblNationalId.ForeColor = Color.FromArgb(76, 90, 109);
        lblNationalId.Location = new Point(244, 264);
        lblNationalId.Name = "lblNationalId";
        lblNationalId.Size = new Size(97, 14);
        lblNationalId.TabIndex = 15;
        lblNationalId.Text = "Số CCCD / hộ chiếu";
        // 
        // txtNationalId
        // 
        txtNationalId.BorderStyle = BorderStyle.FixedSingle;
        txtNationalId.Font = new Font("Segoe UI", 9.5F);
        txtNationalId.Location = new Point(244, 284);
        txtNationalId.Name = "txtNationalId";
        txtNationalId.PlaceholderText = "Số định danh cá nhân";
        txtNationalId.Size = new Size(218, 24);
        txtNationalId.TabIndex = 16;
        // 
        // lblInsurance
        // 
        lblInsurance.AutoSize = true;
        lblInsurance.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblInsurance.ForeColor = Color.FromArgb(76, 90, 109);
        lblInsurance.Location = new Point(14, 322);
        lblInsurance.Name = "lblInsurance";
        lblInsurance.Size = new Size(55, 14);
        lblInsurance.TabIndex = 17;
        lblInsurance.Text = "Số BHYT";
        // 
        // txtInsurance
        // 
        txtInsurance.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtInsurance.BorderStyle = BorderStyle.FixedSingle;
        txtInsurance.Font = new Font("Segoe UI", 9.5F);
        txtInsurance.Location = new Point(14, 342);
        txtInsurance.Name = "txtInsurance";
        txtInsurance.PlaceholderText = "Mã thẻ bảo hiểm y tế (nếu có)";
        txtInsurance.Size = new Size(448, 24);
        txtInsurance.TabIndex = 18;
        // 
        // lblAddress
        // 
        lblAddress.AutoSize = true;
        lblAddress.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblAddress.ForeColor = Color.FromArgb(76, 90, 109);
        lblAddress.Location = new Point(14, 380);
        lblAddress.Name = "lblAddress";
        lblAddress.Size = new Size(46, 14);
        lblAddress.TabIndex = 19;
        lblAddress.Text = "Địa chỉ";
        // 
        // txtAddress
        // 
        txtAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtAddress.BorderStyle = BorderStyle.FixedSingle;
        txtAddress.Font = new Font("Segoe UI", 9.5F);
        txtAddress.Location = new Point(14, 400);
        txtAddress.Multiline = true;
        txtAddress.Name = "txtAddress";
        txtAddress.PlaceholderText = "Số nhà, đường, phường/xã, tỉnh/thành phố";
        txtAddress.Size = new Size(448, 48);
        txtAddress.TabIndex = 20;
        // 
        // cardRegistration
        // 
        cardRegistration.BackColor = Color.White;
        cardRegistration.Controls.Add(lblRegistrationSection);
        cardRegistration.Controls.Add(lblRegistrationHint);
        cardRegistration.Controls.Add(lblDepartment);
        cardRegistration.Controls.Add(cboDepartment);
        cardRegistration.Controls.Add(lblDoctor);
        cardRegistration.Controls.Add(cboDoctor);
        cardRegistration.Controls.Add(lblPriority);
        cardRegistration.Controls.Add(cboPriority);
        cardRegistration.Controls.Add(lblReason);
        cardRegistration.Controls.Add(txtReason);
        cardRegistration.Controls.Add(lblProcessNote);
        cardRegistration.Dock = DockStyle.Fill;
        cardRegistration.Location = new Point(509, 14);
        cardRegistration.Margin = new Padding(8, 0, 0, 0);
        cardRegistration.Name = "cardRegistration";
        cardRegistration.Padding = new Padding(1);
        cardRegistration.Size = new Size(357, 534);
        cardRegistration.TabIndex = 1;
        // 
        // lblRegistrationSection
        // 
        lblRegistrationSection.AutoSize = true;
        lblRegistrationSection.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblRegistrationSection.ForeColor = Color.FromArgb(37, 54, 74);
        lblRegistrationSection.Location = new Point(14, 10);
        lblRegistrationSection.Name = "lblRegistrationSection";
        lblRegistrationSection.Size = new Size(138, 17);
        lblRegistrationSection.TabIndex = 0;
        lblRegistrationSection.Text = "Đăng ký khám bệnh";
        // 
        // lblRegistrationHint
        // 
        lblRegistrationHint.AutoSize = true;
        lblRegistrationHint.Font = new Font("Segoe UI", 8.25F);
        lblRegistrationHint.ForeColor = Color.FromArgb(122, 135, 152);
        lblRegistrationHint.Location = new Point(14, 31);
        lblRegistrationHint.Name = "lblRegistrationHint";
        lblRegistrationHint.Size = new Size(215, 13);
        lblRegistrationHint.TabIndex = 1;
        lblRegistrationHint.Text = "Khoa/phòng và bác sĩ tiếp nhận bệnh nhân";
        // 
        // lblDepartment
        // 
        lblDepartment.AutoSize = true;
        lblDepartment.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblDepartment.ForeColor = Color.FromArgb(76, 90, 109);
        lblDepartment.Location = new Point(14, 60);
        lblDepartment.Name = "lblDepartment";
        lblDepartment.Size = new Size(88, 14);
        lblDepartment.TabIndex = 2;
        lblDepartment.Text = "Khoa / phòng *";
        // 
        // cboDepartment
        // 
        cboDepartment.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
        cboDepartment.Font = new Font("Segoe UI", 9.5F);
        cboDepartment.FormattingEnabled = true;
        cboDepartment.Location = new Point(14, 80);
        cboDepartment.Name = "cboDepartment";
        cboDepartment.Size = new Size(328, 25);
        cboDepartment.TabIndex = 3;
        // 
        // lblDoctor
        // 
        lblDoctor.AutoSize = true;
        lblDoctor.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblDoctor.ForeColor = Color.FromArgb(76, 90, 109);
        lblDoctor.Location = new Point(14, 122);
        lblDoctor.Name = "lblDoctor";
        lblDoctor.Size = new Size(67, 14);
        lblDoctor.TabIndex = 4;
        lblDoctor.Text = "Bác sĩ khám";
        // 
        // cboDoctor
        // 
        cboDoctor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
        cboDoctor.Font = new Font("Segoe UI", 9.5F);
        cboDoctor.FormattingEnabled = true;
        cboDoctor.Location = new Point(14, 142);
        cboDoctor.Name = "cboDoctor";
        cboDoctor.Size = new Size(328, 25);
        cboDoctor.TabIndex = 5;
        // 
        // lblPriority
        // 
        lblPriority.AutoSize = true;
        lblPriority.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblPriority.ForeColor = Color.FromArgb(76, 90, 109);
        lblPriority.Location = new Point(14, 184);
        lblPriority.Name = "lblPriority";
        lblPriority.Size = new Size(88, 14);
        lblPriority.TabIndex = 6;
        lblPriority.Text = "Mức ưu tiên";
        // 
        // cboPriority
        // 
        cboPriority.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboPriority.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPriority.Font = new Font("Segoe UI", 9.5F);
        cboPriority.FormattingEnabled = true;
        cboPriority.Items.AddRange(new object[] { "Bình thường", "Ưu tiên", "Người cao tuổi / trẻ em", "Cấp cứu" });
        cboPriority.Location = new Point(14, 204);
        cboPriority.Name = "cboPriority";
        cboPriority.Size = new Size(328, 25);
        cboPriority.TabIndex = 7;
        // 
        // lblReason
        // 
        lblReason.AutoSize = true;
        lblReason.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblReason.ForeColor = Color.FromArgb(76, 90, 109);
        lblReason.Location = new Point(14, 246);
        lblReason.Name = "lblReason";
        lblReason.Size = new Size(72, 14);
        lblReason.TabIndex = 8;
        lblReason.Text = "Lý do khám";
        // 
        // txtReason
        // 
        txtReason.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtReason.BorderStyle = BorderStyle.FixedSingle;
        txtReason.Font = new Font("Segoe UI", 9.5F);
        txtReason.Location = new Point(14, 266);
        txtReason.Multiline = true;
        txtReason.Name = "txtReason";
        txtReason.PlaceholderText = "Mô tả triệu chứng, lý do đến khám...";
        txtReason.Size = new Size(328, 96);
        txtReason.TabIndex = 9;
        // 
        // lblProcessNote
        // 
        lblProcessNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblProcessNote.AutoSize = false;
        lblProcessNote.BackColor = Color.FromArgb(241, 246, 251);
        lblProcessNote.Font = new Font("Segoe UI", 8.25F);
        lblProcessNote.ForeColor = Color.FromArgb(49, 92, 130);
        lblProcessNote.Location = new Point(14, 382);
        lblProcessNote.Name = "lblProcessNote";
        lblProcessNote.Padding = new Padding(12, 10, 12, 10);
        lblProcessNote.Size = new Size(328, 92);
        lblProcessNote.TabIndex = 10;
        lblProcessNote.Text = "Số thứ tự được cấp tự động sau khi lưu.\r\nBệnh nhân mới sẽ được tạo hồ sơ và tài khoản.\r\nVui lòng kiểm tra kỹ CCCD và thông tin BHYT.";
        // 
        // footerPanel
        // 
        footerPanel.BackColor = Color.FromArgb(247, 249, 251);
        footerPanel.Controls.Add(footerTopLine);
        footerPanel.Controls.Add(lblSecurity);
        footerPanel.Controls.Add(btnSave);
        footerPanel.Controls.Add(btnCancel);
        footerPanel.Controls.Add(btnCloseFooter);
        footerPanel.Dock = DockStyle.Bottom;
        footerPanel.Location = new Point(0, 630);
        footerPanel.Name = "footerPanel";
        footerPanel.Size = new Size(898, 68);
        footerPanel.TabIndex = 2;
        // 
        // footerTopLine
        // 
        footerTopLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        footerTopLine.BackColor = Color.FromArgb(212, 219, 228);
        footerTopLine.Location = new Point(0, 0);
        footerTopLine.Name = "footerTopLine";
        footerTopLine.Size = new Size(898, 1);
        footerTopLine.TabIndex = 0;
        // 
        // lblSecurity
        // 
        lblSecurity.AutoSize = true;
        lblSecurity.Font = new Font("Segoe UI", 8.25F);
        lblSecurity.ForeColor = Color.FromArgb(113, 128, 149);
        lblSecurity.Location = new Point(18, 28);
        lblSecurity.Name = "lblSecurity";
        lblSecurity.Size = new Size(267, 13);
        lblSecurity.TabIndex = 1;
        lblSecurity.Text = "Dữ liệu được bảo vệ theo quy định bảo mật y tế";
        // 
        // btnSave
        // 
        btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnSave.BackColor = Color.FromArgb(22, 102, 177);
        btnSave.Cursor = Cursors.Hand;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 79, 141);
        btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(15, 79, 141);
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(556, 20);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(150, 36);
        btnSave.TabIndex = 2;
        btnSave.Text = "Lưu và tiếp nhận";
        btnSave.UseVisualStyleBackColor = false;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.BackColor = Color.White;
        btnCancel.Cursor = Cursors.Hand;
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(186, 196, 208);
        btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(232, 242, 253);
        btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 9F);
        btnCancel.ForeColor = Color.FromArgb(74, 89, 108);
        btnCancel.Location = new Point(714, 20);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(80, 36);
        btnCancel.TabIndex = 3;
        btnCancel.Text = "Hủy";
        btnCancel.UseVisualStyleBackColor = false;
        // 
        // btnCloseFooter
        // 
        btnCloseFooter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCloseFooter.BackColor = Color.White;
        btnCloseFooter.Cursor = Cursors.Hand;
        btnCloseFooter.DialogResult = DialogResult.Cancel;
        btnCloseFooter.FlatAppearance.BorderColor = Color.FromArgb(186, 196, 208);
        btnCloseFooter.FlatAppearance.MouseDownBackColor = Color.FromArgb(232, 242, 253);
        btnCloseFooter.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnCloseFooter.FlatStyle = FlatStyle.Flat;
        btnCloseFooter.Font = new Font("Segoe UI", 9F);
        btnCloseFooter.ForeColor = Color.FromArgb(74, 89, 108);
        btnCloseFooter.Location = new Point(802, 20);
        btnCloseFooter.Name = "btnCloseFooter";
        btnCloseFooter.Size = new Size(80, 36);
        btnCloseFooter.TabIndex = 4;
        btnCloseFooter.Text = "Đóng";
        btnCloseFooter.UseVisualStyleBackColor = false;
        // 
        // headerPanel
        // 
        headerPanel.BackColor = Color.FromArgb(244, 247, 250);
        headerPanel.Controls.Add(headerLine);
        headerPanel.Controls.Add(iconPanel);
        headerPanel.Controls.Add(lblDialogTitle);
        headerPanel.Controls.Add(lblDialogSubtitle);
        headerPanel.Controls.Add(btnCloseDialog);
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Location = new Point(0, 0);
        headerPanel.Name = "headerPanel";
        headerPanel.Size = new Size(898, 68);
        headerPanel.TabIndex = 0;
        // 
        // headerLine
        // 
        headerLine.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        headerLine.BackColor = Color.FromArgb(212, 219, 228);
        headerLine.Location = new Point(0, 67);
        headerLine.Name = "headerLine";
        headerLine.Size = new Size(898, 1);
        headerLine.TabIndex = 4;
        // 
        // iconPanel
        // 
        iconPanel.BackColor = Color.FromArgb(232, 242, 253);
        iconPanel.Controls.Add(lblDialogIcon);
        iconPanel.Location = new Point(16, 14);
        iconPanel.Name = "iconPanel";
        iconPanel.Size = new Size(40, 40);
        iconPanel.TabIndex = 0;
        // 
        // lblDialogIcon
        // 
        lblDialogIcon.Dock = DockStyle.Fill;
        lblDialogIcon.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblDialogIcon.ForeColor = Color.FromArgb(22, 102, 177);
        lblDialogIcon.Location = new Point(0, 0);
        lblDialogIcon.Name = "lblDialogIcon";
        lblDialogIcon.Size = new Size(40, 40);
        lblDialogIcon.TabIndex = 0;
        lblDialogIcon.Text = "+";
        lblDialogIcon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblDialogTitle
        // 
        lblDialogTitle.AutoSize = true;
        lblDialogTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblDialogTitle.ForeColor = Color.FromArgb(23, 32, 51);
        lblDialogTitle.Location = new Point(66, 12);
        lblDialogTitle.Name = "lblDialogTitle";
        lblDialogTitle.Size = new Size(178, 21);
        lblDialogTitle.TabIndex = 1;
        lblDialogTitle.Text = "Tiếp nhận bệnh nhân";
        // 
        // lblDialogSubtitle
        // 
        lblDialogSubtitle.AutoSize = true;
        lblDialogSubtitle.Font = new Font("Segoe UI", 8.5F);
        lblDialogSubtitle.ForeColor = Color.FromArgb(115, 128, 147);
        lblDialogSubtitle.Location = new Point(68, 36);
        lblDialogSubtitle.Name = "lblDialogSubtitle";
        lblDialogSubtitle.Size = new Size(204, 14);
        lblDialogSubtitle.TabIndex = 2;
        lblDialogSubtitle.Text = "Tạo hồ sơ hành chính và đăng ký khám";
        // 
        // btnCloseDialog
        // 
        btnCloseDialog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCloseDialog.BackColor = Color.FromArgb(244, 247, 250);
        btnCloseDialog.Cursor = Cursors.Hand;
        btnCloseDialog.FlatAppearance.BorderSize = 0;
        btnCloseDialog.FlatAppearance.MouseDownBackColor = Color.FromArgb(225, 232, 240);
        btnCloseDialog.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 238, 245);
        btnCloseDialog.FlatStyle = FlatStyle.Flat;
        btnCloseDialog.Font = new Font("Segoe UI", 14F);
        btnCloseDialog.ForeColor = Color.FromArgb(101, 114, 134);
        btnCloseDialog.Location = new Point(852, 18);
        btnCloseDialog.Name = "btnCloseDialog";
        btnCloseDialog.Size = new Size(30, 30);
        btnCloseDialog.TabIndex = 3;
        btnCloseDialog.Text = "×";
        btnCloseDialog.UseVisualStyleBackColor = false;
        // 
        // NewReceptionDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(186, 196, 208);
        CancelButton = btnCloseFooter;
        ClientSize = new Size(900, 700);
        Controls.Add(dialogShell);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.None;
        KeyPreview = true;
        Name = "NewReceptionDialog";
        Padding = new Padding(1);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Tiếp nhận bệnh nhân";
        dialogShell.ResumeLayout(false);
        bodyLayout.ResumeLayout(false);
        cardPatient.ResumeLayout(false);
        cardPatient.PerformLayout();
        cardRegistration.ResumeLayout(false);
        cardRegistration.PerformLayout();
        footerPanel.ResumeLayout(false);
        footerPanel.PerformLayout();
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        iconPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel dialogShell;
    private TableLayoutPanel bodyLayout;
    private CardPanel cardPatient;
    private Label lblPatientSection;
    private Label lblPatientHint;
    private Label lblLookupCaption;
    private TextBox txtLookup;
    private Button btnLookup;
    private Label lblLookupResult;
    private LinkLabel lnkResetPatient;
    private Label lblFullName;
    private TextBox txtFullName;
    private Label lblDateOfBirth;
    private DateTimePicker dtpDateOfBirth;
    private Label lblGender;
    private ComboBox cboGender;
    private Label lblPhone;
    private TextBox txtPhone;
    private Label lblNationalId;
    private TextBox txtNationalId;
    private Label lblInsurance;
    private TextBox txtInsurance;
    private Label lblAddress;
    private TextBox txtAddress;
    private CardPanel cardRegistration;
    private Label lblRegistrationSection;
    private Label lblRegistrationHint;
    private Label lblDepartment;
    private ComboBox cboDepartment;
    private Label lblDoctor;
    private ComboBox cboDoctor;
    private Label lblPriority;
    private ComboBox cboPriority;
    private Label lblReason;
    private TextBox txtReason;
    private Label lblProcessNote;
    private Panel footerPanel;
    private Panel footerTopLine;
    private Label lblSecurity;
    private Button btnSave;
    private Button btnCancel;
    private Button btnCloseFooter;
    private Panel headerPanel;
    private Panel headerLine;
    private Panel iconPanel;
    private Label lblDialogIcon;
    private Label lblDialogTitle;
    private Label lblDialogSubtitle;
    private Button btnCloseDialog;
}
