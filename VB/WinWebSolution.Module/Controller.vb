Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.SystemModule

Namespace WinWebSolution.Module

    Public Class Mycontroller
        Inherits ViewController(Of ListView)

        Private controller1 As NewObjectViewController
        Private controller2 As ListViewProcessCurrentObjectController

        Public Sub New()
            TargetViewNesting = Nesting.Nested
            TargetObjectType = GetType(Transaction)
        End Sub

        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            controller1 = Frame.GetController(Of NewObjectViewController)()
            If controller1 IsNot Nothing Then
                AddHandler controller1.ObjectCreated, AddressOf controller1_ObjectCreated
            End If
            controller2 = Frame.GetController(Of ListViewProcessCurrentObjectController)()
            If controller2 IsNot Nothing Then
                AddHandler controller2.CustomProcessSelectedItem, AddressOf controller2_CustomProcessSelectedItem
            End If
        End Sub

        Protected Overrides Sub OnDeactivated()
            If controller1 IsNot Nothing Then
                RemoveHandler controller1.ObjectCreated, AddressOf controller1_ObjectCreated
            End If
            If controller2 IsNot Nothing Then
                RemoveHandler controller2.CustomProcessSelectedItem, AddressOf controller2_CustomProcessSelectedItem
            End If
            MyBase.OnDeactivated()
        End Sub

        Private Sub controller1_ObjectCreated(ByVal sender As Object, ByVal e As ObjectCreatedEventArgs)
            AddHandler Application.DetailViewCreating, AddressOf Application_DetailViewCreating
        End Sub

        Private Sub controller2_CustomProcessSelectedItem(ByVal sender As Object, ByVal e As CustomProcessListViewSelectedItemEventArgs)
            AddHandler Application.DetailViewCreating, AddressOf Application_DetailViewCreating
        End Sub

        Private Sub Application_DetailViewCreating(ByVal sender As Object, ByVal e As DetailViewCreatingEventArgs)
            RemoveHandler DirectCast(sender, XafApplication).DetailViewCreating, AddressOf Application_DetailViewCreating
            If TypeOf e.Obj Is Transaction AndAlso TypeOf Frame Is NestedFrame Then
                Dim parentType As Type = CType(Frame, NestedFrame).ViewItem.ObjectType
                If GetType(Consumer).IsAssignableFrom(parentType) Then
                    e.ViewID = "Transaction_DetailView_FromConsumer"
                End If
                If GetType(Producer).IsAssignableFrom(parentType) Then
                    e.ViewID = "Transaction_DetailView_FromProducer"
                End If
            End If
        End Sub
    End Class
End Namespace
