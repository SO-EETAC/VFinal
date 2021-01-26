#include <mysql.h>
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <pthread.h>
#include <stdbool.h>


typedef struct{
	int socket;
	char usuario[20];
		
}TConectado;

typedef struct{
	char usuario[20];
	int aceptado; //1=invitacion aceptada, 0 = invitacion rechazada
}TJugador;

typedef struct{
	int id_partida;
	int num_jugadores;
	TJugador  jugadores[4];
	int turno; // 1 = jugador_1; 2= jugador_2; 3 = jugador_3; ...
}TPartida;

typedef struct{
	TPartida partidas[10];
	int num;
}ListaPartidas;

typedef struct{
	TConectado conectados[100];
	int num;
}ListaConectados;

/***********************************INICI*****************************************************/
//Creamos las estructuras de datos necesarias para el servidor

typedef struct{
	
	int num;
	int sockets[100];
	
}TListaSockets;

TListaSockets lista_sockets;

typedef struct{
	
	char valores[20];
	
}TFicha;

typedef struct{
	
	int num;
	TFicha fichas[200];
	
}TListaFichas;
/*************************************FIN****************************************************/

ListaConectados listaConectados;
ListaPartidas listaPartidas;
TListaFichas lista_fichas; /****HECTOR*/

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int turno = 0;

int sockets[100];
int z;

int PonJugador(ListaPartidas *l, int id_p, char user[20], int a)
{
	//Añade los jugadores que se han invitado a una partida. Retorna 0  si ok. Retorna -1 si la partida ya esta llena
	//y no se puede añadir. Retorna -2 si no existe la partida con ese id.
	int devolver;

	int i = 0;
	while (i < l->num)
	{
		if (l->partidas[i].num_jugadores == 4)
		{
			devolver = -3;
		}
		else
		{
			//Buscamos con el id, la partida donde introduciremos al jugador
			if (l->partidas[i].id_partida == id_p )
			{
				//Introducimos el "ussername" y el valor de "aceptado" del jugador en la partida "i"
				strcpy(l->partidas[i].jugadores[l->partidas[i].num_jugadores].usuario, user);
				l->partidas[i].jugadores[l->partidas[i].num_jugadores].aceptado =  a;
				//Incrementamos el numero de jugadores
				l->partidas[i].num_jugadores = l->partidas[i].num_jugadores;
				devolver = 0;
				
			}
			else
			{
				devolver = -2;
			}
		}
		i++;
	}
	
	return devolver;
}

void CambiarEstado(ListaPartidas *l, int id_p, char user[20], int a)
{
	//Busca el jugador de la partida introducido por parametro, y cambia el estado de aceptado
	
	int j = 0;
	
	while (j < l->partidas[id_p].num_jugadores)
	{
		if ((strcmp(l->partidas[id_p].jugadores[j].usuario, user) == 0))
		{
			l->partidas[id_p].jugadores[j].aceptado =  a;
		}
		j++;
	}
	
}

int ComprobarEstado(ListaPartidas *l, int id_p)
{
	
	int j = 0;
	int devolver = 0;
	
	while (j < l->partidas[id_p].num_jugadores)
	{
		if (l->partidas[id_p].jugadores[j].aceptado ==  0)
		{
			devolver = -1;
		}
		
		j++;
	}
		
	return devolver;
}

int CreaPartida(ListaPartidas *l, int id)
{
	//Crea una partida en la lista de partidas con su id. Retorna 0  si ok. Retorna -1 si la lista ya esta llena
	//y no se puede añadir. 
	if (l->num == 10)
	{
		return -1;
	}
	else
	{
		//Guardamos el id de partida que nos llega como parametro
		l->partidas[l->num].id_partida = id;
		//Inicializamos el turno a 0
		l->partidas[l->num].turno = 0;
		//Incrementamos en 1, el numero de partidas que hay en la lista
		l->num = l->num + 1;
		return 0;
	}
	
}
/****************************inicio******************************************/
void PonConectado(TListaSockets *lista, int socket){
	
	lista->sockets[lista->num] = socket;
	lista->num++;
	
}

