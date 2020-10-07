<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.label5 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.appData = New HierarchicalTreeView.ApplicationData()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.LabelTag = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.AppDataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.IsChannelLabel = New System.Windows.Forms.Label()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParentIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsChannelDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataTreeView1 = New DataTreeViewDLL.Chaliy.Windows.Forms.DataTreeView()
        CType(Me.appData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AppDataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imageList1
        '
        Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.imageList1.Images.SetKeyName(0, "NotSelected")
        Me.imageList1.Images.SetKeyName(1, "Selected")
        Me.imageList1.Images.SetKeyName(2, "")
        Me.imageList1.Images.SetKeyName(3, "")
        Me.imageList1.Images.SetKeyName(4, "endturn.png")
        '
        'label5
        '
        Me.label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.label5.Location = New System.Drawing.Point(12, 429)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(48, 16)
        Me.label5.TabIndex = 15
        Me.label5.Text = "Parent:"
        '
        'label4
        '
        Me.label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.label4.Location = New System.Drawing.Point(12, 404)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(48, 16)
        Me.label4.TabIndex = 14
        Me.label4.Text = "Name:"
        '
        'label1
        '
        Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.label1.Location = New System.Drawing.Point(12, 383)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(32, 15)
        Me.label1.TabIndex = 13
        Me.label1.Text = "ID:"
        '
        'appData
        '
        Me.appData.DataSetName = "ApplicationData"
        Me.appData.Locale = New System.Globalization.CultureInfo("en-US")
        Me.appData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'textBox1
        '
        Me.textBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.textBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.appData, "Test.Name", True))
        Me.textBox1.Location = New System.Drawing.Point(63, 401)
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(284, 20)
        Me.textBox1.TabIndex = 18
        Me.textBox1.Text = "textBox1"
        '
        'label3
        '
        Me.label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label3.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.appData, "Test.ParentID", True))
        Me.label3.Location = New System.Drawing.Point(63, 429)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(113, 16)
        Me.label3.TabIndex = 20
        Me.label3.Text = "label3"
        '
        'label2
        '
        Me.label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.appData, "Test.ID", True))
        Me.label2.Location = New System.Drawing.Point(63, 383)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(284, 15)
        Me.label2.TabIndex = 19
        Me.label2.Text = "label2"
        '
        'LabelTag
        '
        Me.LabelTag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelTag.AutoSize = True
        Me.LabelTag.Location = New System.Drawing.Point(248, 431)
        Me.LabelTag.Name = "LabelTag"
        Me.LabelTag.Size = New System.Drawing.Size(39, 13)
        Me.LabelTag.TabIndex = 21
        Me.LabelTag.Text = "Label6"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label7.Location = New System.Drawing.Point(182, 429)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 16)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Tag:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.NameDataGridViewTextBoxColumn, Me.ParentIDDataGridViewTextBoxColumn, Me.IsChannelDataGridViewCheckBoxColumn})
        Me.DataGridView1.DataMember = "Test"
        Me.DataGridView1.DataSource = Me.appData
        Me.DataGridView1.Location = New System.Drawing.Point(12, 204)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(395, 176)
        Me.DataGridView1.TabIndex = 22
        '
        'AppDataBindingSource
        '
        Me.AppDataBindingSource.DataSource = Me.appData
        Me.AppDataBindingSource.Position = 0
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'IsChannelLabel
        '
        Me.IsChannelLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IsChannelLabel.AutoSize = True
        Me.IsChannelLabel.BackColor = System.Drawing.Color.Red
        Me.IsChannelLabel.Location = New System.Drawing.Point(364, 408)
        Me.IsChannelLabel.Name = "IsChannelLabel"
        Me.IsChannelLabel.Size = New System.Drawing.Size(13, 13)
        Me.IsChannelLabel.TabIndex = 23
        Me.IsChannelLabel.Text = "  "
        Me.IsChannelLabel.Visible = False
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn.Visible = False
        Me.IDDataGridViewTextBoxColumn.Width = 43
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Имя"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        Me.NameDataGridViewTextBoxColumn.ReadOnly = True
        Me.NameDataGridViewTextBoxColumn.Width = 54
        '
        'ParentIDDataGridViewTextBoxColumn
        '
        Me.ParentIDDataGridViewTextBoxColumn.DataPropertyName = "ParentID"
        Me.ParentIDDataGridViewTextBoxColumn.HeaderText = "ParentID"
        Me.ParentIDDataGridViewTextBoxColumn.Name = "ParentIDDataGridViewTextBoxColumn"
        Me.ParentIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.ParentIDDataGridViewTextBoxColumn.Visible = False
        Me.ParentIDDataGridViewTextBoxColumn.Width = 74
        '
        'IsChannelDataGridViewCheckBoxColumn
        '
        Me.IsChannelDataGridViewCheckBoxColumn.DataPropertyName = "IsChannel"
        Me.IsChannelDataGridViewCheckBoxColumn.HeaderText = "Можно выбрать"
        Me.IsChannelDataGridViewCheckBoxColumn.Name = "IsChannelDataGridViewCheckBoxColumn"
        Me.IsChannelDataGridViewCheckBoxColumn.ReadOnly = True
        Me.IsChannelDataGridViewCheckBoxColumn.Width = 85
        '
        'DataTreeView1
        '
        Me.DataTreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataTreeView1.DataMember = "Test"
        Me.DataTreeView1.DataSource = Me.appData
        Me.DataTreeView1.FullRowSelect = True
        Me.DataTreeView1.HideSelection = False
        Me.DataTreeView1.HotTracking = True
        Me.DataTreeView1.IDColumn = "ID"
        Me.DataTreeView1.ImageIndex = 0
        Me.DataTreeView1.ImageList = Me.imageList1
        Me.DataTreeView1.IsChannelColumn = "IsChannel"
        Me.DataTreeView1.Location = New System.Drawing.Point(12, 12)
        Me.DataTreeView1.Name = "DataTreeView1"
        Me.DataTreeView1.NameColumn = "Name"
        Me.DataTreeView1.ParentIDColumn = "ParentID"
        Me.DataTreeView1.SelectedImageIndex = 1
        Me.DataTreeView1.Size = New System.Drawing.Size(395, 186)
        Me.DataTreeView1.TabIndex = 16
        Me.DataTreeView1.ValueColumn = "ID"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 453)
        Me.Controls.Add(Me.IsChannelLabel)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelTag)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.textBox1)
        Me.Controls.Add(Me.DataTreeView1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.appData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AppDataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents imageList1 As System.Windows.Forms.ImageList
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents DataTreeView1 As DataTreeViewDLL.Chaliy.Windows.Forms.DataTreeView
    Friend WithEvents appData As HierarchicalTreeView.ApplicationData
    Private WithEvents textBox1 As System.Windows.Forms.TextBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents LabelTag As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents AppDataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents IsChannelLabel As System.Windows.Forms.Label
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParentIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsChannelDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn

End Class
