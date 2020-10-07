'*******************************************************************
'	purpose:	Редактор полей членов.
'********************************************************************

Imports System
Imports System.Windows.Forms
Imports System.ComponentModel

Namespace Chaliy.Windows.Forms.Design
    ''' <summary>
    '''Редактор полей членов.
    ''' </summary>
    Public Class FieldTypeEditor
        Inherits DropDownTypeEditor

#Region "Constructors"

#End Region

        ''' <summary>
        ''' Заполнить list box полями текущего источника данных.
        ''' </summary>
        ''' <param name="listBox">ListBox control to fill.</param>
        ''' <param name="context">Type descriptor context.</param>
        ''' <param name="value">Current value.</param>
        Protected Overrides Sub FillListBox(listBox As ListBox, context As ITypeDescriptorContext, value As Object)
            Dim selectedFiled As String = DirectCast(value, String)
            If selectedFiled Is Nothing Then
                selectedFiled = String.Empty
            End If

            Dim dataSourceDescriptor As PropertyDescriptor = TypeDescriptor.GetProperties(context.Instance)("DataSource")
            Dim dataMemberDescriptor As PropertyDescriptor = TypeDescriptor.GetProperties(context.Instance)("DataMember")
            If dataSourceDescriptor Is Nothing OrElse dataMemberDescriptor Is Nothing Then
                Return
            End If

            Dim dataSource As Object = dataSourceDescriptor.GetValue(context.Instance)
            Dim dataMember As String = DirectCast(dataMemberDescriptor.GetValue(context.Instance), String)
            If dataSource IsNot Nothing Then
                Dim currencyManager As CurrencyManager = TryCast(New BindingContext().Item(dataSource, dataMember), CurrencyManager)
                If currencyManager IsNot Nothing Then
                    Dim lastIndex As Integer
                    For Each descriptor As PropertyDescriptor In currencyManager.GetItemProperties()
                        lastIndex = listBox.Items.Add(descriptor.Name)
                        If String.Compare(descriptor.Name, selectedFiled) = 0 Then
                            listBox.SelectedIndex = lastIndex
                        End If
                    Next
                End If
            End If
        End Sub
    End Class
End Namespace
