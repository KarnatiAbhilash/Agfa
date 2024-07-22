Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports System.ComponentModel
Imports System.Text
Public Class ExpotToExcel
    Implements INamingContainer

    Private Const C_HTTP_HEADER_CONTENT As String = "Content-Disposition"
    Private Const C_HTTP_ATTACHMENT As String = "attachment;filename="
    Private Const C_HTTP_INLINE As String = "inline;filename="
    Private Const C_HTTP_CONTENT_TYPE_EXCEL As String = "application/vnd.ms-excel"
    Private Const C_HTTP_CONTENT_LENGTH As String = "Content-Length"
    Private Const C_ERROR_NO_RESULT As String = "Data not found."
    Dim _FileNameToExport As String
    Dim _Dataview As Dataview

#Region "Properties"
    'To accept the filename of the excel to be downloaded by the user.
    Public Property FileNameToExport() As String
        Get
            Return _FileNameToExport
        End Get
        Set(ByVal Value As String)
            _FileNameToExport = Value
        End Set
    End Property

    'Dataveiw from which the data has to be export to excel sheet.
    Public Property [Dataview]() As Dataview
        Get
            Return _Dataview
        End Get
        Set(ByVal Value As Dataview)
            _Dataview = Value
        End Set
    End Property
#End Region

#Region "CustomExport"
    'To export the details to the excelsheet and display the download dialog box.
    Public Sub CustomExport()
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
        response.Clear()
        ' Add the header that specifies the default filename 
        ' for the Download/SaveAs dialog
        response.AddHeader(C_HTTP_HEADER_CONTENT, C_HTTP_ATTACHMENT & FileNameToExport)

        ' Specify that the response is a stream that cannot be read _
        ' by the client and must be downloaded

        response.ContentType = C_HTTP_CONTENT_TYPE_EXCEL

        Dim _exportContent As String = String.Empty
        If (Not _Dataview Is Nothing) AndAlso _Dataview.Table.Rows.Count > 0 Then
            Dim dv As Dataview = _Dataview
            _exportContent = ConvertDataViewToString(dv, "", vbTab)
        End If
        If _exportContent.Length <= 0 Then
            _exportContent = C_ERROR_NO_RESULT
        End If

        Dim Encoding As New System.Text.UTF8Encoding()
        response.AddHeader(C_HTTP_CONTENT_LENGTH, Encoding.GetByteCount(_exportContent).ToString())
        response.BinaryWrite(Encoding.GetBytes(_exportContent))
        response.Charset = ""

        ' Stop execution of the current page
        response.End()
    End Sub

#End Region

#Region "ConvertDataViewToString"
    'To convert the data in the dataview to a string and return the string
    Private Function ConvertDataViewToString(ByVal srcDataView As Dataview, Optional ByVal Delimiter As String = Nothing, Optional ByVal Separator As String = ",") As String

        Dim ResultBuilder As StringBuilder
        ResultBuilder = New StringBuilder()
        ResultBuilder.Length = 0

        Dim aCol As DataColumn
        For Each aCol In srcDataView.Table.Columns
            ResultBuilder.Append(aCol.ColumnName)
            ResultBuilder.Append(Separator)
        Next
        If ResultBuilder.Length > Separator.Trim.Length Then
            ResultBuilder.Length = ResultBuilder.Length - Separator.Trim.Length
        End If
        ResultBuilder.Append(Environment.NewLine)

        Dim aRow As DataRowView 'DataRow
        For Each aRow In srcDataView 'srcDataView.Rows
            For Each aCol In srcDataView.Table.Columns
                If Not Delimiter Is Nothing AndAlso (Delimiter.Trim.Length > 0) Then
                    ResultBuilder.Append(Delimiter)
                End If
                ResultBuilder.Append(aRow(aCol.ColumnName))
                If Not Delimiter Is Nothing AndAlso (Delimiter.Trim.Length > 0) Then
                    ResultBuilder.Append(Delimiter)
                End If
                ResultBuilder.Append(Separator)
            Next aCol
            ResultBuilder.Length = ResultBuilder.Length - 1
            ResultBuilder.Append(vbNewLine)
        Next aRow
        If Not ResultBuilder Is Nothing Then
            Return ResultBuilder.ToString()
        Else
            Return String.Empty
        End If
    End Function
#End Region

End Class
