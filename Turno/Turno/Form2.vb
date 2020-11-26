Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LabelHour.Text = TimeOfDay.ToString("hh:mm:ss tt")
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

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()
    End Sub

End Class