Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Text
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmMain
    Private strConn As String = "Server=.;Database=PTSekawanIntiplast;User Id=sa;Password=mukhammadfauzi;Trusted_Connection=True"
    Private sqlCon As SqlConnection

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetData()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        txtIdKaryawan.Text = ""
        txtNamaKaryawan.Text = ""
        txtNIK.Text = ""
        txtAlamat.Text = ""
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MsgBox("Yakin akan menghapus data?", MsgBoxStyle.YesNo,
              "Konfirmasi") = MsgBoxResult.No Then Exit Sub
        DeleteData()
    End Sub

    Private Sub clearData()
        txtIdKaryawan.Text = ""
        txtNamaKaryawan.Text = ""
        txtNIK.Text = ""
        txtAlamat.Text = ""
    End Sub

    Public Sub GetData()
        Try
            sqlCon = New SqlConnection(strConn)
            Using (sqlCon)
                Dim sqlComm As New SqlCommand
                Dim adapter As New SqlDataAdapter()
                Dim ds As New DataSet()

                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "AllSyncProcedure"
                sqlComm.CommandType = CommandType.StoredProcedure

                sqlComm.Parameters.AddWithValue("code", "GetAll")
                sqlComm.Parameters.AddWithValue("idkaryawan", "")
                sqlComm.Parameters.AddWithValue("namakaryawan", "")
                sqlComm.Parameters.AddWithValue("nik", "")
                sqlComm.Parameters.AddWithValue("alamat", "")

                sqlCon.Open()
                sqlComm.ExecuteNonQuery()

                adapter = New SqlDataAdapter(sqlComm)
                adapter.Fill(ds)
                sqlCon.Close()
                GridControl1.DataSource = ds.Tables(0)

            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub SaveData()
        Try
            If txtIdKaryawan.Text = "" Then
                MsgBox("Data tidak boleh kosong")
            Else
                sqlCon = New SqlConnection(strConn)
                Using (sqlCon)
                    Dim sqlComm As New SqlCommand
                    sqlComm.Connection = sqlCon
                    sqlComm.CommandText = "AllSyncProcedure"
                    sqlComm.CommandType = CommandType.StoredProcedure

                    sqlComm.Parameters.AddWithValue("code", "Save")
                    sqlComm.Parameters.AddWithValue("idkaryawan", txtIdKaryawan.Text)
                    sqlComm.Parameters.AddWithValue("namakaryawan", txtNamaKaryawan.Text)
                    sqlComm.Parameters.AddWithValue("nik", txtNIK.Text)
                    sqlComm.Parameters.AddWithValue("alamat", txtAlamat.Text)

                    sqlCon.Open()
                    sqlComm.ExecuteNonQuery()
                End Using
                MsgBox("Data terupdate")
                GetData()
                clearData()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DeleteData()
        sqlCon = New SqlConnection(strConn)
        Using (sqlCon)
            Dim sqlComm As New SqlCommand
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "AllSyncProcedure"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.AddWithValue("code", "Delete")
            sqlComm.Parameters.AddWithValue("idkaryawan", txtIdKaryawan.Text)
            sqlComm.Parameters.AddWithValue("namakaryawan", txtNamaKaryawan.Text)
            sqlComm.Parameters.AddWithValue("nik", txtNIK.Text)
            sqlComm.Parameters.AddWithValue("alamat", txtAlamat.Text)

            sqlCon.Open()
            sqlComm.ExecuteNonQuery()
            GetData()
            clearData()
        End Using
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        txtIdKaryawan.Text = GridView1.GetRowCellValue(e.FocusedRowHandle, "IdKaryawan")
        txtNamaKaryawan.Text = GridView1.GetRowCellValue(e.FocusedRowHandle, "NamaKaryawan")
        txtNIK.Text = GridView1.GetRowCellValue(e.FocusedRowHandle, "NIK")
        txtAlamat.Text = GridView1.GetRowCellValue(e.FocusedRowHandle, "Alamat")
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        frmReport.Show()
    End Sub

    Private Sub btnGetData_Click(sender As Object, e As EventArgs)
        GetData()
    End Sub
End Class
