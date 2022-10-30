Imports System.Data.SQLite
Module Connection
    Public cn As SQLiteConnection
    Public cmd As SQLiteCommand
    Public dr As SQLiteDataReader
    Public sql As String

    Public Sub openCon()
        cn = New SQLiteConnection("Data Source = C:\Users\david\source\repos\BookSQL\BookSQL\bin\Debug\dbBook.db")

        If cn.State = ConnectionState.Closed Then
            cn.Open()

        End If
    End Sub
End Module