void QuitaConectado(TListaSockets *lista, int socket){
	
	int i=0;
	int encontrado = 0;
	
	while ((i < lista->num) && (!encontrado)){
		
		if (lista->sockets[i] == socket)
			
			encontrado = 1;
		
		else
			
			i = i + 1;
		
	}
	if (encontrado){
		while (i < lista->num - 1){
			
			lista->sockets[i] = lista->sockets[i + 1];
			i = i + 1;
			
		}
		lista->num = lista->num - 1;
	}
	
}
void PonFicha(TListaFichas *lista, char valores[20]){
	
	strcpy(lista->fichas[lista->num].valores,valores);
	lista->num++;
	
}

void QuitaFicha(TListaFichas *lista, int i){
	
	while (i < lista->num - 1){
		
		lista->fichas[i] = lista->fichas[i + 1];
		i = i + 1;
		
	}
	lista->num = lista->num - 1;
}

int GetRandom(int lower, int upper) 
{ 
	int num = (rand() % (upper - lower + 1)) + lower; 
	return num; 
} 

/**********************fin*******************************************/
int Pon(ListaConectados *lista, char nombre[20], int socket)
{
	//Añade nuevo conectados. Retorna 0  si ok y -1 si la lista ya esta llena
	//y no se puede añadir
	
	if (lista->num == 100)
	{
		return -1;
	}
	else
	{
		
		strcpy(lista->conectados[lista->num].usuario, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num = lista->num + 1;
		return 0;
		
	}
}
int Eliminar(ListaConectados *lista, char nombre[20])
{
	//Retorna 0 si elimina y -1 si dicho usuario no está en la lista
	
	int i = 0;
	int encontrado = 0;
	
	while ((i < lista->num) && (!encontrado))
	{
		
		if (strcmp(lista->conectados[i].usuario, nombre) == 0)
			encontrado = 1;
		if (!encontrado)
			i = i + 1;
	}
	if (encontrado == 1)
	{
		
		int j;
		for(j = i; j < lista->num-1; j++){
			
			strcpy(lista->conectados[j].usuario, lista->conectados[j+1].usuario);
			lista->conectados[j].socket = lista->conectados[j+1].socket;
		}
		lista->num--;
		return 0;
	}
	else
		return -1;
	
}	

void DameConectados (ListaConectados *lista, char conectados[400])
{
	//Recibe la lista de conectados y retorna un vector de caracteres con los nombres
	//separados por _ --> "3_Juan_Maria_Pedro"
	
	sprintf(conectados, "%d", lista->num);
	
	int i;
	
	for (i = 0; i < lista->num; i++){
		
		sprintf(conectados, "%s_%s", conectados, lista->conectados[i].usuario);
		
	}		
}

int DameSocket(ListaConectados *lista, char nom[20])
{
	//devuelve el numero de socket. Si no encuentra el nombre, devuelve -1.
	int i = 0;
	int encontrado = 0;
	while ((i<lista->num) && encontrado!=1)
	{
		if (strcmp(lista->conectados[i].usuario, nom ) == 0)
		{
			encontrado=1;
		}
		else{
			i++;
		}
		
	}
	if (encontrado==1)
	{
		return lista->conectados[i].socket;
	}
	else
	{
		return -1;
	}
	
}

void *AtenderCliente (void *indice)
{
	
	MYSQL *conn;
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[80];
	char values[80];
	
	//crear conexion a BBDD
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar conexion a BBDD
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "JUEGO",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	int sock_conn;
	int codigo;
	char peticion[512];
	char respuesta[512];
	int ret;
	bool notificacion_n = false;
	
	int devolver;
	char usuario[20];
	char contra[20];
	char nombre[20];
	
	char fecha[20];
	int duracion;
	
	char conectados[200];
	
	int nForm;
	int id_partida = 0;
	int num_jugadores;
	char invitador[20];
	char jugador[20];
	char jugadores[100];
	int aceptado;
	int contestados = 1;

	

	/************************************/
	char notificacion[512];
	char contenido[200];
	
	int jugadas = 0;
	int quien_soy;
	int encontrado_sock = 0;
	int j = 0;
	
	int random;
	char ficha_repartir[20];
	char mensaje_de_ficha[100];
	/************************************/
	
	
	
	
	//Recogemos el indice y obtengo el sockets
	sock_conn = *(int*) indice;
	
	//Bucle de atención al cliente
	int fin = 0;
	while (fin == 0)
	{
		//Esperamos peticion de servicio
		ret = read(sock_conn, peticion, sizeof(peticion));
		peticion[ret] = '\0';
		printf("Peticion recibida: %s \n", peticion);
		
		//Averiguamos que tipo de peticion es
		char *p = strtok(peticion, "/");
		codigo = atoi(p);
		
		if (codigo == 0)
		{
			//DESCONECTARSE
			
			pthread_mutex_lock(&mutex); //No me interrumpas ahora
			
			int err_2 = Eliminar(&listaConectados, usuario);
			
			pthread_mutex_unlock(&mutex); //Ya me puedes interrumpir
			
			if (err_2 == -1)
			{
				printf("No se encuentra en la lista\n");
			}				
			else
			{
				printf("Elminado\n");
			}
			
			notificacion_n = true;
			
			fin = 1;
		}
		
		if (codigo == 1) 
		{
			//INICIAR SESION
			//Peticion: 1/usuario/contraseña
			p = strtok( NULL, "/");
			strcpy (usuario, p);
			p = strtok( NULL, "/");
			strcpy (contra, p);
			printf ("Usuario: %s, Contraseña: %s\n", usuario, contra);
			
			
			//Construimos la consulta SQL
			strcpy (consulta,"SELECT PASSWORD,NOMBRE FROM JUGADOR WHERE USERNAME = '"); 
			strcat (consulta, usuario);
			strcat (consulta,"';");
			
			
			//CONSULTA: Comprobar si usuario y contraseña coinciden 
			err=mysql_query (conn, consulta); 
			if (err!=0) 
			{
				//ERROR: No se ha podido hacer la consulta
				devolver = -1;
				sprintf(respuesta, "1/%d", devolver);
				printf ("Respuesta: ERROR(%s): al consultar datos de la base %u %s\n",respuesta, mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			//Recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				//ERROR: El usuario no existe
				devolver = -2;
				sprintf(respuesta, "1/%d", devolver);
				printf ("Respuesta: ERROR(%s): El usuario no existe\n", respuesta);
			}
			else{
				
				if (strcmp(row[0], contra) == 0) {
					
					//CORRECTO: La contraseña coincide con el usuario
					
					strcpy( nombre, row[1]);
					sprintf(respuesta, "1/%s",nombre);
					printf ("Respuesta: %s; Autentificacion exitosa!  \n", respuesta);
					
					
					pthread_mutex_lock(&mutex); //No me interrumpas ahora
					
					//AÑADIMOS Jugador a lista de conectados
					int err_1 = Pon(&listaConectados, usuario, sock_conn);
					
					pthread_mutex_unlock(&mutex); //Ya me puedes interrumpir
					
					if (err_1!=0)
					{
						//ERROR: Lista de jugadores conectados llena
						devolver = -3;
						sprintf(respuesta, "1/%d", devolver);
						printf( "Respuesta: ERROR(%s): Lista de jugadores conectados llena \n", respuesta);
						
					}
					else
					{
						//CORRECTO: Añadido a la lista correctamente
						devolver = 0;
						printf( "CORRECTO(Devolver:%d): Añadido a la lista correctamente \n", devolver);
						//Se envía la notificación para actualizar la lista de conectados
						notificacion_n = true;
					}
					
				}
				else 
				{
					//ERROR: La contraseña es incorrecta
					devolver = -4;
					sprintf(respuesta, "1/%d", devolver);
					printf ("Respuesta: ERROR(%s): Contraseña incorrecta \n", respuesta);
				}
				
			}
			//Enviamos la respuesta al cliente
			write (sock_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 2)
		{
			//REGISTRARSE
			//peticion: 1/nombre/usuario/contraseña
			
			
			p = strtok( NULL, "/");
			strcpy( nombre, p);
			
			
			p = strtok( NULL, "/");
			strcpy( usuario, p);
			
			p = strtok( NULL, "/");
			strcpy( contra, p);
			
			
			printf("Nombre: %s, Usuario: %s, Contraseña: %s \n", nombre, usuario, contra);
			
			resultado = NULL;
			row = NULL;
			
			//Construimos la consulta
			sprintf(consulta,"SELECT count(*) FROM JUGADOR WHERE JUGADOR.USERNAME ='%s';",usuario );
			
			//CONSULTA: Comprobamos si el usuario introducido ya esta registrado
			err=mysql_query (conn, consulta);
			
			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (err!=0) {
				
				//ERROR: No se ha podido hacer la consulta
				devolver = -1;
				sprintf(respuesta, "2/%d", devolver);
				printf ("Respuesta: ERROR(%s): al consultar datos de la base %u %s\n", respuesta, mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			if (atoi(row[0]) >0)
			{
				//ERROR: El usuario ya existe
				devolver = -2;
				sprintf(respuesta, "2/%d", devolver);
				printf( "Respuesta: ERROR(%s): el usuario ya existe \n", respuesta);
			}
			else
			{
				//CORRECTO: Usuario disponible
				
				//Construimos la consulta
				strcpy (values, "INSERT INTO JUGADOR (NOMBRE, USERNAME, PASSWORD) VALUES ('");
				strcat (values, nombre); 
				strcat (values, "','");
				strcat (values, usuario); 
				strcat (values, "','");
				strcat (values, contra); 
				strcat (values, "');");
				
				//CONSULTA: Insertamos datos del nuevo usuario
				err=mysql_query (conn, values);
				
				if (err!=0) {
					
					//ERROR: No se ha podido hacer la consulta
					devolver = -1;
					sprintf(respuesta, "2/%d", devolver);
					printf ("Respuesta: ERROR(%s): al consultar datos de la base %u %s\n", respuesta, mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				pthread_mutex_lock(&mutex); //No me interrumpas ahora
				
				//AÑADIMOS Jugador a lista de conectados
				int err_1 = Pon(&listaConectados, usuario, sock_conn);
				
				pthread_mutex_unlock(&mutex); //Ya me puedes interrumpir
				
				if (err_1!=0)
				{
					//ERROR: Lista de jugadores conectados llena
					devolver = -3;
					sprintf(respuesta, "2/%d", devolver);
					printf( "Respuesta: ERROR(%s): Lista de jugadores conectados llena \n", respuesta);
					
				}
				else
				{
					//CORRECTO: Añadido a la base de datos y a lista conectados
					devolver = 0;
					sprintf(respuesta, "2/%s", nombre);
					printf( "Respuesta: CORRECTO(%s): Usuario registrado \n", respuesta);
					//Se envía la notificación para actualizar la lista de conectados
					notificacion_n = true;
				}
				
			}
			//Enviamos la respuesta al cliente
			write (sock_conn,respuesta, strlen(respuesta));
			
		}
		
		
		else if (codigo == 3) 
		{
			//CONSULTAR GANADOR POR DURACION
			//Peticion: 4/duracion
			p = strtok( NULL, "/");
			duracion = atoi(p);
			
			printf ("Duracion: %d \n", duracion);
			
			//Construimos la consulta SQL
			sprintf (consulta,"SELECT GANADOR FROM PARTIDA WHERE DURACION = '%d';", duracion); 
		
			printf("%s\n",consulta);
		
			//CONSULTA: Buscar nombre y usuario de ganador por duracion
			err=mysql_query (conn, consulta); 
			
			if (err!=0) 
			{
				//ERROR: No se ha podido hacer la consulta
				devolver = -1;
				sprintf(respuesta, "3/%d", devolver);
				printf ("Respuesta: ERROR(%s): al consultar datos de la base %u %s\n",respuesta, mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			//Recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				//ERROR: No hay ganador con esta duracion
				devolver = -2;
				sprintf(respuesta, "3/%d", devolver);
				printf ("Respuesta: ERROR(%s): No hay ganador con esta duracion\n", respuesta);
			}
			else
			{
				
				//CORRECTO: Si hay ganador para esta duracion
				//Respuesta: 4/nombre_ganador/usuario_ganador
				
				strcpy(nombre, row[0]);
				
				//Construimos la consulta SQL
				sprintf (consulta,"SELECT USERNAME FROM JUGADOR WHERE NOMBRE = '%s';", nombre); 
				printf("%s\n",consulta);
				
				//CONSULTA: Buscar usuario de ganador por el nombre
				err = mysql_query (conn, consulta); 
				
				if (err!=0) 
				{
					//ERROR: No se ha podido hacer la consulta
					devolver = -1;
					sprintf(respuesta, "3/%d", devolver);
					printf ("Respuesta: ERROR(%s): al consultar datos de la base %u %s\n",respuesta, mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				//Recogemos el resultado de la consulta 
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
	
				strcpy(usuario, row[0]);
				
				//Construimos la respuesta
				sprintf(respuesta, "3/%s-%s", nombre, usuario);
				printf ("Respuesta: %s\n",respuesta);
			}
			
			//Enviamos la respuesta al cliente
			write(sock_conn,respuesta,strlen(respuesta));
			
		}
		
		else if (codigo == 4) 
		{
			//CONSULTA GANADOR POR FECHA
			//Peticion: 3/fecha
			p = strtok( NULL, "/");
			strcpy (fecha, p);
			
			printf ("Fecha: %s\n", fecha);
			
			
			//Construimos la consulta SQL
			sprintf (consulta,"SELECT JUGADOR.USERNAME, PARTIDA.GANADOR FROM JUGADOR INNER JOIN PARTIDA ON JUGADOR.NOMBRE = PARTIDA.GANADOR WHERE DATE(PARTIDA.FECHA_HORA) = DATE('%s') ;",fecha); 
			
			//CONSULTA: Buscar nombre y usuario de ganador por fecha 
			err=mysql_query (conn, consulta); 
			
			if (err!=0) 
			{
				//ERROR: No se ha podido hacer la consulta
				devolver = -1;
				sprintf(respuesta, "4/%d", devolver);
				printf ("Respuesta: ERROR(%s): al consultar datos de la base %u %s\n",respuesta, mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			//Recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				//ERROR: No hay ganador en esa fecha
				devolver = -2;
				sprintf(respuesta, "4/%d", devolver);
				printf ("Respuesta: ERROR(%s): No hay ganador ese día\n", respuesta);
				
			}
			else
			{
				//CORRECTO: Si hay ganador para esta fecha
				//Respuesta: 3/nombre_ganador/usuario_ganador
				strcpy(usuario, row[0]);
				strcpy(nombre, row[1]);
				sprintf(respuesta, "4/%s-%s", nombre, usuario);
				printf ("Respuesta: %s\n",respuesta);
			}
			
			//Enviamos la respuesta al cliente
			write(sock_conn,respuesta,strlen(respuesta));
			
		}
		
		else if (codigo == 5)
		{
			//INVITACION ENVIADA
			
			//Peticion: 5/numForm/numJugadores/jug1-jug2-jug3...
			//Se crea una partida
			//Se añade a los jugadores a la partida
			
			//Recogemos el numero de formulario, que sera el id de partida
			p = strtok(NULL, "/");
			nForm = atoi(p); 
			printf("NUM FORM: %d \n", nForm);
			
			//Creamos la partida 
			
			int err = CreaPartida(&listaPartidas, id_partida);
			
			if (err == -1)
			{
				sprintf(respuesta, "5/%d", err);
				printf("Respuesta: ERROR(%s): Lista de partidas llena \n", respuesta);
			}
			
			//Recogemos el numero de jugadores que tendra la partida
			p = strtok(NULL, "/");
			num_jugadores = atoi(p); 
			printf("NUM JUGADORES: %d \n", num_jugadores);
			
			//Recogemos la lista de jugadores de la partida
			p = strtok(NULL, "/");
			char invitados[100];
			strcpy(invitados, p);
			printf("INVITADOS: %s \n", invitados);
			
			//Creamos el mensaje de invitación para el resto de jugadores que han sido invitados y no son el invitador
			sprintf(respuesta, "5/%d/%d_%d_%s", nForm, id_partida, num_jugadores, invitados);
			
			//Recogemos al invitador 
			char *q = strtok(invitados, "-");
			strcpy(invitador, q);
			printf("INVITADOR: %s \n", invitador);
			
			//Metemos al invitador a la partida (con aceptado = 1)
			err = PonJugador(&listaPartidas,id_partida,invitador,1);
			if (err == -1)
			{
				sprintf(respuesta, "5/%d", err);
				printf("Respuesta: ERROR(%s): No puede haber más jugadores en esta partida \n", respuesta);
			}
			else if (err == -2)
			{
				sprintf(respuesta, "5/%d", err);
				printf("Respuesta: ERROR(%s): No se encuentra una partida con este id\n", respuesta);
			}
			
			//Recogemos a cada uno de los invitados y los metemos en la partida (con aceptado = 0)
			q = strtok(NULL, "-");
			while (q != NULL)
			{
				strcpy (jugador, q);
				//Envio a cada jugador invitado, un mensaje para que se abra el Form2
				//Añado al jugador a la partida
				err = PonJugador(&listaPartidas,id_partida,jugador,0);
				
				if (err == -1)
				{
					sprintf(respuesta, "5/%d", err);
					printf("Respuesta: ERROR(%s): No puede haber más jugadores en esta partida \n", respuesta);
				}
				else if (err == -2)
				{
					sprintf(respuesta, "5/%d", err);
					printf("Respuesta: ERROR(%s): No se encuentra una partida con este id\n", respuesta);
				}
				//Enviamos la invitación
				int s = DameSocket(&listaConectados, jugador);
				
				if (s == -1)
				{
					sprintf(respuesta, "5/%d", -3);
					printf("Respuesta: ERROR(5/-3): No es posible dar el socket, porque no se encuentra el jugador \n");
				}
				else
				{
					//CORRECTO: Se ha encontrado un socket para ese jugador
					printf("Invitación enviada \n");
				}
				//Enviamos la respuesta
				printf("RESPUESTA: %s \n", respuesta);
				write(s, respuesta, strlen(respuesta));
				//Siguiente jugador
				q = strtok(NULL, "-");
				
			}
			id_partida++;
			
		}
		else if (codigo == 6)
		{
			//RESPUESTAS INVITACIONES
			//Peticion: : 6/numForm/id_partida/num_jugadores/usuario/SIoNO/jugadores
			
			contestados++;
			
			p = strtok(NULL, "/");
			nForm = atoi(p);
			
			
			
			p = strtok(NULL, "/");
			id_partida = atoi(p);
			
			
			
			p = strtok(NULL, "/");
			num_jugadores = atoi(p);
			
			
			
			p = strtok(NULL, "/");
			strcpy(usuario, p);
			
			
			p = strtok(NULL, "/");
			aceptado = atoi(p);
			
			
			
			p = strtok(NULL, "/");
			strcpy(jugadores, p);
			
			
			printf("Nform: %d, ID:%d, NJug: %d, Usuario: %s, Respuesta: %d \n", nForm, id_partida, num_jugadores, usuario, aceptado);
			
			//Cambiamos el estado aceptado del jugador en la partida
			CambiarEstado(&listaPartidas, id_partida,usuario,aceptado);
			
			
			//Comprobamos si falta alguien por contestar. Si todos han contestado, se envía la respuesta
			if (contestados == num_jugadores)
			{
				printf("Todos los jugadores han contestado \n ");
				devolver = ComprobarEstado(&listaPartidas,id_partida);
				
				sprintf(respuesta, "6/%d/%d", nForm, devolver);
				printf("Respuesta(%s): INICIAR PARTIDA (0) o NO JUGAR(-1) \n", respuesta);
				char *q = strtok(jugadores, "-");
				
				
				while( q != NULL)
				{
					strcpy(jugador,q);
					//Enviar respuesta a todos los jugadores
					int s = DameSocket(&listaConectados, jugador);
					
					
					if (s == -1)
					{
						sprintf(respuesta, "6/%d", -3);
						printf("Respuesta: ERROR(5/-3): No es posible dar el socket, porque no se encuentra el jugador \n");
					}
					else
					{
						//CORRECTO: Se ha encontrado un socket para ese jugador
						printf(" Socket encontrado para mandar respuesta. \n");
					}
					write(s, respuesta, strlen(respuesta));
					//Siguiente jugador
					q = strtok(NULL, "-");
					
				}
				
			}
		}
		/***************************************INICIO************************************************/
		else if (codigo == 7){//Recibimos un numero nuevo en el tablero
			
			p = strtok(NULL, "/");
			strcpy(contenido, p);
			
			//Ahora debemos enviar el mensaje con el nuevo piso a todo los activos con notificaciÃ³n
			sprintf(notificacion, "7/%s", contenido);
			
			for(int i=0; i<lista_sockets.num; i++){
				
				if (lista_sockets.sockets[i] != sock_conn)
					//Introducir modificaciÃ³n luego para la envie a todos los clientes menos al que ha aÃ±adido el piso
					write(lista_sockets.sockets[i], notificacion, strlen(notificacion));
				
			}
		}
		else if (codigo == 8){//Hay que quitar o poner un numero en la lista de posiciones válidas
			
			p = strtok(NULL, "/");
			strcpy(contenido, p);
			
			//Ahora debemos enviar el mensaje con el nuevo piso a todo los activos con notificaciÃ³n
			sprintf(notificacion, "8/%s", contenido);
			
			for(int i=0; i<lista_sockets.num; i++){
				
				if (lista_sockets.sockets[i] != sock_conn)
					//Introducir modificaciÃ³n luego para la envie a todos los clientes menos al que ha aÃ±adido el piso
					write(lista_sockets.sockets[i], notificacion, strlen(notificacion));
				
			}		
		}
		else if (codigo == 9){//El cliente quiere robar una ficha
			
			strcpy(respuesta,"9/1 ");
			//Le repartimos 4 fichas para empezar
			
			if(lista_fichas.num != 0){
				int random;
				char ficha_robar [20];
				random = GetRandom(1,lista_fichas.num);
				printf("La posición de la ficha robada es: %d\n", random);
				strcpy(ficha_robar,lista_fichas.fichas[random].valores);
				sprintf(respuesta,"%s%s ",respuesta, ficha_robar);
				pthread_mutex_lock(&mutex);
				QuitaFicha(&lista_fichas,random);
				pthread_mutex_unlock(&mutex);
			}
			else
			   printf("Vaya, no quedan más fichas :(\n");
			
			write(sock_conn, respuesta, strlen(respuesta));
			
		}
		else if (codigo == 10){//Nos piden forzar turno nuevo
			
			turno++;
			if (turno == lista_sockets.num)
				turno = 0;
			printf("Ahora es el turno de %d\n",lista_sockets.sockets[turno]);
			/*Y ahora notificamos al nuevo jugador de que es su turno*/
			strcpy(notificacion,"10/tuTurno");
			printf("%s \n", notificacion);
			write(lista_sockets.sockets[turno], notificacion, strlen(notificacion));
			printf("turno: %d \n",turno);
			printf("0000000000000000 \n");
		}
		else if (codigo == 11)
		{
			while (encontrado_sock == 0){
				
				if (lista_sockets.sockets[j] == sock_conn){
					quien_soy = j;
					encontrado_sock = 1;
				}
				else
					j++;
				
			}
			if (turno == quien_soy){
				strcpy(notificacion,"10/0");
				write(sock_conn, notificacion, strlen(notificacion));
			}
			
			strcpy(mensaje_de_ficha,"9/4 ");
			//Le repartimos 4 fichas para empezar
			for (int i= 0; i< 4; i++){
				if(lista_fichas.num != 0){
					random = GetRandom(1,lista_fichas.num);
					
					printf("La posición de la ficha robada es: %d\n", random);
					strcpy(ficha_repartir,lista_fichas.fichas[random].valores);
					sprintf(mensaje_de_ficha,"%s%s ",mensaje_de_ficha, ficha_repartir);
					pthread_mutex_lock(&mutex);
					QuitaFicha(&lista_fichas,random);
					
					pthread_mutex_unlock(&mutex);
				}
				else
				   printf("Vaya, no quedan más fichas :(\n");
			}
			
			write(sock_conn, mensaje_de_ficha, strlen(mensaje_de_ficha));
		}
		/****************************************FIN*******************************************/
		
		if (notificacion_n)
		{
			//Enviamos la lista de jugadores_conectados a todos los sockets
			
			char notificacion[400];
			
			DameConectados(&listaConectados,conectados);
			
			sprintf(notificacion, "20/%s", conectados);
			
			for(int j=0; j < z; j++){
				
				write(sockets[j], notificacion, strlen(notificacion));
				
				
			}
			notificacion_n = false;
			
		}
	}
	// Se acabo el servicio para este cliente
	//Primero lo quito la lista de conectados
	pthread_mutex_lock(&mutex);
	QuitaConectado(&lista_sockets, sock_conn);
	pthread_mutex_unlock(&mutex);
	
	close(sock_conn);
}


	
	int main(int argc, char *argv[])
	{
		
		listaConectados.num = 0;
		
		int sock_conn, sock_listen, ret;
		
		struct sockaddr_in serv_adr;
		
		
		// INICIALITZACIONS SOCKET
		// Obrim el socket
		if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
			printf("Error creant socket");
		
		
		// Fem el bind al port
		memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
		serv_adr.sin_family = AF_INET;
		
		// asocia el socket a cualquiera de las IP de la maquina. 
		//htonl formatea el numero que recibe al formato necesario
		serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
		// escucharemos en el port 9050
		serv_adr.sin_port = htons(9030); //50001
		if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
			printf ("Error al bind\n");
		//La cola de peticiones pendientes no podra ser superior a 4
		if (listen(sock_listen, 2) < 0)
			printf("Error en el Listen");
		
		/*****************INICIO******************/
		//Inicializamos las listas de sockets
		lista_sockets.num = 0;
		
		lista_fichas.num = 1;
		
		PonFicha(&lista_fichas,"2-1");
		
		PonFicha(&lista_fichas,"2-2");
		
		PonFicha(&lista_fichas,"3-1");
		PonFicha(&lista_fichas,"3-2");
		PonFicha(&lista_fichas,"3-3");
		
		PonFicha(&lista_fichas,"4-1");
		PonFicha(&lista_fichas,"4-2");
		PonFicha(&lista_fichas,"4-3");
		PonFicha(&lista_fichas,"4-4");
		
		PonFicha(&lista_fichas,"5-1");
		PonFicha(&lista_fichas,"5-2");
		PonFicha(&lista_fichas,"5-3");
		PonFicha(&lista_fichas,"5-4");
		PonFicha(&lista_fichas,"5-5");
		
		PonFicha(&lista_fichas,"6-1");
		PonFicha(&lista_fichas,"6-2");
		PonFicha(&lista_fichas,"6-3");
		PonFicha(&lista_fichas,"6-4");
		PonFicha(&lista_fichas,"6-5");
		PonFicha(&lista_fichas,"6-6");
		/****************FIN************/
		
		pthread_t thread[100];
		
		/****INICIO***/
		// Use current time as  
		// seed for random generator 
		srand(time(0));
		
		
		/****FIN***/
		
		//atendemos infinitas peticiones
		for(z=0;;z++)
		{
			printf ("Escuchando\n");
			
			//1)ESPERANDO CONEXION
			sock_conn = accept(sock_listen, NULL, NULL);
			printf ("He recibido conexion y tu socket es: %d \n", sock_conn);
			//sock_conn es el socket que usaremos para este cliente
			sockets[z] = sock_conn; //guardo el socket para comunicarme con el nuevo cliente
			//sock_conn es el socket que usaremos para este cliente
			
			
			
			/***************************INICIO******************************/
			//AÃ±adimos el socket a la lista de conectados
			pthread_mutex_lock(&mutex);
			PonConectado(&lista_sockets, sock_conn);
			pthread_mutex_unlock(&mutex);
			
			
			
			
			
			/********************************FIN**********************************/
			
			//Crear un thread y decirle lo que tiene que hacer
			pthread_create(&thread[z],NULL, AtenderCliente, &sockets[z]);
			//thread[i] es un parametro de salida, la funcion mete dentro un id de thread
			//socket[i] es un parametro de entrada, la funcion necesita saber que socket se esta usando
			
		}
		return 0;
	}
	
	
