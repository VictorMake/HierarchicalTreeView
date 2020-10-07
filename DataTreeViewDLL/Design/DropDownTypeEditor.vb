'*******************************************************************
'	Назначение:	Базовый класс для простого drop down редактора типа.
'********************************************************************

Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

Namespace Chaliy.Windows.Forms.Design
    ''' <summary>
    ''' Базовый класс для простого drop down редактора типа.
    ''' </summary>	
    Public MustInherit Class DropDownTypeEditor
        Inherits System.Drawing.Design.UITypeEditor

#Region "Fields"

        Private editorService As IWindowsFormsEditorService

#End Region

#Region "Events"

        Private Sub ListBox_SelectedIndexChanged(objSender As Object, eventArgs As EventArgs)
            If Me.editorService IsNot Nothing Then
                Me.editorService.CloseDropDown()
            End If
        End Sub

#End Region

#Region "Implemenatation"

        ''' <summary>
        ''' Переопределяет установку редактора стиля в DropDown.
        ''' </summary>
        ''' <param name="context">Type descriptor context.</param>
        ''' <returns>Style of the editor.</returns>
        Public Overrides Function GetEditStyle(context As ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
            If context IsNot Nothing AndAlso context.Instance IsNot Nothing Then
                Return UITypeEditorEditStyle.DropDown
            End If
            Return MyBase.GetEditStyle(context)
        End Function

        Public Overrides Function EditValue(context As ITypeDescriptorContext, provider As IServiceProvider, value As Object) As Object
            If context IsNot Nothing AndAlso context.Instance IsNot Nothing AndAlso context.Container IsNot Nothing AndAlso provider IsNot Nothing Then
                Me.editorService = TryCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
                If Me.editorService IsNot Nothing Then

                    Using listBox As New ListBox()
                        listBox.BorderStyle = BorderStyle.None

                        Me.FillListBox(listBox, context, value)
                        AddHandler listBox.SelectedIndexChanged, New EventHandler(AddressOf ListBox_SelectedIndexChanged)

                        Me.editorService.DropDownControl(listBox)

                        If listBox.SelectedItem IsNot Nothing Then
                            value = GetValueFromListItem(context, listBox.SelectedItem)
                        End If
                        RemoveHandler listBox.SelectedIndexChanged, New EventHandler(AddressOf ListBox_SelectedIndexChanged)
                    End Using
                    Me.editorService = Nothing
                End If
            End If
            Return value
        End Function

        ''' <summary>
        ''' Реализовать эту процедуру заполнения listBox.Items.
        ''' </summary>
        ''' <param name="listBox">ListBox control to fill.</param>
        ''' <param name="context">Type descriptor context.</param>
        ''' <param name="value">Current value.</param>
        Protected MustOverride Sub FillListBox(listBox As ListBox, context As ITypeDescriptorContext, value As Object)

        ''' <summary>
        ''' Переопределить функцию для изменения выходного значения.
        ''' </summary>
        ''' <param name="context">Type descriptor context.</param>
        ''' <param name="value">Value to change.</param>
        ''' <returns>Changed value.</returns>
        Protected Overridable Function GetValueFromListItem(context As ITypeDescriptorContext, value As Object) As Object
            Return value
        End Function


#End Region
    End Class
End Namespace
