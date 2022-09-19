Imports System.Data.SqlClient

Public Class Form1

    Dim conn As New SqlConnection("Data source=localhost\MSSQLSERVER04;Initial Catalog=CRUDEMPLOYEE;Integrated Security=true")
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub ExecuteQuery(ByVal query As String)
        Dim cmd As New SqlCommand(query, conn)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim insertquery As String = "INSERT INTO tblemployee(FIRSTNAME,LASTNAME,ADDRESS)VALUES('" & TextBoxFirstName.Text & "','" & TextBoxLastName.Text & "','" & TextBoxAddress.Text & "' )"
        ExecuteQuery(insertquery)
        MessageBox.Show("Record inserted successfully", "INSERT", MessageBoxButtons.OK, MessageBoxIcon.Information)
        TextBoxFirstName.Clear()
        TextBoxLastName.Clear()
        TextBoxAddress.Clear()
    End Sub

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim cmd As New SqlCommand("SELECT * FROM tblemployee WHERE ID=@ID", conn)
        cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = TextBoxSearch.Text
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable

        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            TextBoxFirstName.Text = dt.Rows(0)(1).ToString
            TextBoxLastName.Text = dt.Rows(0)(2).ToString
            TextBoxAddress.Text = dt.Rows(0)(3).ToString

        Else
            MessageBox.Show("No record found", "No record", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim updatequery As String = "UPDATE tblemployee SET FIRSTNAME='" & TextBoxFirstName.Text & "',LASTNAME='" & TextBoxLastName.Text & "',ADDRESS='" & TextBoxAddress.Text & "' WHERE ID='" & TextBoxSearch.Text & "' "
        ExecuteQuery(updatequery)
        MessageBox.Show("Record successfully updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        TextBoxFirstName.Clear()
        TextBoxLastName.Clear()
        TextBoxAddress.Clear()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim deletequery As String = "DELETE FROM tblemployee WHERE ID='" & TextBoxSearch.Text & "'"
        ExecuteQuery(deletequery)
        MessageBox.Show("Record successfully deleted", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        TextBoxFirstName.Clear()
        TextBoxLastName.Clear()
        TextBoxAddress.Clear()

    End Sub
End Class
