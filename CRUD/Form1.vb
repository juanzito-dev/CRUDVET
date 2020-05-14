Imports MySql.Data.MySqlClient
Public Class Form1
    Dim cmd As New MySqlCommand

    Dim server As String = "server=localhost;  database=veterinaria;  Uid=root;  Pwd=;"
    Dim conexion As New MySqlConnection(server)
    Sub actualizar()
        conexion.Open()
        Dim consulta As String = "SELECT * FROM perros"
        Dim adap As New MySqlDataAdapter(consulta, conexion)
        Dim dataset As New DataSet()
        adap.Fill(dataset, "Grid")
        data_animales.DataSource = dataset.Tables(0)
        conexion.Close()

        'siempre limpia campos
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub

    Private Sub btn_actualizar_Click(sender As Object, e As EventArgs) Handles btn_actualizar.Click
        actualizar()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        actualizar()
    End Sub
    Private Sub data_animales_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles data_animales.CellContentClick
        Dim fila As DataGridViewRow = data_animales.CurrentRow
        Try
            TextBox1.Text = fila.Cells(0).Value.ToString()
            TextBox2.Text = fila.Cells(1).Value.ToString()
            TextBox3.Text = fila.Cells(2).Value.ToString()
            TextBox4.Text = fila.Cells(3).Value.ToString()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub data_aniamales_SelectionChanged(sender As Object, e As EventArgs) Handles data_animales.SelectionChanged
        If (data_animales.SelectedRows.Count > 0) Then
            TextBox1.Text = data_animales.Item("nombre", data_animales.SelectedRows(0).Index).Value
            TextBox2.Text = data_animales.Item("raza", data_animales.SelectedRows(0).Index).Value
            TextBox3.Text = data_animales.Item("color", data_animales.SelectedRows(0).Index).Value
            TextBox4.Text = data_animales.Item("id", data_animales.SelectedRows(0).Index).Value
        End If
    End Sub

    Private Sub btn_limpiar_Click(sender As Object, e As EventArgs) Handles btn_limpiar.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub

    Private Sub btn_ingresar_Click(sender As Object, e As EventArgs) Handles btn_ingresar.Click
        conexion.Open()
        Try
            cmd = conexion.CreateCommand
            cmd.CommandText = "INSERT INTO perros(id, nombre, raza, color) VALUES('', @nombre, @raza, @color);"
            cmd.Parameters.AddWithValue("@nombre", TextBox1.Text)
            cmd.Parameters.AddWithValue("@raza", TextBox2.Text)
            cmd.Parameters.AddWithValue("@color", TextBox3.Text)
            cmd.ExecuteNonQuery()
            conexion.Close()
            actualizar()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btn_editar_Click(sender As Object, e As EventArgs) Handles btn_editar.Click
        Dim cmd As MySqlCommand
        conexion.Open()
        Try
            cmd = conexion.CreateCommand()
            cmd.CommandText = "UPDATE perros SET nombre=@nombre, raza=@raza, color=@color WHERE id=@id"
            cmd.Parameters.AddWithValue("@nombre", TextBox1.Text)
            cmd.Parameters.AddWithValue("@raza", TextBox2.Text)
            cmd.Parameters.AddWithValue("@color", TextBox3.Text)
            cmd.Parameters.AddWithValue("@id", TextBox4.Text)
            cmd.ExecuteNonQuery()
            conexion.Close()
            actualizar()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btn_borrar_Click(sender As Object, e As EventArgs) Handles btn_borrar.Click
        Dim cmd As MySqlCommand
        Dim resultado As DialogResult = MessageBox.Show("¿Seguro de que desea eliminar los datos seleccionados?",
                              "Confirmación de eliminación de datos", MessageBoxButtons.OKCancel)
        If (resultado = DialogResult.OK) Then
            conexion.Open()
            cmd = conexion.CreateCommand()
            cmd.CommandText = "DELETE FROM perros WHERE id=@id"
            cmd.Parameters.AddWithValue("@id", TextBox4.Text)
            cmd.ExecuteNonQuery()
            conexion.Close()
            actualizar()
        End If
    End Sub
End Class
