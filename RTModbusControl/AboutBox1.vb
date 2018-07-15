Imports System.IO

Public NotInheritable Class AboutBox1
    Private Sub AboutBox1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim applicationTitle As String
        If My.Application.Info.Title <> "" Then
            applicationTitle = My.Application.Info.Title
        Else
            applicationTitle = Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Text = $"About {applicationTitle}"
        ' Initialize all of the text displayed on the About Box.
        LabelProductName.Text = My.Application.Info.ProductName
        LabelVersion.Text = $"Version {My.Application.Info.Version.ToString}"
        LabelCopyright.Text = My.Application.Info.Copyright
        LabelCompanyName.Text = My.Application.Info.CompanyName
        TextBoxDescription.Text = My.Application.Info.Description
    End Sub

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Close()
    End Sub

    Private Shared Sub LabelProductName_Click(sender As Object, e As EventArgs) Handles LabelProductName.Click
    End Sub
End Class
