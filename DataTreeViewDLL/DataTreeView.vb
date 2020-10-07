Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports DataTreeViewDLL.Chaliy.Windows.Forms.Design

Namespace Chaliy.Windows.Forms
    ''' <summary>
    ''' Связывание с данными в иерархическом tree view контроле.
    ''' </summary>
    Public Class DataTreeView
        Inherits TreeView
        Const SB_HORZ As Integer = 0

#Region "Fields"

        Private components As Container = Nothing
        Private m_dataSource As Object
        Private m_dataMember As String
        Private listManager As CurrencyManager

        Private idPropertyName As String
        Private namePropertyName As String
        Private parentIdPropertyName As String
        Private isChannelPropertyName As String

        Private valuePropertyName As String

        Private idProperty As PropertyDescriptor
        Private nameProperty As PropertyDescriptor
        Private parentIdProperty As PropertyDescriptor
        Private isChannelProperty As PropertyDescriptor
        Private valueProperty As PropertyDescriptor

        Private valueConverter As TypeConverter

        Private items_Positions As SortedList
        Private items_Identifiers As SortedList

        Private selectionChanging As Boolean

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Конструктор по умолчанию.
        ''' </summary>
        Public Sub New()
            Me.idPropertyName = String.Empty
            Me.namePropertyName = String.Empty
            Me.parentIdPropertyName = String.Empty
            Me.isChannelPropertyName = String.Empty
            Me.items_Positions = New SortedList()
            Me.items_Identifiers = New SortedList()
            Me.selectionChanging = False

            Me.FullRowSelect = True
            Me.HideSelection = False
            Me.HotTracking = True

            AddHandler Me.AfterSelect, New TreeViewEventHandler(AddressOf Me.DataTreeView_AfterSelect)
            AddHandler Me.BindingContextChanged, New EventHandler(AddressOf Me.DataTreeView_BindingContextChanged)
            AddHandler Me.AfterLabelEdit, New NodeLabelEditEventHandler(AddressOf Me.DataTreeView_AfterLabelEdit)
        End Sub

        ''' <summary>
        ''' Очистка любых ресурсов.
        ''' </summary>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#End Region

#Region "Win32"

        <DllImport("User32.dll")>
        Private Shared Function ShowScrollBar(hWnd As IntPtr, wBar As Integer, bShow As Boolean) As Boolean
        End Function

#End Region

#Region "Internal classes"

        ''' <summary>
        ''' Tree node с добавленной информацией реляционных данных.
        ''' </summary>
        Public Class DataTreeViewNode
            Inherits TreeNode
#Region "Fields"

            Private m_position As Integer

            Private m_parentID As Object

            Private m_isChannel As Boolean ' добавлен для определения, является ли запись нижним уровнем - канал

#End Region

