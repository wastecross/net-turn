'Hacer conexion cliente/servidor a traves de un puerto
Imports System.Net.Sockets
'Crear procesos autonomos a partir de que el programa corre
Imports System.Threading
'Sirve para utilizar cualquier tipo de clase para la red
Imports System.Net
'Enviar datos a traves de una computadora y otra en un formato de bits
Imports System.Runtime.Serialization.Formatters.Binary

Public Class Form1
    'Oyente para la conexion
    Dim ListenerServer As TcpListener
    'Cliente Remoto que se conecta
    Dim Remote As TcpClient
    'Proceso activo del cliente
    Dim Receive As Thread
    'Cadena de informacion en bytes
    Dim MyIP As String
    Dim IP As String
    Dim Port As Integer
    Dim NS As NetworkStream
    Dim numc As String
    Dim numIn As Int16
    Dim numMax As Int16
    Dim i As Integer
    Dim j As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        'LabelDate.Text = Format(Now, "dd/MM/yyyy")
        i = 0
        j = 1
    End Sub

    Private Sub CargarImagenesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CargarImagenesToolStripMenuItem.Click
        OpenFileDialog1.Filter = " Archivos jpg|*.jpg|Archivos gif |*.gif"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ReiniciarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReiniciarToolStripMenuItem.Click
        LabelCA1.Text = "CAJA"
        LabelCA2.Text = "CAJA"
        LabelCA3.Text = "CAJA"
        LabelCA4.Text = "CAJA"
        LabelT1.Text = "0"
        LabelT2.Text = "0"
        LabelT3.Text = "0"
        LabelT4.Text = "0"
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub CajasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CajasToolStripMenuItem.Click
        Me.FontDialog1.ShowDialog()
        LabelCA1.Font = FontDialog1.Font
        LabelCA2.Font = FontDialog1.Font
        LabelCA3.Font = FontDialog1.Font
        LabelCA4.Font = FontDialog1.Font
    End Sub

    Private Sub TurnoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TurnoToolStripMenuItem.Click
        Me.FontDialog2.ShowDialog()
        LabelT1.Font = FontDialog2.Font
        LabelT2.Font = FontDialog2.Font
        LabelT3.Font = FontDialog2.Font
        LabelT4.Font = FontDialog2.Font
        LabelCA3.Font = FontDialog2.Font
        LabelT2.Font = FontDialog2.Font
        Label4.Font = FontDialog2.Font
        Label8.Font = FontDialog2.Font
    End Sub

    Private Sub CajasToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CajasToolStripMenuItem1.Click
        ColorDialog1.ShowDialog()
        LabelCA1.ForeColor = ColorDialog1.Color
        LabelCA2.ForeColor = ColorDialog1.Color
        LabelCA3.ForeColor = ColorDialog1.Color
        LabelCA4.ForeColor = ColorDialog1.Color
    End Sub

    Private Sub TurnoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TurnoToolStripMenuItem1.Click
        ColorDialog2.ShowDialog()
        LabelT1.ForeColor = ColorDialog2.Color
        LabelT2.ForeColor = ColorDialog2.Color
        LabelT3.ForeColor = ColorDialog2.Color
        LabelT4.ForeColor = ColorDialog2.Color
        LabelCA3.ForeColor = ColorDialog2.Color
        LabelT2.ForeColor = ColorDialog2.Color
        Label4.ForeColor = ColorDialog2.Color
        Label8.ForeColor = ColorDialog2.Color
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MyIP = TextBox1.Text
        Port = TextBox2.Text
        If Button1.Text = "Connect" Then
            Try
                Dim MyWebClient As New WebClient
                Dim HTML As String = MyWebClient.DownloadString("127.0.0.1")
                MyIP = HTML
                MyIP = MyIP.Remove(0, MyIP.IndexOf("Tu IP real es") + 14)
                MyIP = MyIP.Substring(0, MyIP.IndexOf("(") - 1)
            Catch ex As Exception
                'Mensaje en caso de no encontrar o no estar disponible
                MsgBox("Problemas con la direccion IP")
            End Try
            Try
                'Protege a los hilos de procesos inesperados de la falta de control en ellos
                CheckForIllegalCrossThreadCalls = False
                'Configura al oyente para su trabajo
                ListenerServer = New TcpListener(IPAddress.Any, Port)
                'Inicia la conexion
                ListenerServer.Start()
                'Configura al cliente para recibir mensajes
                Receive = New Thread(AddressOf Receiver)
                'Inicia al cliente
                Receive.Start()
                'Coloca el color negro al seleccionar el texto
                RichTextBox1.SelectionColor = Color.Black
                'Adiciona la palabra escuchar a la caja de texto y da un salto de linea
                RichTextBox1.AppendText("Listening..." & vbCrLf)
                'Coloca un scroll en la caja de texto
                RichTextBox1.ScrollToCaret()
                Button1.Text = "Close"
            Catch ex As Exception
                'Mensaje cuando el servidor no se puede conectar
                MsgBox("Problemas con la conexion")
            End Try
        Else
            Try
                'Se detiene el servidor
                ListenerServer.Stop()
                'Se detiene el proceso del cliene
                Receive.Abort()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub Receiver()
        'Formato de bytes
        Dim BF As New BinaryFormatter
        Try

            'El cliente ha sido aceptado y forma parte de la conexion 
            Remote = ListenerServer.AcceptTcpClient()
            'Determina el flujo de datos
            NS = Remote.GetStream
            'Convertir mensaje en flujo de datos
            Dim Message As String = System.Text.Encoding.UTF7.GetString(BF.Deserialize(NS))
                    Dim nBox As String = System.Text.Encoding.UTF7.GetString(BF.Deserialize(NS))
                    numc = Message
                    RichTextBox1.SelectionColor = Color.Black
                    RichTextBox1.AppendText(numc & vbCrLf)
            RichTextBox1.ScrollToCaret()
            Beep()
            NumeroTurno()
            RichTextBox1.AppendText(numIn)
        Catch ex As Exception
            RichTextBox1.SelectionColor = Color.Black
            RichTextBox1.AppendText("Remoto desconectado" & vbCrLf)
            RichTextBox1.ScrollToCaret()
        End Try
        Try
            j = 1
            Reconnect()
        Catch ex As Exception

        End Try

    End Sub

    Public Sub NumeroTurno()
        LabelCA4.Text = LabelCA3.Text
        LabelT4.Text = LabelT3.Text
        LabelCA3.Text = LabelCA2.Text
        LabelT3.Text = LabelT2.Text
        LabelCA2.Text = LabelCA1.Text
        LabelT2.Text = LabelT1.Text
        If numIn < numMax Then
            numIn += 1
            LabelT1.Text = numIn
            LabelCA1.Text = numc
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            numIn = Integer.Parse(TextBoxIn.Text)
            numMax = Integer.Parse(TextBoxMax.Text)
            MsgBox("Se completo la accion")
        Catch ex As Exception
            MsgBox("No es valido el tipo de dato")
        End Try
    End Sub

    Private Sub HoraYDiaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HoraYDiaToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Private Sub FondoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FondoToolStripMenuItem.Click
        ColorDialog3.ShowDialog()
        Me.BackColor = ColorDialog3.Color
        Panel1.BackColor = ColorDialog3.Color
        Panel4.BackColor = ColorDialog3.Color
    End Sub

    Public Sub Reconnect()
        While j > 0

            Try
                Dim MyWebClient As New WebClient
                Dim HTML As String = MyWebClient.DownloadString("127.0.0.1")
                MyIP = HTML
                MyIP = MyIP.Remove(0, MyIP.IndexOf("Tu IP real es") + 14)
                MyIP = MyIP.Substring(0, MyIP.IndexOf("(") - 1)
            Catch ex As Exception
            End Try
            Try
                'Protege a los hilos de procesos inesperados de la falta de control en ellos
                CheckForIllegalCrossThreadCalls = False
                'Configura al oyente para su trabajo
                ListenerServer = New TcpListener(IPAddress.Any, Port)
                'Inicia la conexion
                ListenerServer.Start()
                'Configura al cliente para recibir mensajes
                Receive = New Thread(AddressOf Receiver)
                'Inicia al cliente
                Receive.Start()
                'Coloca el color negro al seleccionar el texto
                RichTextBox1.SelectionColor = Color.Black
                'Adiciona la palabra escuchar a la caja de texto y da un salto de linea
                RichTextBox1.AppendText("Listening..." & vbCrLf)
                'Coloca un scroll en la caja de texto
                RichTextBox1.ScrollToCaret()
                j = -1
            Catch ex As Exception
            End Try

        End While

    End Sub
End Class