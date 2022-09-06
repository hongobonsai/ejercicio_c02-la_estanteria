using SysCon = System.Console;
using System.Text;

namespace FirstClassLibrary
{
    public class Cuenta
    {
        private string? _razonSocial;
        public decimal cantidad;

        public Cuenta(string? razonSocial, decimal cantidad)
        {
            _razonSocial = razonSocial;
            this.cantidad = cantidad;
        }
        public void SetRazonSocial(string? razonSocial)
        {
            _razonSocial = razonSocial;
        }
        public void SetCantidad(decimal cantidad)
        {
            this.cantidad = cantidad;
        }
        public string? GetRazonSocial()
        {
            return _razonSocial;
        }
        public decimal GetCantidad()
        {
            return this.cantidad;
        }

        public string Mostrar()
        {
            StringBuilder stringConstruido = new StringBuilder();
            stringConstruido.AppendLine("RAZON SOCIAL:");
            stringConstruido.AppendLine(_razonSocial);
            stringConstruido.AppendLine("DINERO EN CUENTA:");
            stringConstruido.AppendLine(this.cantidad.ToString());

            return stringConstruido.ToString();
        }

        public int Ingresar(decimal montoParaIngresar)
        {
            decimal bufferNuevaCantidad;
            int retorno = -1;
            if (montoParaIngresar >= 0)
            {
                bufferNuevaCantidad = GetCantidad();
                bufferNuevaCantidad += montoParaIngresar;
                SetCantidad(bufferNuevaCantidad);
                retorno = 0;
            }
            return retorno;
        }
        public int Retirar(decimal montoParaRetirar)
        {
            decimal bufferCantidad;
            int retorno = -1;
            if (montoParaRetirar >= 0)
            {
                bufferCantidad = GetCantidad();
                bufferCantidad -= montoParaRetirar;
                SetCantidad(bufferCantidad);
                retorno = 0;
            }
            return retorno;
        }
    }

    public class Producto
    {
        private string? _codigoDeBarra;
        private string? _marca;
        private float _precio;

        #region CONSTRUCTORES
        public Producto(string? codigoDeBarra, string? marca, float precio)
        {
        _codigoDeBarra = codigoDeBarra;
        _marca = marca;
        _precio = precio;
        }
        #endregion

        #region PROP

        public string? CodigoDeBarras
        {
            get { return _codigoDeBarra; }
            set { _codigoDeBarra = value; }
        }
        #endregion

        #region METODOS

        public string? GetMarca()
        {
            return _marca;
        }
        public float GetPrecio()
        {
            return _precio;
        }
        public static string? MostrarProducto(Producto p)
        {
            string? retorno;
            retorno = String.Format("\n-CODIGO: {0}\n-MARCA: {1}\n-PRECIO: {2}\n", (string?)p, p._marca, p._precio);
            return retorno;
        }
        #endregion

        #region SOBRECARGAS

        public static explicit operator string?(Producto p)
        {
            return p.CodigoDeBarras;
        }

        public static bool operator ==(Producto p1, Producto p2)
        {
            bool retorno;
            if ((string?)p1 == (string?)p2 && p1._marca == p2._marca)
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }

        public static bool operator !=(Producto p1, Producto p2)
        {
            return !(p1 == p2);
        }

        public static bool operator ==(Producto p1, string? str)
        {
            bool retorno;
            if (p1._marca == str)
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }

        public static bool operator !=(Producto p1, string? str)
        {
            return !(p1 == str);
        }
        #endregion

    }

    public class Estante
    {


        private Producto[] _productos;
        private int _ubicacionEstante;

        #region CONSTRUCTORES
        private Estante(int capacidad)
        {
            _productos = new Producto[capacidad];
        }
        public Estante(int capacidad, int ubicacion) : this(capacidad)
        {
            _ubicacionEstante = ubicacion;
;
        }
        public Estante(int capacidad, int ubicacion, Producto p1) : this(capacidad, ubicacion)
        {
            _productos[ubicacion] = p1;
        }
        #endregion

        #region PROP
        #endregion

        #region METODOS
        public Producto[] GetProductos(Estante e)
        {
            return e._productos;
        }

