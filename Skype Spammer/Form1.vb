Imports SKYPE4COMLib

Public Class Form1

    Public WithEvents Skype As New SKYPE4COMLib.Skype
    Dim X, Y As Integer
    Dim Point As System.Drawing.Point

    ' drop shadow
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const DROPSHADOW = &H20000
            Dim cParam As CreateParams = MyBase.CreateParams
            cParam.ClassStyle = cParam.ClassStyle Or DROPSHADOW
            Return cParam
        End Get
    End Property

    ' do stuff on load
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = Me.Text
        PictureBox5.ImageLocation = "http://api.skype.com/users/" + Skype.CurrentUserHandle + "/profile/avatar"
        For Each Contacts In Skype.Friends
            ListBox1.Items.Add(Contacts.Handle)
        Next
    End Sub

    ' allow movement of borderless window
    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        X = Control.MousePosition.X - Me.Location.X
        Y = Control.MousePosition.Y - Me.Location.Y
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Point = Control.MousePosition
            Point.X -= (X)
            Point.Y -= (Y)
            Me.Location = Point
        End If
    End Sub

    Private Sub Label1_MouseDown(sender As Object, e As MouseEventArgs) Handles Label1.MouseDown
        X = Control.MousePosition.X - Me.Location.X
        Y = Control.MousePosition.Y - Me.Location.Y
    End Sub

    Private Sub Label1_MouseMove(sender As Object, e As MouseEventArgs) Handles Label1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Point = Control.MousePosition
            Point.X -= (X)
            Point.Y -= (Y)
            Me.Location = Point
        End If
    End Sub

    ' window controls

    Private Sub Form1_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        PictureBox1.Image = My.Resources.inactive
        PictureBox2.Image = My.Resources.inactive
    End Sub

    Private Sub Form1_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        PictureBox1.Image = My.Resources.close
        PictureBox2.Image = My.Resources.minimize
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        End
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        PictureBox1.Image = My.Resources.close_down
    End Sub

    Private Sub PictureBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDown
        PictureBox2.Image = My.Resources.minimize_down
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        PictureBox1.Image = My.Resources.close_hover
    End Sub

    Private Sub PictureBox2_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseUp
        PictureBox2.Image = My.Resources.minimize_hover
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.close_hover
        PictureBox2.Image = My.Resources.minimize_hover
    End Sub

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        PictureBox1.Image = My.Resources.close_hover
        PictureBox2.Image = My.Resources.minimize_hover
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.close
        PictureBox2.Image = My.Resources.minimize
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox1.Image = My.Resources.close
        PictureBox2.Image = My.Resources.minimize
    End Sub

    ' spammer function
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Skype.SendMessage(ListBox1.SelectedItem, RichTextBox1.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RichTextBox1.Text = "" And ListBox1.SelectedItem = "" Then
            MessageBox.Show("You kinda can't send nothing to nobody. :/", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf ListBox1.SelectedItem = "" Then
            MessageBox.Show("You kinda can't send to nobody. :/", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf RichTextBox1.Text = "" Then
            MessageBox.Show("You kinda can't send nothing. :/", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Timer1.Start()
            MessageBox.Show("Started! :)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
        MessageBox.Show("Stopped! :)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'user info

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Label4.Text = ListBox1.SelectedItem
        PictureBox4.ImageLocation = "http://api.skype.com/users/" + ListBox1.SelectedItem + "/profile/avatar"
        PictureBox4.Cursor = Cursors.Hand
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Process.Start("http://api.skype.com/users/" + ListBox1.SelectedItem + "/profile/avatar")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Skype.ChangeUserStatus(SKYPE4COMLib.TUserStatus.cusOnline)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Skype.ChangeUserStatus(SKYPE4COMLib.TUserStatus.cusAway)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Skype.ChangeUserStatus(SKYPE4COMLib.TUserStatus.cusDoNotDisturb)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Skype.ChangeUserStatus(SKYPE4COMLib.TUserStatus.cusInvisible)
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label5.Text = Skype.CurrentUserProfile.FullName
        RichTextBox2.Text = ""
        RichTextBox2.Text = Skype.CurrentUserProfile.MoodText
        Label6.Text = "(" + Skype.CurrentUserHandle + ")"
        If Skype.CurrentUserStatus = TUserStatus.cusOnline Then
            PictureBox6.Image = My.Resources._on
        ElseIf Skype.CurrentUserStatus = TUserStatus.cusAway Then
            PictureBox6.Image = My.Resources.away
        ElseIf Skype.CurrentUserStatus = TUserStatus.cusDoNotDisturb Then
            PictureBox6.Image = My.Resources.dnd
        ElseIf Skype.CurrentUserStatus = TUserStatus.cusInvisible Then
            PictureBox6.Image = My.Resources.off
        ElseIf Skype.CurrentUserStatus = TUserStatus.cusOffline Then
            PictureBox6.Image = My.Resources.off
        End If
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Process.Start("https://github.com/Syrexide/skype-spammer")
    End Sub

    Private Sub Label7_MouseEnter(sender As Object, e As EventArgs) Handles Label7.MouseEnter
        Label7.Text = "Open source on GitHub."
        Label7.Left += "15"
    End Sub

    Private Sub Label7_MouseLeave(sender As Object, e As EventArgs) Handles Label7.MouseLeave
        Label7.Text = "Skype Spammer by Syrexide"
        Label7.Left -= "15"
    End Sub
End Class
