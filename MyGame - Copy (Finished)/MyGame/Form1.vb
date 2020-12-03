Public Class Form1
    Dim xdir As Integer
    Dim ydir As Integer
    ReadOnly r As New Random
    Shadows Sub Move(p As PictureBox, x As Integer, y As Integer)
        p.Location = New Point(p.Location.X + x, p.Location.Y + y)

    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.W
                'MoveTo(PictureBox1, 0, -20)
                xdir = 0
                ydir = -15
            Case Keys.Down, Keys.S
                'MoveTo(PictureBox1, 0, 20)
                xdir = 0
                ydir = 15
            Case Keys.Left, Keys.A
                'MoveTo(PictureBox1, -20, 0)
                PictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                xdir = -15
                ydir = 0
            Case Keys.Right, Keys.D
                'MoveTo(PictureBox1, 20, 0)
                PictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                xdir = 15
                ydir = 0
            Case Keys.Space
                PictureBoxBullet.Location = PictureBox1.Location
                PictureBoxBullet.Visible = True
                Timer2.Enabled = True
            Case Keys.R
                Application.Restart()
        End Select
    End Sub
    Public Sub Chase(p As PictureBox)
        Dim x, y As Integer
        If p.Location.X > PictureBox1.Location.X Then
            x = -5
        Else
            x = 5
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < PictureBox1.Location.Y Then
            y = 5
        Else
            y = -5
        End If
        MoveTo(p, x, y)
    End Sub



    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function
    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If
        Dim other As Object = Nothing
        If p.Name = "PictureBox1" And Collision(p, "WIN", other) Then
            Me.BackColor = Color.Green
            other.visible = False
            Frank.Visible = False
            TextBoxWin.Visible = True
            TextBoxSaveMater.Visible = True
            Return

        End If
        If p.Name = "PictureBoxBullet" And Collision(p, "Frank", other) Then

            ProgressBar1.Increment(1)
            Return

        End If
        If ProgressBar1.Value = 150 - 299 Then
            TextBoxHalfWayThere.Visible = True
        End If
        If ProgressBar1.Value > 299 Then
            TextBoxHalfWayThere.Visible = False
        End If
        If ProgressBar1.Value = 300 Then
            PictureBoxCageWall1.Visible = False
            PictureBoxCageWall2.Visible = False
            Frank.Visible = False
        End If
        If p.Name = "PictureBox1" And Collision(p, "Frank", other) Then
            Me.BackColor = Color.Red
            PictureBox1.Visible = False
            TextBoxLose.Visible = True
            TextBoxRestart.Visible = True
            Return

        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        MoveTo(PictureBoxBullet, 15, 0)
        MoveTo(PictureBox1, xdir, ydir)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Application.Restart()
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Dim x As Integer
        x = r.Next(-50, 50)
        MoveTo(Frank, x, 0)
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        Dim Y As Integer
        Y = r.Next(-50, 50)
        MoveTo(Frank, 0, Y)
    End Sub
End Class
