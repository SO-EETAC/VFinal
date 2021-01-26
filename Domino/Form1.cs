using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Domino
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;

        delegate void DelegadoParaLeer();
        delegate void DelegadoParaEscribir(string mensaje);

        List<Form2> formularios = new List<Form2>();

        Form2 F2;

        string conectados;

        string usuario;
        string contraseña;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public void LeerUsuarioBox ()
        {
            usuario = usuarioBox.Text;
          
        }
        public void LeerContraseñaBox()
        {
            contraseña = contraseñaBox.Text;

        }

        public void EscribirCuentaBoxRegistrar(string m)
        {
            if (m == "-1")
            {
                respuestaMicuentaBox.Text = "Problemas al consultar base de datos";
            }
            else if (m == "-2")
            {
                respuestaMicuentaBox.Text = "Este usuario ya existe";
                usuarioBox.ForeColor = Color.Red;
                contraseñaBox.Text = "";

            }
            else if (m == "-3")
            {
                respuestaMicuentaBox.Text = "El servidor no acepta más registros";
                contraseñaBox.Text = "";
                contraseñaBox.Text = "";

            }
            else if ((usuario == null) || (contraseña == null))
            {
                respuestaMicuentaBox.Text = "Falta algun campo por rellenar";
            }
            else
            {
                respuestaMicuentaBox.Text = "En línea";
                respuestaMicuentaBox.ForeColor = Color.Green;
            }
        }

        public void EscribirCuentaBoxIniciar(string m)
        {
            if (m == "-1")
            {
                respuestaMicuentaBox.Text = "Problemas al consultar base de datos";
            }
            else if (m == "-2")
            {
                respuestaMicuentaBox.Text = "Este usuario no existe";
                usuarioBox.ForeColor = Color.Red;
                contraseñaBox.Text = "";

            }
            else if (m == "-3")
            {
                respuestaMicuentaBox.Text = "El servidor no acepta más registros";
                contraseñaBox.Text = "";
                contraseñaBox.Text = "";

            }
            else if (m == "-4")
            {
                respuestaMicuentaBox.Text = "Contraseña incorrecta";
                contraseñaBox.Text = "";

            }
            else if ((usuario == null) || (contraseña == null))
            {
                respuestaMicuentaBox.Text = "Falta algun campo por rellenar";
            }
            else
            {
                respuestaMicuentaBox.Text = "En línea";
                respuestaMicuentaBox.ForeColor = Color.Green;
            }
        }

        public void EscribirUsuarioBoxIniciar(string m)
        {
            if ((m != "-1") && (m != "-3") && (m != "-3") && ((usuario == null) || (contraseña) == null))
            {
                if( m == "-2")
                {
                    usuarioBox.ForeColor = Color.Red;
                }
                else
                {
                    usuarioBox.ForeColor = Color.Green;
                }
                
            }
            
        }

        public void EscribirContraseñaBoxIniciar(string m)
        {
            if ((m != "-1") && ((usuario == null) || (contraseña) == null))
            {
                if ((m == "-2")||(m ==  "-3"))
                {
                    contraseñaBox.Text = "";
                }
                else if (m == "-4")
                {
                    contraseñaBox.ForeColor = Color.Red;
                }
                else
                {
                    contraseñaBox.ForeColor = Color.Green;
                }
                
            }
        }

        public void EscribirHola(string m)
        {
            if ((m != "-1")&& (m != "-2") && (m != "-3") &&((usuario == null)||(contraseña)==null))
            {
                holaLbl.Text = "¡Hola " + m + "!";
                contraseñaBox.ForeColor = Color.Green;
            }
        }

        private void AtenderServidor()
        {
            while(true)
            {
                //Recibimos la mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                //MessageBox.Show(Encoding.ASCII.GetString(msg2));
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje;

                int nform;

                string[] seg;



                DelegadoParaLeer delegado = new DelegadoParaLeer(LeerUsuarioBox);
                usuarioBox.Invoke(delegado);
                DelegadoParaLeer delegado2 = LeerContraseñaBox;
                contraseñaBox.Invoke(delegado2);

                switch (codigo)
                {
                    case 1:

                        //RESPUESTA DE INICIAR SESION: 2/num_entero {-1,-2,-3,-4} o 2/nombre

                        //Dependendiendo del mensaje que recibamos, escribiremos mensajes diferentes

                        /***********************************************************************
                        //Escribimos en el TextBox "respuestaMicuentaBox" a través de un delegado
                        DelegadoParaEscribir delegado5 = new DelegadoParaEscribir(EscribirCuentaBoxIniciar);
                        respuestaMicuentaBox.Invoke(delegado5, new object[] { mensaje });
                        //Escribimos en el Label "holaLbl" a través de un delegado
                        DelegadoParaEscribir delegado6 = new DelegadoParaEscribir(EscribirHola);
                        holaLbl.Invoke(delegado6, new object[] { mensaje });
                        //Escribimos en el TextBpx "usuarioBox" a través de un delegado
                        DelegadoParaEscribir delegado7 = new DelegadoParaEscribir(EscribirUsuarioBoxIniciar);
                        usuarioBox.Invoke(delegado7, new object[] { mensaje });
                        //Escribimos en el TextBpx "contraseñaBox" a través de un delegado
                        DelegadoParaEscribir delegado8 = new DelegadoParaEscribir(EscribirContraseñaBoxIniciar);
                        contraseñaBox.Invoke(delegado8, new object[] { mensaje });
                        ************************************************************************/

                        mensaje = trozos[1].Split('\0')[0];

                        if (mensaje == "-1")
                        {

                            respuestaMicuentaBox.Text = "Problemas al consultar base de datos";
                            respuestaMicuentaBox.ForeColor = Color.Red;
                        }
                        else if (mensaje == "-2")
                        {
                            respuestaMicuentaBox.Text = "Este usuario no existe";
                            respuestaMicuentaBox.ForeColor = Color.Red;
                            contraseñaBox.Text = "";
                        }
                        else if (mensaje == "-3")
                        {
                            respuestaMicuentaBox.Text = "El servidor no acepta más usuarios conectados";
                            respuestaMicuentaBox.ForeColor = Color.Red;
                            contraseñaBox.Text = "";
                            contraseñaBox.Text = "";
                        }
                        else if (mensaje == "-4")
                        {
                            respuestaMicuentaBox.Text = "Contraseña incorrecta";
                            respuestaMicuentaBox.ForeColor = Color.Red;
                            contraseñaBox.Text = "";
                        }
                        else
                        {
                            holaLbl.Text = " ¡Hola " + mensaje + "!";
                            respuestaMicuentaBox.Text = "EN LINEA";
                            respuestaMicuentaBox.ForeColor = Color.Green;
                            usuarioBox.ForeColor = Color.Green;
                            contraseñaBox.ForeColor = Color.Green;
                        }
                        break;

                    case 2:

                        //RESPUESTA DE REGISTRARSE:  1/num_entero {-1,-2,-3} o 1/nombre

                        //Dependendiendo del mensaje que recibamos, escribiremos mensajes diferentes

                        /*****************************************************************************
                        DelegadoParaEscribir delegado3 = new DelegadoParaEscribir(EscribirCuentaBoxRegistrar);
                        respuestaMicuentaBox.Invoke(delegado3, new object[] { mensaje });
                        DelegadoParaEscribir delegado4 = EscribirHola;
                        holaLbl.Invoke(delegado4, new object[] { mensaje });
                        *****************************************************************************/
                        
                        mensaje = trozos[1].Split('\0')[0];

                        if (mensaje == "-1")
                        {
                            respuestaMicuentaBox.Text = "Problemas al consultar base de datos";
                            respuestaMicuentaBox.ForeColor = Color.Red;
                        }
                        else if (mensaje == "-2")
                        {
                            respuestaMicuentaBox.Text = "Este usuario ya existe";
                            respuestaMicuentaBox.ForeColor = Color.Red;
                            contraseñaBox.Text = "";
                        }
                        else if (mensaje == "-3")
                        {
                            respuestaMicuentaBox.Text = "El servidor no acepta más registros";
                            respuestaMicuentaBox.ForeColor = Color.Red;
                            contraseñaBox.Text = "";
                            contraseñaBox.Text = "";
                        }

                        else
                        {
                            holaLbl.Text = " ¡Hola  " + mensaje + "!";
                            respuestaMicuentaBox.Text = "En línea";
                            respuestaMicuentaBox.ForeColor = Color.Green;
                            usuarioBox.ForeColor = Color.Green;
                            contraseñaBox.ForeColor = Color.Green;
                            nombreLbl.Visible = false;
                            nombreBox.Visible = false;
                        }

                        break;

                        

                    case 3:

                        //Respuesta consulta ganador por duracion: 3/num_entero {-1,-2} o 3/nombre_ganador-usuario_ganador

                        mensaje = trozos[1].Split('\0')[0];

                        if (mensaje == "-1")
                        {
                            respuestaConsultaBox.Text = "Problemas al consultar base de datos";
                        }
                        else if (mensaje == "-2")
                        {
                            respuestaConsultaBox.Text = "No hay ganador en esa fecha";
                        }
                        else
                        {
                            seg = mensaje.Split(new char[] { '-' }, 2);

                            respuestaConsultaBox.Text = seg[0] + " (usuario: " + seg[1] + ")";
                        }
                        break;

                    case 4:

                        //Respuesta consulta ganador por fecha: 4/num_entero {-1,-2} o 4/nombre_ganador-usuario_ganador

                        mensaje = trozos[1].Split('\0')[0];

                        if (mensaje == "-1")
                        {
                            respuestaConsultaBox.Text = "Problemas al consultar base de datos";
                        }
                        else if (mensaje == "-2")
                        {
                            respuestaConsultaBox.Text = "No hay ganador en esa fecha";
                        }
                        else
                        {
                            seg = mensaje.Split(new char[] { '-' }, 2);

                            respuestaConsultaBox.Text = seg[0] + " (usuario: " + seg[1] + ")";
                        }
                        break;

                    case 5:
                        //ME HAN INVITADO

                        //Respuesta: 5/numForm/id_partida/num_jugadores/jug1-jug2-jug3...


                        int nForm = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        //Recogemos los datos que nos da la respuesta
                        seg = mensaje.Split(new char[] { '_' }, 3);
                        
                        int id_partida = Convert.ToInt32(seg[0]);
                        int num_jugadores = Convert.ToInt32(seg[1]);
                        string jugadores = seg[2];

                        string message = "Te han invitado, aceptas?";
                        string title = "Invitación";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result = MessageBox.Show(message, title, buttons);
                        if (result == DialogResult.Yes)
                        {
                            mensaje = "6/" + nForm + "/" + id_partida + "/" + num_jugadores + "/" + usuario + "/1/" + jugadores;
                            // Enviamos al servidor el nombre tecleado
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }
                        else
                        {
                            // Do something  
                            mensaje = "6/" + nForm + "/" + id_partida + "/" + num_jugadores + "/" + usuario + "/0/" + jugadores;
                            // Enviamos al servidor el nombre tecleado
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }


                        /*
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].TomaRespuesta5(mensaje);
                        */
                      

                        /*********************************************************************************
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        seg = mensaje.Split(new char[] { '_' }, 4);
                        int id_partida = Convert.ToInt32(seg[0]);
                        int num_jugadores = Convert.ToInt32(seg[1]);
                        string jugadores = seg[2];
                        string listado = Convert.ToString(num_jugadores) + "/" + jugadores;

                        //Ponemos en marcha un thread para cada partida que abramos
                        ThreadStart ts = delegate { PonerEnMarchaFormulario2Invitados(0, usuario, listado, id_partida); };
                        Thread T = new Thread(ts);
                        T.Start();*/

                        break;

                    case 6:

                        //RESPUESTAS INVITACIONES: 6/0/0 (empezar partida) ó 6/0/-1(no se puede jugar)

                        /*********************************************************************************
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        Form2 f = formularios[nform];
                        f.TomaRespuesta5(mensaje);
                        **********************************************************************************/
                        
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        
                        int iniciar = Convert.ToInt32(mensaje);

                        if (iniciar == 0)
                        {
                            //INICIAR PARTIDA

                            MessageBox.Show("Todos han aceptado, la partida va a empezar");

                            //Ponemos en marcha un thread para cada partida que abramos
                            ThreadStart ts = delegate { PonerEnMarchaFormulario2Invitados(); };
                            Thread T = new Thread(ts);
                            T.Start(); 
                                 
                            //formularios[nform].SetVisible();

                        }
                        else
                        {
                            //comunicar que no se va a jugar la partida
                            MessageBox.Show("Algun jugador ha rechazado la invitación, la partida no se jugará");
                        }



                        break;

                    case 7:
                        // RESPUESTA: 7/mensaje
                        mensaje = trozos[1].Split('\0')[0];
                        F2.TomaRespuesta7(mensaje);
                        break;
                    case 8:
                        mensaje = trozos[1].Split('\0')[0];
                        F2.TomaRespuesta8(mensaje);
                        break;
                    case 9:
                        mensaje = trozos[1].Split('\0')[0];
                        F2.TomaRespuesta9(mensaje);
                        break;
                    case 10:
                        mensaje = trozos[1].Split('\0')[0];
                        F2.TomaRespuesta10(mensaje);
                        break;


                    case 20:

                        //NOTIFICACION
                        //Recogemos la lista de jugadores en línea y los mostramos

                        mensaje = trozos[1].Split('\0')[0];

                        conectados = mensaje;
                        string[] segmentos = conectados.Split('_');

                        if (segmentos[0] == "0")
                        {
                            //No hay usuarios conectadosç

                            //Borramos el contenido del listbox
                            listBox1.Items.Clear();


                            //Llenamos listbox
                            listBox1.Items.Add("No hay ningún jugador en linea");

                        }
                        else if (segmentos[0] == "1")
                        {
                            //hay 1 

                            //Borramos el contenido del listbox
                            listBox1.Items.Clear();

                            //Llenamos listbox
                            listBox1.Items.Add("Jugador en línea: ");

                            if (segmentos[1] == usuarioBox.Text) //cuando coincida el nombre del listado con el usuario que te has registrado, en la lista de conectados ponemos(Tu) al lado de tu usuario.
                                listBox1.Items.Add(segmentos[1] + " (Tú)");
                            else
                                listBox1.Items.Add(segmentos[1]);
                        }
                        else
                        {
                            //hay mas de 1

                            //Borramos el contenido del listbox
                            listBox1.Items.Clear();

                            //Llenamos listbox
                            listBox1.Items.Add("Los " + Convert.ToInt32(segmentos[0]) + " jugadores en linea son: ");

                            for (int i = 1; i < (Convert.ToInt32(segmentos[0]) + 1); i++)
                            {
                                if (segmentos[i] == usuarioBox.Text) //cuando coincida el nombre del listado con el usuario que te has registrado, en la lista de conectados ponemos(Tu) al lado de tu usuario.
                                    listBox1.Items.Add(segmentos[i] + " (Tú)");
                                else
                                    listBox1.Items.Add(segmentos[i]);
                            }
                        }
                        break;


                }
            }
        }

        private void ConectarBtn_Click(object sender, EventArgs e)
        {
            ConectarBtn.Visible = true;
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9030);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                groupBox3.BackColor = Color.Green;
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox4.Visible = true;
                groupBox5.Visible = true;
                

            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            //Ponemos en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }

        private void miCuentaBtn_Click(object sender, EventArgs e)
        {
           

            if (iniciarRadioBtn.Checked)
            {
                
                if ((usuarioBox.Text == "") || (contraseñaBox.Text == ""))
                {
                    respuestaMicuentaBox.Text = "Falta algun campo por rellenar";
                    respuestaMicuentaBox.ForeColor = Color.Red;
                }
                else
                {
                    string mensaje = "1/" + usuarioBox.Text + "/" + contraseñaBox.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
            else if (registrarseRadioBtn.Checked)
            {
                
                if ((usuarioBox.Text == "") || (contraseñaBox.Text == "")||(nombreBox.Text == ""))
                {
                    respuestaMicuentaBox.Text = "Falta algun campo por rellenar";
                    respuestaMicuentaBox.ForeColor = Color.Red;
                }
                else
                {
                    string mensaje = "2/" + nombreBox.Text + "/" + usuarioBox.Text + "/" + contraseñaBox.Text ;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar la opción 'Iniciar sesion' o 'Registrarse'");
            }
        }
            

        private void DesconectarBtn_Click(object sender, EventArgs e)
        {
            ConectarBtn.Visible = true;
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Se terminó el servicio. 
            atender.Abort();

            //Despintamos el color verde y rojo
            groupBox3.BackColor = Color.Transparent;
            usuarioBox.ForeColor = Color.Black;
            contraseñaBox.ForeColor = Color.Black;
            respuestaMicuentaBox.ForeColor = Color.Black;

            //Ocultamos los Groupbox
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;

            //Borramos la informacion escrita
            usuarioBox.Text = null;
            contraseñaBox.Text = null;
            respuestaMicuentaBox.Text = null;
            respuestaConsultaBox.Text = null;
            holaLbl.Text = "";

            //Borramos el contenido del listbox
            listBox1.Items.Clear();

            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void InvitarBtn_Click(object sender, EventArgs e)
        {
            string num_invitados = Convert.ToString(listBox1.SelectedIndices.Count + 1);

            string listado_jugadores = num_invitados + "/" + usuario + "-" + listBox1.SelectedItems[0];

            for (int i = 1; i < listBox1.SelectedIndices.Count; i++)
            {

                listado_jugadores = listado_jugadores + "-" + listBox1.SelectedItems[i];

            }

            /*
            //Ponemos en marcha un thread para cada partida que abramos
            ThreadStart ts = delegate { PonerEnMarchaFormulario2Invitados(); };
            Thread T = new Thread(ts);
            T.Start();
            */

            string mensaje = "5/" + formularios.Count + "/" + listado_jugadores;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            

        }
        /*private void PonerEnMarchaFormulario2(int i, string usuario, string listado)
        {
            int cont = formularios.Count;
            Form2 F2 = new Form2(cont, server);
            formularios.Add(F2);

            F2.SetI(i);
            F2.SetUsuario(usuario);
            F2.SetListado(listado);
            F2.ShowDialog();
        }*/
        private void PonerEnMarchaFormulario2Invitados(/*int i, string usuario, string listado, int id*/)
        {
            int cont = formularios.Count;
            F2 = new Form2(cont, server);
            formularios.Add(F2);
            F2.ShowDialog();

            /*
            F2.SetI(i);
            F2.SetUsuario(usuario);
            F2.SetListado(listado);
            F2.SetID(id);
            */
            
        }

        /*private void PonerEnMarchaFormulario3()
        {
            int cont = formularios.Count;
            Form2 F2 = new Form2(cont, server);
            formularios.Add(F2);

           
            F2.ShowDialog();
        }*/
        private void ConsultaBtn_Click(object sender, EventArgs e)
        {

            if (duracionRadioBtn.Checked)
            {
                string mensaje = "3/" + duracionBox.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (fechaRadioBtn.Checked)
            {
                int year = calendar.SelectionRange.Start.Year;
                int month = calendar.SelectionRange.Start.Month;
                int day = calendar.SelectionRange.Start.Day;
                string date = year.ToString() + "-" + month.ToString() + "-" + day.ToString();
                string mensaje = "4/" + date;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Debe seleccionar hacer la consulta 'Por fecha' o 'Por duracion'");
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            
            if (checkBox1.Checked)
            {
                contraseñaBox.UseSystemPasswordChar = false;
                checkBox1.Text = "Ocultar contraseña";
                

            }
            else
            {
                contraseñaBox.UseSystemPasswordChar = true;
                checkBox1.Text = "Mostrar contraseña";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            usuarioBox.Items.Add("anakilator");
            usuarioBox.Items.Add("martita21");
            usuarioBox.Items.Add("juanito23");
            iniciarRadioBtn.Checked = true;
        }

        private void usuarioBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usuarioBox.SelectedItem != null)
            {
                if (usuarioBox.SelectedItem.ToString() == "anakilator")
                {
                    contraseñaBox.Text = "top568";
                }
                else if (usuarioBox.SelectedItem.ToString() == "martita21")
                {
                    contraseñaBox.Text = "contra";

                }
                else if (usuarioBox.SelectedItem.ToString() == "juanito23")
                {
                    contraseñaBox.Text = "hola12";
                }
            }
        }

        private void iniciarRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (iniciarRadioBtn.Checked)
            {
                miCuentaBtn.Text = "INICIAR SESION";
                nombreLbl.Visible = false;
                nombreBox.Visible = false;
            }
            else 
            {
                miCuentaBtn.Text = "REGISTRARSE";
                nombreLbl.Visible = true;
                nombreBox.Visible = true;
            }
        }
    }
    
}
