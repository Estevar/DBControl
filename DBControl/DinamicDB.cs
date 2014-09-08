using System;
using System.Collections.Generic;
using MySql.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace DBControl
{
    public class DinamicDB
    {
        private MySqlConnection conexion = new MySqlConnection();
        private MySqlCommandBuilder builder;
        private MySqlDataAdapter da;
        public DataSet ds;
        private String path = "DataBase=%database%;Server=%server%;Uid=%user%;Pwd=%pass%;CharSet=utf8", path2 = "", server = "", user = "", pass = "", database = "", conf_file = "conection.cnf", query;
        private String[] funciones = new String[] { "'CURDATE()'", "'NOW()'" };
        MySqlCommand cmd = new MySqlCommand();

        /// <summary>
        /// Costructor
        /// Si no recibe valores en los parametros obtiene los datos del archivo conection.cnf
        /// Si el archivo no existe lo crea y escribe en el los datos dados.
        /// Si recibe valores los utiliza para la conexión sin afectar el archivo de configuración.
        /// </summary>
        /// <param name="server">DNS o IP del servidor de base de datos</param>
        /// <param name="user">Usuario que accede a la base de datos</param>
        /// <param name="pass">Password del usuario</param>
        /// <param name="database">Base de datos a usar</param>
        public DinamicDB(string server = null, string user = null, string pass = null, string database = null)
        {
            if (server == null || user == null || pass == null || database == null)
            {
                if (File.Exists(this.conf_file))
                {
                    StreamReader objReader = new StreamReader(this.conf_file);
                    this.server = objReader.ReadLine();
                    this.user = objReader.ReadLine();
                    this.pass = objReader.ReadLine();
                    this.database = objReader.ReadLine();
                    objReader.Close();
                    objReader.Dispose();
                }
                else
                {
                    this.createCnf();
                }
            }
            else
            {
                this.user = user;
                this.server = server;
                this.pass = pass;
                this.database = database;
            }
            this.path2 = this.path.Replace("%server%", this.server);
            this.path2 = this.path2.Replace("%user%", this.user);
            this.path2 = this.path2.Replace("%pass%", this.pass);
            this.path2 = this.path2.Replace("%database%", this.database);
            this.ds = new DataSet();
        }

        /// <summary>
        /// Crea el archivo de configuración para la conexión a la base de datos.
        /// </summary>
        private void createCnf()
        {
            DatosConexion dc = new DatosConexion();
            dc.FormClosing += new FormClosingEventHandler((sender, e) =>
            {
                var obj = (Form)sender;
                this.server = obj.Controls["server"].Text;
                this.user = obj.Controls["user"].Text;
                this.pass = obj.Controls["pass"].Text;
                this.database = obj.Controls["database"].Text;
            });
            if (dc.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(this.conf_file, false, Encoding.UTF8);
                sw.WriteLine(this.server);
                sw.WriteLine(this.user);
                sw.WriteLine(this.pass);
                sw.WriteLine(this.database);
                sw.Close();
                sw.Dispose();
            }
            else
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Establece una conexión con la base de datos.
        /// </summary>
        /// <returns>True: si fue exitosa la conexion; False: de lo contrario</returns>
        private bool conecta()
        {
            try
            {
                this.conexion.ConnectionString = path2;
                this.conexion.Open();
                return true;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                this.createCnf();
                return this.conecta();
            }
        }

        /// <summary>
        /// Cierra la conexión a la base de datos.
        /// </summary>
        private void cerrar()
        {
            this.conexion.Close();
            this.conexion.Dispose();
        }

        /// <summary>
        /// Realiza una consulta select a la base de datos rellenando un DataTable con el resultado dentro del atributo ds.
        /// </summary>
        /// <param name="query">Consulta select a realizar</param>
        /// <param name="tabla">Nombre de la tabla en donde se guardará el resultado</param>
        public void consultas(string query, string tabla)
        {
            this.conecta();
            try
            {
                this.da = new MySqlDataAdapter(query, this.conexion);
                this.da.Fill(this.ds, tabla);
                this.da.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message+"\n\r"+e.StackTrace, "Error en la consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.cerrar();
        }

        /// <summary>
        /// Realiza las consultas UPDATE, INSERT y DELETE del resultado de la consulta SELECT dada en base a la tabla existente en ds con nombre "tabla"
        /// </summary>
        /// <param name="query">Consulta select para obtener los datos a actualizar</param>
        /// <param name="tabla">Tabla con los datos a actualizar existente en ds</param>
        public void actualizaTabla(string query, string tabla)
        {
            this.conecta();
            this.da = new MySqlDataAdapter(query, this.conexion);
            this.builder = new MySqlCommandBuilder(this.da);
            this.da.Update(this.ds.Tables[tabla]);
            this.cerrar();
        }

        /// <summary>
        /// Ejecuta un comando sql que no regresará resultados como UPDATE, INSERT, DELETE, CREATE, ETC.
        /// </summary>
        /// <param name="query">Comando a realizar</param>
        /// <returns>True si no hubo error de lo contrario False.</returns>
        public bool consulta(string query)
        {
            this.conecta();
            try
            {
                this.cmd.Connection = this.conexion;
                this.cmd.CommandText = query;
                if (this.cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n\r" + e.StackTrace, "Error en la consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.cerrar();
            return false;
        }

        /// <summary>
        /// Realiza una insercion en la tabla dada
        /// </summary>
        /// <param name="tabla">Nombre de la tabla en donde se incertarán los datos</param>
        /// <param name="campos">Array de los nombres de campos de la tabla</param>
        /// <param name="valores">Array de los valores a insertar</param>
        /// <returns>True si no hubo error de lo contrario False.</returns>
        public bool agrega(String tabla, String[] campos, String[] valores)
        {
            String campo = "`" + String.Join("`,`", campos) + "`";
            String valor = "'" + String.Join("','", valores) + "'";
            for (int i = 0; i < funciones.Length; i++)
            {
                valor = valor.Replace(funciones[i], funciones[i].Replace("'", ""));
            }
            this.query = "insert into " + tabla + "(" + campo + ") values(" + valor + ")";
            return this.consulta(this.query);
        }

        /// <summary>
        /// Realiza una insercion en la tabla dada de un solo campo
        /// </summary>
        /// <param name="tabla">Nombre de la tabla en donde se incertarán los datos</param>
        /// <param name="campo">Nombre del campo que obtendra el valor</param>
        /// <param name="valor">Valor a agregar</param>
        /// <returns>True si no hubo error de lo contrario False.</returns>
        public bool agrega(String tabla, String campo, String valor)
        {
            this.query = "insert into " + tabla + "(`" + campo + "`) values('" + valor + "')";
            for (int i = 0; i < funciones.Length; i++)
                this.query = this.query.Replace(funciones[i], funciones[i].Replace("'", ""));
            return this.consulta(this.query);
        }

        /// <summary>
        /// Realiza una query UPDATE en la base de datos
        /// </summary>
        /// <param name="tabla">Nombre de la tabla que se actualizará</param>
        /// <param name="campos">Nombre de los campos que se actualizarán</param>
        /// <param name="valores">Valores que reemplazarán a los actuales</param>
        /// <param name="condicion">Condición que deben cumplir los registros para ser actualizados</param>
        /// <returns>True si no hubo error de lo contrario False.</returns>
        public bool actualiza(String tabla, String[] campos, String[] valores, String condicion)
        {
            if (campos.Length != valores.Length)
            {
                MessageBox.Show("La cantidad de campos no coinside con los valores");
                return false;
            }
            else
            {
                String[] compuesto = new String[campos.Length];

                for (int i = 0; i < campos.Length; i++)
                {
                    campos[i] = campos[i];
                    valores[i] = "'" + valores[i] + "'";
                    compuesto[i] = campos[i] + "=" + valores[i];
                }

                String valor = String.Join(",", compuesto);
                this.query = "update " + tabla + " set " + valor + " where " + condicion;
                return this.consulta(this.query);
            }
        }

        /// <summary>
        /// Realiza una query UPDATE en la base de datos
        /// </summary>
        /// <param name="tabla">Nombre de la tabla que se actualizará</param>
        /// <param name="campos">Nombre del campo que se actualizará</param>
        /// <param name="valores">Valor que reemplazará al actual</param>
        /// <param name="condicion">Condición que deben cumplir los registros para ser actualizados</param>
        /// <returns>True si no hubo error de lo contrario False.</returns>
        public bool actualiza(String tabla, String campos, String valores, String condicion)
        {
            String valor = "`" + campos + "`='" + valores + "'";
            this.query = "update `" + tabla + "` set " + valor + " where " + condicion;
            return this.consulta(this.query);
        }

        /// <summary>
        /// Realiza una query DELETE
        /// </summary>
        /// <param name="tabla">Nombre de la tabla de donde se eliminarán los registros</param>
        /// <param name="condicion">Condición de eliminación</param>
        /// <returns>True si no hubo error de lo contrario False.</returns>
        public bool elimina(String tabla, String condicion)
        {
            this.query = "DELETE FROM " + tabla + " WHERE " + condicion;
            return this.consulta(this.query);
        }
    }
}
