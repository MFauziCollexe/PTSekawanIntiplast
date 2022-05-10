Imports System.Data.SqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmReport
    Private strConn As String = "Server=.;Database=PTSekawanIntiplast;User Id=sa;Password=mukhammadfauzi;Trusted_Connection=True"
    Private sqlCon As SqlConnection
    Private Sub frmReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetReport()
    End Sub

    Private Sub GetReport()
        Dim cr As ReportDocument = New ReportDocument()
        Dim FILEPATH As String = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Application.StartupPath))
        Dim sqlComm As New SqlCommand
        Dim adapter As New SqlDataAdapter()
        Dim dt As New DataTable
        Try
            sqlCon = New SqlConnection(strConn)
            Using (sqlCon)
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "GETALLDATA"
                sqlComm.CommandType = CommandType.StoredProcedure

                sqlCon.Open()
                sqlComm.ExecuteNonQuery()

                adapter = New SqlDataAdapter(sqlComm)
                adapter.Fill(dt)
                sqlCon.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        cr.Load(FILEPATH + "\CrystalReport1.rpt")
        cr.SetDataSource(dt)
        CrystalReportViewer1.ReportSource = cr
        CrystalReportViewer1.Refresh()
    End Sub

    Private Sub btnRefreshData_Click(sender As Object, e As EventArgs) Handles btnRefreshData.Click
        GetReport()
    End Sub
End Class