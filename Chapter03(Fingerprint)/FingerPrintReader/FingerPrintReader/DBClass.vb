'-------------------------------------------------------------------------------
'GrFinger Sample
'(c) 2005 Griaule Tecnologia Ltda.
'http://www.griaule.com
'-------------------------------------------------------------------------------
'
'This sample is provided with "GrFinger Fingerprint Recognition Library" and
'can't run without it. It's provided just as an example of using GrFinger
'Fingerprint Recognition Library and should not be used as basis for any
'commercial product.
'
'Griaule Tecnologia makes no representations concerning either the merchantability
'of this software or the suitability of this sample for any particular purpose.
'
'THIS SAMPLE IS PROVIDED BY THE AUTHOR "AS IS" AND ANY EXPRESS OR
'IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
'OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
'IN NO EVENT SHALL GRIAULE BE LIABLE FOR ANY DIRECT, INDIRECT,
'INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
'NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
'DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
'THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
'(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
'THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'
'You can download the free version of GrFinger directly from Griaule website.
'
'These notices must be retained in any copies of any part of this
'documentation and/or sample.
'
'-------------------------------------------------------------------------------

' -----------------------------------------------------------------------------------
' Database routines
' -----------------------------------------------------------------------------------

Imports System.Data.OleDb
Imports System.Runtime.InteropServices

' Template data
Public Class TTemplate
	' Template itself
    Public tpt As System.Array = Array.CreateInstance(GetType(Byte), GrFingerXLib.GRConstants.GR_MAX_SIZE_TEMPLATE)


	' Template size
    Public Size As Long
End Class

' Template list
Public Structure TTemplates
	' ID
	Public ID As Integer
	' Template itself
	Public template As TTemplate
End Structure

Public Class DBClass

	' the database we'll be connecting to
    Const DBFile As String = "GrFingerSample.mdb"
    Const ConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="

	' the connection object
	Dim connection As New OleDbConnection

	' Open connection
	Public Function OpenDB() As Boolean
		Dim filePath As String
		Try
			filePath = Application.StartupPath() & "\" & DBFile
			connection = New OleDb.OleDbConnection(ConnectionString & filePath)
			Return True
		Catch
			Return False
		End Try
	End Function

	' Close conection
	Public Sub closeDB()
		connection.Close()
	End Sub

	' Clear database
	Public Sub clearDB()
		Dim sqlCMD As OleDbCommand = New OleDbCommand("DELETE FROM enroll", connection)
		' run "clear" query
		sqlCMD.Connection.Open()
		sqlCMD.ExecuteNonQuery()
		sqlCMD.Connection.Close()
	End Sub

	' Add template to database. Returns added template ID.
	Public Function AddTemplate(ByRef template As TTemplate) As Long
		Dim da As New OleDbDataAdapter("select * from enroll", connection)

		' Create SQL command containing ? parameter for BLOB.
		da.InsertCommand = New OleDbCommand("INSERT INTO enroll (template) Values(?)", connection)
		da.InsertCommand.CommandType = CommandType.Text
		da.InsertCommand.Parameters.Add("@template", OleDbType.Binary, template.Size, "template")

		' Open connection
		connection.Open()

		' Fill DataSet.
		Dim enroll As DataSet = New DataSet
		da.Fill(enroll, "enroll")

		' Add a new row.
        ' Create parameter for ? contained in the SQL statement.
		Dim newRow As DataRow = enroll.Tables("enroll").NewRow()
		newRow("template") = template.tpt
		enroll.Tables("enroll").Rows.Add(newRow)

		' Include an event to fill in the Autonumber value.
		AddHandler da.RowUpdated, New OleDbRowUpdatedEventHandler(AddressOf OnRowUpdated)

		' Update DataSet.
		da.Update(enroll, "enroll")
		connection.Close()

		' return ID
		Return newRow("ID")
	End Function

	' Event procedure for OnRowUpdated
	Private Sub OnRowUpdated(ByVal sender As Object, ByVal args As OleDbRowUpdatedEventArgs)
		' Include a variable and a command to retrieve identity value
		' from Access database.
		Dim newID As Integer = 0
		Dim idCMD As OleDbCommand = New OleDbCommand("SELECT @@IDENTITY", connection)

		If args.StatementType = StatementType.Insert Then
			' Retrieve identity value and store it in column
			newID = CInt(idCMD.ExecuteScalar())
			args.Row("ID") = newID
		End If
	End Sub

	' Returns a DataTable with all enrolled templates from database.
	Public Function getTemplates() As TTemplates()
		Dim ds As New DataSet
		Dim da As New OleDbDataAdapter("select * from enroll", connection)
		Dim ttpts As TTemplates()
		Dim i As Integer

		' Get query response
		da.Fill(ds)
		Dim tpts As DataRowCollection = ds.Tables(0).Rows
		' Create response array
		ReDim ttpts(tpts.Count)
		' No results?
		If tpts.Count = 0 Then Return ttpts
		' get each template and put results in our array
		For i = 1 To tpts.Count
			ttpts(i).template = New TTemplate
			ttpts(i).ID = tpts.Item(i - 1).Item("ID")
			ttpts(i).template.tpt = tpts.Item(i - 1).Item("template")
			ttpts(i).template.Size = ttpts(i).template.tpt.Length
		Next
		Return ttpts
	End Function

	' Returns template with supplied ID.
	Public Function getTemplate(ByVal id As Long) As Byte()
        Dim ds As New DataSet
		Dim da As New OleDbDataAdapter("select * from enroll where ID = " & id, connection)
		Dim tpt As New TTemplate

		' Get query response
		da.Fill(ds)
		Dim tpts As DataRowCollection = ds.Tables(0).Rows
		' No results?
        If tpts.Count <> 1 Then Return Nothing
        ' Deserialize template and return it
        Return tpts.Item(0).Item("template")
	End Function

End Class
