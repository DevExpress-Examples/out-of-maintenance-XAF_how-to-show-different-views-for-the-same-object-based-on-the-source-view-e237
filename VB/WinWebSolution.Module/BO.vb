Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base

Namespace WinWebSolution.Module
    <DefaultClassOptions> _
    Public Class Producer
        Inherits BaseObject

        Public Sub New(ByVal s As Session)
            MyBase.New(s)
        End Sub

        Public Property Name() As String
            Get
                Return GetPropertyValue(Of String)("Name")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", value)
            End Set
        End Property

        <Aggregated, Association("Producer-Transactions")> _
        Public ReadOnly Property Transactions() As XPCollection(Of Transaction)
            Get
                Return GetCollection(Of Transaction)("Transactions")
            End Get
        End Property
    End Class

    <DefaultClassOptions> _
    Public Class Consumer
        Inherits BaseObject

        Public Sub New(ByVal s As Session)
            MyBase.New(s)
        End Sub

        Public Property Name() As String
            Get
                Return GetPropertyValue(Of String)("Name")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", value)
            End Set
        End Property

        <Aggregated, Association("Consumer-Transactions")> _
        Public ReadOnly Property Transactions() As XPCollection(Of Transaction)
            Get
                Return GetCollection(Of Transaction)("Transactions")
            End Get
        End Property
    End Class

    Public Class Transaction
        Inherits BaseObject

        Public Sub New(ByVal s As Session)
            MyBase.New(s)
        End Sub

        Public Property Product() As String
            Get
                Return GetPropertyValue(Of String)("Product")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Product", value)
            End Set
        End Property

        Public Property Amount() As Decimal
            Get
                Return GetPropertyValue(Of Decimal)("Amount")
            End Get
            Set(ByVal value As Decimal)
                SetPropertyValue(Of Decimal)("Amount", value)
            End Set
        End Property

        <Association("Producer-Transactions")> _
        Public Property Producer() As Producer
            Get
                Return GetPropertyValue(Of Producer)("Producer")
            End Get
            Set(ByVal value As Producer)
                SetPropertyValue(Of Producer)("Producer", value)
            End Set
        End Property

        <Association("Consumer-Transactions")> _
        Public Property Consumer() As Consumer
            Get
                Return GetPropertyValue(Of Consumer)("Consumer")
            End Get
            Set(ByVal value As Consumer)
                SetPropertyValue(Of Consumer)("Consumer", value)
            End Set
        End Property
    End Class
End Namespace
