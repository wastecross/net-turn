Imports System.Net.Sockets
Imports System.Runtime.Serialization.Formatters.Binary
Public Class Form1
    Dim Cajas As New TcpClient
    Dim NS As NetworkStream

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Conectar" Then
            Connecting()
        Else
            Connect2()
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim BF As New BinaryFormatter
        Try
            NS = Cajas.GetStream
            If NS.DataAvailable Then
                Dim MENSAJE As String = System.Text.Encoding.UTF7.GetString(BF.Deserialize(NS))
            End If
        Catch ex As Exception
            Timer1.Stop()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        End
    End Sub

    Private Sub CambiarNombreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CambiarNombreToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        Try
            Dim Send As Byte() = System.Text.Encoding.UTF7.GetBytes(ButtonNext.Text)
            NS = Cajas.GetStream()
            Dim BF As New BinaryFormatter
            BF.Serialize(NS, Send)
            If ButtonNext.Text = "Siguiente" Then
                Dim ENVIO As Byte() = System.Text.Encoding.UTF7.GetBytes(LabelCaja.Text)
                NS = Cajas.GetStream()
                BF.Serialize(NS, ENVIO)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FijarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FijarToolStripMenuItem.Click
        Me.Hide()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.Show()
        Me.TopMost = True
    End Sub

    Private Sub MoverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoverToolStripMenuItem.Click
        Me.Hide()
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.Show()
        Me.TopMost = False
    End Sub

    Public Sub Connecting()
        Try
            Cajas.Connect(TextBox1.Text, TextBox2.Text)
            Timer1.Interval = 3000
            Timer1.Start()
            Dim ENVIO As Byte() = System.Text.Encoding.UTF7.GetBytes(LabelCaja.Text)
            NS = Cajas.GetStream()
            Dim BF As New BinaryFormatter
            BF.Serialize(NS, ENVIO)
            Label1.Text = "Conectado"
            Button1.Text = "Cerrar"
        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Public Sub Connect2()
        Try
            Dim ENVIO As Byte() = System.Text.Encoding.UTF7.GetBytes("DESCONECTADO")
            NS = Cajas.GetStream()
            Dim BF As New BinaryFormatter
            BF.Serialize(NS, ENVIO)
        Catch ex As Exception
        End Try
        Try
            NS.Dispose()
            Cajas.Close()
        Catch ex As Exception
        End Try
        Me.Close()
    End Sub

End Class
