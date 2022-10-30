Imports System.Data.SQLite
Public Class Form1
    '---------------------------Selecting Controls-------------------------'
    Dim lv As ListViewItem
    Dim selected As String

    Private Sub loadTable()
        ListView1.Clear()

        With ListView1
            .View = View.Details
            .GridLines = True
            .Columns.Add("ID", 40)
            .Columns.Add("Author's Name", 110)
            .Columns.Add("Title", 110)
            .Columns.Add("Genre", 110)
            .Columns.Add("Condition", 40)
            .Columns.Add("Date Stored", 110)
        End With

        openCon()
        sql = "select * from tblBook"
        cmd = New SQLiteCommand(sql, cn)
        dr = cmd.ExecuteReader

        Do While dr.Read = True
            lv = New ListViewItem(dr("id").ToString)
            lv.SubItems.Add(dr("author"))
            lv.SubItems.Add(dr("title"))
            lv.SubItems.Add(dr("genre"))
            lv.SubItems.Add(dr("condition"))
            lv.SubItems.Add(dr("datestored"))
            ListView1.Items.Add(lv)
        Loop
        dr.Close()
        cn.Close()

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

        Dim i As Integer
        For i = 0 To ListView1.SelectedItems.Count - 1
            selected = ListView1.SelectedItems(i).Text
            openCon()
            sql = "select * from tblBook where id = '" & selected & "'"
            cmd = New SQLiteCommand(sql, cn)
            dr = cmd.ExecuteReader

            dr.Read()
            txtId.Text = dr("id")
            txtAuthor.Text = dr("author")
            txtTitle.Text = dr("title")
            cmbGenre.Text = dr("genre")
            cmbCondition.Text = dr("condition")
            txtDatestored.Text = dr("datestored")

            dr.Close()
            cn.Close()

        Next
        btnUpdate.Enabled = True
        btnDelete.Enabled = True
        btnSave.Enabled = False
    End Sub

    '---------------------------Button Commands-------------------------'
    Private Sub SaveCommand()
        If txtId.Text = "" Or txtAuthor.Text = "" Or txtTitle.Text = "" Or cmbGenre.Text = "" Or cmbCondition.Text = "" Or txtDatestored.Text = "" Then
            MsgBox("Please Fill all inputs.")
        Else
            If btnSave.Text = "Save" Then
                If MsgBox("Are you sure to Save", vbYesNo + vbQuestion) = vbYes Then
                    openCon()
                    sql = "insert into tblBook(id,author,title,genre,condition,datestored) values ('" & Me.txtId.Text & "', '" & Me.txtAuthor.Text & "', '" & Me.txtTitle.Text & "', '" & Me.cmbGenre.Text & "', '" & Me.cmbCondition.Text & "', '" & Me.txtDatestored.Text & "')"
                    cmd = New SQLiteCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Added Record")
                End If

            End If
        End If
        loadTable()
    End Sub
    Private Sub DeleteCommand()
        If MsgBox("Are you sure you want to deleete", vbYesNo, vbQuestion) = vbYes Then
            openCon()
            sql = "delete from tblBook where id = '" & selected & "'"
            cmd = New SQLiteCommand(sql, cn)
            cmd.ExecuteNonQuery()
            MsgBox("Succefully Deleted!!!")
            cn.Close()
        End If
        loadTable()

    End Sub
    Private Sub UpdateCommand()
        If MsgBox("are you sure you want to update", vbYesNo, vbQuestion) = vbYes Then
            openCon()
            sql = "update tblBook set author = '" & Me.txtAuthor.Text & "', title = '" & Me.txtTitle.Text & "', genre = '" & Me.cmbGenre.Text & "', condition = '" & Me.cmbCondition.Text & "', datestored = '" & Me.txtDatestored.Text & "' where id = '" & selected & "'"
            cmd = New SQLiteCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
            MsgBox("Succefully Updated")
        End If
        loadTable()

    End Sub

    '---------------------------Cleaning Controls-------------------------'
    Private Sub clearalltext()
        txtId.Text = ""
        txtAuthor.Text = ""
        txtDatestored.Text = ""
        txtTitle.Text = ""
        cmbGenre.Text = ""
        cmbCondition.Text = ""
    End Sub

    Private Sub resetall()
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
        clearalltext()
    End Sub

    '---------------------------Button Controls-------------------------'
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadTable()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveCommand()
        clearalltext()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        UpdateCommand()
        clearalltext()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteCommand()
        clearalltext()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel?", vbYesNo, vbQuestion) = vbYes Then
            resetall()
        End If

    End Sub
End Class
