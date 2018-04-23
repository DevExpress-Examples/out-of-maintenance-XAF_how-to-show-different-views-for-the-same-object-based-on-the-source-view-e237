Imports Microsoft.VisualBasic
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

		Public Name As String

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

		Public Name As String

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

		Public Product As String
		Public Amount As Decimal

		Private producer_Renamed As Producer
		<Association("Producer-Transactions")> _
		Public Property Producer() As Producer
			Get
				Return producer_Renamed
			End Get
			Set(ByVal value As Producer)
				SetPropertyValue("Producer", producer_Renamed, value)
			End Set
		End Property

		Private consumer_Renamed As Consumer
		<Association("Consumer-Transactions")> _
		Public Property Consumer() As Consumer
			Get
				Return consumer_Renamed
			End Get
			Set(ByVal value As Consumer)
				SetPropertyValue("Consumer", consumer_Renamed, value)
			End Set
		End Property
	End Class
End Namespace
