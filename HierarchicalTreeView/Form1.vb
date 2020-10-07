Imports DataTreeViewDLL.Chaliy.Windows.Forms.DataTreeView

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.appData.Test.AddTestRow("Шасси 1", "Шасси 1", "", False)
        Me.appData.Test.AddTestRow("<Шасси 1><Модуль 1>", "Модуль 1", "Шасси 1", False)
        Me.appData.Test.AddTestRow("<Шасси 1><Модуль 1><Канал 1>", "Канал 1", "<Шасси 1><Модуль 1>", True)
        Me.appData.Test.AddTestRow("<Шасси 1><Модуль 1><Канал 2>", "Канал 2", "<Шасси 1><Модуль 1>", True)
        Me.appData.Test.AddTestRow("<Шасси 1><Модуль 1><Канал 3>", "Канал 3", "<Шасси 1><Модуль 1>", True)

        Me.appData.Test.AddTestRow("<Шасси 1><Модуль 2>", "Модуль 2", "Шасси 1", False)
        Me.appData.Test.AddTestRow("<Шасси 1><Модуль 2><Канал 1>", "Канал 1", "<Шасси 1><Модуль 2>", True)
        Me.appData.Test.AddTestRow("<Шасси 1><Модуль 2><Канал 2>", "Канал 2", "<Шасси 1><Модуль 2>", True)
        Me.appData.Test.AddTestRow("<Шасси 1><Модуль 2><Канал 3>", "Канал 3", "<Шасси 1><Модуль 2>", True)

        Me.appData.Test.AddTestRow("Шасси 2", "Шасси 2", "", False)
        Me.appData.Test.AddTestRow("<Шасси 2>Модуль 1>", "Модуль 1", "Шасси 2", False)
        Me.appData.Test.AddTestRow("<Шасси 2><Модуль 1><Канал 1>", "Канал 1", "<Шасси 2>Модуль 1>", True)
        Me.appData.Test.AddTestRow("<Шасси 2><Модуль 1><Канал 2>", "Канал 2", "<Шасси 2>Модуль 1>", True)
        Me.appData.Test.AddTestRow("<Шасси 2><Модуль 1><Канал 3>", "Канал 3", "<Шасси 2>Модуль 1>", True)

        Me.appData.Test.AddTestRow("<Шасси 2><Модуль 2>", "Модуль 2", "Шасси 2", False)
        Me.appData.Test.AddTestRow("<Шасси 2><Модуль 2><Канал 1>", "Канал 1", "<Шасси 2><Модуль 2>", True)
        Me.appData.Test.AddTestRow("<Шасси 2><Модуль 2><Канал 2>", "Канал 2", "<Шасси 2><Модуль 2>", True)
        Me.appData.Test.AddTestRow("<Шасси 2><Модуль 2><Канал 3>", "Канал 3", "<Шасси 2><Модуль 2>", True)
    End Sub


    Private Sub DataTreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles DataTreeView1.AfterSelect
        IsChannelLabel.Visible = False

        Dim selNode As DataTreeViewNode = TryCast(DataTreeView1.SelectedNode, DataTreeViewNode)
        If selNode IsNot Nothing Then

            If selNode.IsChannel Then
                'selNode.ImageKey = "Selected"
                IsChannelLabel.Visible = True
            End If
            LabelTag.Text = selNode.Tag

            'RefreshTreeView()
            'Timer1.Enabled = True
        End If
    End Sub

    Private Sub RefreshTreeView()
        For Each rootNode As TreeNode In DataTreeView1.Nodes
            rootNode.ImageKey = "NotSelected"
            If rootNode.Nodes.Count > 0 Then
                RefreshNode(rootNode)
            End If
        Next
    End Sub

    Private Sub RefreshNode(UpNode As TreeNode)
        UpNode.ImageKey = "NotSelected"
        For Each mNode As TreeNode In UpNode.Nodes
            mNode.ImageKey = "NotSelected"
            If mNode.Nodes.Count > 0 Then
                RefreshNode(mNode)
            End If
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        RefreshTreeView()
        Dim selNode As DataTreeViewNode = TryCast(DataTreeView1.SelectedNode, DataTreeViewNode)
        If selNode IsNot Nothing Then
            If selNode.IsSelected Then
                selNode.ImageKey = "Selected"
            End If
        End If
    End Sub

End Class
