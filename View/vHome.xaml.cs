namespace ddelgadoT1.View;

using System.Text;

public partial class vHome : ContentPage
{
	public vHome()
	{
		InitializeComponent();
	}

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // 1. Validar los campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNumero.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoPaterno.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoMaterno.Text) ||
                string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmarCorreo.Text))
            {
                await DisplayAlert(
                    "Atención",
                    "Complete todos los datos personales.",
                    "Aceptar");
                return;
            }

            // 2. Validar que los correos coincidan
            if (!string.Equals(
                    txtCorreo.Text.Trim(),
                    txtConfirmarCorreo.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase))
            {
                await DisplayAlert(
                    "Error",
                    "Los correos electrónicos no coinciden.",
                    "Aceptar");
                return;
            }

            // 3. Validar las listas desplegables
            if (cmbIdentificacion.SelectedIndex == -1 ||
                cmbCarrera.SelectedIndex == -1 ||
                cmbModalidad.SelectedIndex == -1 ||
                cmbCampus.SelectedIndex == -1)
            {
                await DisplayAlert(
                    "Atención",
                    "Seleccione el tipo de identificación, carrera, modalidad y campus.",
                    "Aceptar");
                return;
            }

            // 4. Crear el contenido del archivo TXT
            string contenido =
                "========== FORMULARIO DE INSCRIPCIÓN ==========\r\n\r\n" +
                "FASE A - GRADO\r\n\r\n" +
                "DATOS PERSONALES\r\n" +
                "Tipo de identificación: " + cmbIdentificacion.SelectedItem + "\r\n" +
                "Número de identificación: " + txtNumero.Text.Trim() + "\r\n" +
                "Apellido paterno: " + txtApellidoPaterno.Text.Trim() + "\r\n" +
                "Apellido materno: " + txtApellidoMaterno.Text.Trim() + "\r\n" +
                "Nombres completos: " + txtNombres.Text.Trim() + "\r\n" +
                "Teléfono personal: " + txtTelefono.Text.Trim() + "\r\n" +
                "Correo personal: " + txtCorreo.Text.Trim() + "\r\n\r\n" +
                "DATOS ACADÉMICOS\r\n" +
                "Carrera: " + cmbCarrera.SelectedItem + "\r\n" +
                "Modalidad: " + cmbModalidad.SelectedItem + "\r\n" +
                "Campus: " + cmbCampus.SelectedItem + "\r\n\r\n" +
                "Fecha de registro: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") +
                "\r\n\r\n===============================================\r\n";

            // 5. Guardar el TXT en el directorio de la aplicación
            string ruta = Path.Combine(FileSystem.AppDataDirectory, "ddelgadoT1.txt");

            await File.WriteAllTextAsync(ruta, contenido);

            // 6. Confirmar que se guardó
            await DisplayAlert(
                "Inscripción guardada",
                "El archivo TXT se generó correctamente.",
                "Aceptar");

            // Abrir el archivo
            await Launcher.Default.OpenAsync(
                new OpenFileRequest
                {
                    Title = "Abrir archivo de texto",
                    File = new ReadOnlyFile(ruta)
                });
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                "No se pudo exportar el archivo TXT.\n" + ex.Message,
                "Aceptar");
        }
    }
}