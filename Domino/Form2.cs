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
    public partial class Form2 : Form
    {
        int nForm;
        Socket server;

        //inici hector
        string[] nuevos_trozos; 
        Coordenada punto;


        ListaPosiciones posiciones_validas;

        Coordenada posicion_seleccionada;

        Ficha ficha1;

        int pon_este, pon_otro, columnas_filas;

        int actual_index;

        delegate void DelegadoParaActualizarPosicionesValidas(Coordenada posicion);
        delegate void DelegadoParaActualizarTablero(Coordenada punto, int num);
        delegate void DelegadoParaPonerTexto(string texto);
        delegate void DelegadoParaGestionarTurno();

        //fin hector



        string listado_jugadores;
        int num_jugadores;
        string jugadores;
        string[] jugador;
        string[] seg;
        int id_partida;
        string usuario;

        int i = 0;

        public class Coordenada
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Coordenada(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        public class Ficha
        {
            public int NUM_1 { get; set; }
            public int NUM_2 { get; set; }
            public int ID { get; set; }
            public Ficha(int num_1, int num_2, int id)
            {
                NUM_1 = num_1;
                NUM_2 = num_2;
                ID = id;
            }
        }
        public class ListaPosiciones
        {
            public int NUM { get; set; }
            public Coordenada[] LISTA_COORDENADAS { get; set; }
            public ListaPosiciones(int num, Coordenada[] lista_coordenadas)
            {
                NUM = num;
                LISTA_COORDENADAS = lista_coordenadas;
            }
        }

        public Form2(int nForm, Socket server)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
        }

        //inici hector

        //El primer delegado lo usaremos para añadir una posición jugable
        private void AgregaPosicionValida(Coordenada posicion)
        {
            this.Añadir_Posicion_Jugable(posicion);
        }

        //El segundo delegado lo usaremos para quitar una posición jugable
        private void QuitaPosicionValida(Coordenada posicion)
        {
            this.Eliminar_Posicion_Jugable(posicion);
        }

        //El tercer delegado lo usaremos para poner un número en el tablero
        private void ActualizaTablero(Coordenada punto, int num)
        {
            this.dataGridView1.Rows[punto.Y].Cells[punto.X].Value = num;
        }

        //El cuarto delegado lo usaremos para poner una ficha en el cliente
        private void PonFicha(string ficha)
        {
            this.listBox2.Items.Add(ficha);
        }

        private void SiTurno()
        {
            this.button1.Enabled = true;
            this.robar_btn.Enabled = true;
        }

        private void PonMensajeServidor(string mensaje)
        {
            DateTime dt = DateTime.Now;
            string Timeonly = dt.ToLongTimeString();
            this.listBox3.Items.Add(Timeonly + ": " + mensaje);
        }
        //fin hector

        public void SetI(int i) //i = 1 si es el invitador
        {
            this.i = i;
        }

        public void SetUsuario(string usuario) //i = 1 si es el invitador
        {
            this.usuario = usuario;
        }

        public void SetListado(string listado) // num_jug/jug1-jug2
        {
            this.listado_jugadores = listado;
        }

        public void SetID(int id) // num_jug/jug1-jug2
        {
            this.id_partida = id;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Le enviamos un mensaje al servidor conforme queremos una ficha
            byte[] msg = System.Text.Encoding.ASCII.GetBytes("11/");
            server.Send(msg);

            idLbl.Text = "ID: " + Convert.ToString(id_partida) + "    Formulario: " + Convert.ToString(nForm);
            label1.Text = "Jugadores en esta partida: ";

            if (i == 1)
            {
                //Form2 del invitador
                preguntaLbl.Visible = false;
                aceptarBtn.Visible = false;
                rechazarBtn.Visible = false;
            }

            /*seg = listado_jugadores.Split(new char[] { '/' }, 2);
            num_jugadores = Convert.ToInt32(seg[0]);
            jugadores = seg[1];*/
           

            int j = 0;
            while (j < num_jugadores)
            {
                jugador = jugadores.Split(new char[] { '-' }, num_jugadores);
                listBox1.Items.Add(jugador[j]);
                j++;
            }


            //inici hector
            button1.Enabled = false;
            robar_btn.Enabled = false;

            posiciones_validas = new ListaPosiciones(0, new Coordenada[100]);

            posicion_seleccionada = new Coordenada(0, 0);

            columnas_filas = 20;
            dataGridView1.ColumnCount = columnas_filas;
            dataGridView1.RowCount = columnas_filas;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            for (int i = 0; i < columnas_filas; i++)
            {
                dataGridView1.Columns[i].Width = 20;
                dataGridView1.Rows[i].Height = 20;
            }

            for (int x = 0; x < columnas_filas; x++)
            {
                for (int y = 0; y < columnas_filas; y++)
                {
                    dataGridView1.Rows[y].Cells[x].Value = null;
                }
            }

            //Creamos una jugada inicial, añadimos los puntos de inserción, y creamos una ficha para jugar (esto es para las pruebas del juego)

            dataGridView1.Rows[9].Cells[9].Value = 1;
            dataGridView1.Rows[9].Cells[10].Value = 1;
            //dataGridView1.Rows[7].Cells[7].Value = 4;
            //dataGridView1.Rows[6].Cells[7].Value = 3;
            //dataGridView1.Rows[6].Cells[8].Value = 3;
            //dataGridView1.Rows[6].Cells[9].Value = 2;

            dataGridView1.Rows[9].Cells[9].Style.BackColor = Color.Chartreuse;
            dataGridView1.Rows[9].Cells[10].Style.BackColor = Color.Chartreuse;

            Coordenada punto1 = new Coordenada(9, 9);
            Coordenada punto2 = new Coordenada(10, 9);

            Añadir_Posicion_Jugable(punto1);
            Añadir_Posicion_Jugable(punto2);

            //ficha1 = new Ficha(6, 4, 23);

            //listBox1.Items.Add("2-1");
            //listBox1.Items.Add("2-2");

            //listBox1.Items.Add("3-1");
            //listBox1.Items.Add("3-2");
            //listBox1.Items.Add("3-3");

            //listBox1.Items.Add("4-1");
            //listBox1.Items.Add("4-2");
            //listBox1.Items.Add("4-3");
            //listBox1.Items.Add("4-4");

            //listBox1.Items.Add("5-1");
            //listBox1.Items.Add("5-2");
            //listBox1.Items.Add("5-3");
            //listBox1.Items.Add("5-4");
            //listBox1.Items.Add("5-5");

            //listBox1.Items.Add("6-1");
            //listBox1.Items.Add("6-2");
            //listBox1.Items.Add("6-3");
            //listBox1.Items.Add("6-4");
            //listBox1.Items.Add("6-5");
            //listBox1.Items.Add("6-6");



            //fin hector



        }





        //inici hector
        private void Añadir_Posicion_Jugable(Coordenada posicion)
        {
            //Añadimos una posición de juego a la lista
            posiciones_validas.LISTA_COORDENADAS[posiciones_validas.NUM] = posicion;
            posiciones_validas.NUM++;
        }

        private void Eliminar_Posicion_Jugable(Coordenada posicion)
        {
            //Eliminamos una posición de juego a la lista
            for (int i = 0; i < posiciones_validas.NUM; i++)
            {
                if ((posicion.X == posiciones_validas.LISTA_COORDENADAS[i].X) && (posicion.Y == posiciones_validas.LISTA_COORDENADAS[i].Y))
                {
                    posiciones_validas.LISTA_COORDENADAS[i].X = -1;
                    posiciones_validas.LISTA_COORDENADAS[i].Y = -1;
                }
            }
        }

        private void Notificar_Numero_Nuevo(Coordenada posicion, int numero)
        {
            //Notificamos a los demás jugadores
            string peticion = "7/" + Convert.ToString(posicion.X) + " " + Convert.ToString(posicion.Y) + " " + Convert.ToString(numero);
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(peticion);
            server.Send(msg);
        }

        private void Pintar_Posiciones_Jugables()
        {
            for (int x = 0; x < columnas_filas; x++)
            {
                for (int y = 0; y < columnas_filas; y++)
                {
                    dataGridView1.Rows[y].Cells[x].Style.BackColor = Color.White;
                }
            }
            for (int i = 0; i < posiciones_validas.NUM; i++)
            {
                if ((posiciones_validas.LISTA_COORDENADAS[i].X != -1) && (posiciones_validas.LISTA_COORDENADAS[i].Y != -1))
                {
                    dataGridView1.Rows[posiciones_validas.LISTA_COORDENADAS[i].Y].Cells[posiciones_validas.LISTA_COORDENADAS[i].X].Style.BackColor = Color.Chartreuse;
                }
            }
        }

        private void Limpiar_Asteriscos()
        {
            for (int x = 0; x < columnas_filas; x++)
            {
                for (int y = 0; y < columnas_filas; y++)
                {
                    if (dataGridView1.Rows[y].Cells[x].Value == "*")
                    {
                        dataGridView1.Rows[y].Cells[x].Value = null;
                    }
                }
            }
        }

        private int Comprobar_Celdas_Colindantes_Si0(Coordenada punto)
        {
            Coordenada arriba = new Coordenada(punto.X, punto.Y - 1);
            Coordenada abajo = new Coordenada(punto.X, punto.Y + 1);
            Coordenada izquierda = new Coordenada(punto.X - 1, punto.Y);
            Coordenada derecha = new Coordenada(punto.X + 1, punto.Y);

            int ocupada = 0;

            if (Convert.ToInt32(dataGridView1.Rows[arriba.Y].Cells[arriba.X].Value) != 0)
            {
                ocupada++;
            }
            if (Convert.ToInt32(dataGridView1.Rows[abajo.Y].Cells[abajo.X].Value) != 0)
            {
                ocupada++;
            }
            if (Convert.ToInt32(dataGridView1.Rows[izquierda.Y].Cells[izquierda.X].Value) != 0)
            {
                ocupada++;
            }
            if (Convert.ToInt32(dataGridView1.Rows[derecha.Y].Cells[derecha.X].Value) != 0)
            {
                ocupada++;
            }

            return ocupada;
        }

        private void Comprobar_Afinidad_Ficha(Ficha ficha_jugada, Coordenada punto_insercion)
        {
            //Primero comprobamos si punto de inserción está en la lista de posiciones válidas

            Boolean encontrado = false;
            string peticion;

            for (int i = 0; i < posiciones_validas.NUM; i++)
            {
                if ((punto_insercion.X == posiciones_validas.LISTA_COORDENADAS[i].X) && (punto_insercion.Y == posiciones_validas.LISTA_COORDENADAS[i].Y))
                {
                    encontrado = true;
                }
            }
            if (encontrado)
            {
                //Hemos encontrado la posición, por tanto podemos continuar
                //Ahora deberemos comprobar si algún número de la ficha puede encajar en la posición
                //Para ello comprobaremos si el valor de la casilla, coincide con algun valor
                //de nuestra ficha

                //Comprobamos que el número que haya en la posición seleccionada concuerde con uno de los de la ficha
                if (ficha_jugada.NUM_1 == Convert.ToInt32(dataGridView1.Rows[punto_insercion.Y].Cells[punto_insercion.X].Value))
                {
                    //Ahora que la ficha es válida, comprobamos en que posiciones podemos colocar el primer número

                    Coordenada arriba = new Coordenada(punto_insercion.X, punto_insercion.Y - 1);
                    Coordenada abajo = new Coordenada(punto_insercion.X, punto_insercion.Y + 1);
                    Coordenada izquierda = new Coordenada(punto_insercion.X - 1, punto_insercion.Y);
                    Coordenada derecha = new Coordenada(punto_insercion.X + 1, punto_insercion.Y);

                    int ocupadas_arriba = Comprobar_Celdas_Colindantes_Si0(arriba);
                    int ocupadas_abajo = Comprobar_Celdas_Colindantes_Si0(abajo);
                    int ocupadas_izquierda = Comprobar_Celdas_Colindantes_Si0(izquierda);
                    int ocupadas_derecha = Comprobar_Celdas_Colindantes_Si0(derecha);

                    if ((ocupadas_arriba <= 1) && (Convert.ToInt32(dataGridView1.Rows[arriba.Y].Cells[arriba.X].Value) == 0))
                        dataGridView1.Rows[arriba.Y].Cells[arriba.X].Value = "*";
                    if ((ocupadas_abajo <= 1) && (Convert.ToInt32(dataGridView1.Rows[abajo.Y].Cells[abajo.X].Value) == 0))
                        dataGridView1.Rows[abajo.Y].Cells[abajo.X].Value = "*";
                    if ((ocupadas_izquierda <= 1) && (Convert.ToInt32(dataGridView1.Rows[izquierda.Y].Cells[izquierda.X].Value) == 0))
                        dataGridView1.Rows[izquierda.Y].Cells[izquierda.X].Value = "*";
                    if ((ocupadas_derecha <= 1) && (Convert.ToInt32(dataGridView1.Rows[derecha.Y].Cells[derecha.X].Value) == 0))
                        dataGridView1.Rows[derecha.Y].Cells[derecha.X].Value = "*";

                    pon_este = ficha_jugada.NUM_1;

                    pon_otro = ficha_jugada.NUM_2;

                    //Antes de nada notificamos a los demás jugadores que esa ya no será una posición de juego válida
                    peticion = "8/- " + Convert.ToString(posicion_seleccionada.X) + " " + Convert.ToString(posicion_seleccionada.Y);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(peticion);
                    server.Send(msg);

                    Eliminar_Posicion_Jugable(posicion_seleccionada);

                    textBox1.Text = null;

                    listBox2.Items.RemoveAt(actual_index);
                }
                else if (ficha_jugada.NUM_2 == Convert.ToInt32(dataGridView1.Rows[punto_insercion.Y].Cells[punto_insercion.X].Value))
                {
                    //Ahora que la ficha es válida, comprobamos en que posiciones podemos colocar el primer número

                    Coordenada arriba = new Coordenada(punto_insercion.X, punto_insercion.Y - 1);
                    Coordenada abajo = new Coordenada(punto_insercion.X, punto_insercion.Y + 1);
                    Coordenada izquierda = new Coordenada(punto_insercion.X - 1, punto_insercion.Y);
                    Coordenada derecha = new Coordenada(punto_insercion.X + 1, punto_insercion.Y);

                    int ocupadas_arriba = Comprobar_Celdas_Colindantes_Si0(arriba);
                    int ocupadas_abajo = Comprobar_Celdas_Colindantes_Si0(abajo);
                    int ocupadas_izquierda = Comprobar_Celdas_Colindantes_Si0(izquierda);
                    int ocupadas_derecha = Comprobar_Celdas_Colindantes_Si0(derecha);

                    if ((ocupadas_arriba <= 1) && (Convert.ToInt32(dataGridView1.Rows[arriba.Y].Cells[arriba.X].Value) == 0))
                        dataGridView1.Rows[arriba.Y].Cells[arriba.X].Value = "*";
                    if ((ocupadas_abajo <= 1) && (Convert.ToInt32(dataGridView1.Rows[abajo.Y].Cells[abajo.X].Value) == 0))
                        dataGridView1.Rows[abajo.Y].Cells[abajo.X].Value = "*";
                    if ((ocupadas_izquierda <= 1) && (Convert.ToInt32(dataGridView1.Rows[izquierda.Y].Cells[izquierda.X].Value) == 0))
                        dataGridView1.Rows[izquierda.Y].Cells[izquierda.X].Value = "*";
                    if ((ocupadas_derecha <= 1) && (Convert.ToInt32(dataGridView1.Rows[derecha.Y].Cells[derecha.X].Value) == 0))
                        dataGridView1.Rows[derecha.Y].Cells[derecha.X].Value = "*";

                    pon_este = ficha_jugada.NUM_2;

                    pon_otro = ficha_jugada.NUM_1;

                    //Antes de nada notificamos a los demás jugadores que esa ya no será una posición de juego válida
                    peticion = "8/- " + Convert.ToString(posicion_seleccionada.X) + " " + Convert.ToString(posicion_seleccionada.Y);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(peticion);
                    server.Send(msg);

                    Eliminar_Posicion_Jugable(posicion_seleccionada);

                    textBox1.Text = null;

                    listBox2.Items.RemoveAt(actual_index);
                }
                else
                    PonMensajeServidor("Vaya, la ficha no encaja aquí :(");
            }
            else
            {
                PonMensajeServidor("Vaya, elige otra casilla y vuelve a pulsar a jugar :(");
            }
        }
        private void Poner_Segundo_Numero(int numero, Coordenada punto_insercion)
        {
            //Comprobamos en que posiciones podemos colocar el primer número

            Coordenada arriba = new Coordenada(punto_insercion.X, punto_insercion.Y - 1);
            Coordenada abajo = new Coordenada(punto_insercion.X, punto_insercion.Y + 1);
            Coordenada izquierda = new Coordenada(punto_insercion.X - 1, punto_insercion.Y);
            Coordenada derecha = new Coordenada(punto_insercion.X + 1, punto_insercion.Y);

            int ocupadas_arriba = Comprobar_Celdas_Colindantes_Si0(arriba);
            int ocupadas_abajo = Comprobar_Celdas_Colindantes_Si0(abajo);
            int ocupadas_izquierda = Comprobar_Celdas_Colindantes_Si0(izquierda);
            int ocupadas_derecha = Comprobar_Celdas_Colindantes_Si0(derecha);

            if (ocupadas_arriba <= 1)
                dataGridView1.Rows[arriba.Y].Cells[arriba.X].Value = "*";
            if (ocupadas_abajo <= 1)
                dataGridView1.Rows[abajo.Y].Cells[abajo.X].Value = "*";
            if (ocupadas_izquierda <= 1)
                dataGridView1.Rows[izquierda.Y].Cells[izquierda.X].Value = "*";
            if (ocupadas_derecha <= 1)
                dataGridView1.Rows[derecha.Y].Cells[derecha.X].Value = "*";

            pon_este = numero;
        }


        //fin hector











        public void TomaRespuesta5(string mensaje)
        {
            //Recogemos los datos que nos da la respuesta
            seg = mensaje.Split(new char[] { '_' }, 4);
            id_partida = Convert.ToInt32(seg[0]);
            num_jugadores = Convert.ToInt32(seg[1]);
            jugadores = seg[3];
           
        }
        
        public void TomaRespuesta7(string mensaje)
        {
            // Tenemos un nuevo movimiento

            //Recibimos el punto y el numero como (coordX coordY numero)
            nuevos_trozos = mensaje.Split(' ');
            punto = new Coordenada(Convert.ToInt32(nuevos_trozos[0]), Convert.ToInt32(nuevos_trozos[1]));
            this.Invoke(new DelegadoParaActualizarTablero(ActualizaTablero), new object[] { punto, Convert.ToInt32(nuevos_trozos[2]) });

        }
        public void TomaRespuesta8(string mensaje)
        {
            // Actualizamos la lista de posiciones jugables

            //Recinimos el punto como (+ coordX coordY) si hay que añadirlo o (- coordX coordY) si hay que quitarlo de la lista
            nuevos_trozos = mensaje.Split(' ');
            punto = new Coordenada(Convert.ToInt32(nuevos_trozos[1]), Convert.ToInt32(nuevos_trozos[2]));
            if (nuevos_trozos[0] == "+")

                this.Invoke(new DelegadoParaActualizarPosicionesValidas(AgregaPosicionValida), new object[] { punto });

            else if (nuevos_trozos[0] == "-")

                this.Invoke(new DelegadoParaActualizarPosicionesValidas(QuitaPosicionValida), new object[] { punto });

            Pintar_Posiciones_Jugables();

        }
        public void TomaRespuesta9(string mensaje)
        {
            nuevos_trozos = mensaje.Split(' ');
            for (int i = 1; i <= Convert.ToInt32(nuevos_trozos[0]); i++)
            {
                this.Invoke(new DelegadoParaPonerTexto(PonFicha), new object[] { nuevos_trozos[i] });
            }

        }
        public void TomaRespuesta10(string mensaje)
        {
            this.Invoke(new DelegadoParaGestionarTurno(SiTurno), new object[] { });
            this.Invoke(new DelegadoParaPonerTexto(PonMensajeServidor), new object[] { "Es tu turno!" });

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //inici hector
            string peticion;
            byte[] msg;

            //MessageBox.Show("Se ha pulsado en la columna: " + Convert.ToString(e.ColumnIndex) + " y en la fila: " + Convert.ToString(e.RowIndex));
            posicion_seleccionada = new Coordenada(e.ColumnIndex, e.RowIndex);

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "*")
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = pon_este;
                Notificar_Numero_Nuevo(posicion_seleccionada, pon_este);
                Limpiar_Asteriscos();
                if (pon_otro != -1)
                {
                    Poner_Segundo_Numero(pon_otro, posicion_seleccionada);
                    pon_otro = -1;
                    //Le enviamos un mensaje al servidor conforme queremos forzar un cambio de turno
                    msg = System.Text.Encoding.ASCII.GetBytes("10/");
                    server.Send(msg);
                    button1.Enabled = false;
                    robar_btn.Enabled = false;
                    PonMensajeServidor("Dejó de ser tu turno!");
                }
                else
                {
                    //Antes de nada lo notificamos a los demás jugadores
                    peticion = "8/+ " + Convert.ToString(posicion_seleccionada.X) + " " + Convert.ToString(posicion_seleccionada.Y);
                    msg = System.Text.Encoding.ASCII.GetBytes(peticion);
                    server.Send(msg);
                    Añadir_Posicion_Jugable(posicion_seleccionada);
                    Pintar_Posiciones_Jugables();
                }

                //fin hector
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] trozos = textBox1.Text.Split('-');
            ficha1 = new Ficha(Convert.ToInt32(trozos[0]), Convert.ToInt32(trozos[1]), 99);
            Comprobar_Afinidad_Ficha(ficha1, posicion_seleccionada);
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            actual_index = listBox2.SelectedIndex;
            string ficha_string = Convert.ToString(listBox2.Items[listBox2.SelectedIndex]);
            textBox1.Text = ficha_string;
        }

        private void robar_btn_Click(object sender, EventArgs e)
        {
            //Le enviamos un mensaje al servidor conforme queremos una ficha
            byte[] msg = System.Text.Encoding.ASCII.GetBytes("9/");
            server.Send(msg);
        }

        private void aceptarBtn_Click(object sender, EventArgs e)
        {
            preguntaLbl.Visible = false;
            aceptarBtn.Visible = false;
            rechazarBtn.Visible = false;

            //Enviamos al servidor la respuesta de cada invitado
            string mensaje = "6/" + nForm + "/" + id_partida + "/" + num_jugadores + "/" + usuario + "/1/" + jugadores;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
        }

        private void rechazarBtn_Click(object sender, EventArgs e)
        {
            //Enviamos al servidor la respuesta de cada invitado
            string mensaje = "6/" + nForm + "/" + id_partida +"/" + num_jugadores + "/" + usuario + "/0/" + jugadores;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            Close();
        }
    }
}