#Region "Constructors"

            ''' <summary>
            ''' Конструктор узла по умолчанию.
            ''' </summary>
            Public Sub New(position As Integer)
                Me.m_position = position
            End Sub

#End Region

#Region "Implementation"

#End Region

#Region "Properties"

            ''' <summary>
            ''' Идентификатор узла.
            ''' </summary>
            Public Property ID() As Object
                Get
                    Return Me.Tag
                End Get
                Set(value As Object)
                    Me.Tag = value
                End Set
            End Property

            ''' <summary>
            ''' Идентификатор родительского узла.
            ''' </summary>
            Public Property ParentID() As Object
                Get
                    Return Me.m_parentID
                End Get
                Set(value As Object)
                    Me.m_parentID = value
                End Set
            End Property

            ''' <summary>
            ''' Является ли запись нижним уровнем - канал.
            ''' </summary>
            Public Property IsChannel() As Boolean
                Get
                    Return Me.m_isChannel
                End Get
                Set(value As Boolean)
                    Me.m_isChannel = value
                End Set
            End Property

            ''' <summary>
            ''' Позиция в текущем currency manager.
            ''' </summary>
            Public Property Position() As Integer
                Get
                    Return Me.m_position
                End Get
                Set(value As Integer)
                    Me.m_position = value
                End Set
            End Property

#End Region
        End Class


#End Region

#Region "Properties"

        ''' <summary>
        ''' Источник данных для дерева.
        ''' </summary>
        <DefaultValue(DirectCast(Nothing, String)),
        TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
        RefreshProperties(RefreshProperties.Repaint), Category("Data"),
        Description("Data source of the tree.")>
        Public Property DataSource() As Object
            Get
                Return Me.m_dataSource
            End Get
            Set(value As Object)
                If Me.m_dataSource IsNot value Then
                    Me.m_dataSource = value
                    Me.ResetData()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Член источника данных для дерева.
        ''' </summary>
        <DefaultValue(""),
        Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor)),
        RefreshProperties(RefreshProperties.Repaint),
        Category("Data"),
        Description("Data member of the tree.")>
        Public Property DataMember() As String
            Get
                Return Me.m_dataMember
            End Get
            Set(value As String)
                If Me.m_dataMember <> value Then
                    Me.m_dataMember = value
                    Me.ResetData()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Идинтификатор члена, в общем случае первичный ключ, для колонки таблицы.
        ''' </summary>
        <DefaultValue(""), Editor(GetType(FieldTypeEditor), GetType(UITypeEditor)), Category("Data"), Description("Идинтификатор члена, в общем случае первичный ключ, для колонки таблицы.")>
        Public Property IDColumn() As String
            Get
                Return Me.idPropertyName
            End Get
            Set(value As String)
                If Me.idPropertyName <> value Then
                    Me.idPropertyName = value
                    Me.idProperty = Nothing

                    If Me.valuePropertyName Is Nothing OrElse Me.valuePropertyName.Length = 0 Then
                        Me.ValueColumn = Me.idPropertyName
                    End If

                    Me.ResetData()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Имя члена. Проимечание: редактирование этой колонки доступно только с типом поддерживающим конвертацию из строки.
        ''' </summary>
        <DefaultValue(""),
        Editor(GetType(FieldTypeEditor), GetType(UITypeEditor)),
        Category("Data"),
        Description("Имя члена. Проимечание: редактирование этой колонки доступно только с типом поддерживающим конвертацию из строки.")>
        Public Property NameColumn() As String
            Get
                Return Me.namePropertyName
            End Get
            Set(value As String)
                If Me.namePropertyName <> value Then
                    Me.namePropertyName = value
                    Me.nameProperty = Nothing
                    Me.ResetData()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Идентификатор родителя. Примечание: этот член обязан иметь тот же тип как идентификатор колонки.
        ''' </summary>
        <DefaultValue(""),
        Editor(GetType(FieldTypeEditor), GetType(UITypeEditor)),
        Category("Data"),
        Description("Идентификатор родителя. Примечание: этот член обязан иметь тот же тип как идентификатор колонки.")>
        Public Property ParentIDColumn() As String
            Get
                Return Me.parentIdPropertyName
            End Get
            Set(value As String)
                If Me.parentIdPropertyName <> value Then
                    Me.parentIdPropertyName = value
                    Me.parentIdProperty = Nothing
                    Me.ResetData()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Идентификатор принадлежности к типу канал. Примечание: этот член обязан иметь тот же тип как идентификатор колонки.
        ''' </summary>
        <DefaultValue(""),
        Editor(GetType(FieldTypeEditor), GetType(UITypeEditor)),
        Category("Data"),
        Description("Идентификатор принадлежности к типу канал. Примечание: этот член обязан иметь тот же тип как идентификатор колонки.")>
        Public Property IsChannelColumn() As String
            Get
                Return Me.isChannelPropertyName
            End Get
            Set(value As String)
                If Me.isChannelPropertyName <> value Then
                    Me.isChannelPropertyName = value
                    Me.isChannelProperty = Nothing
                    Me.ResetData()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Значение члена. Значение члена будет взято из этой колонки.
        ''' </summary>
        <DefaultValue(""),
        Editor(GetType(FieldTypeEditor), GetType(UITypeEditor)),
        Category("Data"),
        Description("Значение члена. Значение члена будет взято из этой колонки.")>
        Public Property ValueColumn() As String
            Get
                Return Me.valuePropertyName
            End Get
            Set(value As String)
                If Me.valuePropertyName <> value Then
                    Me.valuePropertyName = value
                    Me.valueProperty = Nothing
                    Me.valueConverter = Nothing
                End If
            End Set
        End Property

        ''' <summary>
        ''' Получить значение текущего выделенного элемента.
        ''' </summary>
        <Category("Data"), Description("Получить значение текущего выделенного элемента.")>
        Public ReadOnly Property Value() As Object
            Get
                If Me.SelectedNode IsNot Nothing Then
                    Dim node As DataTreeViewNode = TryCast(Me.SelectedNode, DataTreeViewNode)
                    If node IsNot Nothing AndAlso Me.PrepareValueDescriptor() Then
                        Return Me.valueProperty.GetValue(Me.listManager.List(node.Position))
                    End If
                End If
                Return Nothing
            End Get
        End Property

#End Region

#Region "Events"

        Private Sub DataTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs)
            Me.BeginSelectionChanging()

            Dim node As DataTreeViewNode = TryCast(e.Node, DataTreeViewNode)
            If node IsNot Nothing Then
                Me.listManager.Position = node.Position
            End If
            Me.EndSelectionChanging()
        End Sub

        Private Sub DataTreeView_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs)
            Dim node As DataTreeViewNode = TryCast(e.Node, DataTreeViewNode)
            If node IsNot Nothing Then
                If Me.PrepareValueConvertor() AndAlso Me.valueConverter.IsValid(e.Label) Then
                    Me.nameProperty.SetValue(Me.listManager.List(node.Position), Me.valueConverter.ConvertFromString(e.Label))
                    Me.listManager.EndCurrentEdit()
                    Return
                End If
            End If
            e.CancelEdit = True
        End Sub

        Private Sub DataTreeView_BindingContextChanged(sender As Object, e As EventArgs)
            Me.ResetData()
        End Sub

        Private Sub listManager_PositionChanged(sender As Object, e As EventArgs)
            Me.SynchronizeSelection()
        End Sub

        Private Sub DataTreeView_ListChanged(sender As Object, e As ListChangedEventArgs)
            Select Case e.ListChangedType
                Case ListChangedType.ItemAdded
                    If Not TryAddNode(Me.CreateNode(Me.listManager, e.NewIndex)) Then
                        Throw New ApplicationException("Элемент не был добавлен.")
                    End If
                    Exit Select

                Case ListChangedType.ItemChanged
                    Dim chnagedNode As DataTreeViewNode = TryCast(Me.items_Positions(e.NewIndex), DataTreeViewNode)
                    If chnagedNode IsNot Nothing Then
                        Me.RefreshData(chnagedNode)
                        Me.ChangeParent(chnagedNode)
                    Else
                        Throw New ApplicationException("Элемент не найден или неправильный тип.")
                    End If
                    Exit Select

                Case ListChangedType.ItemMoved
                    Dim movedNode As DataTreeViewNode = TryCast(Me.items_Positions(e.OldIndex), DataTreeViewNode)
                    If movedNode IsNot Nothing Then
                        Me.items_Positions.Remove(e.OldIndex)
                        Me.items_Positions.Add(e.NewIndex, movedNode)
                    Else
                        Throw New ApplicationException("Элемент не найден или неправильный тип.")
                    End If
                    Exit Select

                Case ListChangedType.ItemDeleted
                    Dim deletedNode As DataTreeViewNode = TryCast(Me.items_Positions(e.OldIndex), DataTreeViewNode)
                    If deletedNode IsNot Nothing Then
                        Me.items_Positions.Remove(e.OldIndex)
                        Me.items_Identifiers.Remove(deletedNode.ID)
                        deletedNode.Remove()
                    Else
                        Throw New ApplicationException("Элемент не найден или неправильный тип.")
                    End If
                    Exit Select

                Case ListChangedType.Reset
                    Me.ResetData()
                    Exit Select

            End Select
        End Sub

#End Region

#Region "Implementation"

        Private Sub Clear()
            Me.items_Positions.Clear()
            Me.items_Identifiers.Clear()

            Me.Nodes.Clear()
        End Sub

        Private Function PrepareDataSource() As Boolean
            If Me.BindingContext IsNot Nothing Then
                If Me.m_dataSource IsNot Nothing Then
                    Me.listManager = TryCast(Me.BindingContext(Me.m_dataSource, Me.m_dataMember), CurrencyManager)
                    Return True
                Else
                    Me.listManager = Nothing
                    Me.Clear()
                End If
            End If
            Return False
        End Function

        Private Function PrepareDescriptors() As Boolean
            If Me.idPropertyName.Length <> 0 AndAlso
                Me.namePropertyName.Length <> 0 AndAlso
                Me.parentIdPropertyName.Length <> 0 AndAlso
                Me.isChannelPropertyName.Length <> 0 Then

                If Me.idProperty Is Nothing Then
                    Me.idProperty = Me.listManager.GetItemProperties()(Me.idPropertyName)
                End If
                If Me.nameProperty Is Nothing Then
                    Me.nameProperty = Me.listManager.GetItemProperties()(Me.namePropertyName)
                End If
                If Me.parentIdProperty Is Nothing Then
                    Me.parentIdProperty = Me.listManager.GetItemProperties()(Me.parentIdPropertyName)
                End If
                If Me.isChannelProperty Is Nothing Then
                    Me.isChannelProperty = Me.listManager.GetItemProperties()(Me.isChannelPropertyName)
                End If
            End If

            Return (Me.idProperty IsNot Nothing AndAlso
                    Me.nameProperty IsNot Nothing AndAlso
                    Me.parentIdProperty IsNot Nothing AndAlso
                    Me.isChannelPropertyName IsNot Nothing)
        End Function

        Private Function PrepareValueDescriptor() As Boolean
            If Me.valueProperty Is Nothing Then
                If Me.valuePropertyName = String.Empty Then
                    Me.valuePropertyName = Me.idPropertyName
                End If
                Me.valueProperty = Me.listManager.GetItemProperties()(Me.valuePropertyName)
            End If

            Return (Me.valueProperty IsNot Nothing)
        End Function

        Private Function PrepareValueConvertor() As Boolean
            If Me.valueConverter Is Nothing Then
                Me.valueConverter = TryCast(TypeDescriptor.GetConverter(Me.nameProperty.PropertyType), TypeConverter)
            End If

            Return (Me.valueConverter IsNot Nothing AndAlso Me.valueConverter.CanConvertFrom(GetType(String)))
        End Function

        Private Sub WireDataSource()
            AddHandler Me.listManager.PositionChanged, New EventHandler(AddressOf listManager_PositionChanged)
            AddHandler DirectCast(Me.listManager.List, IBindingList).ListChanged, New ListChangedEventHandler(AddressOf DataTreeView_ListChanged)
        End Sub

        Private Sub ResetData()
            Me.BeginUpdate()

            Me.Clear()

            If Me.PrepareDataSource() Then
                Me.WireDataSource()
                If Me.PrepareDescriptors() Then
                    'ArrayList unsortedNodes = new ArrayList() заменил на обобщённый тип
                    Dim unsortedNodes As New List(Of DataTreeViewNode)()

                    For i As Integer = 0 To Me.listManager.Count - 1
                        unsortedNodes.Add(Me.CreateNode(Me.listManager, i))
                    Next

                    Dim startCount As Integer

                    While unsortedNodes.Count > 0
                        startCount = unsortedNodes.Count

                        For i As Integer = unsortedNodes.Count - 1 To 0 Step -1
                            'if (this.TryAddNode((DataTreeViewNode)unsortedNodes[i]))
                            If Me.TryAddNode(unsortedNodes(i)) Then
                                unsortedNodes.RemoveAt(i)
                            End If
                        Next

                        If startCount = unsortedNodes.Count Then
                            Throw New ApplicationException("Tree view не смог корректно отобразить иерархические данные.")
                        End If
                    End While
                End If
            End If

            Me.EndUpdate()
        End Sub

        Private Function TryAddNode(node As DataTreeViewNode) As Boolean
            If Me.IsIDNull(node.ParentID) Then
                Me.AddNode(Me.Nodes, node)
                Return True
            Else
                If Me.items_Identifiers.ContainsKey(node.ParentID) Then
                    Dim parentNode As TreeNode = TryCast(Me.items_Identifiers(node.ParentID), TreeNode)
                    If parentNode IsNot Nothing Then
                        Me.AddNode(parentNode.Nodes, node)
                        Return True
                    End If
                End If
            End If
            Return False
        End Function

        Private Sub AddNode(nodes As TreeNodeCollection, node As DataTreeViewNode)
            Me.items_Positions.Add(node.Position, node)
            Me.items_Identifiers.Add(node.ID, node)
            nodes.Add(node)
        End Sub

        Private Sub ChangeParent(node As DataTreeViewNode)
            Dim dataParentID As Object = Me.parentIdProperty.GetValue(Me.listManager.List(node.Position))
            If node.ParentID IsNot dataParentID Then
                Dim newParentNode As DataTreeViewNode = TryCast(Me.items_Identifiers(dataParentID), DataTreeViewNode)
                If newParentNode IsNot Nothing Then
                    node.Remove()
                    newParentNode.Nodes.Add(node)
                Else
                    Throw New ApplicationException("Элемент не найден или неправильный тип.")
                End If
            End If
        End Sub

        Private Sub SynchronizeSelection()
            If Not Me.selectionChanging Then
                Dim node As DataTreeViewNode = TryCast(Me.items_Positions(Me.listManager.Position), DataTreeViewNode)
                If node IsNot Nothing Then
                    Me.SelectedNode = node
                End If
            End If
        End Sub

        Private Sub RefreshData(node As DataTreeViewNode)
            Dim position As Integer = node.Position
            node.ID = Me.idProperty.GetValue(Me.listManager.List(position))
            node.Text = DirectCast(Me.nameProperty.GetValue(Me.listManager.List(position)), String)
            node.ParentID = Me.parentIdProperty.GetValue(Me.listManager.List(position))
            node.IsChannel = CBool(Me.isChannelProperty.GetValue(Me.listManager.List(position)))
        End Sub

        ''' <summary>
        ''' Создать DataTreeViewNode с текущим менеджером и позицией.
        ''' </summary>
        ''' <param name="currencyManager"></param>
        ''' <param name="position"></param>
        ''' <returns></returns>
        Private Function CreateNode(currencyManager As CurrencyManager, position As Integer) As DataTreeViewNode
            Dim node As New DataTreeViewNode(position)
            Me.RefreshData(node)
            Return node
        End Function

        Private Function GetPerentID(currencyManager As CurrencyManager, position As Integer) As Object
            Return Me.parentIdProperty.GetValue(currencyManager.List(position))
        End Function

        Private Function IsIDNull(id As Object) As Boolean
            If id Is Nothing OrElse Convert.IsDBNull(id) Then
                Return True
            Else
                If id.[GetType]() Is GetType(String) Then
                    Return (DirectCast(id, String).Length = 0)
                ElseIf id.[GetType]() Is GetType(Guid) Then
                    Return (CType(id, Guid) = Guid.Empty)
                End If
            End If
            Return False
        End Function

        Protected Overrides Sub InitLayout()
            MyBase.InitLayout()
            ShowScrollBar(Handle, SB_HORZ, False)
        End Sub

        Private Sub BeginSelectionChanging()
            Me.selectionChanging = True
        End Sub

        Private Sub EndSelectionChanging()
            Me.selectionChanging = False
        End Sub

#End Region
    End Class
End Namespace