        public static string? MostrarEstante(Estante e)
        {
            string? buffer;
            StringBuilder sb = new StringBuilder();
            sb.Append("CAPACIDAD DEL ESTANTE: " + e._productos.Length + "\n" + "PRODUCTOS: \n\n");
            for (int i = 0; i < e._productos.Length; i++)
            {
                if (e._productos[i] is not null)
                {
                    buffer = Producto.MostrarProducto(e._productos[i]);
                    sb.Append(buffer);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region SOBRECARGAS

        public static bool operator ==(Estante e, Producto p)
        {
            bool retorno = false;
            for (int i = 0; i < e._productos.Length; i++)
            {
                if(e._productos[i] is not null)
                {
                    if (p == e._productos[i])
                    {
                        retorno = true;
                    }
                }
            }
            return retorno;
        }

        public static bool operator !=(Estante e, Producto p)
        {
            return !(e == p);
        }

        public static bool operator +(Estante e, Producto p)
        {
            bool retorno = false;
            if (e != p)
            {
                for (int i = 0; i < e._productos.Length; i++)
                {
                    if (e._productos[i] is null)
                    {
                        e._productos[i] = p;
                        retorno = true;
                        break;
                    }
                }
            }
            return retorno;
        }
        public static Estante operator -(Estante e, Producto p)
        {
            Estante retorno = null;
            for (int i = 0; i < e._productos.Length; i++)
            {
                if (e == p)
                {
                    e._productos[i] = null;
                    retorno = e;
                    break;
                }
            }
            return retorno;

        }
        #endregion
    }
    public class ProductoPrueba
    {
        private int _id;
        private string? _nombre;
        private int _precio;
        private int _stock;

        public ProductoPrueba(int id)
        {
            _id = id;
            _nombre = "";
            _precio = 0;
            _stock = 0;
        }
        #region CONSTRUCTORES
        public ProductoPrueba(int id, string nombre) : this(id)
        {
            _nombre = nombre;
            _precio = 0;
            _stock = 0;
        }
        public ProductoPrueba(int id, string nombre, int precio) : this(id, nombre)
        {
            _precio = precio;
            _stock = 0;
        }

        public ProductoPrueba(int id, string nombre, int precio, int stock) : this(id, nombre, precio)
        {
            _stock = stock;
        }
        #endregion

        #region PROP
        public int GetId
        {
            get { return this._id; }
        }
        public string? GetSetNombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int GetSetPrecio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        public int GetSetStock
        {
            get { return _stock; }
            set { _stock = value; }
        }
        #endregion

        #region METODOS

        public void Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRODUCTO");
            sb.Append("\t");
            sb.Append("ID");
            sb.Append("\t");
            sb.Append("PRECIO");
            sb.Append("\t");
            sb.Append("STOCK");

            SysCon.WriteLine(sb.ToString());
        }

        public void Mostrar(ConsoleColor backColor)
        {
            SysCon.BackgroundColor = backColor;
            Mostrar();
        }

        public void Mostrar(ConsoleColor backColor, ConsoleColor letra)
        {
            SysCon.ForegroundColor = letra;
            Mostrar(backColor);
        }

        #endregion

        #region SOBRECARGAS
        //casteo implicito = conversion implicita = no hay perdida de datos
        public static implicit operator string?(ProductoPrueba p1)
        {
            return p1.GetSetNombre;
        }
        //casteo explicito = conversion explicita = hay perdida de datos por ejemplo
        public static explicit operator int(ProductoPrueba p1)
        {
            return p1.GetId;
        }
        public static int operator +(ProductoPrueba p1, ProductoPrueba p2)
        {
            return (p1.GetSetStock * p1.GetSetPrecio) * (p2.GetSetStock * p2.GetSetPrecio);
        }

        public static int operator +(ProductoPrueba p1, int n1)
        {
            return p1.GetSetStock + n1;
        }

        public static int operator -(ProductoPrueba p1, int n1)
        {
            return p1.GetSetStock - n1;
        }

        public static int operator -(int n1, ProductoPrueba p1)
        {
            return n1 - p1.GetSetStock;
        }

        public static bool operator ==(ProductoPrueba p1, int id)
        {
            return p1.GetId == id;
        }

        public static bool operator !=(ProductoPrueba p1, int id)
        {
            return !(p1.GetId == id);
        }
        #endregion
    }
}